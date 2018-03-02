using DataAccess.BLL;
using Service.Contracts;
using Service.Services;
using StructureMap;
using StructureMap.Graph;

namespace Mvc.DI
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x =>
            {
                x.Scan(scan =>
                {
                    scan.WithDefaultConventions();

                    scan.TheCallingAssembly();
                    scan.Assembly("DataAccess");
                    scan.Assembly("Service");

                });

                x.For<IMemberService>().Use<MemberService>();

            });

            return ObjectFactory.Container;
        }
    }
}




