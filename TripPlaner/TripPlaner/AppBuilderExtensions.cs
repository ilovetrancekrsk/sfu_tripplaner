﻿using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using TripPlaner.DAL;
using TripPlaner.DAL.Entities;
using TripPlaner.DAL.Repositories;
using TripPlaner.Infrastructure.Identity;
using TripPlaner.Services.Services;

namespace TripPlaner
{
    public static class AppBuilderExtensions
    {
        private static IContainer _container;
        private static AutofacDependencyResolver _autofacDependencyResolver;
        private static AutofacWebApiDependencyResolver _autofacWebApiDependencyResolver;

        public static IAppBuilder UseAutofacContainer(this IAppBuilder app)
        {
            var container = CreateContainer(app);
            DependencyResolver.SetResolver(CreateAutofacDependencyResolver(app));

            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
            app.UseAutofacWebApi(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configuration.DependencyResolver = CreateAutofacWebApiDependencyResolver(app);

            return app;
        }

        public static AutofacDependencyResolver CreateAutofacDependencyResolver(IAppBuilder app)
        {
            if (_autofacDependencyResolver != null)
                return _autofacDependencyResolver;

            _autofacDependencyResolver = 
                new AutofacDependencyResolver(CreateContainer(app));

            return _autofacDependencyResolver;
        }

        public static AutofacWebApiDependencyResolver CreateAutofacWebApiDependencyResolver(IAppBuilder app)
        {
            if (_autofacWebApiDependencyResolver != null)
                return _autofacWebApiDependencyResolver;

            _autofacWebApiDependencyResolver = 
                new AutofacWebApiDependencyResolver(CreateContainer(app));

            return _autofacWebApiDependencyResolver;
        }

        public static IContainer CreateContainer(IAppBuilder app)
        {
            if (_container != null)
                return _container;

            var builder = new ContainerBuilder();

            // DataBase
            RegisterContext(app, builder);
            // Frameworks
            RegisterFrameworks(app, builder);
            // Types
            RegisterTypes(app, builder);
            // MVC
            RegisterInfrastructure(app, builder);
            // WebApi
            RegisterWebApi(app, builder, GlobalConfiguration.Configuration);

            _container = builder.Build();

            return _container;
        }

        private static void RegisterContext(IAppBuilder app, ContainerBuilder builder)
        {
            builder.RegisterType<TripPlanerDbContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
        }

        private static void RegisterInfrastructure(IAppBuilder app, ContainerBuilder builder)
        {
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterSource(new ViewRegistrationSource());
            builder.RegisterFilterProvider();
        }

        private static void RegisterWebApi(IAppBuilder app, 
            ContainerBuilder builder, HttpConfiguration configuration)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(configuration);
        }

        private static void RegisterFrameworks(IAppBuilder app, ContainerBuilder builder)
        {
            builder.RegisterType<TripPlanerUserStore>().As<IUserStore<Traveler>>().InstancePerRequest();
            builder.RegisterType<TripPlanerUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<TripPlanerSignInManager>().AsSelf().InstancePerRequest();

            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication)
                   .InstancePerRequest();
            builder.Register<IDataProtectionProvider>(c => app.GetDataProtectionProvider())
                   .InstancePerRequest();
        }

        private static void RegisterTypes(IAppBuilder app, ContainerBuilder builder)
        {
            // Repositories
            builder.RegisterGeneric(typeof (EntityRepository<>))
                   .As(typeof (IEntityRepository<>))
                   .InstancePerRequest();

            // Services
            builder.RegisterGeneric(typeof(EntityService<>))
                   .As(typeof(IEntityService<>))
                   .InstancePerRequest();

            //...
        }
    }
}