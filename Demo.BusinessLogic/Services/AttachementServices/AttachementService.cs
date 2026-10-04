using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Demo.BusinessLogic.Services.AttachementServices
{
    public class AttachementService : IAttachementService
    {
        List<string> AllowedExtenSions = [".png", ".jpg", ".jpeg"];
        const int MaxSize = 2_097_152;
        //public bool Delete(string FilePath)  
        //{
        //    if (!File.Exists(FilePath)) return false;
        //    else
        //    {
        //        File.Delete(FilePath);
        //        return true; 
        //    }

        //}
        public bool Delete(string FileName)
        {
            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "Files",
                "Images",
                FileName);

            if (!File.Exists(filePath))
                return false;

            File.Delete(filePath);
            return true;
        }


        public string? Upload(IFormFile File, string FolderName)
        {


            //1.Check Extension
            var Extension = Path.GetExtension(File.FileName);
            if (!AllowedExtenSions.Contains(Extension)) return null;

            //2.Check Size
            if (File.Length == 0 || File.Length > MaxSize) return null;


            //3.Get Located Folder Path
            var FolderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Files\\{FolderName}";  //=====> {Directory.GetCurrentDirectory} = G:\ASP.NET MVC\DemoMvcSolution\Demo.Presentation   ,, Folder Name = image 
                                                                                                  //var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);   //===> بيضيف \


            //4.Make Attachment Name Unique-- GUID
            var FileName = $"{Guid.NewGuid()}_{File.FileName}";

            //5.Get File Path
            var FilePath = Path.Combine(FolderPath, FileName);

            //6.Create File Stream To Copy File[Unmanaged]
            using FileStream Fs = new FileStream(FilePath, FileMode.Create); 
            //Open 
            //Create => over write if file already exsist
            //CreateNew => throw exseption if file already exsist


            //7.Use Stream To Copy File
            File.CopyTo(Fs);

            //8.Return FileName To Store In Database
            return FileName;



        }
    }
}
