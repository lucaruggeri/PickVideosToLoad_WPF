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
        public List<MyFileInfo> filesInfoList;
        public List<MyFileInfo> filesToCopy;
        public int existingFilesCopies;

        public FilesAndFolders()
        {
        }

        private string GetFileExtension(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            string[] fileNameElements = fileName.Split('.');

            //actual last element (ignores the dots ('.') in the file name)
            int lastEmelentIndex = fileNameElements.Length - 1;

            return fileNameElements[lastEmelentIndex];
        }


        //TODO cercare nelle folder in modo recursivo
        //TODO modalità preleva il primo video sulla cartella (per vedere puntate in ordine)

        public void TransferRandomFiles(FilesTransferConfiguration config)
        {
            filesToCopy = new List<MyFileInfo>();
            existingFilesCopies = 0;

            //save all infos
            filesInfoList = new List<MyFileInfo>();
            SaveFilesInfo(config);

            //filter wanted file infos
            float totalMegabytes = 0;
            while ((totalMegabytes < config.maxMegabytesAllowed) && (filesInfoList.Count() > 0))
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

            //copy files
            if (filesToCopy.Count() > 0)
            {
                foreach(var fileToCopy in filesToCopy)
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

        private string CreateCopyName(string fileName, int index)
        {
            return Path.GetFileNameWithoutExtension(fileName) + "_" + existingFilesCopies.ToString() + "." + GetFileExtension(fileName);            
        }

        private void TransferFiles(FilesTransferConfiguration config)
        {
            List<string> rootFiles = Directory.GetFiles(config.sourceFolder).ToList();
            List<string> folders = Directory.GetDirectories(config.sourceFolder).ToList();

            //TODO - add folders ignore filters

            //root
            if (rootFiles != null)
            {
                if (rootFiles.Count() > 0)
                {
                    foreach (string file in rootFiles)
                    {
                        //contains extension
                        if (config.extensionsToTransfer.Contains(GetFileExtension(file)))
                        {
                            //doesn't contains extension
                            if (!config.extensionsToIgnore.Contains(GetFileExtension(file)))
                            {
                                if (config.moveAndDeleteFromSource)
                                {
                                    //move
                                    File.Move(file, config.destinationFolder + "\\" + Path.GetFileName(file));
                                }
                                else
                                {
                                    //copy
                                    File.Copy(file, config.destinationFolder + "\\" + Path.GetFileName(file), true);
                                }
                            }
                        }
                    }
                }
            }

            //subfolders
            if (folders != null)
            {
                if (folders.Count() > 0)
                {
                    foreach (string folder in folders)
                    {
                        List<string> files = Directory.GetFiles(folder).ToList();

                        foreach (string file in files)
                        {
                            //contains extension
                            if (config.extensionsToTransfer.Contains(GetFileExtension(file)))
                            {
                                //doesn't contains extension
                                if (!config.extensionsToIgnore.Contains(GetFileExtension(file)))
                                {
                                    if (config.moveAndDeleteFromSource)
                                    {
                                        //move
                                        File.Move(file, config.destinationFolder + "\\" + Path.GetFileName(file));
                                    }
                                    else
                                    {
                                        //copy
                                        File.Copy(file, config.destinationFolder + "\\" + Path.GetFileName(file), true);
                                    }
                                }
                            }
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

                        filesInfoList.Add(new MyFileInfo { Name = Path.GetFileName(file), PathAndName = file, Size = fileLenght });
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

                                    filesInfoList.Add(new MyFileInfo { Name = Path.GetFileName(file), PathAndName = file, Size = fileLenght });
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
    }

    public class MyFileInfo
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

