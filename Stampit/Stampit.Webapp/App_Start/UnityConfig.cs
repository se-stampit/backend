using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Stampit.Logic.Interface;
using Stampit.Logic;
using Stampit.Logic.Fakes;

namespace Stampit.Webapp.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });
        
        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IQrCodeGenerator, GoogleQrCodeGenerator>();
            container.RegisterType<IStampCodeProvider, FakeStampcodeProvider>();
            container.RegisterType<IStampCodeService, StampCodeService>();
            container.RegisterInstance<IStampCodeStorage>(LocalStampCodeStorage.GetStampCodeStorage());
            container.RegisterType<Microsoft.AspNet.Identity.IUserStore<Models.ApplicationUser>, Microsoft.AspNet.Identity.EntityFramework.UserStore<Models.ApplicationUser>>(
            new InjectionConstructor(typeof(Models.ApplicationDbContext)));
            container.RegisterType<System.Data.Entity.DbContext, Models.ApplicationDbContext>(
    new HierarchicalLifetimeManager());
            container.RegisterType<Microsoft.AspNet.Identity.UserManager<Models.ApplicationUser>>(
                new HierarchicalLifetimeManager());
            container.RegisterType<Microsoft.AspNet.Identity.IUserStore<Models.ApplicationUser>, Microsoft.AspNet.Identity.EntityFramework.UserStore<Models.ApplicationUser>>(
                new HierarchicalLifetimeManager());

            container.RegisterType<Controllers.AccountController>(
                new InjectionConstructor());

            var blobRepository = new FakeBlobRepository();
            var enduserRepository = new FakeEnduserRepository();
            var roleRepository = new FakeRoleRepository();
            var companyRepository = new FakeCompanyRepository(blobRepository);
            var businessuserRepository = new FakeBusinessuserRepository(roleRepository, companyRepository);
            var productRepository = new FakeProductRepository(companyRepository);
            var stampcardRepository = new FakeStampcardRepository(productRepository, enduserRepository);
            var storeRepository = new FakeStoreRepository(companyRepository);

            container.RegisterInstance<IBlobRepository>(blobRepository);
            container.RegisterInstance<IEnduserRepository>(enduserRepository);
            container.RegisterInstance<IRoleRepository>(roleRepository);
            container.RegisterInstance<IBusinessuserRepository>(businessuserRepository);
            container.RegisterInstance<ICompanyRepository>(companyRepository);
            container.RegisterInstance<IProductRepository>(productRepository);
            container.RegisterInstance<IStampcardRepository>(stampcardRepository);
            container.RegisterInstance<IStoreRepository>(storeRepository);
        }
    }
}
