using System;

using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using Unity.Lifetime;
using GYMProject.Models; // Add your models namespace here
using GYMProject.Data;   // Add your data namespace here
using GYMProject.Repositories;

namespace GYMProject
{
    public static class UnityConfig
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer Container => container.Value;

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IRepository<Member>, Repository<Member>>();
            container.RegisterType<IRepository<Staff>, Repository<Staff>>();
            container.RegisterType<IRepository<Session>, Repository<Session>>();
            container.RegisterType<GYMContext>();
        }
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}