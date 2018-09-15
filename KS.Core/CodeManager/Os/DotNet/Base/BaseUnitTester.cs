using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KS.Core.Exceptions;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;


namespace KS.Core.CodeManager.Os.DotNet.Base
{
    public abstract class BaseUnitTester
    {
        public virtual List<string> GetUnitTestMethods(string dllFullPath, string nameSpace, string className, string methodeName)
        {
            //var n = from t in Assembly.LoadFrom(dllFullPath).GetTypes()
            //        where
            //        t.IsClass && t.Namespace == configurationTypeNameSpace &&
            //        t.IsSubclassOf(typeof(System.Data.Entity.Migrations.DbMigration))
            //        select t;

            var methods = new List<string>();
            var corMethods = new List<string>()
            {
                "Equals"
                ,"GetHashCode"
                ,"GetType"
                ,"ToString"

            };
            if (methodeName != "")
            {
                var classe = (from t in Assembly.LoadFrom(dllFullPath).GetTypes()
                        where t.IsClass && t.Namespace == nameSpace 
                        && t.Name == className
                        select t).FirstOrDefault();
                if (classe != null)
                {
                    return classe.GetMethods().Where(mt=>mt.Name == methodeName).Select(mt=>mt.Name)
                        .Where(mt => corMethods.All(cm => cm != mt)).ToList();
                }
            }
            else if (className != "")
            {
                var classe = (from t in Assembly.LoadFrom(dllFullPath).GetTypes()
                               where t.IsClass && t.Namespace == nameSpace
                               && t.Name == className
                               select t).FirstOrDefault();
                if (classe != null)
                {
                    return classe.GetMethods().Select(mt => mt.Name).Where(mt=> corMethods.All(cm => cm != mt)).ToList();
                }
            }
            else if (nameSpace != "")
            {
                
                var classes = (from t in Assembly.LoadFrom(dllFullPath).GetTypes()
                               where t.IsClass && t.Namespace == nameSpace
                              
                               select t).ToList();

                foreach (var clas in classes)
                {
                    methods.AddRange(clas.GetMethods()
                         .Where(mt => corMethods.All(cm => cm != mt.Name))
                        .Select(method => clas.Name + "=>" + method.Name)
                       );
                }
            }
            return methods;
        }

        public virtual bool RunUnitTestMethod(string dllFullPath, string nameSpace, string className, string methodeName,string parameter)
        {
            var corMethods = new List<string>()
            {
                "Equals"
                ,"GetHashCode"
                ,"GetType"
                ,"ToString"

            };

                var classe = (from t in Assembly.LoadFrom(dllFullPath).GetTypes()
                              where t.IsClass && t.Namespace == nameSpace
                              && t.Name == className
                              select t).FirstOrDefault();
                if (classe != null)
                {
                    var method = classe.GetMethods().Where(mt => mt.Name == methodeName)
                        .FirstOrDefault(mt => corMethods.All(cm => cm != mt.Name));

                    var testInstance = Activator.CreateInstance(classe);

                    if (method != null)
                    {
                        if (parameter != "")
                            return (bool) method.Invoke(testInstance, new object[] {parameter});
                        else
                        {
                            return (bool) method.Invoke(testInstance, null);
                        }
                    }
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.MethodNotFound, methodeName));
               
                }
            throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ClassNotFound, className));

        }
    }
}
