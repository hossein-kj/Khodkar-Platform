using System;

namespace KS.Core.Model.Log
{
    public interface ILogEntity
    {
        int? CreateUserId { get; set; }
        int? ModifieUserId { get; set; }
        //int ViewCount { get; set; }
        string CreateLocalDateTime { get; set; }
        string ModifieLocalDateTime { get; set; }
        string AccessLocalDateTime { get; set; }
        DateTime CreateDateTime { get; set; }
        DateTime ModifieDateTime { get; set; }
        DateTime AccessDateTime { get; set; }
        int Status { get; set; }
    }
}
