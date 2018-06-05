using System;
using System.Collections.Generic;


namespace KS.Core.CodeManager.Os.DotNet.Base
{
    public interface IUnitTester
    {
        List<string> GetUnitTestMethods(string dllFullPath, string nameSpace, string className, string methodeName);

        bool RunUnitTestMethod(string dllFullPath, string nameSpace, string className, string methodeName,
            string parameter);
    }
}
