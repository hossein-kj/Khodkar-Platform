using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using KS.Core.Model.Develop;

namespace KS.Core.EntityFramework.Base
{
    public abstract class BaseMigration
    {
        protected const string VisualBasic = "VisualBasic";
        protected const string CSharp = "CSharp";
        public virtual MigrationCode GenerateMigration(MigrationInfo migrationInfo)
        {




            //var assembly = Assembly.LoadFrom((migrationInfo.ContextAssemblyPath
            //    + "\\KS.Dynamic.Migration_r3.dll").Replace(@"\\", @"\"));
            //var type = assembly.GetType("KS.Dynamic.Migration.EntityFramework.Migration");

            //// Get the constructor and create an instance of MagicClass

            ////Type magicType = Type.GetType("MagicClass");
            //ConstructorInfo magicConstructor = type.GetConstructor(Type.EmptyTypes);
            //object magicClassObject = magicConstructor.Invoke(new object[] { });

            //// Get the ItsMagic method and invoke with a parameter value of 100

            //MethodInfo magicMethod = type.GetMethod("GenerateMigration");
            //object magicValue = magicMethod.Invoke(magicClassObject, new object[] { migrationInfo });


            //return (MigrationCode) magicValue;

            var infos = new StringBuilder();
            var warnings = new StringBuilder();
            var verbose = new StringBuilder();

            //connection:connectionString, NameSpaceQualifiedConnectionType:for example :"System.Data.SqlClient"
            var connectionStringInfo = new DbConnectionInfo(migrationInfo.Connection,
                migrationInfo.NameSpaceQualifiedConnectionType);

            var toolingFacade = new ToolingFacade(
                migrationInfo.ContextAssemblyName, //"MyDll",   // MigrationAssemblyName. In this case dll should be located in "C:\\Temp\\MigrationTest" dir
                migrationInfo.ContextAssemblyName, //"MyDll",  // ContextAssemblyName. Same as above
                migrationInfo.ConfigurationTypeName, //KS.ObjectiveDataAccess.ContentManagementDbContextMigrations.Configuration
                migrationInfo.ContextAssemblyPath,  //"C:\\Temp\\MigrationTest",   // Where the dlls are located
                migrationInfo.WebConfigPath, //"C:\\Temp\\MigrationTest\\App.config", // Insert the right directory and change with Web.config if required
                migrationInfo.AppDataPath, //"C:\\Temp\\App_Data",
                connectionStringInfo)
            {
                LogInfoDelegate = s => { infos.AppendLine($"Infos : {s} . <br>"); },
                LogWarningDelegate = s => { warnings.AppendLine($"Warning : {s} . <br>"); },
                LogVerboseDelegate = s => { verbose.AppendLine($"Verbose : {s} . <br>"); }
            };



            ScaffoldedMigration scaffoldedMigration = toolingFacade
                .Scaffold(migrationInfo.TargetName,
                 migrationInfo.Language == GlobalVarioable.SourceType.Csharp ?
                CSharp : VisualBasic, migrationInfo.ContextAssemblyRootNameSpace, false);


            var designCode = scaffoldedMigration.DesignerCode.Insert(
                scaffoldedMigration.DesignerCode.IndexOf("private readonly ResourceManager Resources", StringComparison.Ordinal), "//");

            return new MigrationCode()
            {
                DesignerCode = designCode.Replace("Resources.GetString(\"Target\")", "\"" + scaffoldedMigration.Resources["Target"] + "\""),
                UserCode = scaffoldedMigration.UserCode,
                Resources = scaffoldedMigration.Resources,
                MigrationId = scaffoldedMigration.MigrationId,
                Infos = infos.ToString(),
                Warnings = warnings.ToString(),
                Verbose = verbose.ToString()
            };

            //using (var db = new KS.DataAccess.Contexts.SecurityContext())
            //{
            //    var services = ((IInfrastructure<IServiceProvider>)db).Instance;
            //    var codeHelper = new CSharpHelper();
            //    var scaffolder = ActivatorUtilities.CreateInstance<MigrationsScaffolder>(
            //        services,
            //        new CSharpMigrationsGenerator(
            //            codeHelper,
            //            new CSharpMigrationOperationGenerator(codeHelper),
            //            new CSharpSnapshotGenerator(codeHelper)));

            //    var migration = scaffolder.ScaffoldMigration(
            //        "MyMigration",
            //        "MyApp.Data");

            //    File.WriteAllText(
            //        migration.MigrationId + migration.FileExtension,
            //        migration.MigrationCode);
            //    File.WriteAllText(
            //        migration.MigrationId + ".Designer" + migration.FileExtension,
            //        migration.MetadataCode);
            //    File.WriteAllText(
            //        migration.SnapshotName + migration.FileExtension,
            //        migration.SnapshotCode);
            //}
        }

        public virtual List<string> GetDbMigrationClasses(string dllFullPath, string configurationTypeNameSpace)
        {

            //var n = from t in Assembly.LoadFrom(dllFullPath).GetTypes()
            //        where
            //        t.IsClass && t.Namespace == configurationTypeNameSpace &&
            //        t.IsSubclassOf(typeof(System.Data.Entity.Migrations.DbMigration))
            //        select t;

            return (from t in Assembly.LoadFrom(dllFullPath).GetTypes()
                    where t.IsClass && t.Namespace == configurationTypeNameSpace &&
                    t.BaseType == typeof(System.Data.Entity.Migrations.DbMigration)
                    select t).Select(type => type.Name).ToList();
        }

        public virtual void RunMigration(MigrationInfo migrationInfo)
        {

            var infos = new StringBuilder();
            var warnings = new StringBuilder();
            var verbose = new StringBuilder();

            //connection:connectionString, NameSpaceQualifiedConnectionType:for example :"System.Data.SqlClient"
            var connectionStringInfo = new DbConnectionInfo(migrationInfo.Connection,
                migrationInfo.NameSpaceQualifiedConnectionType);

            var toolingFacade = new ToolingFacade(
                migrationInfo.ContextAssemblyName, //"MyDll",   // MigrationAssemblyName. In this case dll should be located in "C:\\Temp\\MigrationTest" dir
                migrationInfo.ContextAssemblyName, //"MyDll",  // ContextAssemblyName. Same as above
                migrationInfo.ConfigurationTypeName, //KS.ObjectiveDataAccess.ContentManagementDbContextMigrations.Configuration
                migrationInfo.ContextAssemblyPath,  //"C:\\Temp\\MigrationTest",   // Where the dlls are located
                migrationInfo.WebConfigPath, //"C:\\Temp\\MigrationTest\\App.config", // Insert the right directory and change with Web.config if required
                migrationInfo.AppDataPath, //"C:\\Temp\\App_Data",
                connectionStringInfo)
            {
                LogInfoDelegate = s => { infos.AppendLine($"Infos : {s} . <br>"); },
                LogWarningDelegate = s => { warnings.AppendLine($"Warning : {s} . <br>"); },
                LogVerboseDelegate = s => { verbose.AppendLine($"Verbose : {s} . <br>"); }
            };


            toolingFacade.Update(migrationInfo.TargetName, migrationInfo.Force);

        }

        public virtual string GetMigrationScript(MigrationInfo migrationInfo)
        {

            var infos = new StringBuilder();
            var warnings = new StringBuilder();
            var verbose = new StringBuilder();

            //connection:connectionString, NameSpaceQualifiedConnectionType:for example :"System.Data.SqlClient"
            var connectionStringInfo = new DbConnectionInfo(migrationInfo.Connection,
                migrationInfo.NameSpaceQualifiedConnectionType);

            var toolingFacade = new ToolingFacade(
                migrationInfo.ContextAssemblyName, //"MyDll",   // MigrationAssemblyName. In this case dll should be located in "C:\\Temp\\MigrationTest" dir
                migrationInfo.ContextAssemblyName, //"MyDll",  // ContextAssemblyName. Same as above
                migrationInfo.ConfigurationTypeName, //KS.ObjectiveDataAccess.ContentManagementDbContextMigrations.Configuration
                migrationInfo.ContextAssemblyPath,  //"C:\\Temp\\MigrationTest",   // Where the dlls are located
                migrationInfo.WebConfigPath, //"C:\\Temp\\MigrationTest\\App.config", // Insert the right directory and change with Web.config if required
                migrationInfo.AppDataPath, //"C:\\Temp\\App_Data",
                connectionStringInfo)
            {
                LogInfoDelegate = s => { infos.AppendLine($"Infos : {s} . <br>"); },
                LogWarningDelegate = s => { warnings.AppendLine($"Warning : {s} . <br>"); },
                LogVerboseDelegate = s => { verbose.AppendLine($"Verbose : {s} . <br>"); }
            };

            if (migrationInfo.SourceName == "")
                return toolingFacade.ScriptUpdate(null, migrationInfo.TargetName,
                    migrationInfo.Force);
            else
                return toolingFacade.ScriptUpdate(migrationInfo.SourceName,
                    migrationInfo.TargetName, migrationInfo.Force);

        }

        protected virtual T CreateInstance<T>(string dllFullPath) where T : class
        {
            var assembly = Assembly.LoadFrom(dllFullPath);
            var type = assembly.GetType("KS.Dynamic.Migration.EntityFramework.Migration");





            return Activator.CreateInstance(type) as T;
        }
    }
}
