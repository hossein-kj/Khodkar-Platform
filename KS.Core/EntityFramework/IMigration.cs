using System.Collections.Generic;
using KS.Core.Model.Develop;

namespace KS.Core.EntityFramework
{
    public interface IMigration
    {
        MigrationCode GenerateMigration(MigrationInfo migrationInfo);
        void RunMigration(MigrationInfo migrationInfo);
        List<string> GetDbMigrationClasses(string dllFullPath, string configurationTypeNameSpace);
        string GetMigrationScript(MigrationInfo migrationInfo);
    }
}