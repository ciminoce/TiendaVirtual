[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TiendaVirtual.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(TiendaVirtual.Web.App_Start.NinjectWebCommon), "Stop")]

namespace TiendaVirtual.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Neptuno2022EF.Datos.Repositorios;
    using Neptuno2022EF.Servicios.Servicios;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using TiendaVirtual.Datos;
    using TiendaVirtual.Datos.Interfaces;
    using TiendaVirtual.Datos.Repositorios;
    using TiendaVirtual.Servicios.Interfaces;
    using TiendaVirtual.Servicios.Servicios;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<NeptunoDbContext>().ToSelf().InRequestScope();
            kernel.Bind<IRepositorioCategorias>().To<RepositorioCategorias>().InRequestScope();
            kernel.Bind<IServiciosCategorias>().To<ServiciosCategorias>().InRequestScope();
            kernel.Bind<IRepositorioPaises>().To<RepositorioPaises>().InRequestScope();
            kernel.Bind<IServiciosPaises>().To<ServiciosPaises>().InRequestScope();
            kernel.Bind<IRepositorioCiudades>().To<RepositorioCiudades>().InRequestScope();
            kernel.Bind<IServiciosCiudades>().To<ServiciosCiudades>().InRequestScope();
            kernel.Bind<IRepositorioClientes>().To<RepositorioClientes>().InRequestScope();
            kernel.Bind<IServiciosClientes>().To<ServiciosClientes>().InRequestScope();
            kernel.Bind<IRepositorioProveedores>().To<RepositorioProveedores>().InRequestScope();
            kernel.Bind<IServiciosProveedores>().To<ServiciosProveedores>().InRequestScope();
            kernel.Bind<IRepositorioProductos>().To<RepositorioProductos>().InRequestScope();
            kernel.Bind<IServiciosProductos>().To<ServiciosProductos>().InRequestScope();
            kernel.Bind<IRepositorioVentas>().To<RepositorioVentas>().InRequestScope();
            kernel.Bind<IServiciosVentas>().To<ServiciosVentas>().InRequestScope();
            kernel.Bind<IRepositorioDetalleVentas>().To<RepositorioDetalleVentas>().InRequestScope();
            kernel.Bind<IRepositorioCarritos>().To<RepositorioCarritos>().InRequestScope();
            kernel.Bind<IServiciosCarrito>().To<ServiciosCarritos>().InRequestScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
        }
    }
}