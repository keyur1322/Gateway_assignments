using Hotel_Management_System_Logic;
using Hotel_Management_System_Logic.Interface;
using Hotel_Management_System_Logic.UnityDBHelper;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace Hotel_Management_System
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();


            container.RegisterType<IHotelManager, HotelManager>();
            container.AddNewExtension<UnityDBHelper>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}