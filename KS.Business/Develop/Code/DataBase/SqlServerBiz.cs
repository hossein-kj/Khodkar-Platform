using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using KS.Business.Develop.Code.Base;
using KS.Core.CodeManager.Base;
using KS.Core.Exceptions;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;
using KS.Core.Security;
using KS.Model.ContentManagement;
using Newtonsoft.Json.Linq;
using KS.Core.FileSystemProvide.Base;
using KS.Core.UI.Configuration;
using KS.Core.CodeManager.DataBase.Base;
using KS.Core.Model.Core;
using KS.DataAccess.Contexts.Base;

namespace KS.Business.Develop.Code.DataBase
{
    public class SqlServerBiz: BaseCodeBiz, ISqlServerBiz
    {
        private readonly IDataBaseManager _dataBaseManager;
        public SqlServerBiz(ISourceControl sourceControl, 
            IContentManagementContext contentManagementContext
            ,IFileSystemManager fileSystemManager
            , IWebConfigManager webConfigManager
            , IDataBaseManager dataBaseManager
            ,ISecurityContext securityContext) 
            : base(sourceControl, contentManagementContext, fileSystemManager
                  , webConfigManager, securityContext)
        {
            _dataBaseManager = dataBaseManager;
        }

        public async Task<List<KeyValue>> GetConnections(string lang="")
        {
            var connectionByPermission = await ContentManagementContext.MasterDataKeyValues
                .Include(md=>md.LocalValues)
                .Where(md => (md.TypeId == (int)EntityIdentity.SqlServerConnections && md.IsLeaf)||
                (md.TypeId == (int)EntityIdentity.Permission && md.ForeignKey1 == (int)ActionKey.ConnectToConnection))
                
                .ToListAsync();

            return (from connection in connectionByPermission
                from permission in connectionByPermission
                where connection.Id == permission.ForeignKey3
                where AuthorizeManager.IsAuthorize(permission.ForeignKey2)
                select new KeyValue()
                {
                    Key = connection.Id.ToString(), Value = lang==""? connection.Name
                    : (connection.LocalValues.FirstOrDefault(lc => lc.Language == lang) == null ? "No Name": connection.LocalValues.FirstOrDefault(lc => lc.Language == lang)?.Name )
                }).ToList();
        }
        public async Task<MasterDataKeyValue> Save(JObject data)
        {
            dynamic dataDto = data;
            if (dataDto.TypeId != (int)EntityIdentity.SqlServerCode)
                throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));
            dataDto.PathOrUrlProtocolId = (int)Protocol.PathProtocol;
            var code = await Save((JObject)dataDto, true);
            return code;
        }


        public async Task<bool> Delete(JObject data)
        {
            dynamic dataDto = data;
            int id = dataDto.Id;
            var code = await ContentManagementContext.MasterDataKeyValues.FirstOrDefaultAsync(md => md.Id == id
                                                                                                           &&
                                                                                                           md.TypeId ==
                                                                                                           (int)
                                                                                                           EntityIdentity
                                                                                                               .SqlServerCode);
            if (code == null)
                throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));
            return await base.DeleteCode(data);
        }

        public async Task<JArray> Execute(JObject data)
        {
            dynamic dataDto = data;
            bool isQuery = dataDto.IsQuery;
            string query = dataDto.Code;
            if (string.IsNullOrEmpty(query.Trim()))
                return new JArray();
            int connectionId = dataDto.ConnectionId;

            var connectionByPermission = await ContentManagementContext.MasterDataKeyValues
                .Where(md => (md.Id == connectionId && md.TypeId == (int)EntityIdentity.SqlServerConnections)
                || (md.ForeignKey3 == connectionId && md.TypeId == (int)EntityIdentity.Permission)).ToListAsync();
            var connection = connectionByPermission.FirstOrDefault(con => con.Id == connectionId);
            if (connection == null)
                throw new KhodkarInvalidException(string.Format(LanguageManager.ToAsErrorMessage(ExceptionKey.NotFound),
               " Connection "));
            var permission = connectionByPermission.FirstOrDefault(prm => prm.ForeignKey3 == connectionId
            && prm.TypeId == (int)EntityIdentity.Permission);

            if (permission == null)
                throw new KhodkarInvalidException(string.Format(LanguageManager.ToAsErrorMessage(ExceptionKey.NotFound),
               " Permission "));

            if (!AuthorizeManager.IsAuthorize(permission.ForeignKey2))
                throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));
            if (isQuery)
            {
                return _dataBaseManager.ExecuteQuery(connection.SecondCode, query);
            }
            else
            {
                return _dataBaseManager.ExecuteNonQuery(connection.SecondCode, query);
            }
        }


    }
}
