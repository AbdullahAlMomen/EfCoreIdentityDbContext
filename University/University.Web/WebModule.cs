using Autofac;
using University.Web.Areas.Admin.Models;
using University.Web.Models;

namespace University.Web
{
    public class WebModule:Module
    {
        public WebModule()
        {

        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StudentListModel>().AsSelf()
            .InstancePerLifetimeScope();
            builder.RegisterType<StudentCreateModel>().AsSelf()
            .InstancePerLifetimeScope();
            builder.RegisterType<StudentUpdateModel>().AsSelf()
          .InstancePerLifetimeScope();

            builder.RegisterType<RegisterModel>().AsSelf()
           .InstancePerLifetimeScope();

            builder.RegisterType<LoginModel>().AsSelf()
            .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
