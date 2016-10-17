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

        public void TransferRandomFiles(FilesTransferConfiguration config)
        {
            filesToCopy = new List<MyFileInfo>();

            //save all infos
            SaveFilesInfo(config.sourceFolder, config.destinationFolder, config.foldersToIgnore, config.extensionsToTransfer, config.extensionsToIgnore);

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
                        File.Move(fileToCopy.PathAndName, config.destinationFolder + "\\" + Path.GetFileName(fileToCopy.Name));
                    }
                    else
                    {
                        //copy
                        File.Copy(fileToCopy.PathAndName, config.destinationFolder + "\\" + Path.GetFileName(fileToCopy.Name), true);
                    }
                }
            }
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

        private void SaveFilesInfo(string sourceFolder, string destinationFolder, List<string> foldersToIgnore, List<string> extensionsToTransfer, List<string> extensionsToIgnore)
        {
            filesInfoList = new List<MyFileInfo>();

            List<string> rootFiles = Directory.GetFiles(sourceFolder).ToList();
            List<string> folders = Directory.GetDirectories(sourceFolder).ToList();

            //root
            if (rootFiles != null)
            {
                if (rootFiles.Count() > 0)
                {
                    foreach (string file in rootFiles)
                    {
                        //contains extension
                        if (extensionsToTransfer.Contains(GetFileExtension(file)))
                        {
                            //doesn't contains extension
                            if (!extensionsToIgnore.Contains(GetFileExtension(file)))
                            {
                                FileInfo f = new FileInfo(file);
                                long fileLenght = f.Length;

                                filesInfoList.Add(new MyFileInfo { Name = Path.GetFileName(file), PathAndName = file, Size = fileLenght });
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
                            if (extensionsToTransfer.Contains(GetFileExtension(file)))
                            {
                                //doesn't contains extension
                                if (!extensionsToIgnore.Contains(GetFileExtension(file)))
                                {
                                    FileInfo f = new FileInfo(file);
                                    long fileLenght = f.Length;

                                    filesInfoList.Add(new MyFileInfo { Name = Path.GetFileName(file), PathAndName = file, Size = fileLenght });
                                }
                            }
                        }
                    }
                }
            }
        }

        public List<string> SearchForLocalFiles(string path, string extensionsToFind)
        {
            List<string> filesFound = new List<string>();
            List<string> folders = Directory.GetDirectories(path).ToList();

            if (folders != null)
            {
                if (folders.Count() > 0)
                {
                    foreach(string folder in folders)
                    {
                        List<string> files = Directory.GetFiles(folder).ToList();

                        foreach (string file in files)
                        {
                            string extension = file.Substring(file.Length - 3);

                            if (extensionsToFind.Contains(extension))
                            {
                                filesFound.Add(Path.GetFileNameWithoutExtension(file));
                            }
                        }
                    }
                }
            }
            return filesFound;
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

