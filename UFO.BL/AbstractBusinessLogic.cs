using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.BL {

	internal abstract class AbstractBusinessLogic : IBusinessLogic, IBusinessLogicAsync {

		public abstract Artist CreateArtist(Artist artist);

		public abstract void CreatePdfScheduleForSpectacleDay(Spectacleday spectacleDay);

		public abstract Venue CreateVenue(Venue venue);

		public abstract void DeleteArtist(Artist artist);

		public abstract IEnumerable<Area> GetAreas();

		public abstract Artist GetArtistById(Artist artist);

		public abstract IEnumerable<Artist> GetArtists();

		public abstract IEnumerable<Artist> GetArtistsForCategory(Category category);

		public abstract IEnumerable<Category> GetCategories();

		public abstract Category GetCategoryById(Category category);

		public abstract IEnumerable<Country> GetCountries();

		public abstract Country GetCountryById(Country country);

		public abstract IEnumerable<Performance> GetPerformanesForSpetacleDay(Spectacleday day);

		public abstract IEnumerable<Spectacleday> GetSpectacleDays();

		public abstract IEnumerable<SpectacledayTimeSlot> GetSpectacleDayTimeSlotsForSpectacleDay(Spectacleday day);

		public abstract IEnumerable<TimeSlot> GetTimeSlots();

		public abstract IEnumerable<Venue> GetVenues();

		public abstract IEnumerable<Venue> GetVenuesForArea(Area area);

		public abstract void Login(string username, string password);

		public abstract Artist UpdateArtist(Artist artist);

		public abstract void UpdatePerformances(Spectacleday spectacleDay, IEnumerable<Performance> performances);

		public abstract Venue UpdateVenue(Venue venue);

		public Task<Artist> CreateArtistAsync(Artist artist) {
			return Task.Run(() => CreateArtist(artist));
		}

		public void CreatePdfScheduleForSpectacleDayAsync(Spectacleday spectacleDay) {
			Task.Run(() => CreatePdfScheduleForSpectacleDay(spectacleDay));
		}

		public Task<Venue> CreateVenueAsync(Venue venue) {
			return Task.Run(() => CreateVenue(venue));
		}

		public void DeleteArtistAsync(Artist artist) {
			Task.Run(() => DeleteArtist(artist));
		}

		public Task<IEnumerable<Area>> GetAreasAsync() {
			return Task.Run(() => GetAreas());
		}

		public Task<Artist> GetArtistByIdAsync(Artist artist) {
			return Task.Run(() => GetArtistById(artist));
		}

		public Task<IEnumerable<Artist>> GetArtistsAsync() {
			return Task.Run(() => GetArtists());
		}

		public Task<IEnumerable<Artist>> GetArtistsForCategoryAsync(Category category) {
			return Task.Run(() => GetArtistsForCategory(category));
		}

		public Task<IEnumerable<Category>> GetCategoriesAsync() {
			return Task.Run(() => GetCategories());
		}

		public Task<Category> GetCategoryByIdAsync(Category category) {
			return Task.Run(() => GetCategoryById(category));
		}

		public Task<IEnumerable<Country>> GetCountriesAsync() {
			return Task.Run(() => GetCountries());
		}

		public Task<Country> GetCountryByIdAsync(Country country) {
			return Task.Run(() => GetCountryById(country));
		}

		public Task<IEnumerable<Performance>> GetPerformanesForSpetacleDayAsync(Spectacleday day) {
			return Task.Run(() => GetPerformanesForSpetacleDay(day));
		}

		public Task<IEnumerable<Spectacleday>> GetSpectacleDaysAsync() {
			return Task.Run(() => GetSpectacleDays());
		}

		public Task<IEnumerable<SpectacledayTimeSlot>> GetSpectacleDayTimeSlotsForSpectacleDayAsync(Spectacleday day) {
			return Task.Run(() => GetSpectacleDayTimeSlotsForSpectacleDay(day));
		}

		public Task<IEnumerable<TimeSlot>> GetTimeSlotsAsync() {
			return Task.Run(() => GetTimeSlots());
		}

		public Task<IEnumerable<Venue>> GetVenuesAsync() {
			return Task.Run(() => GetVenues());
		}

		public Task<IEnumerable<Venue>> GetVenuesForAreaAsync(Area area) {
			return Task.Run(() => GetVenuesForArea(area));
		}

		public void LoginAsync(string username, string password) {
			Task.Run(() => Login(username, password));
		}

		public Task<Artist> UpdateArtistAsync(Artist artist) {
			return Task.Run(() => UpdateArtist(artist));
		}

		public void UpdatePerformancesAsync(Spectacleday spectacleDay, IEnumerable<Performance> performances) {
			Task.Run(() => UpdatePerformances(spectacleDay, performances));
		}

		public Task<Venue> UpdateVenueAsync(Venue venue) {
			return Task.Run(() => UpdateVenue(venue));
		}
	}
}