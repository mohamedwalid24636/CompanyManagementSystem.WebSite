using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;                    // ===========> IFormFile Backage "Microsoft.AspNetCore.Http.Features"

namespace Demo.BusinessLogic.Services.AttachementServices
{
    public interface IAttachementService
    {
        //Upload Attachement
        public string? Upload (IFormFile file , string FolderName);

        //Delete Attachement
        public bool Delete(string FilePath);
    }
}
