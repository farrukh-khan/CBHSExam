using AutoMapper;
using DataAccess.BLL;
using Mvc.ViewModel;

namespace Mvc.Mappings
{
    public class ModelMapping : IAutoMap
    {
        public void Initialize()
        {

            

            //Mapper.CreateMap<SupplierContactPerson, SupplierContactPersonModel>();

            //CreateMap<Member, MemberModel>();
            //CreateMap<Notification, NotificationDTO>();


            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<Member, MemberModel>();
            //    cfg.CreateMap<MemberModel, Member>();
            //});

            //IMapper mapper = config.CreateMapper();
            //var source = new Source();
            //var dest = mapper.Map<Source, Dest>(source);

        }
    }
}