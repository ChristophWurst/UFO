using System;
using System.Configuration;
using System.Reflection;

namespace UFO.DAL.Common {

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

		public IAreaDAO CreateAreaDAO(IDatabase database) {
			Type classType = this.GetType(this.assemblyName + ".AreaDAO");
			this.EnsureConstructorExists(classType, typeof(IDatabase));
			return Activator.CreateInstance(classType, new object[] { database }) as IAreaDAO;
		}

		public IArtistDAO CreateArtistDAO(IDatabase database) {
			Type classType = this.GetType(this.assemblyName + ".ArtistDAO");
			this.EnsureConstructorExists(classType, typeof(IDatabase));
			return Activator.CreateInstance(classType, new object[] { database }) as IArtistDAO;
		}

		public ICategoryDAO CreateCategoryDAO(IDatabase database) {
			Type classType = this.GetType(this.assemblyName + ".CategoryDAO");
			this.EnsureConstructorExists(classType, typeof(IDatabase));
			return Activator.CreateInstance(classType, new object[] { database }) as ICategoryDAO;
		}

		public ICountryDAO CreateCountryDAO(IDatabase database) {
			Type classType = this.GetType(this.assemblyName + ".CountryDAO");
			this.EnsureConstructorExists(classType, typeof(IDatabase));
			return Activator.CreateInstance(classType, new object[] { database }) as ICountryDAO;
		}

		public IVenueDAO CreateVenueDAO(IDatabase database) {
			Type classType = this.GetType(this.assemblyName + ".VenueDAO");
			this.EnsureConstructorExists(classType, typeof(IDatabase));
			return Activator.CreateInstance(classType, new object[] { database }) as IVenueDAO;
		}

		public IPerformanceDAO CreatePerformanceDAO(IDatabase database) {
			Type classType = this.GetType(this.assemblyName + ".PerformanceDAO");
			this.EnsureConstructorExists(classType, typeof(IDatabase));
			return Activator.CreateInstance(classType, new object[] { database }) as IPerformanceDAO;
		}

		public IUserDAO CreateUserDAO(IDatabase database) {
			Type classType = this.GetType(this.assemblyName + ".UserDAO");
			this.EnsureConstructorExists(classType, typeof(IDatabase));
			return Activator.CreateInstance(classType, new object[] { database }) as IUserDAO;
		}
	}
}