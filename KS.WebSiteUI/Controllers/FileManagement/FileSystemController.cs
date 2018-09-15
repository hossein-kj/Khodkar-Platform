using KS.Business.FileManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using KS.Core.Localization;
using KS.Model.FileManagement;
using KS.Core.Model.FileSystem;
using KS.WebSiteUI.Controllers.Base;
using KS.Core.Exceptions;
using KS.Core.GlobalVarioable;

namespace KS.WebSiteUI.Controllers.FileManagement
{
    public class FileSystemController : BaseAuthorizedWebApiController
    {
        private readonly IFileSystemBiz _fileSystemBiz;

        public FileSystemController(IFileSystemBiz fileSystemBiz)
        {
            _fileSystemBiz = fileSystemBiz;
        }

        [Route("fms/GetByPagination/{path}/{orderBy}/{skip}/{take}/{createThumbnail}")]
        [HttpGet]
        public JObject GetByPagination(string path,string orderBy,int skip,int take,bool createThumbnail)
        {
            var count = 0;
            var diskInfos = _fileSystemBiz.GetPathDirectoriesAndFiles(path, orderBy, skip, take,out count, createThumbnail: createThumbnail);

            return JObject.Parse( JsonConvert.SerializeObject
                (new {
                    rows = diskInfos.Select(fl=>new { Id= fl.Id ,Name= fl.Name,Size=fl.Size,fl.ModifieDateTime,fl.ModifieLocalDateTime ,fl.IsFolder}).ToList(),
                    total= count
                }, Formatting.None,
                new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        [Route("fms/GetFoldersByPagination/{path}/{orderBy}/{skip}/{take}")]
        [HttpGet]
        public JObject GetFoldesrByPagination(string path, string orderBy, int skip, int take)
        {
            var count = 0;
            var diskInfos = _fileSystemBiz.GetPathDirectoriesAndFiles(path, orderBy, skip, take, out count, false);

            return JObject.Parse(JsonConvert.SerializeObject
                (new
                {
                    rows = diskInfos.Select(fl => new { Id = fl.Id, Name = fl.Name, Size = fl.Size, fl.ModifieDateTime, fl.ModifieLocalDateTime, fl.IsFolder }),
                    total = count
                }, Formatting.None,
                new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        [Route("fms/move")]
        [HttpPost]
        public bool Move(DiskOprationInfo moveInfo)
        {

            return _fileSystemBiz.Move(moveInfo);
        }

        [Route("fms/copy")]
        [HttpPost]
        public bool Copy(DiskOprationInfo copyInfo)
        {

            return _fileSystemBiz.Copy(copyInfo);
        }

        [Route("fms/zip/add-update")]
        [HttpPost]
        public bool Zip(ZipOprationInfo zipOprationInfo)
        {

            return _fileSystemBiz.Zip(zipOprationInfo);
        }

        [Route("fms/unzip")]
        [HttpPost]
        public bool UnZip(UnZipOprationInfo unZipOprationInfo)
        {

            return _fileSystemBiz.UnZip(unZipOprationInfo);
        }

        [Route("fms/openzip/{zipFullName}/{orderBy}/{skip}/{take}")]
        [HttpGet]
        public JObject OpenZip(string zipFullName, string orderBy, int skip, int take)
        {
            var count = 0;
            var zipinfos = _fileSystemBiz.OpenZip(zipFullName, orderBy, skip, take,out count);

            return JObject.Parse(JsonConvert.SerializeObject
             (new
               {
                   rows = zipinfos,
                   total = count
               }, Formatting.None,
                new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore}));
         }

        [Route("fms/rename")]
        [HttpPost]
        public bool Rename(RenameOprationInfo renameInfo)
        {
            return _fileSystemBiz.Rename(renameInfo);
        }

        [Route("fms/delete")]
        [HttpPost]
        public bool Delete(DeleteOprationInfo deleteInfo)
        {
            return _fileSystemBiz.Delete(deleteInfo);
        }

        [Route("fms/createdir")]
        [HttpPost]
        public string Createdir(CreateDirectoryOprationInfo createdirInfo)
        {
             _fileSystemBiz.CreateDirectory(createdirInfo.Path);
            return LanguageManager.ToLocalDateTime(DateTime.UtcNow);
        }

        [Route("fms/Get/{path}")]
        [HttpGet]
        public async Task<string> Get(string path)
        {

            return await _fileSystemBiz.GetFileContenAsync(path).ConfigureAwait(false);
        }

        [Route("fms/save")]
        [HttpPost]
        public async Task<string> Save(JObject data)
        {
            await _fileSystemBiz.Save(data);
            return LanguageManager.ToLocalDateTime(DateTime.UtcNow);
        }

        [Route("fms/DownlodFromUrl")]
        [HttpPost]
        public bool DownlodFromUrl(JObject data)
        {
            dynamic dataDto = data;

            string filePath = dataDto.FilePath;
            string fileName = dataDto.FileName;
            string url = dataDto.Url;
            int baseUrlId = 0;
            try
            {
                baseUrlId = dataDto.BaseUrlId;
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "Base Url Id"));
            }
            return _fileSystemBiz.DownlodFromUrl(baseUrlId,url,filePath, fileName);
        }
    }
}
