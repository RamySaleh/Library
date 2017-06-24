using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library.Infrastructure.DependencyInjection;
using Library.Infrastructure.ExceptionHandling;

namespace Library.DependencyInjection
{
    public class IocContainer
    {      
        static IDependencyResolver dependencyResolver;

        public static void RegisterDependencies()
        {
            if (dependencyResolver == null)
            {
                dependencyResolver = new AutofacDependencyResolver();
            }
            dependencyResolver.RegisterDependency<IExceptionHandler>(typeof(LogFileExceptionHandler));
        }

        public static T Resolve<T>()
        {
            return dependencyResolver.ResolveDependency<T>();
        }
    }
}