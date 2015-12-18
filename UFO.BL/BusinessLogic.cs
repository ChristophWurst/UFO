using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace UFO.BL {

	internal class BusinessLogic : IBusinessLogic {
		private DALFactory dalFactory;
		private IDatabase db;
		private IMailService ms;

		public BusinessLogic(DALFactory dalFactory, IMailService ms) {
			this.db = dalFactory.CreateDatabase();
			this.dalFactory = dalFactory;
			this.ms = ms;
		}

		public BusinessLogic(DALFactory dalFactory) {
			this.db = dalFactory.CreateDatabase();
			this.dalFactory = dalFactory;
			this.ms = new MailService();
		}

		public Artist CreateArtist(Artist artist) {
			return dalFactory.CreateArtistDAO(db).Create(artist);
		}

		public void DeleteArtist(Artist artist) {
			dalFactory.CreateArtistDAO(db).Delete(artist);
		}

		public IEnumerable<Area> GetAreas() {
			return dalFactory.CreateAreaDAO(db).GetAll();
		}

		public IEnumerable<Artist> GetArtists() {
			return dalFactory.CreateArtistDAO(db).GetAll();
		}

		public IEnumerable<Artist> GetArtistsForCategory(Category category) {
			return dalFactory.CreateArtistDAO(db).GetForCategory(category);
		}

		public IEnumerable<Category> GetCategories() {
			return dalFactory.CreateCategoryDAO(db).GetAll();
		}

		public IEnumerable<Venue> GetVenuesForArea(Area area) {
			return dalFactory.CreateVenueDAO(db).GetForArea(area);
		}

		public Venue CreateVenue(Venue venue) {
			return dalFactory.CreateVenueDAO(db).Create(venue);
		}

		public Venue UpdateVenue(Venue venue) {
			return dalFactory.CreateVenueDAO(db).Update(venue);
		}

		public Artist UpdateArtist(Artist artist) {
			return dalFactory.CreateArtistDAO(db).Update(artist);
		}

		public IEnumerable<TimeSlot> GetTimeSlots() {
			return dalFactory.CreateTimeSlotDAO(db).GetAll();
		}

		public IEnumerable<Country> GetCountries() {
			return dalFactory.CreateCountryDAO(db).GetAll();
		}

		public Category GetCategoryById(Category category) {
			return dalFactory.CreateCategoryDAO(db).GetById(category.Id);
		}

		public Country GetCountryById(Country country) {
			return dalFactory.CreateCountryDAO(db).GetById(country.Id);
		}

		public Artist GetArtistById(Artist artist) {
			return dalFactory.CreateArtistDAO(db).GetById(artist.Id);
		}

		public void MailPerformanceChangesToArtists(IEnumerable<Artist> artists, IEnumerable<Performance> performances) {
			throw new NotImplementedException();
		}
	}
}