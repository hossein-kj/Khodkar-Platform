using Autofac;
using Autofac.Integration.Mvc;


namespace KS.Core.DependencyProvider.Adapters
{
    public abstract class BaseDependencyAdapter
    {
        public virtual T Get<T>()
        {
            
            return (T)(new Autofac.Extras.CommonServiceLocator.AutofacServiceLocator(AutofacDependencyResolver.Current.ApplicationContainer)).GetService(typeof(T));
            //using (var scope = AutofacDependencyResolver.Current.RequestLifetimeScope.BeginLifetimeScope())
            //{
            //    return scope.Resolve<T>();
            //}
        }
    }
}
