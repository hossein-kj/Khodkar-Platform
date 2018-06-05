using Autofac.Integration.Mvc;
using KS.Core.DependencyProvider.Adapters;

namespace KS.Core.DependencyProvider
{
    public static class DependencyManager
    {
        private static IDependencyAdapter Adapter
        {
            get
            {
                return (IDependencyAdapter)(new Autofac.Extras.CommonServiceLocator.AutofacServiceLocator(AutofacDependencyResolver.Current.ApplicationContainer))
                    .GetService(typeof(IDependencyAdapter));
              
            }
        }
          
        public static T Get<T>()
        {
            return Adapter.Get<T>();
        }
    }
}
