using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using WebExperiance.Test.BussinesLogic;

namespace WebExperience.Test.Controllers
{
    [RoutePrefix("api/FildsApi")]
    public class FildsApiController : ApiController
    {
        // TODO
        // Create an API controller via REST to perform all CRUD operations on the asset objects created as part of the CSV processing test
        // Visualize the assets in a paged overview showing the title and created on field
        // Clicking an asset should navigate the user to a detail page showing all properties
        // Any data repository is permitted
        // Use a client MVVM framework

        static readonly IFilesRepository repository = new FilesRepository();

        #region GetList()
        [Route("{page:int}")]
        public object GetList(int page = 1)
        {
            int rowCount = 1;
            var results = repository.GetFiles(page, ref rowCount);
            var pageSkip = page > 0 ? page - 1 : page;
            return new { results, pageSkip, rowCount };
        }
        #endregion

        #region Get()
        public FilesModel Get(Guid id)
        {
            var record = repository.GetById(id);
            return record;
        }
        #endregion

        #region Add()
        [HttpPut]
        [Route("")]
        public bool Add(FilesModel model)
        {
            var isSuccesed = repository.Create(model);
            return isSuccesed;
        }
        #endregion

        #region Delete()
        public bool Delete(Guid id)
        {
            var isSuccesed = repository.Delete(id);
            return isSuccesed;
            
        }
        #endregion

        #region Put()
        [HttpPut]
        public bool Save(Guid id, FilesModel model)
        {
            var isSuccesed = repository.Update(model);
            return isSuccesed;
        }
        #endregion

        #region Dwonload()
        public string Dwonload()
        {
            var record = repository.SaveFileToDatabase();
            return record;
        }
        #endregion
    }
}
