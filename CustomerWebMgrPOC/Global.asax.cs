using CustomerWebMgrPOC.Enums;
using CustomerWebMgrPOC.Interfaces;
using DataManagers;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CustomerWebMgrPOC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected override IKernel CreateKernel()
        {
            var colonel = new StandardKernel();
            colonel.Load(Assembly.GetExecutingAssembly());



            colonel.Bind<IDataManager<ICustomer>>().To<CustomerDBManager>()
                .WithConstructorArgument("allowDupes", AllowDuplicates.Allow)
                .WithConstructorArgument("ConnectionString", CleanSqLiteConnectionString());

            return colonel;
        }

        /// <summary>
        /// Having problems with path resolution, so I'm going to try doing a little cleanup work on the file path here.
        /// </summary>
        /// <returns></returns>
        private string CleanSqLiteConnectionString()
        {
            string[] rawConnectionString = ConfigurationManager.ConnectionStrings["CustomerDB"].ConnectionString.Split(';');
            string cleanedConnectionString = "data source={0};version={1}";
            string fixedPath = Server.MapPath(rawConnectionString[0].Split('=')[1]);
            string version = rawConnectionString[1].Split('=')[1];
            return String.Format(cleanedConnectionString, fixedPath, version);
        }
    }
}