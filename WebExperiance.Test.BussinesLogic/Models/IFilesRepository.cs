using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebExperiance.Test.BussinesLogic
{
    public interface IFilesRepository
    {
        bool Update(FilesModel model);
        bool Delete(Guid id);
        bool Create(FilesModel model);
        List<FilesModel> GetFiles(int page, ref int rowCount);
        FilesModel GetById(Guid id);
        string SaveFileToDatabase();        
    }
}
