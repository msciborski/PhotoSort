using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;


namespace MovingJPGSToFolders {
    public class PhotoManager {
        public string DirectoryPath { get; set; }
        public string FilePattern { get; set; }

        public FileInfo[] FileInfos{
            get{
                DirectoryInfo di = new DirectoryInfo(DirectoryPath);
                return di.GetFiles(FilePattern, SearchOption.AllDirectories);
            }
        }

        private Regex r = new Regex(":");
        private string topDirectory = @"e:\ZdjeciaPorzadek"; // Destination path

        public PhotoManager(string path, string filePattern){
            DirectoryPath = path;
            FilePattern = filePattern;
        }

        public void OrganizePhotosByDate(){
            CreateTopDirectory();
            foreach (var fileInfo in FileInfos){
                DateTime date = GetDateTakenFromImage(fileInfo.FullName);
                string folderPath = System.IO.Path.Combine(topDirectory, date.ToShortDateString());
                string destPath = System.IO.Path.Combine(folderPath, fileInfo.Name);
                DirectoryInfo di = new DirectoryInfo(folderPath);
                try{
                    if (!di.Exists){
                        di.Create();
                    }
                    System.IO.File.Move(fileInfo.FullName,destPath);
                }
                catch (Exception e){
                    Console.WriteLine("Error: {0}",e.Message);
                }

            }
        }


        private DateTime GetDateTakenFromImage(string path){
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read)){
                using (Image image = Image.FromFile(path)){
                    PropertyItem propItem = image.GetPropertyItem(36867);
                    string date = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                    return DateTime.Parse(date);
                }
            }
        }

        private void CreateTopDirectory(){
            DirectoryInfo directory = new DirectoryInfo(topDirectory);
            try {
                if (!directory.Exists) {
                    directory.Create();
                }
            }
            catch (Exception e) {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }
    }
}
