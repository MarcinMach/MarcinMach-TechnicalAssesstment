using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebExperiance.Test.BussinesLogic
{
    public class FilesModel
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public string CreatedBy { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }

        public FilesModel() { }

        public FilesModel(File model)
        {
            Id = model.Id;
            FileName = model.FileName;
            MimeType = model.MimeType;
            CreatedBy = model.CreatedBy;
            Email = model.Email;
            Country = model.Country;
            Description = model.Description;
        }
    }
    
}
