using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StructureMap;

namespace Mvc.DI
{
    public class SmDependencyResolver : IDependencyResolver
    {
        private readonly StructureMap.IContainer _container;

        public SmDependencyResolver(StructureMap.IContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null) return null;
            try
            {
                return serviceType.IsAbstract || serviceType.IsInterface
                         ? _container.TryGetInstance(serviceType)
                         : _container.GetInstance(serviceType);
            }
            catch (Exception ex)
            {
                //System.Diagnostics.EventLog.WriteEntry("Web Distribution System", ex.ToString());
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }
    }
}
