

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using KS.Core.GlobalVarioable;
using KS.Core.Utility;
using KS.Core.Data.Contexts.SqlServer.Base;
using KS.Core.Model.ContentManagement;
using KS.Core.Model.Core;


namespace KS.Core.Data.Contexts.Base
{
    public abstract class BaseDataBaseContextManager
    {

        private readonly ISqlHelper _sqlHelper;

        protected BaseDataBaseContextManager(ISqlHelper sqlHelper)
        {
            _sqlHelper = sqlHelper;
        }

        //public static string GetEceptionMessage(ExceptionKey exceptionName, string language)
        //{
        //    var query = new string[]
        //    {
        //        //"declare @exceptionNam  nvarchar(1024)",
        //        //"declare @language  nvarchar(8)",
        //        "declare @name nvarchar(255) ",
        //        "declare @Id  int ",
        //        "set @name = null ",
        //        "SELECT @name = [Name] FROM [ContentManagement].[MasterDataKeyValues] where TypeId = 1008 and Code = @exceptionName and [Language] = @language ",
        //        "if(@name is null) ",
        //        "begin ",
        //        "SELECT @Id =Id FROM [ContentManagement].[MasterDataKeyValues] where TypeId = 1008 and Code = @exceptionName ",
        //        "select @name = Name from [ContentManagement].MasterDataLocalKeyValues where MasterDataKeyValueId = @Id and [Language] = @language ",
        //        "end ",
        //        "select @name as Name "
        //    };
        //    return SqlHelper.ExecuteReader<string>(CommandType.Text,
        //        string.Join("",query)
        //         , r => r[0].ToString(), new[]
        //    {
        //      new SqlParameter("@exceptionName", SqlDbType.NVarChar,1024){Value = exceptionName.ToString()},
        //      new SqlParameter("@language", SqlDbType.NVarChar,8){Value = language},
        //    }).FirstOrDefault();
        //}

        public virtual List<KeyValue> GetRolesIdByGroupsId(List<int> groupsId)
        {
            return _sqlHelper.ExecuteReader<KeyValue>(CommandType.StoredProcedure,
                "[Security].[GetRolesIdByGroupsId]"
                 , r => new KeyValue()

                 {
                     Key = r["GroupId"].ToString(),
                     Value = r["RoleId"].ToString(),
                 }
                 , new[]
            {
              new SqlParameter("@GroupsId", SqlDbType.VarChar,-1){Value = string.Join(",",groupsId)}
            });
        }

        public virtual KeyValue GetMasterDataLocalKeyValue(int typeId, string code, string language)
        {
            return _sqlHelper.ExecuteReader<KeyValue>(CommandType.StoredProcedure,
                "[ContentManagement].[GetMasterDataLocalKeyValue]"
                 , r => new KeyValue()

                 {
                     Key = r["Name"].ToString(),
                     Value = r["Description"].ToString(),
                 }
                 , new[]
            {
              new SqlParameter("@TypeId", SqlDbType.Int){Value = typeId},
              new SqlParameter("@Code", SqlDbType.NVarChar,1024){Value = code},
              new SqlParameter("@language", SqlDbType.NVarChar,8){Value = language},
            }).FirstOrDefault();
        }

        public virtual int AuthorizeViewSourceCodeOfMasterDataKeyValues(int codeId, int userId, int typeId, int actionKey, int permissionTypeId)
        {
            return _sqlHelper.ExecuteReader<int>(CommandType.StoredProcedure,
                "[Security].[AuthorizeViewSourceCodeOfMasterDataKeyValues]"
                , r => Convert.ToInt32(r[0].ToString()), new[]
            {
                          new SqlParameter("@permissionTypeId", SqlDbType.Int){Value = permissionTypeId},
                          new SqlParameter("@TypeId", SqlDbType.Int){Value = typeId},
                          new SqlParameter("@ActionKey", SqlDbType.Int){Value = actionKey},
                          new SqlParameter("@UserId", SqlDbType.Int){Value = userId},
                          new SqlParameter("@codeId", SqlDbType.Int){Value = codeId}
            }).FirstOrDefault();
        }
        public virtual int AuthorizeViewDebugScriptOfWebPage(string guid, int userId,int typeId,int actionKey,int permissionTypeId)
        {
            return _sqlHelper.ExecuteReader<int>(CommandType.StoredProcedure,
                "[Security].[AuthorizeViewDebugScriptOfWebPage]"
                , r => Convert.ToInt32(r[0].ToString()), new[]
            {
              new SqlParameter("@permissionTypeId", SqlDbType.Int){Value = permissionTypeId},
              new SqlParameter("@TypeId", SqlDbType.Int){Value = typeId},
              new SqlParameter("@ActionKey", SqlDbType.Int){Value = actionKey},
              new SqlParameter("@UserId", SqlDbType.Int){Value = userId},
              new SqlParameter("@guid", SqlDbType.NVarChar,32){Value = guid}
            }).FirstOrDefault();
        }

        public virtual int AuthorizeViewDebugScriptOfCode(int bundleTypeId,int codeTypeId, string path, int userId, int actionKey, int permissionTypeId)
        {
            return _sqlHelper.ExecuteReader<int>(CommandType.StoredProcedure,
                "[Security].[AuthorizeViewDebugScriptOfCode]"
                , r => Convert.ToInt32(r[0].ToString()), new[]
            {
                   new SqlParameter("@permissionTypeId", SqlDbType.Int){Value = permissionTypeId},
              new SqlParameter("@UserId", SqlDbType.Int){Value = userId},
              new SqlParameter("@bundleTypeId", SqlDbType.Int){Value = bundleTypeId},
              new SqlParameter("@codeTypeId", SqlDbType.Int){Value = codeTypeId},
             new SqlParameter("@ActionKey", SqlDbType.Int){Value = actionKey},
              new SqlParameter("@path", SqlDbType.NVarChar,1024){Value = path},
            }).FirstOrDefault();
        }


        public virtual List<string> GetPermissionOfPath(int permissionTypeId, int typeId, int actionKey, string urlOrPath)
        {
            return _sqlHelper.ExecuteReader<string>(CommandType.StoredProcedure,
                "[Security].[GetPermissionOfPath]"
                , r => r[0].ToString(), new[]
            {
              new SqlParameter("@permissionTypeId", SqlDbType.Int){Value = permissionTypeId},
              new SqlParameter("@TypeId", SqlDbType.Int){Value = typeId},
              new SqlParameter("@ActionKey", SqlDbType.Int){Value = actionKey},
              new SqlParameter("@UrlOrPath", SqlDbType.NVarChar,1024){Value = urlOrPath}
            });
        }

        public virtual List<string> GetPermissionOfEntityId(int permissionTypeId, int typeId, int actionKey, int entityId)
        {
            return _sqlHelper.ExecuteReader<string>(CommandType.StoredProcedure,
                "[Security].[GetPermissionOfEntityId]"
                , r => r[0].ToString(), new[]
            {
              new SqlParameter("@permissionTypeId", SqlDbType.Int){Value = permissionTypeId},
              new SqlParameter("@TypeId", SqlDbType.Int){Value = typeId},
              new SqlParameter("@ActionKey", SqlDbType.Int){Value = actionKey},
              new SqlParameter("@entityId", SqlDbType.Int){Value = entityId}
            });
        }

        public virtual List<string> GetPermissionOfEntityIdByVersion(int permissionTypeId, int typeId, int actionKey, int entityId, int entityVersion)
        {
            return _sqlHelper.ExecuteReader<string>(CommandType.StoredProcedure,
                "[Security].[GetPermissionOfEntityIdByVersion]"
                , r => r[0].ToString(), new[]
            {
              new SqlParameter("@permissionTypeId", SqlDbType.Int){Value = permissionTypeId},
              new SqlParameter("@TypeId", SqlDbType.Int){Value = typeId},
              new SqlParameter("@ActionKey", SqlDbType.Int){Value = actionKey},
              new SqlParameter("@entityId", SqlDbType.Int){Value = entityId},
              new SqlParameter("@entityVersion", SqlDbType.Int){Value = entityVersion}
            });
        }

        public virtual IAspect GetAspectForWebPage(string url, string mobileUrl, string type)
        {
            return _sqlHelper.ExecuteReader<IAspect>(CommandType.StoredProcedure,
                "[Security].[GetAspectForWebPage]"
                , r => new Aspect()
                {
                    Permission = Convert.ToInt32(r["Permission"].ToString()),
                    EnableCache = Convert.ToBoolean(r["EnableCache"].ToString()),
                    HasMobileVersion = Convert.ToBoolean(r["HasMobileVersion"].ToString()),
                    CacheSlidingExpirationTimeInMinutes = Convert.ToDouble(r["CacheSlidingExpirationTimeInMinutes"].ToString()),
                    Status = Convert.ToInt32(r["Status"].ToString()),

                }, new[]
            {
              new SqlParameter("@Url", SqlDbType.NVarChar,1024){Value =url
              

            //url.Replace(Config.MobileSign,Helper.RootUrl)
              },
              new SqlParameter("@Type", SqlDbType.NVarChar,10){Value = type},
               new SqlParameter("@MobileUrl", SqlDbType.NVarChar,1024){Value = mobileUrl}
            }).FirstOrDefault();
        }

        public virtual IWebPageCore GetWebPageForView(string url, string type)
        {
            return _sqlHelper.ExecuteReader<IWebPageCore>(CommandType.StoredProcedure,
                "[ContentManagement].[GetWebPageForView]"
                , r => new WebPageCore()
                {
                    Url = url,
                    Title = r["Title"].ToString(),
                    Html = r["Html"].ToString(),
                    DependentModules = r["DependentModules"].ToString(),
                    PageId =  r["PageId"].ToString(),
                    Param = r["Params"].ToString(),
                    HaveStyle = Convert.ToBoolean(r["HaveStyle"].ToString()),
                    HaveScript = Convert.ToBoolean(r["HaveScript"].ToString()),
                    Version= r["Version"].ToString()
                }, new[]
            {
              new SqlParameter("@Url", SqlDbType.NVarChar,1024){Value = url},
              new SqlParameter("@Type", SqlDbType.NVarChar,10){Value = type}
            }).FirstOrDefault();
        }

        public virtual IAspect GetAspectForMasterDataKeyValueUrl(int actionKey, string url)
        {
            return _sqlHelper.ExecuteReader<IAspect>(CommandType.StoredProcedure,
                "[Security].[GetAspectForMasterDataKeyValueUrl]"
                , r => new Aspect()
                {
                    Name = r["Name"].ToString(),
                    Permission = Convert.ToInt32(r["Permission"].ToString()),
                    EnableLog = r["EnableLog"].ToString() == "1",
                    EnableCache = Convert.ToBoolean(r["EnableCache"].ToString())

                }, new[]
            {
              new SqlParameter("@TypeId", SqlDbType.Int){Value = (int)EntityIdentity.Permission},
              new SqlParameter("@ActionKey", SqlDbType.Int){Value = actionKey},
              new SqlParameter("@UrlOrPath", SqlDbType.NVarChar,1024){Value = url},
            }).FirstOrDefault();
        }


        public virtual IAspect GetAspectForPublicMasterDataKeyValueUrl(string url)
        {
            return _sqlHelper.ExecuteReader<IAspect>(CommandType.StoredProcedure,
                "[Security].[GetAspectForPublicMasterDataKeyValueUrl]"
                , r => new Aspect()
                {
                    Name = r["Name"].ToString(),
                    EnableCache = Convert.ToBoolean(r["EnableCache"].ToString()),
                    EnableLog = r["EnableLog"].ToString() == "1"
                }, new[]
            {
              new SqlParameter("@UrlOrPath", SqlDbType.NVarChar,1024){Value = url},
            }).FirstOrDefault();
        }

        public virtual KeyValue GetUserInfoById(int userId)
        {
            return _sqlHelper.ExecuteReader<KeyValue>(CommandType.StoredProcedure,
                "[Security].[GetUserInfoById]"
                                 , r => new KeyValue()

                                 {
                                     Key = r["Email"].ToString(),
                                     Value = r["AliasName"].ToString(),
                                 }, new[]
            {
              new SqlParameter("@UserId", SqlDbType.Int){Value = userId}
            }).FirstOrDefault();
        }

        public virtual async Task<string> GetPermissionOfUrlOrPathAsync(int typeId, int actionKey, string urlOrPath)
        {
            return (await _sqlHelper.ExecuteReaderAsync<string>(CommandType.StoredProcedure,
                "[Security].[GetPermissionOfUrlOrPath]"
                , r => r[0].ToString(), new[]
            {
              new SqlParameter("@TypeId", SqlDbType.Int){Value = typeId},
              new SqlParameter("@ActionKey", SqlDbType.Int){Value = actionKey},
              new SqlParameter("@UrlOrPath", SqlDbType.NVarChar,1024){Value = urlOrPath},
            })).FirstOrDefault();
        }

        public virtual async Task<List<KeyValue>> GetListOfDllReferencingPermissionFromeListOfDllIdAsync(int permissionTypeId, int actionKey, List<int> usedDll)
        {
            return await _sqlHelper.ExecuteReaderAsync<KeyValue>(CommandType.StoredProcedure,
               "[Security].[GetListOfDllReferencingPermissionFromeListOfDllId]"
                , r => new KeyValue()

                {
                    Key = r["Code"].ToString(),
                    Value = r["ForeignKey2"].ToString()
                }
                , new[]
           {
             new SqlParameter("@permissionTypeId", SqlDbType.Int){Value = permissionTypeId},
             new SqlParameter("@ActionKey", SqlDbType.Int){Value = actionKey},
              new SqlParameter("@DllIds", SqlDbType.VarChar,-1){Value = string.Join(",",usedDll)}
           });
        }

    }
}
