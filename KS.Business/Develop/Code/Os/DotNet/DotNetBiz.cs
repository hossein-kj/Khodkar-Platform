using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using KS.Core.CodeManager.Base;
using KS.Core.CodeManager.Os.DotNet.Base;
using KS.Core.Exceptions;
using KS.Core.FileSystemProvide.Base;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;
using KS.Core.Model.Develop;
using KS.Core.Security;
using KS.Core.Utility;
using KS.DataAccess.Contexts.Base;
using KS.Model.ContentManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KS.Model.FileManagement;
using System.Linq.Dynamic;
using System.Net;
using KS.Core.SignalR;
using KS.Core.EntityFramework;

namespace KS.Business.Develop.Code.Os.DotNet
{
    public class DotNetBiz : IDotNetBiz
    {
        private readonly IContentManagementContext _contentManagementContext;
        private readonly IFileSystemManager _fileSystemManager;
        private readonly ISourceControl _sourceControl;
        private readonly ICompiler _compiler;
        private readonly ISecurityContext _securityContext;
        private readonly IDebugger _debugger;
        private readonly INotificationManager _notificationManager;
        private readonly IMigration _migration;
        private readonly IUnitTester _unitTester;
        private const string CSharpStartComment = " /*khodkar c# comment ";
        private const string CSharpEndComment = " khodkar c# comment*/ ";
        private const string CompileOutPutDirectory = "/output/";
        private const string PubliahBackUpDirectory = "/backup/";
        private const string ReleaseMode = "_r";
        private const string DebugMode = "_d";
        private const string Log = "_log";
        private const string DllExtention = ".dll";
        private const string PdbExtention = ".pdb";
        private const string TxtExtention = ".txt";
        private const string CodePathTemplate = "@asCodePath";
        private const string DebugInfoTemplate = "KS.Core.Model.Develop.DebugInfo()";
        private const string WebConfig = "web.config";
        public DotNetBiz(IContentManagementContext contentManagementContext
            , IFileSystemManager fileSystemManager, ISourceControl sourceControl
            , ISecurityContext securityContext
            ,ICompiler compiler, IDebugger debugger,
            INotificationManager notificationManager, IMigration migration
            , IUnitTester unitTester)
        {
            _contentManagementContext = contentManagementContext;
            _fileSystemManager = fileSystemManager;
            _sourceControl = sourceControl;
            _securityContext = securityContext;
            _compiler = compiler;
            _debugger = debugger;
            _notificationManager = notificationManager;
            _migration = migration;
            _unitTester = unitTester;
        }

        public async Task<bool> DellOutputDll(JObject data)
        {


            dynamic outputDto = data;
            int id;

            try
            {
                id = outputDto.Id;
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "Code Id"));

            }

            var code =
               await _contentManagementContext.MasterDataKeyValues.AsNoTracking().SingleOrDefaultAsync(cd => cd.Id == id);

            if (code == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeNotFound));

            if (AuthorizeManager.AuthorizeActionOnEntityId(code.Id, (int)EntityIdentity.DotNetCode, (int)ActionKey.DellDllOutput))
            {
                var dotnetCode = await _contentManagementContext.MasterDataKeyValues.AsNoTracking()
     .SingleOrDefaultAsync(cd => cd.TypeId == (int)EntityIdentity.DotNetCode && cd.IsType);

                if (dotnetCode == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, CompileOutPutDirectory));
              


                JArray outputArray = outputDto.SelectedOutputs;
                var outputs = outputArray.ToObject<List<string>>();
                foreach(var output in outputs)
                {
                    var version = output.Replace(code.Code.Replace(DllExtention,"") + ReleaseMode, "")
                        .Replace(code.Code.Replace(DllExtention, "") + DebugMode, "").Replace(DllExtention, "");
                    _fileSystemManager.DeleteFile(dotnetCode.PathOrUrl + CompileOutPutDirectory +

                                                  code.Code.Insert(
                                                          code.Code.IndexOf(
                                                              DllExtention,
                                                              StringComparison
                                                                  .OrdinalIgnoreCase),
                                                          Log + version)
                                                      .Replace(
                                                          DllExtention,
                                                          TxtExtention)
                                                  +
                                                  output.Replace(DllExtention, PdbExtention));
                    _fileSystemManager.DeleteFile(dotnetCode.PathOrUrl + CompileOutPutDirectory + output);
                    _fileSystemManager.DeleteFile(dotnetCode.PathOrUrl + CompileOutPutDirectory + output.Replace(DllExtention,PdbExtention));
                }
            }

            else
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidAccessToDellDllOutput, code.Code));


            }

            return true;
        }

        public async Task<JObject> AddOutputDll(JObject data)
        {
            dynamic outputDto = data;
            int id;

            try
            {
                id = outputDto.Id;
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "Code Id"));

            }

            var code =
               await _contentManagementContext.MasterDataKeyValues.AsNoTracking().SingleOrDefaultAsync(cd => cd.Id == id);

            if (code == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeNotFound));

            string path = outputDto.Path;
            string outputName = outputDto.Name;
            if (AuthorizeManager.AuthorizeActionOnEntityId(code.Id, (int)EntityIdentity.DotNetCode, (int)ActionKey.AddDllOutput))
            {
                var dotnetCode = await _contentManagementContext.MasterDataKeyValues.AsNoTracking()
                    .SingleOrDefaultAsync(cd => cd.TypeId == (int) EntityIdentity.DotNetCode && cd.IsType);

                if (dotnetCode == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, CompileOutPutDirectory));
               

                if(path.StartsWith(code.PathOrUrl))
                {
                    _fileSystemManager.CopyFile(path, dotnetCode.PathOrUrl + CompileOutPutDirectory + outputName);
                    await _fileSystemManager.WriteAsync(_fileSystemManager.RelativeToAbsolutePath(dotnetCode.PathOrUrl
                                                                                                  +
                                                                                                  CompileOutPutDirectory
                                                                                                  +
                                                                                                  code.Code.Insert(
                                                                                                          code.Code.IndexOf(
                                                                                                              DllExtention,
                                                                                                              StringComparison
                                                                                                                  .OrdinalIgnoreCase),
                                                                                                          Log + code.Value)
                                                                                                      .Replace(
                                                                                                          DllExtention,
                                                                                                          TxtExtention)), "");
                }
                else
                {
                    throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidAccessToPath, path));
                    
                }

                return JObject.Parse(JsonConvert.SerializeObject
                (new
                {
                    Id = 0,
                    Name = outputName,
                    Size = 0,
                    ModifieDateTime = "",
                    ModifieLocalDateTime = ""
                }, Formatting.None));
            }
            throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidAccessToAddDllOutput, code.Code));

          

        }

        protected async Task WriteFileAsync(string path, string name, string extention,
string content,bool isClassOrMethodOrLine, bool creatDirectoryIfNotExist = false)
        {
            path = path.Replace("//", "/");
            var finallName = name;
            if (isClassOrMethodOrLine)
            {
                var directory = path.Substring(path.LastIndexOf("/", StringComparison.Ordinal) + 1).Replace("-", ".");
                finallName = name.Substring(name.LastIndexOf(directory, StringComparison.Ordinal) + directory.Length + 1);
            }

            await
                _fileSystemManager.WriteAsync(
                (creatDirectoryIfNotExist
                    ? _fileSystemManager.CreatDirectoryIfNotExist(path)
                    : (_fileSystemManager.RelativeToAbsolutePath(path)) + "/" + finallName + extention).Replace("//", "/"), content);
        }

        public async Task<JObject> GetOutputs(string orderBy, int skip, int take, int codeId)
        {

            var code =
               await _contentManagementContext.MasterDataKeyValues.AsNoTracking().SingleOrDefaultAsync(cd => cd.Id == codeId);

            if (code == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeNotFound));

            AuthorizeManager.CheckViewAccess(code);

            var dotnetCode = await _contentManagementContext.MasterDataKeyValues.AsNoTracking()
       .SingleOrDefaultAsync(cd => cd.TypeId == (int)EntityIdentity.DotNetCode && cd.IsType == true);

            if (dotnetCode == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, CompileOutPutDirectory));
        

            var diskInfos = GetOutputs(dotnetCode.PathOrUrl + CompileOutPutDirectory,code.Code.Replace(DllExtention,"_*")+DllExtention);

            var count = diskInfos.Count();
            if (count == 0)
                diskInfos = GetOutputs(dotnetCode.PathOrUrl + CompileOutPutDirectory,
                    code.Code);
            var outputList = diskInfos.AsQueryable().OrderBy(orderBy)
                .Skip(skip)
                .Take(take).ToList();

            return JObject.Parse(JsonConvert.SerializeObject
               (new
               {
                   rows = outputList.Select(fl => new { Id = fl.Id, Name = fl.Name, Size = fl.Size, fl.ModifieDateTime, fl.ModifieLocalDateTime }),
                   total = count
               }, Formatting.None));

        }

        public async Task<JArray> GetOutputVersions(int codeId,bool showSuggestionLatestVersion = true)
        {

            var code =
               await _contentManagementContext.MasterDataKeyValues.AsNoTracking().SingleOrDefaultAsync(cd => cd.Id == codeId);

            if (code == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeNotFound));

            AuthorizeManager.CheckViewAccess(code);

            var dotnetCode = await _contentManagementContext.MasterDataKeyValues.AsNoTracking()
       .SingleOrDefaultAsync(cd => cd.TypeId == (int)EntityIdentity.DotNetCode && cd.IsType);

            if (dotnetCode == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, CompileOutPutDirectory));
          

            var diskInfos = GetOutputs(dotnetCode.PathOrUrl + CompileOutPutDirectory, code.Code.Replace(DllExtention, "_*") + DllExtention);

            if(diskInfos.Count == 0)
                diskInfos = GetOutputs(dotnetCode.PathOrUrl + CompileOutPutDirectory, code.Code);

            var outputVersions = new List<KeyValue>();
            if (showSuggestionLatestVersion)
            {
                outputVersions.Add(new KeyValue() { Id = 0, Value = LanguageManager.GetText(TextKey.LastCompiledVersionText) });
            }
            foreach (var output in diskInfos)
            {
                if (output.Name.IndexOf(DllExtention, StringComparison.OrdinalIgnoreCase) == output.Name.Length - 4)
                {
                    var temp = output.Name.Substring(code.Code.IndexOf(DllExtention, StringComparison.OrdinalIgnoreCase));
                    var strVersion = temp.Replace(DllExtention, "").Replace(ReleaseMode, "").Replace(DebugMode, "");
                    var version =
                       Convert.ToInt32(strVersion == "" ? "0" : strVersion);

                    if (!outputVersions.Exists(ov => ov.Id == version))
                        outputVersions.Add(new KeyValue() {Id = version, Value = version.ToString()});
                }
            }

            return JArray.Parse(JsonConvert.SerializeObject
               (outputVersions, Formatting.None));

        }

        private List<DiskInfo> GetOutputs(string path,string pattern)
        {
            _fileSystemManager.CreatDirectoryIfNotExist(path);

            var outPuts = _fileSystemManager.GetFiles(path, pattern);

            var id = 0;
            var diskInfos = new List<DiskInfo>();
            foreach (var output in outPuts)
            {
                diskInfos.Add(new DiskInfo()
                {
                    Id = ++id,
                    Name = output.Name,
                    ModifieDateTime = output.LastWriteTimeUtc,
                    Size = output.Length,
                    ModifieLocalDateTime = LanguageManager.ToLocalDateTime(output.LastWriteTimeUtc)
                });
            }

            return diskInfos;
        }
        protected bool DeleteFile(string path, string name, string extention,bool isClassOrMethodOrLine)
        {


            path = path.Replace("//", "/");
            var finallName = name;
            if (isClassOrMethodOrLine)
            {
                var directory = path.Substring(path.LastIndexOf("/", StringComparison.Ordinal) + 1).Replace("-", ".");
                finallName = name.Substring(name.LastIndexOf(directory, StringComparison.Ordinal) + directory.Length + 1);
            }


            return _fileSystemManager.DeleteFile((path + "/" + finallName + extention).Replace("//", "/"));
        }
        #region [Save]
        public async Task<JObject> Save(JObject data)
        {
            dynamic codeDto = data;
            int? codeId = codeDto.Id;
            var code = new MasterDataKeyValue
            {
                Id = codeId ?? 0
            };
            bool isNew = codeDto.IsNew;

            bool checkIn = codeDto.CheckIn;
            string comment = codeDto.Comment;

            //is close changin in code
            bool isCloseChangin = codeDto.EnableCache;

            var currentCode = await _contentManagementContext.MasterDataKeyValues.AsNoTracking().SingleOrDefaultAsync(cd => cd.Id == code.Id);

            if (!isNew)
            {
                
                if (currentCode == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeNotFound));

                if (currentCode.EditMode)
                {
                    _sourceControl.CheckCodeCheckOute(currentCode);

                }


                if (isCloseChangin != currentCode.EnableCache)
                {
                    if (!AuthorizeManager.AuthorizeActionOnEntityId(currentCode.Id, (int) EntityIdentity.DotNetCode,
                        (int) ActionKey.CloseOrOpenChangingCode))
                        throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidCloseOrOpenChangingCodeGrant, currentCode.Code));
 ;
                }
                else if (currentCode.EnableCache)
                {
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidCloseOrOpenChangingCodeGrant, currentCode.Code));

                }

                code = currentCode;
                code.RowVersion = codeDto.RowVersion;

                _contentManagementContext.MasterDataKeyValues.Attach(code);
            }
            else
            {

                _contentManagementContext.MasterDataKeyValues.Add(code);
            }

           


            int? parentId = codeDto.ParentId;
            var checkParentModifyAccess = parentId != currentCode?.ParentId || isNew;
           
            code.ParentId = codeDto.ParentId;

            var parentCode = await _contentManagementContext.MasterDataKeyValues.AsNoTracking()
                .SingleOrDefaultAsync(cd => cd.Id== code.ParentId);

            if (parentCode == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ParentRecordNotFound));



            if (checkParentModifyAccess)
            {
                if (parentCode.EnableCache)
                {
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidCloseOrOpenChangingCodeGrant, parentCode.Code));

                }

                AuthorizeManager.CheckParentNodeModifyAccessForAddingChildNode(parentCode, parentCode.Id);
            }

           


            if ((currentCode?.ForeignKey1 == (int) DotNetCodeType.CompiledDll
                 || currentCode?.ForeignKey1 == (int) DotNetCodeType.NotCompiledDll
                 || currentCode?.ForeignKey1 == (int) DotNetCodeType.NugetDll
                 || currentCode?.ForeignKey1 == (int)DotNetCodeType.UnitTestDll) && parentCode.ForeignKey1 != null)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.DllParentMustBeFolder));
            }
            if (currentCode?.ForeignKey1 == (int)DotNetCodeType.NamaeSpace && parentCode.ForeignKey1 != (int)DotNetCodeType.CompiledDll
                 && parentCode.ForeignKey1 != (int)DotNetCodeType.NotCompiledDll
                 && parentCode.ForeignKey1 != (int)DotNetCodeType.UnitTestDll
                 && parentCode.ForeignKey1 != (int)DotNetCodeType.NugetDll)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.NameSpaceParentMustBeDll));
            }
            if (currentCode?.ForeignKey1 == (int)DotNetCodeType.Class && parentCode.ForeignKey1 != (int)DotNetCodeType.NamaeSpace)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ClassParentMustBeNameSpace));
            }
            if (currentCode?.ForeignKey1 == (int)DotNetCodeType.Method && parentCode.ForeignKey1 != (int)DotNetCodeType.Class)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.MethodParentMustBeClass));
            }
            if (currentCode?.ForeignKey1 == (int)DotNetCodeType.LinesOfCode && parentCode.ForeignKey1 != (int)DotNetCodeType.Class
                && parentCode.ForeignKey1 != (int)DotNetCodeType.Method)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.LineOfCodeParentMustBeMethodOrClass));
            }

            //DotNet Code Type
            code.ForeignKey1 = codeDto.ForeignKey1;
      

            if (code.ForeignKey1 == (int) DotNetCodeType.CompiledDll
                || code.ForeignKey1 == (int) DotNetCodeType.NotCompiledDll
                 || code.ForeignKey1 == (int)DotNetCodeType.UnitTestDll
                || code.ForeignKey1 == (int) DotNetCodeType.NugetDll)
            {
                //ancient Parent
                code.ForeignKey2 = null;

                //editor Language
                code.ForeignKey3 = codeDto.ForeignKey3;


                //dll name
                code.Code =  codeDto.Code;

                //dll storePlace Type : global local or bin
                code.Key = codeDto.Key;

                //local dll version that use for refrence in other dll 
                if (code.Key == (int)DllStorePlace.Output)
                {
                    try
                    {
                        code.SlidingExpirationTimeInMinutes = codeDto.SlidingExpirationTimeInMinutes;
                    }
                    catch (Exception)
                    {
                        //convert null to defaults value : 0
                        code.SlidingExpirationTimeInMinutes = 0;
                    }
                }
            }
            else
            {
                //ancient Parent
                code.ForeignKey2 = 
                    code.ForeignKey1 == (int)DotNetCodeType.NamaeSpace
                    ? parentCode.Id : parentCode.ForeignKey2;

                var ancientParent = await _contentManagementContext.MasterDataKeyValues.SingleOrDefaultAsync(cd => cd.Id == code.ForeignKey2);

                if (ancientParent == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ParentRecordNotFound));

                if (ancientParent.EnableCache)
                {
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidCloseOrOpenChangingCodeGrant, ancientParent.Code));

                }

                //editor Language
                code.ForeignKey3 = parentCode.ForeignKey3;

                //full name
                code.Code = parentCode.Code + "." + codeDto.Code;

            }


        

            var repeatedCode = await _contentManagementContext.MasterDataKeyValues
               .Where(cd => cd.Code == code.Code && cd.ParentId == code.ParentId).CountAsync();

            if ((repeatedCode > 0 && isNew) || (repeatedCode > 1 && !isNew))
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.RepeatedValue, code.Code));


            if (code.ForeignKey1 == (int) DotNetCodeType.NamaeSpace 
                || code.ForeignKey1 == (int)DotNetCodeType.CompiledDll
                || code.ForeignKey1 == (int)DotNetCodeType.NotCompiledDll
                || code.ForeignKey1 == (int)DotNetCodeType.UnitTestDll
                || code.ForeignKey1 == (int)DotNetCodeType.NugetDll
                || code.ForeignKey1 == null)
            {
                
                //if (code.ForeignKey1 == (int) DotNetCodeType.NamaeSpace)
                //{
                    code.PathOrUrl = parentCode.PathOrUrl + "/" + code.Code.Replace(parentCode.Code + ".", "").Replace(".", "-");
                //}
                //else
                //{
                //    code.PathOrUrl = parentCode.PathOrUrl + "/" + code.Code.Replace(".", "-");
                //}
            }
            else
            {
                code.PathOrUrl = parentCode.PathOrUrl;
            }




                //optional name
            code.Name = codeDto.Name;


            


            if (code.ForeignKey1 == (int)DotNetCodeType.CompiledDll
                || code.ForeignKey1 == (int)DotNetCodeType.NotCompiledDll
                || code.ForeignKey1 == (int)DotNetCodeType.UnitTestDll
                || code.ForeignKey1 == (int)DotNetCodeType.NugetDll)
            {
                JArray dependentDllArray = codeDto.DependentDlls;
                var dllId = dependentDllArray.ToObject<List<int>>();

                //var dlls = await _contentManagementContext.MasterDataKeyValues
                //    .Where(md => dllId.Contains(md.Id)).Include(md => md.Parent).ToListAsync();
                //var nameSpacesName = new List<KeyValue>();
                //foreach (var dll in dlls)
                //{
                //    nameSpacesName.Add(new KeyValue()
                //    {
                //        Key = dll.Parent.Code,
                //        Value = dll.Code.Replace(dll.Parent.Code+".", "")
                //    });
                //}


                var unAuthorizedDlls = await AuthorizeManager.AuthorizeReferencingDllAsync(dllId);
                if (unAuthorizedDlls.Any())
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.DllReferencingAccessDeny, string.Join(",", unAuthorizedDlls)));
        
                code.SecondCode = dependentDllArray.ToString();
            }
            else
            {
                //placeHolder
                code.SecondCode = (string)codeDto.SecondCode;

                dynamic parentCodeObject = await GetAsync(parentCode.Id);
                string paerntCodeFile = parentCodeObject.DotNetCode;

                if (paerntCodeFile.IndexOf(code.SecondCode, StringComparison.Ordinal) <= -1)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PlaceHolderSignNotExistInParentCode));
            }


            code.EnableCache = isCloseChangin;
            code.Guid = codeDto.Guid;
            code.Description = codeDto.Description;
            code.Version =  (currentCode?.Version ?? 0)+1;
        

            try
            {
                code.Order = codeDto.Order;
            }
            catch (Exception)
            {
                code.Order = 1;
            }



            code.IsLeaf = codeDto.IsLeaf;
            code.Language = Config.DefaultsLanguage;

            //if(service.IsLeaf)
            if (currentCode != null)
            {
                code.ViewRoleId = currentCode.ViewRoleId;
                code.ModifyRoleId = currentCode.ModifyRoleId;
                code.AccessRoleId = currentCode.AccessRoleId;
            }
            AuthorizeManager.SetAndCheckModifyAndAccessRole(code, codeDto);

            code.Status = codeDto.Status;
            code.EditMode = codeDto.EditMode;


            code.TypeId = (int)EntityIdentity.DotNetCode;

            //if (code.ForeignKey2 != null)
            //{
            //    var ancientParent = await _contentManagementContext.MasterDataKeyValues.FirstOrDefaultAsync(
            //        cd => cd.Id == code.ForeignKey2);
            //    ancientParent.Version++;
            //}

            await _contentManagementContext.SaveChangesAsync();

            string dotNetCode = codeDto.DotNetCode;

            _fileSystemManager.CreatDirectoryIfNotExist(code.PathOrUrl);

            if (code.ForeignKey1 == (int) DotNetCodeType.NotCompiledDll ||
                code.ForeignKey1 == (int)DotNetCodeType.UnitTestDll ||
                code.ForeignKey1 == (int) DotNetCodeType.NamaeSpace
                || code.ForeignKey1 == (int) DotNetCodeType.Class
                || code.ForeignKey1 == (int) DotNetCodeType.Method
                || code.ForeignKey1 == (int) DotNetCodeType.LinesOfCode)
            {
                if (!string.IsNullOrEmpty(dotNetCode))
                {
                    var changeFileName = code.Code;
                    if (code.ForeignKey1 == (int) DotNetCodeType.NamaeSpace)
                    {
                        code.Code = code.Code.Replace(parentCode.Code, "").TrimStart('.');
                    }

                    await _sourceControl.AddChange(code.PathOrUrl, changeFileName +
                                                                   GetCodeFileExtention(code.ForeignKey3 ??
                                                                                        (int) SourceType.Csharp)
                        , dotNetCode, code.Version,
                        comment);


                    if (checkIn)
                    {
                        if (code.ForeignKey3 == (int) SourceType.Csharp)
                        {
                            dotNetCode = CSharpStartComment + dotNetCode + CSharpEndComment;
                        }
                        await WriteFileAsync(code.PathOrUrl, code.Code,
                            GetCodeFileExtention(code.ForeignKey3 ?? (int) SourceType.Csharp), dotNetCode,
                            code.ForeignKey1 == (int)DotNetCodeType.Class || 
                            code.ForeignKey1 == (int)DotNetCodeType.Method || code.ForeignKey1 == (int)DotNetCodeType.LinesOfCode);
                    }

                }
            }


            //code.Code = code.Code.Replace(parentCode.Code, "").TrimStart('.');
            return await GetAsync(code.Id);
        }
        #endregion [Save]


        public async Task<string> GetChangeFromSourceControlAsync(int changeId, int codeId)
        {
            //path = path.Replace(Config.UrlDelimeter, Helper.RootUrl);

            if (!AuthorizeManager.IsAuthorizeToViewSourceCodeOfDotNetCode(codeId))
                throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));

            var code = await _contentManagementContext.MasterDataKeyValues.FirstOrDefaultAsync(cd => cd.Id == codeId);

            var change = _sourceControl.GeChangeById(changeId, code.PathOrUrl);

            if (change == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ChangeNotFound));

            if (change.Code == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ChangeHasNoCode));
            return change.Code;

        }

        protected async Task<string> GetChangeByVersionFromSourceControlAsync(int version, int codeId)
        {
            //path = path.Replace(Config.UrlDelimeter, Helper.RootUrl);

            if (!AuthorizeManager.IsAuthorizeToViewSourceCodeOfDotNetCode(codeId))
                throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));

            var code = await _contentManagementContext.MasterDataKeyValues.FirstOrDefaultAsync(cd => cd.Id == codeId);

            var change = _sourceControl.GeChangeByNameAndVersion(version, code.PathOrUrl, code.Code+GetCodeFileExtention(code.ForeignKey3 ?? (int)SourceType.Csharp));

            if (change == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ChangeNotFound));

            if (change.Code == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ChangeHasNoCode));
            return change.Code;

        }
        public async Task<JObject> GetChangesFromSourceControlAsync(string orderBy, int skip, int take
          , string comment
           , string user
          , string fromDateTime
          , string toDateTime
            , int codeId)
        {


            var count = 0;

            var code = await _contentManagementContext.MasterDataKeyValues.FirstOrDefaultAsync(cd => cd.Id == codeId);
            return JObject.Parse(JsonConvert.SerializeObject
            (new
            {
                rows = _sourceControl.GeChangesByPagination(orderBy,
                        skip,
                        take,
                        code.PathOrUrl,
                        code.Code+GetCodeFileExtention(code.ForeignKey3 ?? (int)SourceType.Csharp),
                        comment,
                        user,
                        fromDateTime.Replace("-", Helper.RootUrl).Replace("_", ":"),
                        toDateTime.Replace("-", Helper.RootUrl).Replace("_", ":"),
                        out count)
                    .Select(sr => new
                    {
                        Id = sr.Id.ToString().Trim(),
                        sr.LocalDateTime,
                        sr.Comment,
                        sr.Version,
                        sr.User,
                        sr.DateTime
                    }),
                total = count
            }, Formatting.None));
        }

        public async Task<bool> PublishDll(JObject data)
        {
            dynamic codeData = data;
            int id;

            try
            {
                id = codeData.Id;
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "Code Id"));
       
            }
           // string path = codeData.Path;
            string outputName = codeData.Name;
            var code =
               await _contentManagementContext.MasterDataKeyValues.AsNoTracking().SingleOrDefaultAsync(cd => cd.Id == id);

            if (code == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeNotFound));

            var dotnetCode = await _contentManagementContext.MasterDataKeyValues.AsNoTracking()
       .SingleOrDefaultAsync(cd => cd.TypeId == (int)EntityIdentity.DotNetCode && cd.IsType);

            if (dotnetCode == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, CompileOutPutDirectory));
          

        

           var dllPath = dotnetCode.PathOrUrl  +  CompileOutPutDirectory 
                + outputName;

            if (!_fileSystemManager.FileExist(dllPath))
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.NotFound, outputName));


            if (AuthorizeManager.AuthorizeActionOnEntityId(code.Id, (int)EntityIdentity.DotNetCode,
                (int)ActionKey.PublishDll))
            {
                var backupPath = dotnetCode.PathOrUrl + PubliahBackUpDirectory + code.Guid + "/";
                if (code.ForeignKey1 == (int) DotNetCodeType.NotCompiledDll || code.ForeignKey1 == (int)DotNetCodeType.UnitTestDll)
                {
                    //var binDllPath = _fileSystemManager.RelativeToAbsolutePath("~/bin/");

                    //var binReleasePath = binDllPath +
                    //                 code.Code.Insert(code.Code.IndexOf(DllExtention, StringComparison.OrdinalIgnoreCase),
                    //                     ReleaseMode+code.SlidingExpirationTimeInMinutes);
                    //var binDebugPath = binDllPath +
                    //                 code.Code.Insert(code.Code.IndexOf(DllExtention, StringComparison.OrdinalIgnoreCase),
                    //                     DebugMode+code.SlidingExpirationTimeInMinutes);

                    
                    _fileSystemManager.CreatDirectoryIfNotExist(backupPath);
                    var dll =
                        await
                            _contentManagementContext.MasterDataKeyValues.SingleOrDefaultAsync(cd => cd.Id == id);
                    var temp = outputName.Substring(code.Code.IndexOf(DllExtention, StringComparison.OrdinalIgnoreCase));

                    //dll.SlidingExpirationTimeInMinutes =
                    //    Convert.ToInt32(temp.Remove(temp.IndexOf(DllExtention, StringComparison.OrdinalIgnoreCase)));

                    dll.SlidingExpirationTimeInMinutes =
                       Convert.ToInt32(temp.Replace(DllExtention,"").Replace(ReleaseMode,"").Replace(DebugMode,""));




                    _fileSystemManager.MoveAllFileSByPattern(_fileSystemManager.RelativeToAbsolutePath("~/bin/"), backupPath,
                 code.Code.Replace(DllExtention, "_*"),true);



                    //if (_fileSystemManager.FileExist(binReleasePath))
                    //    _fileSystemManager.MoveFile(binReleasePath,
                    //        backupPath
                    //        + code.Code.Insert(code.Code.IndexOf(DllExtention, StringComparison.OrdinalIgnoreCase),
                    //            ReleaseMode + code.SlidingExpirationTimeInMinutes.ToString()));

                    //if (_fileSystemManager.FileExist(binReleasePath.Replace(DllExtention, PdbExtention)))
                    //    _fileSystemManager.MoveFile(binReleasePath.Replace(DllExtention, PdbExtention),
                    //        dotnetCode.PathOrUrl + CompileOutPutDirectory + code.Guid + "/"
                    //        + outputName.Replace(DllExtention, PdbExtention));



                    //if (_fileSystemManager.FileExist(binDebugPath))
                    //    _fileSystemManager.MoveFile(binDebugPath,
                    //        backupPath
                    //        + code.Code.Insert(code.Code.IndexOf(DllExtention, StringComparison.OrdinalIgnoreCase),
                    //            DebugMode+code.SlidingExpirationTimeInMinutes.ToString()));

                    //if (_fileSystemManager.FileExist(binDebugPath.Replace(DllExtention, PdbExtention)))
                    //    _fileSystemManager.MoveFile(binDebugPath.Replace(DllExtention, PdbExtention),
                    //        dotnetCode.PathOrUrl + CompileOutPutDirectory + code.Guid + "/"
                    //        + outputName.Replace(DllExtention, PdbExtention));

                    

                }
                else
                {
                    //var binDllPath = _fileSystemManager.RelativeToAbsolutePath("~/bin/") + outputName;


                    _fileSystemManager.MoveAllFileSByPattern(_fileSystemManager.RelativeToAbsolutePath("~/bin/"), backupPath,
                code.Code,true);
                    _fileSystemManager.MoveAllFileSByPattern(_fileSystemManager.RelativeToAbsolutePath("~/bin/"), backupPath,
              code.Code.Replace(DllExtention, PdbExtention),true);

                    //if (_fileSystemManager.FileExist(binDllPath))
                    //    _fileSystemManager.MoveFile(binDllPath,
                    //        dotnetCode.PathOrUrl + CompileOutPutDirectory + code.Guid + "/"
                    //        + outputName);

                    //if (_fileSystemManager.FileExist(binDllPath.Replace(DllExtention, PdbExtention)))
                    //    _fileSystemManager.MoveFile(binDllPath.Replace(DllExtention, PdbExtention),
                    //        dotnetCode.PathOrUrl + CompileOutPutDirectory + code.Guid + "/"
                    //        + outputName.Replace(DllExtention, PdbExtention));
                }

             

                _fileSystemManager.CopyFile(dllPath, _fileSystemManager.RelativeToAbsolutePath("~/bin/") + outputName);

                if(_fileSystemManager.FileExist(dllPath.Replace(DllExtention,PdbExtention)))
                    _fileSystemManager.CopyFile(dllPath.Replace(DllExtention, PdbExtention),
                        _fileSystemManager.RelativeToAbsolutePath("~/bin/") + outputName.Replace(DllExtention, PdbExtention));



                await _contentManagementContext.SaveChangesAsync();
            }

            else
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidAccessToPublishDll, code.Code));

            }
            _notificationManager.BroadcastMessage("warning", "restartAppMessage");
            return true;
        }






        public async Task<string> ReadDllBuildLog(int codeId,string name)
        {

            var code =
               await _contentManagementContext.MasterDataKeyValues.AsNoTracking().SingleOrDefaultAsync(cd => cd.Id == codeId);

            if (code == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeNotFound));

            var dotnetCode = await _contentManagementContext.MasterDataKeyValues.AsNoTracking()
       .SingleOrDefaultAsync(cd => cd.TypeId == (int)EntityIdentity.DotNetCode && cd.IsType);

            if (dotnetCode == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, CompileOutPutDirectory));
         



            var logPath = dotnetCode.PathOrUrl + CompileOutPutDirectory
                 + name.Replace(DllExtention,TxtExtention).Replace(ReleaseMode,Log).Replace(DebugMode, Log);

            if (!_fileSystemManager.FileExist(logPath))
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.NotFound, name.Replace(DllExtention, TxtExtention)));
       

            if (AuthorizeManager.AuthorizeActionOnEntityId(code.Id, (int)EntityIdentity.DotNetCode,
                (int)ActionKey.ViewBuildLogOfDllOutput))
            {

                return await _fileSystemManager.ReadAsync(logPath);
            }
            throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidAccessToViewBuildLogOfDllOutput, code.Code));
 

        }

        public async Task<bool> DebugCode(JObject data)
        {
            dynamic codeData = data;
            int id;

            try
            {
                id = codeData.Id;
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "Code Id"));

            }
            var code =
                await _contentManagementContext.MasterDataKeyValues.AsNoTracking().SingleOrDefaultAsync(cd => cd.Id == id);

            if (code == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeNotFound));

            AuthorizeManager.SetAndCheckModifyAndAccessRole(code, null, false);


            if(code.ForeignKey1 == (int)DotNetCodeType.NotCompiledDll || code.ForeignKey1 == (int)DotNetCodeType.UnitTestDll)
                await DebugDll(code, code.Id, code.Version);
            else
            {
                var ancientParent = await _contentManagementContext.MasterDataKeyValues.AsNoTracking()
                    .SingleOrDefaultAsync(cd => cd.Id == code.ForeignKey2);

                if (ancientParent == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ParentRecordNotFound));

                await DebugDll(ancientParent, code.Id, code.Version);
            }



            return true;
        }

        private async Task<bool> DebugDll(MasterDataKeyValue dll,int debugCodeId,int debugCodeVersion)
        {
            var dllCodes = await _contentManagementContext.MasterDataKeyValues
                .Where(cd => cd.Id == dll.Id || cd.ForeignKey2 == dll.Id).AsNoTracking().ToListAsync();

       

            var dotnetCode = await _contentManagementContext.MasterDataKeyValues.AsNoTracking()
    .SingleOrDefaultAsync(cd => cd.TypeId == (int)EntityIdentity.DotNetCode && cd.IsType);

            if (dotnetCode == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, CompileOutPutDirectory));
          


            var listOfDotNetCode = await GetListOfDotnetCodeAsync(dllCodes);

            listOfDotNetCode[debugCodeId] = await GetChangeByVersionFromSourceControlAsync(debugCodeVersion,debugCodeId);

            var completeNode = new List<int>();

            while (completeNode.Count < dllCodes.Count)
            {
                foreach (var code in dllCodes)
                {
                    if ((dllCodes.All(cd => cd.ParentId != code.Id)
                     || (dllCodes.Any(cd => cd.ParentId == code.Id) &&
                     dllCodes.Any(cd => cd.ParentId == code.Id && completeNode.Exists(cn => cn == cd.Id))))
                     && !completeNode.Exists(cn => cn == code.Id))
                    {
                        var childCode = listOfDotNetCode.First(cd => cd.Key == code.Id);
                        if (code.ForeignKey1 != (int)DotNetCodeType.NotCompiledDll && code.ForeignKey1 != (int)DotNetCodeType.UnitTestDll)
                        {
                            var parentCode = listOfDotNetCode.First(cd => cd.Key == code.ParentId);

                            listOfDotNetCode[parentCode.Key] = parentCode.Value.Replace(code.SecondCode, childCode.Value);
                        }
                        else
                        {
                            listOfDotNetCode[childCode.Key] = childCode.Value.Replace(code.SecondCode, childCode.Value);
                        }

                        if (!completeNode.Exists(cn => cn == code.Id))
                            completeNode.Add(code.Id);
                    }
                }
            }



            var dependentDllArray = JArray.Parse(dll.SecondCode);
            var dependentDlls = dependentDllArray.ToObject<List<int>>();

            var refrencedDll = await _contentManagementContext.MasterDataKeyValues
                .Where(cd => dependentDlls.Contains(cd.Id)).AsNoTracking().ToListAsync();


             Compile(dll, dotnetCode.PathOrUrl, refrencedDll, listOfDotNetCode, true,true);
             Compile(dll, dotnetCode.PathOrUrl, refrencedDll, listOfDotNetCode, false,true);



            return true;
        }

        public async Task<bool> DllCompile(JObject data)
        {
            dynamic codeData = data;
            int id;

            try
            {
                id = codeData.Id;
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "Code Id"));

            }
            var code =
                await _contentManagementContext.MasterDataKeyValues.AsNoTracking().SingleOrDefaultAsync(cd => cd.Id == id);

            if (code == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeNotFound));

            //AuthorizeManager.SetAndCheckModifyAndAccessRole(code, null, false);

            if (code.ForeignKey1 != (int)DotNetCodeType.NotCompiledDll && code.ForeignKey1 != (int)DotNetCodeType.UnitTestDll)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeTypeNoNeedToCompile));


            if (AuthorizeManager.AuthorizeActionOnEntityId(code.Id, (int) EntityIdentity.DotNetCode,
                (int) ActionKey.CompileDll))
            {
                bool byDependency = codeData.ByDependency;
                if(byDependency)
                    await CompileDllByDependency(code);
                else
                    await CompileDll(code);
            }
                
            else
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidAccessToCompileDll, code.Code));

            }
            return true;
        }

        private async Task<bool> CompileDllByDependency(MasterDataKeyValue dll)
        {

            var dependentDllArray = JArray.Parse(dll.SecondCode);
            var dependentDlls = dependentDllArray.ToObject<List<int>>();

            var refrencedDlls = await _contentManagementContext.MasterDataKeyValues
                .Where(cd => dependentDlls.Contains(cd.Id)).AsNoTracking().ToListAsync();

            foreach (var refrencedDll in refrencedDlls)
            {
                if (refrencedDll.ForeignKey1 == (int) DotNetCodeType.NotCompiledDll  || refrencedDll.ForeignKey1 == (int)DotNetCodeType.UnitTestDll)
                {
                    await CompileDllByDependency(refrencedDll);
                }
            }

            //try
            //{
                await CompileDll(dll);
            //}
            //catch (DllAlreadyCompiledException)
            //{
            //    if (showAlreadyCompiledDllException)
            //        throw;
            //}

            return true;
        }

        

        private async Task<bool> CompileDll(MasterDataKeyValue dll)
        {
            var dllCodes = await _contentManagementContext.MasterDataKeyValues
                .Where(cd => cd.Id == dll.Id || cd.ForeignKey2 == dll.Id).AsNoTracking().ToListAsync();

            CheckCodeCheckout(dllCodes);

            var dotnetCode = await _contentManagementContext.MasterDataKeyValues.AsNoTracking()
    .SingleOrDefaultAsync(cd => cd.TypeId == (int)EntityIdentity.DotNetCode && cd.IsType);

            if (dotnetCode == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, CompileOutPutDirectory));
     

            //if (dll.Value == dll.Version && _fileSystemManager.FileExist(dotnetCode.PathOrUrl +  CompileOutPutDirectory + dll.Guid + "/" 
            //    + dll.Code.Insert(dll.Code.IndexOf(DllExtention, StringComparison.OrdinalIgnoreCase),ReleaseMode + dll.Version))
            //    && _fileSystemManager.FileExist(dotnetCode.PathOrUrl + CompileOutPutDirectory + dll.Guid + "/"
            //    + dll.Code.Insert(dll.Code.IndexOf(DllExtention, StringComparison.OrdinalIgnoreCase),DebugMode+ dll.Version)))
            //    throw new DllAlreadyCompiledException();

            var listOfDotNetCode = await GetListOfDotnetCodeAsync(dllCodes);
            var completeNode = new List<int>();

            while (completeNode.Count < dllCodes.Count)
            {
                foreach (var code in dllCodes)
                {

                    //if (((code.Childrens == null)
                    //    || (code.Childrens != null && code.Childrens.All(cd => completeNode.Exists(cn => cn == cd.Id))))
                    //    && !completeNode.Exists(cn => cn == code.Id))
                    if ((dllCodes.All(cd => cd.ParentId != code.Id)
                     || (dllCodes.Any(cd => cd.ParentId == code.Id) &&
                     dllCodes.Any(cd => cd.ParentId == code.Id && completeNode.Exists(cn => cn == cd.Id))))
                     && !completeNode.Exists(cn => cn == code.Id))
                    {
                            var childCode = listOfDotNetCode.First(cd => cd.Key == code.Id);
                        if (code.ForeignKey1 != (int) DotNetCodeType.NotCompiledDll && code.ForeignKey1 != (int)DotNetCodeType.UnitTestDll)
                        {
                            var parentCode = listOfDotNetCode.First(cd => cd.Key == code.ParentId);

                            listOfDotNetCode[parentCode.Key] = parentCode.Value.Replace(code.SecondCode, childCode.Value);
                        }
                        else
                        {
                            listOfDotNetCode[childCode.Key] = childCode.Value.Replace(code.SecondCode, childCode.Value);
                        }

                        if (!completeNode.Exists(cn => cn == code.Id))
                            completeNode.Add(code.Id);
                    }
                } 
            }

            

            var dependentDllArray = JArray.Parse(dll.SecondCode);
            var dependentDlls = dependentDllArray.ToObject<List<int>>();

            var refrencedDll = await _contentManagementContext.MasterDataKeyValues
                .Where(cd => dependentDlls.Contains(cd.Id)).AsNoTracking().ToListAsync();

            var newVersion = 0;

            foreach (var code in dllCodes.OrderBy(dc => dc.Id))
            {
                newVersion += code.Version;
            }

            var dllCode = await _contentManagementContext.MasterDataKeyValues
               .Where(cd => cd.Id == dll.Id).FirstOrDefaultAsync();

            if (dllCode.Value == null)
                dllCode.Value = 0;

            if (Convert.ToInt32(dllCode.Data ?? "0") != newVersion)
            {
                dllCode.Value++;
                dll.Value = dll.Value == null ? 1 : dll.Value +1;
                dllCode.Data = newVersion.ToString();
            }

            var debugReferences = Compile(dll,dotnetCode.PathOrUrl, refrencedDll, listOfDotNetCode, true);
            var releaseReferences = Compile(dll, dotnetCode.PathOrUrl, refrencedDll, listOfDotNetCode, false);

           
          
            var log = "Debug References : " + string.Join(",", debugReferences) + Environment.NewLine;
            log += "Release References : " + string.Join(",", releaseReferences) + Environment.NewLine;
            log += "Codes : " + Environment.NewLine;

            foreach (var code in dllCodes.OrderBy(dc=>dc.Id))
            {
                log += code.Name + " : " + Environment.NewLine;
                log +=  "Version : " + code.Version + Environment.NewLine;
            }

           

           

            await _contentManagementContext.SaveChangesAsync();

            await _fileSystemManager.WriteAsync(_fileSystemManager.RelativeToAbsolutePath(dotnetCode.PathOrUrl
                + CompileOutPutDirectory
                + dllCode.Code.Insert(dllCode.Code.IndexOf(DllExtention, StringComparison.OrdinalIgnoreCase),
                Log + dllCode.Value).Replace(DllExtention,TxtExtention)), log);

            _debugger.DeleteDebugDataBase(dll.PathOrUrl);
            return true;
        }

        private string[] Compile(MasterDataKeyValue dll,string outputPath
            ,IReadOnlyCollection<MasterDataKeyValue> refrencedDll,Dictionary<int,string> listOfDotNetCode, bool isDebugMode
            , bool isDebugCompileMode=false)
        {
            var mode = isDebugMode ? DebugMode : ReleaseMode;
            var compileOption = new CompileOption();
            compileOption.IncludeDebugInformation = isDebugMode;


            if (listOfDotNetCode[dll.Id].IndexOf(DebugInfoTemplate.Replace("()", ""), StringComparison.Ordinal) >=
               0 && listOfDotNetCode[dll.Id].IndexOf(DebugInfoTemplate, StringComparison.Ordinal) < 0 
               && listOfDotNetCode[dll.Id].IndexOf(CodePathTemplate, StringComparison.Ordinal) >=
               0)
                throw new CodeTemplateException(DebugInfoTemplate);

            if (listOfDotNetCode[dll.Id].IndexOf(DebugInfoTemplate, StringComparison.Ordinal) >= 0)
            {

                listOfDotNetCode[dll.Id] = listOfDotNetCode[dll.Id].Replace(DebugInfoTemplate,
                 DebugInfoTemplate.Replace("()","(true," + dll.Value + ")"));
            }

           

            compileOption.Code = listOfDotNetCode[dll.Id]= listOfDotNetCode[dll.Id].Replace(CodePathTemplate , dll.PathOrUrl );



            compileOption.CodeProvider = dll.ForeignKey3 == (int)SourceType.Csharp ? SourceType.Csharp
                : SourceType.VisualBasic;
            compileOption.GenerateExecutable = false;




            var coreRefrences = refrencedDll.Where(rd => rd.Key == (int)DllStorePlace.Global).Select(rd => rd.Code).ToArray();
            var binRefrences = refrencedDll.Where(rd => rd.Key == (int)DllStorePlace.Bin).Select(rd => rd.Code).ToArray();
            var outputRefrences = refrencedDll.Where(rd => rd.Key == (int)DllStorePlace.Output).Select(rd =>
            new { rd.Code, rd.PathOrUrl, rd.Value, rd.SlidingExpirationTimeInMinutes, rd.Guid, rd.ForeignKey1 }).ToArray();

            var librararyRefrence = new string[binRefrences.Length + coreRefrences.Length + outputRefrences.Length];
            var i = 0;
            for (; i <= binRefrences.Length - 1; i++)
            {
                librararyRefrence[i] = _fileSystemManager.RelativeToAbsolutePath("~/bin/") + binRefrences[i];
            }

            var j = 0;
            for (; j <= coreRefrences.Length - 1; j++)
            {
                librararyRefrence[i + j] = coreRefrences[j];
            }

            for (var k = 0; k <= outputRefrences.Length - 1; k++)
            {
                if (outputRefrences[k].ForeignKey1 == (int)DotNetCodeType.NotCompiledDll || outputRefrences[k].ForeignKey1 == (int)DotNetCodeType.UnitTestDll)
                {
                    librararyRefrence[i + j + k] = _fileSystemManager.RelativeToAbsolutePath(outputPath + CompileOutPutDirectory
                    ) + outputRefrences[k].Code.Insert(outputRefrences[k].Code.IndexOf(DllExtention, StringComparison.OrdinalIgnoreCase),
                    mode + (outputRefrences[k].SlidingExpirationTimeInMinutes == 0 ?
                    (outputRefrences[k].Value ?? 0).ToString(): outputRefrences[k].SlidingExpirationTimeInMinutes.ToString()));
                }
                else
                {
                    librararyRefrence[i + j + k] = _fileSystemManager.RelativeToAbsolutePath(outputPath + CompileOutPutDirectory
                    ) + outputRefrences[k].Code;
                }
            }

            compileOption.LibraryRefrences = librararyRefrence;
            _fileSystemManager.CreatDirectoryIfNotExist(outputPath + CompileOutPutDirectory);

            if (isDebugCompileMode)
            {
                var dllPath =
                    _fileSystemManager.RelativeToAbsolutePath(outputPath + CompileOutPutDirectory
                                                              + SecureGuid.NewGuid().ToString("N") + DllExtention);
                _compiler.Compile(compileOption, dllPath);

                _fileSystemManager.DeleteFile(dllPath);
                _fileSystemManager.DeleteFile(dllPath.Replace(DllExtention,PdbExtention));
            }
            else
            {
                _compiler.Compile(compileOption,
                    _fileSystemManager.RelativeToAbsolutePath(outputPath + CompileOutPutDirectory
                                                              +
                                                              dll.Code.Insert(
                                                                  dll.Code.IndexOf(DllExtention,
                                                                      StringComparison.OrdinalIgnoreCase),
                                                                  mode + dll.Value)));
            }

            return librararyRefrence;
        }



        private async Task<Dictionary<int,string>> GetListOfDotnetCodeAsync(List<MasterDataKeyValue> dllCodes)
        {
            var codeByDotnetcOde = new Dictionary<int,string>();

            foreach (var code in dllCodes)
            {
                var realCode = code.Code;
                if (code.ForeignKey1 == (int)DotNetCodeType.NamaeSpace)
                {
                    code.Code = code.Code.Replace(dllCodes.Find(cd=>cd.Id == code.ParentId).Code, "").TrimStart('.');  
                }
                codeByDotnetcOde.Add(code.Id, await GetResorcesAsync(code, realCode));
            }
            return codeByDotnetcOde;
        }
        private void CheckCodeCheckout(List<MasterDataKeyValue> dllCodes)
        {

            var checkOutExceptionMessage = new List<string>();
            foreach (var code in dllCodes)
            {
                if (code.EditMode)
                {
                    try
                    {
                        _sourceControl.CheckCodeCheckOute(code);
                    }
                    catch (CheckOutRecordException ex)
                    {

                        checkOutExceptionMessage.Add(
                            LanguageManager.ToAsErrorMessage(
                                message:
                                string.Format(LanguageManager.GetException(ExceptionKey.CheckOutCode), code.Code,
                                    ex.ModifyUser)));

                    }

                }
            }

            if (checkOutExceptionMessage.Count > 0)
                throw new CheckOutCodeException(string.Join(",", checkOutExceptionMessage));
        }

        public async Task<bool> Delete(JObject data)
        {
            dynamic codeData = data;
            int id;

            try
            {
                id = codeData.Id;
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "Code Id"));

            }
            var code = await _contentManagementContext.MasterDataKeyValues.SingleOrDefaultAsync(cd => cd.Id == id);

            if (code == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeNotFound));

            AuthorizeManager.SetAndCheckModifyAndAccessRole(code, null, false);


            if (code.EditMode)
            {
                _sourceControl.CheckCodeCheckOute(code);

            }

            bool codeIsFolder = code.ForeignKey1 == (int) DotNetCodeType.NamaeSpace
                || code.ForeignKey1 == (int) DotNetCodeType.CompiledDll
                || code.ForeignKey1 == (int) DotNetCodeType.NotCompiledDll
                || code.ForeignKey1 == (int)DotNetCodeType.UnitTestDll
                || code.ForeignKey1 == (int) DotNetCodeType.NugetDll
                || code.ForeignKey1 == null;

            string codeFileName;

            if (!codeIsFolder)
            {
                //if (codeFileName.LastIndexOf("-" + GetCodeFileExtention(code.ForeignKey3 ?? (int)SourceType.Csharp), StringComparison.Ordinal) == codeFileName.Length - 4)
                //{
                //    codeFileName = codeFileName.Remove(codeFileName.Length - 4);
                //    codeFileName += "." + GetCodeFileExtention(code.ForeignKey3 ?? (int)SourceType.Csharp);
                //}
                codeFileName = code.Code;
                if (
                    codeFileName.LastIndexOf(GetCodeFileExtention(code.ForeignKey3 ?? (int) SourceType.Csharp),
                        StringComparison.Ordinal) != codeFileName.Length - 4)
                {
                    codeFileName += GetCodeFileExtention(code.ForeignKey3 ?? (int)SourceType.Csharp);
                }
            }
            else
            {
                var parentCode = await _contentManagementContext.MasterDataKeyValues
                    .SingleOrDefaultAsync(cd => cd.Id == code.ParentId);

                if (parentCode == null)
                    throw new KhodkarInvalidException(LanguageManager
                        .ToAsErrorMessage(ExceptionKey.ParentRecordNotFound));

                codeFileName = code.Code.Replace(parentCode.Code+".", "").Replace(".", "-");
                code.PathOrUrl = code.PathOrUrl.Remove(code.PathOrUrl.LastIndexOf("/", StringComparison.Ordinal));
            }

            var recycleFileName = codeFileName;
            if (recycleFileName.IndexOf(".", StringComparison.Ordinal) > -1 || recycleFileName.IndexOf("-", StringComparison.Ordinal) > -1)
            {
                var arr = codeFileName.Split('-');
                if (!codeIsFolder)
                {
                    arr = codeFileName.Split('.');

                }
                recycleFileName = arr[arr.Length - 1];
                if (arr.Length >= 3)
                    recycleFileName = arr[arr.Length - 2];
            }

            _contentManagementContext.MasterDataKeyValues.Remove(code);

            await _contentManagementContext.SaveChangesAsync();

            var finallName = codeFileName;
            var isClassOrMethodOrLine = code.ForeignKey1 == (int) DotNetCodeType.Class
                                        || code.ForeignKey1 == (int) DotNetCodeType.Method
                                        || code.ForeignKey1 == (int) DotNetCodeType.LinesOfCode;
            if (isClassOrMethodOrLine)
            {
                var directory = code.PathOrUrl.Substring(code.PathOrUrl.LastIndexOf("/", StringComparison.Ordinal) + 1).Replace("-", ".");
                finallName = codeFileName.Substring(codeFileName.LastIndexOf(directory, StringComparison.Ordinal) + directory.Length + 1);
            }

            _sourceControl.RecycleBin(code.PathOrUrl.Replace(code.Code.Replace(".", "-"), ""), finallName, recycleFileName, codeIsFolder);

            if(codeIsFolder)
            _fileSystemManager.DeleteDirectory((code.PathOrUrl + "/" + codeFileName).Replace(@"//","/"));
            else
            {
                
                    DeleteFile(code.PathOrUrl, code.Code, GetCodeFileExtention(code.ForeignKey3 ?? (int) SourceType.Csharp), isClassOrMethodOrLine);
            }

            return true;
        }

        //public async Task<JObject> GetAsync(int id)
        //{

        //    return await GetCodeAsync(id);

        //}

        public async Task<JObject> GetAsync(int id)
        {
            
            var codeQuery = _contentManagementContext.MasterDataKeyValues.Where(cd => cd.Id == id).AsNoTracking().FutureFirstOrDefault();
            var maxIdQuery = _contentManagementContext.MasterDataKeyValues.OrderByDescending(md => md.Id).AsNoTracking().FutureFirstOrDefault();

            var code = codeQuery.Value;
            var maxId = maxIdQuery.Value;

            if (code == null)
                return null;

            var parentCode = await _contentManagementContext.MasterDataKeyValues.AsNoTracking()
                .FirstOrDefaultAsync(cd => cd.Id == code.ParentId);

            if (parentCode == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ParentRecordNotFound));

            var realCode = code.Code;
            if (code.ForeignKey1 == (int)DotNetCodeType.NamaeSpace)
            {
                code.Code = code.Code.Replace(parentCode.Code, "").TrimStart('.');
            }

            AuthorizeManager.CheckViewAccess(code);
            if (!AuthorizeManager.IsAuthorizeToViewSourceCodeOfDotNetCode(code.Id))
                throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));
            return await ConvertToJsonAsync(code, maxId.Id + 1, realCode, code.Code.Replace(parentCode.Code, "").TrimStart('.'));
        }

        private async Task<MasterDataKeyValue> GetAncientParent(int codeId)
        {
            var code = await _contentManagementContext.MasterDataKeyValues
               .SingleOrDefaultAsync(cd => cd.Id == codeId);

            if (code == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeNotFound));

            AuthorizeManager.SetAndCheckModifyAndAccessRole(code, null, false);

            if (code.ForeignKey1 == (int) DotNetCodeType.CompiledDll
                || code.ForeignKey1 == (int) DotNetCodeType.NotCompiledDll
                || code.ForeignKey1 == (int)DotNetCodeType.UnitTestDll
                || code.ForeignKey1 == (int) DotNetCodeType.NugetDll)
            {
                return code;
            }

            var ancientParent = await _contentManagementContext.MasterDataKeyValues
                .SingleOrDefaultAsync(cd => cd.Id == code.ForeignKey2);

            if (ancientParent == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ParentRecordNotFound));
            return ancientParent;

        }

        public async Task<DebugInfo> AddOrUpdateDebugInfo(DebugInfo debugInfo)
        {

            var ancientParent = await GetAncientParent(debugInfo.CodeId);

            

            return _debugger.AddOrUpdateDebugInfo(debugInfo, ancientParent.PathOrUrl);
        }

        public async Task<bool> DeleteDebugInfo(JObject data)
        {
            dynamic codeData = data;
            int codeId;

            try
            {
                codeId = codeData.CodeId;
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "Code Id"));
            }


            var ancientParent = await GetAncientParent(codeId);

           if(!AuthorizeManager.AuthorizeActionOnEntityIdByVersion(ancientParent.Id,
                (int)EntityIdentity.DotNetCode,
                ancientParent.Value??0,
                (int)ActionKey.DeleteDebugInfo))
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidAccessToDeleteDebugInfo, ancientParent.Code));
      

            JArray servicesCodeArray = codeData.DebugInfoIds;
            var debugInfoIds = servicesCodeArray.ToObject<List<int>>();


            _debugger.DeleteDebugInfo(debugInfoIds, ancientParent.PathOrUrl, ancientParent.Value??0);

            return true;
        }

        public async Task<DebugInfo> GetDebugInfo(int debugInfoId, int codeId)
        {

            var ancientParent = await GetAncientParent(codeId);

            if(!AuthorizeManager.AuthorizeActionOnEntityIdByVersion(ancientParent.Id,
                 (int)EntityIdentity.DotNetCode,
                 ancientParent.Value ?? 0,
                 (int)ActionKey.ReadDebugInfo))
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidAccessToReadDebugInfo, ancientParent.Code));
       

            return _debugger.GetDebugInfo(debugInfoId, ancientParent.Code);


        }

        public async Task<JObject> GetDebugInfosByPagination(
        string orderBy, int skip, int take
        , int codeId
        , string data
        , int? integerValue
        , decimal? decimalValue
        , string fromDateTime
        , string toDateTime)
        {

            var ancientParent = await GetAncientParent(codeId);

            if(!AuthorizeManager.AuthorizeActionOnEntityIdByVersion(ancientParent.Id,
                  (int)EntityIdentity.DotNetCode,
                  ancientParent.Value ?? 0,
                  (int)ActionKey.ReadDebugInfo))
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidAccessToReadDebugInfo, ancientParent.Code));


            var count = 0;
         
            return JObject.Parse(JsonConvert.SerializeObject
           (new
           {
               rows = _debugger.GetDebugInfosByPagination(orderBy, skip, take
                , ancientParent.PathOrUrl
                , codeId
               , ancientParent.Value ?? 0
                , data
                , integerValue
                , decimalValue
                , fromDateTime
                , toDateTime
                , out count),
               total = count
           }, Formatting.None));
        }

        public async Task<bool> WriteWcfWebServiceMetaDataAsync(JObject data,string outPutName)
        {

            dynamic wcfData = data;
            string url = wcfData.Url;
            string username = wcfData.Username;
            string password = wcfData.Password;
            string domain = wcfData.Domain;
            string proxy = wcfData.Proxy;


            var request = WebRequest.Create(url);

            request.Method = "GET";
            //request.UseDefaultCredentials = false;
            //request.PreAuthenticate = true;
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(domain))
                request.Credentials = new NetworkCredential(username, password, domain);
            else if(!string.IsNullOrEmpty(username))
                request.Credentials = new NetworkCredential(username, password);
            if (!string.IsNullOrEmpty(proxy))
                request.Proxy = new WebProxy(proxy, true);

            var response = (HttpWebResponse)request.GetResponse(); // Raises Unauthorized Exception


            var xml = "";
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var strem = response.GetResponseStream();
                if (strem != null)
                    using (var re = new StreamReader(strem))
                    {
                        xml = re.ReadToEnd();
                    }

                var tempPath = _contentManagementContext.MasterDataKeyValues
                    .FirstOrDefault(md => md.TypeId == (int) EntityIdentity.Path &&
                                          md.Id == (int) Paths.TempPath);
                if (tempPath == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, "Temp"));


                await _fileSystemManager.WriteAsync((_fileSystemManager.CreatDirectoryIfNotExist(tempPath.PathOrUrl) + "/" + outPutName + ".xml").Replace("//", "/")
                    , xml);
                return true;
            }
            throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.NotFound, url));
 
        }

            public string GetWcfWebServiceCode(string wcfLocalUrl,string language)
            {
                return _compiler.GenrateWcfCode(wcfLocalUrl,language);
            }

        public async Task<string> ReadWcfWebServiceMetaDataAsync(string wcfGuid)
        {

            var tempPath = _contentManagementContext.MasterDataKeyValues
                .FirstOrDefault(md => md.TypeId == (int) EntityIdentity.Path &&
                                      md.Id == (int) Paths.TempPath);
            if (tempPath == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, "Temp"));


            var xmlPath = (tempPath.PathOrUrl + "/" + wcfGuid + ".xml").Replace("//", "/");
            var xml = await _fileSystemManager.ReadAsync(xmlPath);
            _fileSystemManager.DeleteFile(xmlPath);
            return xml;

        }

        private async Task<JObject> ConvertToJsonAsync(MasterDataKeyValue code,int newId,string realCode,string codeForShowToUser)
        {
            var lastModifieUser =
              await _securityContext.Users.SingleOrDefaultAsync(us => us.Id == code.CreateUserId);

         
            var dependentDlls = new List<int>();
            var secondCode = code.SecondCode;
            if (code.ForeignKey1 == (int) DotNetCodeType.CompiledDll
                || code.ForeignKey1 == (int) DotNetCodeType.NotCompiledDll
                || code.ForeignKey1 == (int)DotNetCodeType.UnitTestDll
                || code.ForeignKey1 == (int) DotNetCodeType.NugetDll)
            {
                JArray dependentDllArray = JArray.Parse(code.SecondCode);
                dependentDlls = dependentDllArray.ToObject<List<int>>();
                secondCode = "";
            }

            return JObject.FromObject(new
            {
                code.Id,
                FileName=realCode,
                code.Guid,
                NewGuid = SecureGuid.NewGuid().ToString("N"),
                NewId= newId,
                Path = code.PathOrUrl,
                DependentDlls=dependentDlls,
                SecondCode= secondCode,
                code.Key,
                code.Name,
                Code= codeForShowToUser,
                code.Description,
                code.EditMode,
                code.ForeignKey1,
                code.ForeignKey2,
                code.IsLeaf,
                code.ParentId,
                code.Order,
                code.ViewRoleId,
                code.ModifyRoleId,
                code.AccessRoleId,
                code.Version,
                code.ForeignKey3,
                code.Status,
                code.RowVersion,
                code.SlidingExpirationTimeInMinutes,
                code.EnableCache,
                LastModifieUser = lastModifieUser.UserName,
                LastModifieLocalDateTime = code.ModifieLocalDateTime,
                DotNetCode = await GetResorcesAsync(code,realCode)
            });

        }

        private async Task<string> GetResorcesAsync(MasterDataKeyValue code, string realCode)
        {

            //var path = (code.PathOrUrl +"/"+ ((code.ForeignKey1 == (int)DotNetCodeType.Class 
            //    || code.ForeignKey1 == (int)DotNetCodeType.Method 
            //    || code.ForeignKey1 == (int)DotNetCodeType.LinesOfCode) ? realCode : code.Code)  +
            //        GetCodeFileExtention(code.ForeignKey3 ?? (int)SourceType.Csharp))
            //    .Replace("//", "/");


            var path = "";

                //.Replace("//", "/");

            var name = code.ForeignKey1 == (int) DotNetCodeType.Class
                         || code.ForeignKey1 == (int) DotNetCodeType.Method
                         || code.ForeignKey1 == (int) DotNetCodeType.LinesOfCode
                ? realCode
                : code.Code;

           

            var dotNetCode = "";


           

            if (code.ForeignKey1 == (int) DotNetCodeType.Class
                || code.ForeignKey1 == (int) DotNetCodeType.Method
                || code.ForeignKey1 == (int) DotNetCodeType.LinesOfCode)
            {
                var directory = code.PathOrUrl.Substring(code.PathOrUrl.LastIndexOf("/", StringComparison.Ordinal) + 1).Replace("-", ".");
                var classOrMethodOrLineName = name.Substring(name.LastIndexOf(directory, StringComparison.Ordinal) + directory.Length + 1);

                path = (code.PathOrUrl + "/" + classOrMethodOrLineName +
                    GetCodeFileExtention(code.ForeignKey3 ?? (int)SourceType.Csharp))
                .Replace("//", "/");
            }
            else
            {
                path = (code.PathOrUrl + "/" + name +
                    GetCodeFileExtention(code.ForeignKey3 ?? (int)SourceType.Csharp))
                .Replace("//", "/");
            }

            if (await _fileSystemManager.FileExistAsync(path))
                dotNetCode= await _fileSystemManager.ReadAsync(path);
            if(code.ForeignKey3 == (int)SourceType.Csharp)
            {
                dotNetCode=dotNetCode.Replace(CSharpStartComment, " ");
                dotNetCode = dotNetCode.Replace(CSharpEndComment, " ");
            }
            return dotNetCode;
        }

        private string GetCodeFileExtention(int editorId)
        {
            return (editorId == (int)SourceType.Csharp ? ".cs" : ".vb");
        }

        public async Task<JObject> GenerateMigration(JObject migrationInfo)
        {
            dynamic migrationInfoDto = migrationInfo;

            //    JObject.FromObject(new
            //{
            //    LanguageId=197,
            //    ConnectionId= 601,
            //    ConfigurationCodeId= 943,
            //    DllVersion = 6,
            //    RootNamespace = "KS.Dynamic.DataAccess"
            //});
       
     
               string language = migrationInfoDto.Language;


            int connectionId;
            try
            {
                connectionId = migrationInfoDto.ConnectionId;
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "Connection Id"));
          
            }
    var connectionByPermission = await _contentManagementContext.MasterDataKeyValues.AsNoTracking()
                .Where(md => (md.Id == connectionId && md.TypeId == (int)EntityIdentity.SqlServerConnections)
                || (md.ForeignKey3 == connectionId && md.TypeId == (int)EntityIdentity.Permission)).ToListAsync();

            var connection = connectionByPermission.FirstOrDefault(con => con.Id == connectionId);

            if (connection == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.NotFound, " Connection "));
     

            var permission = connectionByPermission.FirstOrDefault(prm => prm.ForeignKey3 == connectionId
            && prm.TypeId == (int)EntityIdentity.Permission);

            if (permission == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.NotFound, " Permission "));
          

            if (!AuthorizeManager.IsAuthorize(permission.ForeignKey2))
                throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));

            var tempPath = _contentManagementContext.MasterDataKeyValues.AsNoTracking()
                      .FirstOrDefault(md => md.TypeId == (int)EntityIdentity.Path &&
                                            md.Id == (int)Paths.TempPath);
            if (tempPath == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, "Temp"));
  



            int id;

            try
            {
                id = migrationInfoDto.ConfigurationCodeId;
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "Configuration Code Id"));
      
            }

            int dllVersion;

            try
            {
                dllVersion = migrationInfoDto.DllVersion;
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "Dll Version"));
  
            }

            var configurationCode =
               await _contentManagementContext.MasterDataKeyValues.AsNoTracking().SingleOrDefaultAsync(cd =>
               cd.Id == id);
          
            if (configurationCode == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeNotFound));

            if(configurationCode.ForeignKey1 != (int)DotNetCodeType.Class)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeMustBeDotNetClassType));

            var dll = await _contentManagementContext.MasterDataKeyValues.AsNoTracking().SingleOrDefaultAsync(cd => cd.Id == configurationCode.ForeignKey2);

            if (dll == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ParentRecordNotFound));

       



            var dotnetCode = await _contentManagementContext.MasterDataKeyValues.AsNoTracking()
       .SingleOrDefaultAsync(cd => cd.TypeId == (int)EntityIdentity.DotNetCode && cd.IsType);

            if (dotnetCode == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, CompileOutPutDirectory));
            



            if (!AuthorizeManager.AuthorizeActionOnEntityId(configurationCode.Id, (int)EntityIdentity.DotNetCode, (int)ActionKey.BuildConfigurationCodeEFMigration))
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidAccessToGenerateMigration, configurationCode.Code));
         
            }
            var dllPath = (dotnetCode.PathOrUrl + CompileOutPutDirectory).Replace("//", "/");
            var dllName = dll.Code.Insert(
                dll.Code.IndexOf(DllExtention,
                    StringComparison.OrdinalIgnoreCase),
                ReleaseMode + dllVersion);

            if (!_fileSystemManager.FileExist(dllPath + dllName))
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.NotFound, dllName));



            return JObject.FromObject(_migration.GenerateMigration(new MigrationInfo()
            {
                Language = language == SourceType.Csharp.ToString() ? SourceType.Csharp : SourceType.VisualBasic,
                TargetName= migrationInfoDto.Name,
                ConfigurationTypeName = configurationCode.Code.Replace(dll.Code+".",""),
                ContextAssemblyName = dllName.Replace(DllExtention,""),
                ContextAssemblyRootNameSpace = migrationInfoDto.RootNamespace,
                Connection = connection.SecondCode,
                NameSpaceQualifiedConnectionType =
                connection.TypeId == (int)EntityIdentity.SqlServerConnections
                ? ConnectionProvider.SqlServer : ConnectionProvider.Oracle,
                WebConfigPath = _fileSystemManager.RelativeToAbsolutePath(dllPath) + WebConfig,
                AppDataPath = _fileSystemManager.RelativeToAbsolutePath(tempPath.PathOrUrl),
                ContextAssemblyPath = _fileSystemManager.RelativeToAbsolutePath(dllPath)
            }));
        }

        public async Task<JArray> GetDbMigrationClasses(int dllVersion, int configurationClassId)
        {
            var configurationCode =
              await _contentManagementContext.MasterDataKeyValues.AsNoTracking().SingleOrDefaultAsync(cd =>
              cd.Id == configurationClassId);

            if (configurationCode == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeNotFound));

            if (configurationCode.ForeignKey1 != (int)DotNetCodeType.Class)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeMustBeDotNetClassType));

            var dll = await _contentManagementContext.MasterDataKeyValues.SingleOrDefaultAsync(cd => cd.Id == configurationCode.ForeignKey2);

            if (dll == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ParentRecordNotFound));

  



            var dotnetCode = await _contentManagementContext.MasterDataKeyValues.AsNoTracking()
       .SingleOrDefaultAsync(cd => cd.TypeId == (int)EntityIdentity.DotNetCode && cd.IsType);

            if (dotnetCode == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, CompileOutPutDirectory));
           



       
            var dllPath = (dotnetCode.PathOrUrl + CompileOutPutDirectory).Replace("//", "/");
            var dllName = dll.Code.Insert(
                dll.Code.IndexOf(DllExtention,
                    StringComparison.OrdinalIgnoreCase),
                ReleaseMode + dllVersion);

            if (!_fileSystemManager.FileExist(dllPath + dllName))
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.NotFound, dllName));


            var configurationCodeFullName = configurationCode.Code.Replace(dll.Code + ".", "");
 

            return JArray.Parse(JsonConvert.SerializeObject
              (_migration.GetDbMigrationClasses(_fileSystemManager.RelativeToAbsolutePath(dllPath) + dllName,
              configurationCodeFullName.Remove(configurationCodeFullName.LastIndexOf(".", StringComparison.Ordinal))
             ), Formatting.None));
        }

        public async Task<bool> RunMigration(JObject migrationInfo)
        {
            dynamic migrationInfoDto = migrationInfo;


            string language = migrationInfoDto.Language;


            int connectionId;
            try
            {
                connectionId = migrationInfoDto.ConnectionId;
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "Connection Id"));

            }
            var connectionByPermission = await _contentManagementContext.MasterDataKeyValues
                        .Where(md => (md.Id == connectionId && md.TypeId == (int)EntityIdentity.SqlServerConnections)
                        || (md.ForeignKey3 == connectionId && md.TypeId == (int)EntityIdentity.Permission)).ToListAsync();

            var connection = connectionByPermission.FirstOrDefault(con => con.Id == connectionId);

            if (connection == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.NotFound, " Connection "));


            var permission = connectionByPermission.FirstOrDefault(prm => prm.ForeignKey3 == connectionId
            && prm.TypeId == (int)EntityIdentity.Permission);

            if (permission == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.NotFound, " Permission "));
     

            if (!AuthorizeManager.IsAuthorize(permission.ForeignKey2))
                throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));

            var tempPath = _contentManagementContext.MasterDataKeyValues
                      .FirstOrDefault(md => md.TypeId == (int)EntityIdentity.Path &&
                                            md.Id == (int)Paths.TempPath);
            if (tempPath == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, "Temp"));




            int id;

            try
            {
                id = migrationInfoDto.ConfigurationCodeId;
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "Configuration Code Id"));

            }

            int dllVersion;

            try
            {
                dllVersion = migrationInfoDto.DllVersion;
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "Dll Version"));

            }

            var configurationCode =
               await _contentManagementContext.MasterDataKeyValues.AsNoTracking().SingleOrDefaultAsync(cd =>
               cd.Id == id);

            if (configurationCode == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeNotFound));

            if (configurationCode.ForeignKey1 != (int)DotNetCodeType.Class)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeMustBeDotNetClassType));

            var dll = await _contentManagementContext.MasterDataKeyValues.SingleOrDefaultAsync(cd => cd.Id == configurationCode.ForeignKey2);

            if (dll == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ParentRecordNotFound));

            if (dll.EnableCache)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidCloseOrOpenChangingCodeGrant, dll.Code));

            }



            var dotnetCode = await _contentManagementContext.MasterDataKeyValues.AsNoTracking()
       .SingleOrDefaultAsync(cd => cd.TypeId == (int)EntityIdentity.DotNetCode && cd.IsType);

            if (dotnetCode == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, CompileOutPutDirectory));
           



            if (!AuthorizeManager.AuthorizeActionOnEntityIdByVersion(configurationCode.Id, (int)EntityIdentity.DotNetCode, configurationCode.Version,
                (int)ActionKey.RubConfigurationCodeEFMigration))
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidAccessToUpgradeMigration, configurationCode.Code));

            }
            var dllPath = (dotnetCode.PathOrUrl + CompileOutPutDirectory).Replace("//", "/");
            var dllName = dll.Code.Insert(
                dll.Code.IndexOf(DllExtention,
                    StringComparison.OrdinalIgnoreCase),
                ReleaseMode + dllVersion);

            if (!_fileSystemManager.FileExist(dllPath + dllName))
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.NotFound, dllName));
         


            _migration.RunMigration(new MigrationInfo()
            {
                Language = language == SourceType.Csharp.ToString() ? SourceType.Csharp : SourceType.VisualBasic,
                TargetName = migrationInfoDto.Name,
                ConfigurationTypeName = configurationCode.Code.Replace(dll.Code + ".", ""),
                ContextAssemblyName = dllName.Replace(DllExtention, ""),
                ContextAssemblyRootNameSpace = migrationInfoDto.RootNamespace,
                Connection = connection.SecondCode,
                NameSpaceQualifiedConnectionType =
                connection.TypeId == (int)EntityIdentity.SqlServerConnections
                ? ConnectionProvider.SqlServer : ConnectionProvider.Oracle,
                WebConfigPath = _fileSystemManager.RelativeToAbsolutePath(dllPath) + WebConfig,
                AppDataPath = _fileSystemManager.RelativeToAbsolutePath(tempPath.PathOrUrl),
                ContextAssemblyPath = _fileSystemManager.RelativeToAbsolutePath(dllPath),
                Force = migrationInfoDto.Force
            });

            return true;
        }

        public async Task<string> GetMigrationScript(JObject migrationInfo)
        {
            dynamic migrationInfoDto = migrationInfo;


            string language = migrationInfoDto.Language;


            int connectionId;
            try
            {
                connectionId = migrationInfoDto.ConnectionId;
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "Connection Id"));

            }
            var connectionByPermission = await _contentManagementContext.MasterDataKeyValues
                        .Where(md => (md.Id == connectionId && md.TypeId == (int)EntityIdentity.SqlServerConnections)
                        || (md.ForeignKey3 == connectionId && md.TypeId == (int)EntityIdentity.Permission)).ToListAsync();

            var connection = connectionByPermission.FirstOrDefault(con => con.Id == connectionId);

            if (connection == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.NotFound, " Connection "));
 

            var permission = connectionByPermission.FirstOrDefault(prm => prm.ForeignKey3 == connectionId
            && prm.TypeId == (int)EntityIdentity.Permission);

            if (permission == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.NotFound, " Permission "));
        

            if (!AuthorizeManager.IsAuthorize(permission.ForeignKey2))
                throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));

            var tempPath = _contentManagementContext.MasterDataKeyValues
                      .FirstOrDefault(md => md.TypeId == (int)EntityIdentity.Path &&
                                            md.Id == (int)Paths.TempPath);
            if (tempPath == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, "Temp"));




            int id;

            try
            {
                id = migrationInfoDto.ConfigurationCodeId;
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "Configuration Code Id"));
 
            }

            int dllVersion;

            try
            {
                dllVersion = migrationInfoDto.DllVersion;
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "Dll Version"));

            }

            var configurationCode =
               await _contentManagementContext.MasterDataKeyValues.AsNoTracking().SingleOrDefaultAsync(cd =>
               cd.Id == id);

            if (configurationCode == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeNotFound));

            if (configurationCode.ForeignKey1 != (int)DotNetCodeType.Class)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeMustBeDotNetClassType));

            var dll = await _contentManagementContext.MasterDataKeyValues.SingleOrDefaultAsync(cd => cd.Id == configurationCode.ForeignKey2);

            if (dll == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ParentRecordNotFound));

      



            var dotnetCode = await _contentManagementContext.MasterDataKeyValues.AsNoTracking()
       .SingleOrDefaultAsync(cd => cd.TypeId == (int)EntityIdentity.DotNetCode && cd.IsType);

            if (dotnetCode == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, CompileOutPutDirectory));
           



            if (!AuthorizeManager.AuthorizeActionOnEntityId(configurationCode.Id, (int)EntityIdentity.DotNetCode, (int)ActionKey.BuildConfigurationCodeEFMigration))
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidAccessToGenerateMigration, configurationCode.Code));
           
            }
            var dllPath = (dotnetCode.PathOrUrl + CompileOutPutDirectory).Replace("//", "/");
            var dllName = dll.Code.Insert(
                dll.Code.IndexOf(DllExtention,
                    StringComparison.OrdinalIgnoreCase),
                ReleaseMode + dllVersion);

            if (!_fileSystemManager.FileExist(dllPath + dllName))
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.NotFound, dllName));



           return _migration.GetMigrationScript(new MigrationInfo()
            {
                Language = language == SourceType.Csharp.ToString() ? SourceType.Csharp : SourceType.VisualBasic,
                TargetName = migrationInfoDto.TargetName,
                SourceName= migrationInfoDto.SourceName,
                ConfigurationTypeName = configurationCode.Code.Replace(dll.Code + ".", ""),
                ContextAssemblyName = dllName.Replace(DllExtention, ""),
                ContextAssemblyRootNameSpace = migrationInfoDto.RootNamespace,
                Connection = connection.SecondCode,
                NameSpaceQualifiedConnectionType =
                connection.TypeId == (int)EntityIdentity.SqlServerConnections
                ? ConnectionProvider.SqlServer : ConnectionProvider.Oracle,
                WebConfigPath = _fileSystemManager.RelativeToAbsolutePath(dllPath) + WebConfig,
                AppDataPath = _fileSystemManager.RelativeToAbsolutePath(tempPath.PathOrUrl),
                ContextAssemblyPath = _fileSystemManager.RelativeToAbsolutePath(dllPath),
                Force = migrationInfoDto.Force
            });
        }

        public async Task<bool> RunUnitTestMethod(JObject unitTestInfo)
        {
            dynamic unitTestInfoDto = unitTestInfo;

            int codeId;

            try
            {
                codeId = unitTestInfoDto.CodeId;
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "code Id"));
       
            }

            int dllVersion;

            try
            {
                dllVersion = unitTestInfoDto.DllVersion;
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "Dll Version"));

            }

            var code =
               await _contentManagementContext.MasterDataKeyValues.Include(cd => cd.Parent).AsNoTracking().SingleOrDefaultAsync(cd =>
                 cd.Id == codeId);

            if (code == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeNotFound));

            if (code.ForeignKey1 == (int)DotNetCodeType.LinesOfCode)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeCanNotBeDotNetLineOfCodeType, code.Name));
            


            var dll = await _contentManagementContext.MasterDataKeyValues.SingleOrDefaultAsync(cd => cd.Id == code.ForeignKey2);

            if (dll == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ParentRecordNotFound));





            var dotnetCode = await _contentManagementContext.MasterDataKeyValues.AsNoTracking()
       .SingleOrDefaultAsync(cd => cd.TypeId == (int)EntityIdentity.DotNetCode && cd.IsType);

            if (dotnetCode == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, CompileOutPutDirectory));
           


            


            if (!AuthorizeManager.AuthorizeActionOnEntityIdByVersion(dll.Id, (int)EntityIdentity.DotNetCode, dllVersion,
                (int)ActionKey.RunUnitTest))
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidAccessToRunUnitTest, dll.Name));
  
            }

            //if (!AuthorizeManager.AuthorizeActionOnEntityIdByVersion(code.Id, (int)EntityIdentity.DotNetCode, code.Version,
            //   (int)ActionKey.RunUnitTest))
            //{
            //    throw new KhodkarInvalidException((
            //           LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidAccessToRunUnitTest), code.Name));
            //}

            var dllPath = (dotnetCode.PathOrUrl + CompileOutPutDirectory).Replace("//", "/");
            var dllName = dll.Code.Insert(
                dll.Code.IndexOf(DllExtention,
                    StringComparison.OrdinalIgnoreCase),
                ReleaseMode + dllVersion);

            if (!_fileSystemManager.FileExist(dllPath + dllName))
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.NotFound, dllName));
    


            string methodeName = unitTestInfoDto.MethodeName;
            string className = unitTestInfoDto.ClassName;
            string parameter = unitTestInfoDto.Parameter;
            var nameSpace = "";

            if (code.ForeignKey1 == (int)DotNetCodeType.NamaeSpace)
            {
                nameSpace = code.Code.Replace(code.Parent.Code + ".", "");
            }
            else if (code.ForeignKey1 == (int)DotNetCodeType.Class)
            {
                if (className == "")
                    className = code.Code.Replace(code.Parent.Code + ".", "");
                nameSpace = code.Code.Remove(code.Code.LastIndexOf(".", StringComparison.Ordinal)).Replace(dll.Code + ".", "");
            }
            else if (code.ForeignKey1 == (int)DotNetCodeType.Method)
            {
               
                var namseSpaceAndClass = code.Code.Remove(code.Code.LastIndexOf(".", StringComparison.Ordinal))
                    .Replace(dll.Code + ".", "");

                nameSpace = namseSpaceAndClass.Remove(namseSpaceAndClass.LastIndexOf(".", StringComparison.Ordinal));
                if (className == "")
                    className = namseSpaceAndClass.Replace(nameSpace + ".", "");
            }


            return _unitTester.RunUnitTestMethod(_fileSystemManager.RelativeToAbsolutePath(dllPath) + dllName,
              nameSpace, className.Trim(), methodeName.Trim(), parameter);
        }

        public async Task<JArray> GetUnitTestMethods(int dllId, int dllVersion, int codeId)
        {
            var code =
              await _contentManagementContext.MasterDataKeyValues.Include(cd=>cd.Parent).AsNoTracking().SingleOrDefaultAsync(cd =>
              cd.Id == codeId);

            if (code == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeNotFound));

            if (code.ForeignKey1 == (int)DotNetCodeType.LinesOfCode)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeCanNotBeDotNetLineOfCodeType, code.Name));
         
    

            var dll = await _contentManagementContext.MasterDataKeyValues.SingleOrDefaultAsync(cd => cd.Id == code.ForeignKey2);

            if (dll == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ParentRecordNotFound));



            var dotnetCode = await _contentManagementContext.MasterDataKeyValues.AsNoTracking()
       .SingleOrDefaultAsync(cd => cd.TypeId == (int)EntityIdentity.DotNetCode && cd.IsType);

            if (dotnetCode == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, CompileOutPutDirectory));
           




            var dllPath = (dotnetCode.PathOrUrl + CompileOutPutDirectory).Replace("//", "/");
            var dllName = dll.Code.Insert(
                dll.Code.IndexOf(DllExtention,
                    StringComparison.OrdinalIgnoreCase),
                ReleaseMode + dllVersion);

            if (!_fileSystemManager.FileExist(dllPath + dllName))
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.NotFound, dllName));
     

            var methodeName = "";
            var className = "";
            var nameSpace = "";

            if (code.ForeignKey1 == (int) DotNetCodeType.NamaeSpace)
            {
                nameSpace = code.Code.Replace(code.Parent.Code + ".", "");
            }
            else if (code.ForeignKey1 == (int)DotNetCodeType.Class)
            {
                className = code.Code.Replace(code.Parent.Code + ".", "");
                nameSpace = code.Code.Remove(code.Code.LastIndexOf(".", StringComparison.Ordinal)).Replace(dll.Code + ".", "");
            }
            else if (code.ForeignKey1 == (int)DotNetCodeType.Method)
            {
                methodeName = code.Code.Replace(code.Parent.Code + ".", "");
                var namseSpaceAndClass = code.Code.Remove(code.Code.LastIndexOf(".", StringComparison.Ordinal))
                    .Replace(dll.Code + ".", "");

                nameSpace = namseSpaceAndClass.Remove(namseSpaceAndClass.LastIndexOf(".", StringComparison.Ordinal));
                className = namseSpaceAndClass.Replace(nameSpace + ".", "");
            }


            return JArray.Parse(JsonConvert.SerializeObject
              (_unitTester.GetUnitTestMethods(_fileSystemManager.RelativeToAbsolutePath(dllPath) + dllName,
              nameSpace,className,methodeName
             ), Formatting.None));
        }

        class KeyValue
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

    }
}


