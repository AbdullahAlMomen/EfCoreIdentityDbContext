using Autofac;
using University.Application.Services;
using University.Infrastructure.Services;

namespace University.Infrastructure
{
    public class InfrastructureModule:Module
    {
        public InfrastructureModule()
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StudentService>().As<IStudentService>().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}