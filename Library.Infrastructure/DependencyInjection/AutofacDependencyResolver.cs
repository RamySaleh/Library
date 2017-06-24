using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.DependencyInjection
{
    public class AutofacDependencyResolver : IDependencyResolver
    {
        private IContainer iocContainer;
        private ContainerBuilder containerBuilder;

        public AutofacDependencyResolver()
        {
            containerBuilder = new ContainerBuilder();
        }

        public void RegisterDependency<T>(Type dependency)
        {
            containerBuilder.Register(c => Activator.CreateInstance(dependency)).As<T>().SingleInstance();
        }

        public T ResolveDependency<T>()
        {
            if (iocContainer == null)
            {
                iocContainer = containerBuilder.Build();
            }
            return iocContainer.Resolve<T>();
        }
    }
}
