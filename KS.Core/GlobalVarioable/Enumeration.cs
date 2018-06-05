namespace KS.Core.GlobalVarioable
{

    public enum JavaScriptType
    {
        String,
        Array,
        Object,
        Number,
        Bool,
        None
    }
    public enum CacheType
    {
        Memory,
        None
    }

    public enum Paths
    {
        TempPath = 878
    }
    public enum Groups
    {
        Developers = 3
    }

    public enum Roles
    {
        Admin = 5,
        Public = 6,
        Debug = 15
    }

    public enum SourceType
    {
        JavaScript = 1,
        Style = 2,
        Html = 3,
        Json = 4,
        Csharp = 197,
        VisualBasic = 198
    }

    public enum WebPageType
    {
        Modal = 20,
        Form = 16,
        Template = 15,
        FrameWork = 14
    }

    public enum WebPageJsonType
    {
        //View = 1,
        Debug = 1,
        //Build = 3,
        Reload = 2,
        ChangeTemplate = 3,
        //Edit =6,
        Source = 4
    }
    public enum DllStorePlace
    {
        Global = 811,
        Output = 812,
        Bin = 813
    }
    public enum DotNetCodeType
    {
        NamaeSpace = 752,
        Class = 753,
        Method = 754,
        LinesOfCode = 755,
        CompiledDll = 756,
        NotCompiledDll = 757,
        NugetDll = 758,
        UnitTestDll = 979
    }

    public enum ConnectionKey
    {
        DefaultsSqlServerConnection,
        DefaultsOracleConnection
    }
    public static class ConnectionProvider
    {
        public static string SqlServer => "System.Data.SqlClient";
        public static string Oracle => "System.Data.SqlClient";
    }

    public enum TextKey
    {
        LastCompiledVersionText
    }

    public enum ExceptionKey
    {
        InvalidDebugGrant,
        InvalidGrant,
        DisabledUser,
        PropertyTooShort,
        PasswordRequireDigit,
        PasswordRequireNonLetterOrDigit,
        PasswordRequireLower,
        PasswordRequireUpper,
        DataConcurrencyException,
        DefaultsLanguageFoundException,
        InvalidAccessTreeDirectoryRoleException,
        InvalidLogin,
        FrameWorkNotFound,
        PageNotFound,
        MasterDataTypeIsLeaf,
        MasterDataTypeId,
        PageResourcesNotFoundException,
        PageTemporaryInaccessible,
        CheckOutRecord,
        CodeTemplateException,
        InvalidUserName,
        UserNotFound,
        UserProfileNotFound,
        DuplicateName,
        InvalidEmail,
        DuplicateEmail,
        FieldMustBeNumeric,
        InvalidAccessToPath,
        InvalidAccessToService,
        PathNotFound,
        RepeatedValue,
        RepeatedPath,
        NotFound,
        TemplateNotFound,
        ChangeNotFound,
        ChangeHasNoCode,
        CodeNotFound,
        CodeMustBeDotNetClassType,
        CodeCanNotBeDotNetLineOfCodeType,
        LinkNotFound,
        RoleNotFound,
        GroupNotFound,
        BundleNotFound,
        BundleHasNoSource,
        SettingNotFound,
        MasterDataKeyValuesNotFound,
        FileNotFound,
        LanguageAndCultureNotFound,
        ServiceNotFound,
        InUseItem,
        CodeHasBundle,
        SourceOfOneByOneBundleNotValid,
        Required,
        PropertyValueRequired,
        TranslateNotFound,
        ParentRecordNotFound,
        InvalidAccessForAddingChildToParenRecord,
        DllParentMustBeFolder,
        NameSpaceParentMustBeDll,
        ClassParentMustBeNameSpace,
        MethodParentMustBeClass,
        LineOfCodeParentMustBeMethodOrClass,
        DllReferencingAccessDeny,
        InvalidAccessToCompileDll,
        InvalidAccessToDellDllOutput,
        InvalidAccessToAddDllOutput,
        InvalidAccessToPublishDll,
        CodeIsntNotCompiledDll,
        CheckOutCode,
        PlaceHolderSignNotExistInParentCode,
        DllAlreadyCompiled,
        InvalidCloseOrOpenChangingCodeGrant,
        InvalidAccessToViewBuildLogOfDllOutput,
        WebServiceCodeGenratorError,
        InvalidAccessToUpdateCodeMadeDebugInfo,
        InvalidAccessToReadDebugInfo,
        InvalidAccessToDeleteDebugInfo,
        InvalidAccessToDowngradeMigration,
        InvalidAccessToUpgradeMigration,
        InvalidAccessToGenerateMigration,
        CodeTypeNoNeedToCompile,
        DllNotFound,
        InvalidAccessToRunUnitTest,
        ClassNotFound,
        MethodNotFound,
        InvalidAccessToDownloadFromUrl,
        UrlNotFound,
        RoleIsNull,
        FileAlreadyExist,
        UploadException,
        InvalidAccessToEditGroup,
        DevelopServerException,
        UnhandledException,
        None
    }

    public enum CacheKey
    {
        //Modal,
        //Form,
        //Template,
        //FrameWork,
        TemplatePatternUrls,
        Aspect,
        Group,
        DebugUser,
        BrowsersCodeInfo
    }

    public enum ConfigKey
    {
        LanguageAndCultures
    }

    public enum Protocol
    {
        LocalUrlPorotocol = 1000,
        Http = 1001,
        Https = 1002,
        Ftp = 1003,
        PathProtocol = 1004,
        NetworkPathProtocol = 1005
    }

    public enum ActionKey
    {
        ReadFromDisk = 215,
        WriteToDisk = 216,
        DeleteFromDisk = 217,
        RequestService = 269,
        Add,
        Delete,
        ViewSourceCode = 687,
        ConnectToConnection = 616,
        ViewServiceLog = 693,
        ReferenceDll = 804,
        CompileDll = 815,
        PublishDll = 835,
        DellDllOutput = 838,
        AddDllOutput = 844,
        CloseOrOpenChangingCode = 858,
        ViewBuildLogOfDllOutput = 860,
        ReadDebugInfo = 924,
        WriteDebugInfo = 925,
        DeleteDebugInfo = 926,
        BuildConfigurationCodeEFMigration = 933,
        RubConfigurationCodeEFMigration = 934,
        //DowngradeConfigurationCodeEFMigration=935,
        ViewDebugSourceCode = 947,
        RunUnitTest = 982,
        DownloadFromAddress = 1007,
        EditGroup = 1075
    }

    public enum EntityIdentity
    {
        Link = 101,
        Service = 1001,

        Tag = 1002,

        WebPageType = 1003,

        WebPageTemplate = 1004,

        WebPageFrameWork = 1005,

        WebPageFile = 1006,
        DynamiEntityFile = 1007,

        KhodkarException = 1008,

        FileType = 1009,
        WebPageFilePath = 1010,
        DynamicEntityFilePath = 1011,
        MasterDataKeyValueType = 1012,

        LinkFile = 1013,

        LinkFilePath = 1014,
        Group = 1015,
        MasterDataKeyValueGroup = 1016,
        LinkGroup = 1017,
        DynamicEntityGroup = 1018,

        CommentGroup = 1019,

        FileTag = 1020,

        DynamicEntityTag = 1021,
        LinkTag = 1022,
        FilePathTag = 1023,
        LinkType = 1024,
        Path = 1025,
        Code = 1026,
        OsCode = 1027,
        BrowserCode = 1028,
        DatabaseCode = 1029,
        Editor = 1030,
        Opreation = 1031,
        Permission = 1032,
        Bundle = 1033,
        BundleSource = 1034,
        Script = 1035,
        Style = 1036,
        Connections = 1037,
        SqlServerConnections = 1038,
        ODataContextsMetaData = 1039,
        SqlServerCode = 1040,
        DotNetCode = 1041,
        CodeTypes = 1042,
        DotNetCodeTypes = 1043,
        DllStorePlace = 1044,
        Texts = 1045,
        Protocoles = 1046,
        Urls = 1047
    }

}