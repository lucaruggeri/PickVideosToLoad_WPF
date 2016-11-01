using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using PickVideosToLoad.Model;

namespace Utilities
{
    public class FilesAndFolders
    {
        public List<string> rootFolders { get; set; }
        public List<string> rootDiscardedFolders { get; set; }
        public List<FileInformations> filesInfoList { get; set; }
        public List<FileInformations> filesToCopy { get; set; }
        public int existingFilesCopies { get; set; }
        public int filesNumberPerLoop { get; set; }
        float totalMegabytes { get; set; }

        public FilesAndFolders()
        {
        }

        #region public methods
        public void TransferRandomFiles(FilesTransferConfiguration config)
        {
            filesInfoList = new List<FileInformations>();
            filesToCopy = new List<FileInformations>();
            existingFilesCopies = 0;
            filesNumberPerLoop = 0;

            if (config.startFromTheFirstFile == true)
            {
                FirstRandomFiles(config);
                TransferFiles(config);
            }
            else
            {
                RandomFiles(config);
                TransferFiles(config);
            }
        }
        #endregion

        #region private methods
        private void RandomFiles(FilesTransferConfiguration config)
        {
            //save all infos
            SaveFilesInfo(config);

            //filter wanted file infos
            totalMegabytes = 0;
            while ((totalMegabytes < config.maxMBAllowed) && (filesInfoList.Count() > 0))
            {
                //select random file
                int index = RandomUtility.GenerateRandomNumber(0, filesInfoList.Count() - 1);

                if (filesInfoList[index] != null)
                {
                    //add to 2
                    filesToCopy.Add(filesInfoList[index]);

                    //update totalMegabytes
                    totalMegabytes = totalMegabytes + filesInfoList[index].Megabytes;

                    //remove from 1
                    filesInfoList.RemoveAt(index);
                }
            }
        }

        private float GetFileInfosTotalMB()
        {
            return filesInfoList.Sum(x => x.Megabytes);
        }

        private bool LitimReached(float maxMBAllowed)
        {
            if (GetFileInfosTotalMB() >= maxMBAllowed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsFolderDiscarded(string folder)
        {
            return rootDiscardedFolders.Contains(folder);
        }

        private string GetFirstFileFromFolder(string folder)
        {
            return Directory.GetFiles(folder).ToList().FirstOrDefault();
        }

        private string GetFirstSubFolder(string folder)
        {
            return Directory.GetDirectories(folder).ToList().FirstOrDefault();
        }

        private void FirstRandomFiles(FilesTransferConfiguration config)
        {
            //tutte le folder della root
            rootFolders = Directory.GetDirectories(config.sourceFolder).ToList();
            rootDiscardedFolders = new List<string>();


            int randomIndex;
            string randomFolder;
            while (!LitimReached(config.maxMBAllowed))
            {
                //prendi una random
                randomIndex = RandomUtility.GenerateRandomNumber(0, rootFolders.Count() - 1);
                randomFolder = rootFolders[randomIndex];

                if (!IsFolderDiscarded(randomFolder))
                {
                    //root
                    string firstFile = GetFirstFileFromFolder(randomFolder);

                    //subfolder
                    if (firstFile == null)
                    {
                        string firstSubFolder = GetFirstSubFolder(randomFolder);
                        if (firstSubFolder != null)
                        {
                            firstFile = GetFirstFileFromFolder(firstSubFolder);
                        }
                    }

                    if (firstFile != null)
                    {
                        FileInfo f = new FileInfo(firstFile);
                        long fileLenght = f.Length;

                        filesInfoList.Add(new FileInformations { Name = Path.GetFileName(firstFile), PathAndName = firstFile, Size = fileLenght });
                        filesNumberPerLoop = filesNumberPerLoop + 1;

                        rootDiscardedFolders.Add(randomFolder);
                    }
                }

            }

            //filter wanted file infos
            totalMegabytes = 0;
            while ((totalMegabytes < config.maxMBAllowed) && (filesInfoList.Count() > 0))
            {
                //select random file
                int index = RandomUtility.GenerateRandomNumber(0, filesInfoList.Count() - 1);

                if (filesInfoList[index] != null)
                {
                    //add to 2
                    filesToCopy.Add(filesInfoList[index]);

                    //update totalMegabytes
                    totalMegabytes = totalMegabytes + filesInfoList[index].Megabytes;

                    //remove from 1
                    filesInfoList.RemoveAt(index);
                }
            }
        }

        private string GetFileExtension(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            string[] fileNameElements = fileName.Split('.');

            //actual last element (ignores the dots ('.') in the file name)
            int lastEmelentIndex = fileNameElements.Length - 1;

            return fileNameElements[lastEmelentIndex];
        }

        private string CreateCopyName(string fileName, int index)
        {
            return Path.GetFileNameWithoutExtension(fileName) + "_" + existingFilesCopies.ToString() + "." + GetFileExtension(fileName);
        }

        private void TransferFiles(FilesTransferConfiguration config)
        {
            //copy files
            if (filesToCopy.Count() > 0)
            {
                foreach (var fileToCopy in filesToCopy)
                {
                    if (config.moveAndDeleteFromSource)
                    {
                        //move
                        if (!File.Exists(config.destinationFolder + "\\" + Path.GetFileName(fileToCopy.Name)))
                        {
                            File.Move(fileToCopy.PathAndName, config.destinationFolder + "\\" + Path.GetFileName(fileToCopy.Name));
                        }
                        else
                        {
                            existingFilesCopies = existingFilesCopies + 1;
                            File.Move(fileToCopy.PathAndName, config.destinationFolder + "\\" + Path.GetFileName(CreateCopyName(fileToCopy.Name, existingFilesCopies)));
                        }
                    }
                    else
                    {
                        //copy
                        if (!File.Exists(config.destinationFolder + "\\" + Path.GetFileName(fileToCopy.Name)))
                        {
                            File.Copy(fileToCopy.PathAndName, config.destinationFolder + "\\" + Path.GetFileName(fileToCopy.Name));
                        }
                        else
                        {
                            existingFilesCopies = existingFilesCopies + 1;
                            File.Copy(fileToCopy.PathAndName, config.destinationFolder + "\\" + Path.GetFileName(CreateCopyName(fileToCopy.Name, existingFilesCopies)));
                        }
                    }
                }
            }
        }

        private void SaveFilesInfo(FilesTransferConfiguration config)
        {
            SaveRootInfo(config);
            SaveFoldersInfo(config);
        }

        private void SaveRootInfo(FilesTransferConfiguration config)
        {
            foreach (string file in Directory.GetFiles(config.sourceFolder))
            {
                //contains extension
                if (config.extensionsToTransfer.Contains(GetFileExtension(file)))
                {
                    //doesn't contains extension
                    if (!config.extensionsToIgnore.Contains(GetFileExtension(file)))
                    {
                        FileInfo f = new FileInfo(file);
                        long fileLenght = f.Length;

                        filesInfoList.Add(new FileInformations { Name = Path.GetFileName(file), PathAndName = file, Size = fileLenght });
                        filesNumberPerLoop = filesNumberPerLoop + 1;
                    }
                }
            }
        }

        private void SaveFoldersInfo(FilesTransferConfiguration config)
        {
            if (Directory.GetDirectories(config.sourceFolder) != null)
            {
                if (Directory.GetDirectories(config.sourceFolder).Count() > 0)
                {
                    foreach (string folder in Directory.GetDirectories(config.sourceFolder))
                    {

                        foreach (string file in Directory.GetFiles(folder))
                        {
                            //contains extension
                            if (config.extensionsToTransfer.Contains(GetFileExtension(file)))
                            {
                                //doesn't contains extension
                                if (!config.extensionsToIgnore.Contains(GetFileExtension(file)))
                                {
                                    FileInfo f = new FileInfo(file);
                                    long fileLenght = f.Length;

                                    filesInfoList.Add(new FileInformations { Name = Path.GetFileName(file), PathAndName = file, Size = fileLenght });
                                    filesNumberPerLoop = filesNumberPerLoop + 1;
                                }
                            }
                        }

                        //recursive call
                        config.sourceFolder = folder;
                        SaveFilesInfo(config);
                    }
                }
            }
        }
        #endregion

    }

    public class FileInformations
    {
        public string Name { get; set; }
        public long Size { get; set; }

        public double Gigabytes {
            get { return Megabytes / 1024.0; }
        }

        public float Megabytes {
            get { return (Size / 1024f) / 1024f; }
        }

        public string PathAndName { get; set; }
    }

}

