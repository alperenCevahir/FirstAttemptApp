using Autofac;
using FirstAttempt.Core.Repositories;
using FirstAttempt.Core.Services;
using FirstAttempt.Core.UnitOfWorks;
using FirstAttempt.Repository;
using FirstAttempt.Repository.Repositories;
using FirstAttempt.Repository.UnitOfWork;
using FirstAttempt.Service.Mapping;
using FirstAttempt.Service.Services;
using System.Reflection;
//ŞU KISIM BENCE ÖNEMLİ
using Module = Autofac.Module;


namespace FirstAttemtp.API.Modules
{
    public class RepoServiceModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            



            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x =>
            x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x =>
            x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();

            //InstancePerLifetimeScope => Scope
            //InstancePerDependency => transient

        }
    }
}
