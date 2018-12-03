using KS.WebSetup.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;


namespace KS.WebSetup.Controllers
{
    public class DefaultsController : ApiController
    {
        private readonly SqlHelper _sqlHelper;

        public DefaultsController()
        {
            _sqlHelper = new SqlHelper();
        }
        [Route("setup/run/")]
        [HttpPost]
        public async Task<bool> RunSetup(JObject data)
        {
            dynamic dataDto = data;
            int step = dataDto.step;
            var path = "~/app_data/";
            var currentSql = "";
            string[] scripts;
            switch (step)
            {
                
                case 1:
                    scripts = new []
                    {
                        "ContentManagement.Schema.sql"
                        , "Security.Schema.sql"

                    };
                    
                    try
                    {
                        for (var i = 0; i < scripts.Length; i++)
                        {
                            currentSql = scripts[i];
                            await
                                _sqlHelper.ExecuteNonQueryAsync(System.Data.CommandType.Text, await ReadAsync(path + "/schema/" + scripts[i]));
                        }
                    }
                    catch (Exception ex)
                    {

                        throw new Exception(ex.Message + " , currentSql : " + currentSql);
                    }
                    break;
                case 2:
                    scripts = new[]
                    {
                        "Security.ApplicationQueryAuthrizes.Table.sql"
                        , "Security.AspNetRoles.Table.sql"
                        , "Security.AspNetLocalRole.Table.sql"
                        , "Security.AspNetGroups.Table.sql"
                        , "Security.AspNetLocalGroup.Table.sql"
                        , "Security.AspNetGroupRoles.Table.sql"
                        , "Security.AspNetUsers.Table.sql"
                        , "Security.AspNetUserClaims.Table.sql"
                        , "Security.AspNetUserGroups.Table.sql"
                        , "Security.AspNetUserLogins.Table.sql"
                        , "Security.AspNetUserRoles.Table.sql"

                    };
                  
                    try
                    {
                        for (var i = 0; i < scripts.Length; i++)
                        {
                            currentSql = scripts[i];
                            await
                                _sqlHelper.ExecuteNonQueryAsync(System.Data.CommandType.Text, await ReadAsync(path + "/security/" + scripts[i]));
                        }
                    }
                    catch (Exception ex)
                    {

                        throw new Exception(ex.Message + " , currentSql : " + currentSql);
                    }
                    break;
                case 3:
                    scripts = new[]
                    {
                          "ContentManagement.Users.Table.sql"
                        ,"ContentManagement.Links.Table.sql"
                        , "ContentManagement.FilePaths.Table.sql"
                        , "ContentManagement.LocalFilePaths.Table.sql"
                        , "ContentManagement.LanguageAndCultures.Table.sql"
                        , "ContentManagement.Files.Table.sql"
                        , "ContentManagement.LocalFiles.Table.sql"
                        , "ContentManagement.MasterDataKeyValues.Table.sql"
                        , "ContentManagement.MasterDataLocalKeyValues.Table.sql"
                         , "ContentManagement.WebPages.Table.sql"
                        , "ContentManagement.Comments.Table.sql"
                        , "ContentManagement.EntityMasterDataKeyValues.Table.sql"
                        , "ContentManagement.EntityGroups.Table.sql"
                        , "ContentManagement.EntityFilePaths.Table.sql"
                        , "ContentManagement.EntityFiles.Table.sql"


                    };

                    try
                    {
                        for (var i = 0; i < scripts.Length; i++)
                        {
                            currentSql = scripts[i];
                            await
                                _sqlHelper.ExecuteNonQueryAsync(System.Data.CommandType.Text, await ReadAsync(path + "/cms/" + scripts[i]));
                        }
                    }
                    catch (Exception ex)
                    {

                        throw new Exception(ex.Message + " , currentSql : " + currentSql);
                    }
                    break;
                case 4:
                    scripts = new[]
                    {
                        "ContentManagement.GetMasterDataLocalKeyValue.StoredProcedure.sql"
                        ,"ContentManagement.GetWebPageForPublish.StoredProcedure.sql"
                        , "ContentManagement.GetWebPageForView.StoredProcedure.sql"
                         , "Security.AuthorizeViewDebugScriptOfCode.StoredProcedure.sql"
                          , "Security.AuthorizeViewDebugScriptOfWebPage.StoredProcedure.sql"
                          , "Security.AuthorizeViewSourceCodeOfMasterDataKeyValues.StoredProcedure.sql"
                          , "Security.GetAspectForMasterDataKeyValueUrl.StoredProcedure.sql"
                          , "Security.GetAspectForPublicMasterDataKeyValueUrl.StoredProcedure.sql"
                          , "Security.GetAspectForWebPage.StoredProcedure.sql"
                          , "Security.GetListOfDllReferencingPermissionFromeListOfDllId.StoredProcedure.sql"
                          , "Security.GetPermissionOfEntityId.StoredProcedure.sql"
                          , "Security.GetPermissionOfEntityIdByVersion.StoredProcedure.sql"
                          , "Security.GetPermissionOfPath.StoredProcedure.sql"
                          , "Security.GetRolesIdByGroupsId.StoredProcedure.sql"
                          , "Security.GetUserInfoById.StoredProcedure.sql"

                    };

                    try
                    {
                        for (var i = 0; i < scripts.Length; i++)
                        {
                            currentSql = scripts[i];
                            await
                                _sqlHelper.ExecuteNonQueryAsync(System.Data.CommandType.Text, await ReadAsync(path + "/sp/" + scripts[i]));
                        }
                    }
                    catch (Exception ex)
                    {

                        throw new Exception(ex.Message + " , currentSql : " + currentSql);
                    }
                    break;
            }
            return true;
        }

        private async Task<string> ReadAsync(string path)
        {
            using (var fs = new FileStream(RelativeToAbsolutePath(path), FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite | FileShare.Delete))
            {
                using (var strReader = new StreamReader(fs, Encoding.UTF8))
                {
                    return await strReader.ReadToEndAsync();
                }
            }
        }

        private string RelativeToAbsolutePath(string path)
        {

            if (path.StartsWith("/") || path.StartsWith("~/"))
            {

                if (path[0] != '~')
                    path = path[0] == '/' ? "~" + path : "~/" + path;


                var realPath = HostingEnvironment.MapPath(path);
                if (realPath == null)
                    throw new Exception(realPath + " Not Found !");
                return realPath;
            }
            return path;
        }
    }
}
