using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingJPGSToFolders {
    class Program {
        static void Main(string[] args){
            PhotoManager manager = new PhotoManager(@"e:\zdjecia", "*.jpg");
            manager.OrganizePhotosByDate();
            Console.ReadKey();
        }
    }
}
