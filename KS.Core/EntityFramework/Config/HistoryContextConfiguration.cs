using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;

namespace KS.Core.EntityFramework.Config
{
    public sealed class HistoryContextConfiguration: HistoryContext
    {
        private readonly string _defaultSchema;
        private readonly string _tableName;
        public HistoryContextConfiguration(DbConnection dbConnection, string defaultSchema, string tableName)
            : base(dbConnection, defaultSchema)
        {
            _defaultSchema = defaultSchema;
            _tableName = tableName;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //In Oracle "SECURITY" Is Name Of User In ConnectionString
            modelBuilder.Entity<HistoryRow>().ToTable(tableName: _tableName, schemaName: _defaultSchema);
        } 
    }
}
