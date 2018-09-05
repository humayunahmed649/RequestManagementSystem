using System;
using System.Data.Entity;
using RMS.BLL;
using RMS.BLL.Contracts;
using RMS.Models.DatabaseContext;
using RMS.Repositories;
using RMS.Repositories.Contracts;
using Unity;

namespace RMS.App
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IOrganizationManager, OrganizationManager>();
            container.RegisterType<IOrganizationRepository, OrganizationRepository>();

            container.RegisterType<IDepartmentManager, DepartmentManager>();
            container.RegisterType<IDepartmentRepository, DepartmentRepository>();

            container.RegisterType<IEmployeeTypeManager, EmployeeTypeManager>();
            container.RegisterType<IEmployeeTypeRepository, EmployeeTypeRepository>();

            container.RegisterType<IEmployeeManager, EmployeeManager>();
            container.RegisterType<IEmployeeRepository, EmployeeRepository>();

            container.RegisterType<IDesignationManager, DesignationManager>();
            container.RegisterType<IDesignationRepository, DesignationRepository>();

            container.RegisterType<IVehicleTypeManager, VehicleTypeManager>();
            container.RegisterType<IVehicleTypeRepository, VehicleTypeRepository>();

            container.RegisterType<IVehicleManager, VehicleManager>();
            container.RegisterType<IVehicleRepository, VehicleRepository>();

            container.RegisterType<IRequisitionManager, RequisitionManager>();
            container.RegisterType<IRequisitionRepository, RequisitionRepository>();

            container.RegisterType<IAssignRequisitionManager, AssignRequisitionManager>();
            container.RegisterType<IAssignRequisitionRepository, AssignRequisitionRepository>();

            container.RegisterType<DbContext, RmsDbContext>();

            container.RegisterType<IDivisionRepository, DivisionRepository>();
            container.RegisterType<IDivisionManager, DivisionManager>();

            container.RegisterType<IDistrictRepository, DistrictRepository>();
            container.RegisterType<IDistrictManager, DistrictManager>();

            container.RegisterType<IUpazilaRepository, UpazilaRepository>();
            container.RegisterType<IUpazilaManager, UpazilaManager>();

            container.RegisterType<IRequisitionStatusManager, RequisitionStatusManager>();
            container.RegisterType<IRequisitionStatusRepository, RequisitionStatusRepository>();

            container.RegisterType<IAddressManager, AddressManager>();
            container.RegisterType<IAddressRepository, AddressRepository>();

        }
    }
}