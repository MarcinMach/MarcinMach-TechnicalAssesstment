using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using WebExperiance.Test;
using System.IO;

namespace WebExperiance.Test.BussinesLogic
{
    public class FilesRepository : DbContext, IFilesRepository
    {
        #region Update()
        /// <summary>
        /// Update the existing record in database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(FilesModel model)
        {
            try
            {
                using (SimpleDBEntities db = new SimpleDBEntities())
                {
                    var record = db.Files.FirstOrDefault(x => x.Id == model.Id);
                    record.FileName = model.FileName;
                    record.MimeType = model.MimeType;
                    record.Email = model.Email;
                    record.Description = model.Description;
                    record.CreatedBy = model.CreatedBy;
                    record.Country = model.Country;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion 

        #region Delete()
        /// <summary>
        /// Delete existing record in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(Guid id)
        {
            try
            {
                using (SimpleDBEntities db = new SimpleDBEntities())
                {
                    var record = db.Files.FirstOrDefault(x => x.Id == id);
                    db.Files.Remove(record);
                    db.SaveChanges();
                    return true;

                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Create()
        /// <summary>
        /// Create new object in the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Create(FilesModel model)
        {
            try
            {
                using (SimpleDBEntities db = new SimpleDBEntities())
                {
                    var record = new File()
                    {
                        Id = Guid.NewGuid(),
                        FileName = model.FileName,
                        MimeType = model.MimeType,
                        Country = model.Country,
                        CreatedBy = model.CreatedBy,
                        Description = model.Description,
                        Email = model.Email
                    };
                    db.Files.Add(record);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region GetFiles()
        /// <summary>
        /// Get List of Files from database
        /// </summary>
        /// <returns>List<FilesModel></returns>
        public List<FilesModel> GetFiles(int page, ref int rowCount)
        {
            try
            {
                using (SimpleDBEntities db = new SimpleDBEntities())
                {

                    var fileList = db.Files.OrderBy(x=>x.FileName).Skip<File>(page * 10)
                    .Take<File>(10)
                    .ToList(); 
                    var result = new List<FilesModel>();
                    rowCount = db.Files.Count();
                    foreach (var item in fileList)
                    {
                        var model = new FilesModel()
                        {
                            Id = item.Id,
                            FileName = item.FileName,
                            MimeType = item.MimeType,
                            Country = item.Country,
                            CreatedBy = item.CreatedBy,
                            Description = item.Description,
                            Email = item.Email
                        };
                        result.Add(model);

                    }
                    return result; 
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region GetById()
        /// <summary>
        /// Get record form database using his Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>FileModel</returns>
        public FilesModel GetById(Guid id)
        {
            try
            {
                using (SimpleDBEntities db = new SimpleDBEntities())
                {
                    var record = db.Files.FirstOrDefault(x => x.Id == id);
                    var model = new FilesModel()
                    {
                        Id = record.Id,
                        FileName = record.FileName,
                        MimeType = record.MimeType,
                        Country = record.Country,
                        CreatedBy = record.CreatedBy,
                        Description = record.Description,
                        Email = record.Email
                    };
                    return model;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region SaveFileToDatabase()
        /// <summary>
        /// Saving the record form the file into the database
        /// </summary>
        /// <returns></returns>
        public string SaveFileToDatabase()
        {
            try
            {
                using (SimpleDBEntities db = new SimpleDBEntities())
                {
                    var basePath = System.AppDomain.CurrentDomain.BaseDirectory;
                    var filePath = basePath.Replace("WebExperience.Test\\", "GeneralKnowledge.Test\\Resources\\AssetImport.csv");


                    var stream = new StreamReader(filePath);

                    var excelSheat = new List<string>();

                    var databaseCount = db.Files.Count();
                    if (databaseCount > 15)
                    {
                        return "The Data Base is already completed";
                    }
                    else
                    {
                        while (!stream.EndOfStream)
                        {
                            excelSheat.Add(stream.ReadLine());
                        }
                        foreach (var record in excelSheat.Skip(4))
                        {
                            var value = record.Split(',');
                            var file = new File();
                            file.Id = new Guid(value[0]);
                            file.FileName = value[1];
                            file.MimeType = value[2];
                            file.CreatedBy = value[3];
                            file.Email = value[4];
                            file.Country = value[5];
                            file.Description = value[6];

                            db.Files.Add(file);
                            db.SaveChanges();

                        }
                        return "Success";
                    }
                }

            }
            catch (Exception ex)
            {
                return "Error";
            }

        }
        #endregion
    }
}
