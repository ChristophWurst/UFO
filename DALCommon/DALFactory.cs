using System;
using System.Configuration;
using System.Reflection;

namespace UFO.DAL.Common {

	/// <summary>
	/// Database-independent data-access-layer (DAL)
	///
	/// This is a singleton, use <b>GetInstance</b> to get an instance
	///
	/// Uses app.config for dynamic assembly loading
	/// - Config value 'DALAssembly' specifies the assembly to use
	/// - Config value 'DefultConnectionString' specifies the connection string
	/// </summary>
	public class DALFactory {
		private static DALFactory instance;
		private string assemblyName;
		private Assembly dalAssembly;

		private DALFactory() {
			this.assemblyName = ConfigurationManager.AppSettings["DALAssembly"];
			if (this.assemblyName == null) {
				throw new ArgumentException($"Parameter \"DALAssembly\" not set in AppSettings.");
			}
			this.dalAssembly = Assembly.Load(this.assemblyName);
		}

		/// <summary>
		/// Get a DALFactory instance
		/// </summary>
		/// <returns>An instance of DALFactory (singleton)</returns>
		public static DALFactory GetInstance() {
			if (instance == null) {
				instance = new DALFactory();
			}
			return instance;
		}

		private Type GetType(string typeName) {
			Type type = this.dalAssembly.GetType(typeName);
			if (type == null) {
				throw new ArgumentException($"Type {type.FullName} not found.");
			}
			return type;
		}

		private bool ConstructorExists(Type type, params Type[] constructorParamTypes) {
			return type.GetConstructor(constructorParamTypes) != null;
		}

		private void EnsureConstructorExists(Type type, params Type[] constructorParamTypes) {
			if (!this.ConstructorExists(type, constructorParamTypes)) {
				throw new ArgumentException($"No suitable constructor for {type.FullName} found.");
			}
		}

		/// <summary>
		/// Create a new database
		/// </summary>
		/// <returns></returns>
		public IDatabase CreateDatabase() {
			var connSettings = ConfigurationManager.ConnectionStrings["DefaultConnectionString"];
			if (connSettings == null) {
				throw new ArgumentException($"Parameter \"DefaultConnectionString\" not set in AppSettings.");
			}
			return CreateDatabase(connSettings.ConnectionString);
		}

		private IDatabase CreateDatabase(string connectionString) {
			Type dbType = this.GetType(assemblyName + ".Database");
			this.EnsureConstructorExists(dbType, typeof(string));
			return Activator.CreateInstance(dbType, new object[] { connectionString }) as IDatabase;
		}

		/// <summary>
		/// Create a new IAreaDAO instance
		/// </summary>
		/// <param name="database"></param>
		/// <returns></returns>
		public IAreaDAO CreateAreaDAO(IDatabase database) {
			Type classType = this.GetType(this.assemblyName + ".AreaDAO");
			this.EnsureConstructorExists(classType, typeof(IDatabase));
			return Activator.CreateInstance(classType, new object[] { database }) as IAreaDAO;
		}

		/// <summary>
		/// Create a new IArtistDAO instance
		/// </summary>
		/// <param name="database"></param>
		/// <returns></returns>
		public IArtistDAO CreateArtistDAO(IDatabase database) {
			Type classType = this.GetType(this.assemblyName + ".ArtistDAO");
			this.EnsureConstructorExists(classType, typeof(IDatabase));
			return Activator.CreateInstance(classType, new object[] { database }) as IArtistDAO;
		}

		/// <summary>
		/// Create a new ICategoryDAO instance
		/// </summary>
		/// <param name="database"></param>
		/// <returns></returns>
		public ICategoryDAO CreateCategoryDAO(IDatabase database) {
			Type classType = this.GetType(this.assemblyName + ".CategoryDAO");
			this.EnsureConstructorExists(classType, typeof(IDatabase));
			return Activator.CreateInstance(classType, new object[] { database }) as ICategoryDAO;
		}

		/// <summary>
		/// Create a new ICountryDAO instance
		/// </summary>
		/// <param name="database"></param>
		/// <returns></returns>
		public ICountryDAO CreateCountryDAO(IDatabase database) {
			Type classType = this.GetType(this.assemblyName + ".CountryDAO");
			this.EnsureConstructorExists(classType, typeof(IDatabase));
			return Activator.CreateInstance(classType, new object[] { database }) as ICountryDAO;
		}

		/// <summary>
		/// Create a new IVenueDAO instance
		/// </summary>
		/// <param name="database"></param>
		/// <returns></returns>
		public IVenueDAO CreateVenueDAO(IDatabase database) {
			Type classType = this.GetType(this.assemblyName + ".VenueDAO");
			this.EnsureConstructorExists(classType, typeof(IDatabase));
			return Activator.CreateInstance(classType, new object[] { database }) as IVenueDAO;
		}

		/// <summary>
		/// Create a new IPerformanceDAO instance
		/// </summary>
		/// <param name="database"></param>
		/// <returns></returns>
		public IPerformanceDAO CreatePerformanceDAO(IDatabase database) {
			Type classType = this.GetType(this.assemblyName + ".PerformanceDAO");
			this.EnsureConstructorExists(classType, typeof(IDatabase));
			return Activator.CreateInstance(classType, new object[] { database }) as IPerformanceDAO;
		}

		/// <summary>
		/// Create a new IUserDAO instance
		/// </summary>
		/// <param name="database"></param>
		/// <returns></returns>
		public IUserDAO CreateUserDAO(IDatabase database) {
			Type classType = this.GetType(this.assemblyName + ".UserDAO");
			this.EnsureConstructorExists(classType, typeof(IDatabase));
			return Activator.CreateInstance(classType, new object[] { database }) as IUserDAO;
		}

		/// <summary>
		/// Create a new ISpectacledayDAO instance
		/// </summary>
		/// <param name="database"></param>
		/// <returns></returns>
		public ISpectacledayDAO CreateSpectacledayDAO(IDatabase database) {
			Type classType = this.GetType(this.assemblyName + ".SpectacledayDAO");
			this.EnsureConstructorExists(classType, typeof(IDatabase));
			return Activator.CreateInstance(classType, new object[] { database }) as ISpectacledayDAO;
		}

		/// <summary>
		/// Create a new ITimeSlotDAO instance
		/// </summary>
		/// <param name="database"></param>
		/// <returns></returns>
		public ITimeSlotDAO CreateTimeSlotDAO(IDatabase database) {
			Type classType = this.GetType(this.assemblyName + ".TimeSlotDAO");
			this.EnsureConstructorExists(classType, typeof(IDatabase));
			return Activator.CreateInstance(classType, new object[] { database }) as ITimeSlotDAO;
		}

		/// <summary>
		/// Create a new ISpectacledayTimeSlotDAO instance
		/// </summary>
		/// <param name="database"></param>
		/// <returns></returns>
		public ISpectacledayTimeSlotDAO CreateSpectacledayTimeSlotDAO(IDatabase database) {
			Type classType = this.GetType(this.assemblyName + ".SpectacledayTimeSlotDAO");
			this.EnsureConstructorExists(classType, typeof(IDatabase));
			return Activator.CreateInstance(classType, new object[] { database }) as ISpectacledayTimeSlotDAO;
		}
	}
}