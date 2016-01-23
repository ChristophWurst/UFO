using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.BL {

	/// <summary>
	/// Abstract business logic that implements both synchronous and asynchronous methods
	/// </summary>
	internal abstract class ABusinessLogic : IBusinessLogic, IBusinessLogicAsync {

		public abstract Artist CreateArtist(Artist artist);

		public abstract byte[] CreatePdfScheduleForSpectacleDay(Spectacleday spectacleDay);

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

		public abstract bool Login(string username, string password);

		public abstract Artist UpdateArtist(Artist artist);

		public abstract void UpdatePerformances(Spectacleday spectacleDay, IEnumerable<Performance> performances);

		public abstract Venue UpdateVenue(Venue venue);

		public abstract IEnumerable<Performance> GetPerformancesForArtist(Artist artist);

		public abstract TimeSlot GetTimeSlotForPerformance(Performance performance);

		public abstract IEnumerable<TimeSlot> GetTimeSlotsForPerformances(IEnumerable<Performance> performances);

		public abstract IEnumerable<Artist> GetArtistsForPerformances(IEnumerable<Performance> performances);

		public abstract IEnumerable<Venue> GetVenuesForPerformances(IEnumerable<Performance> performances);

		public abstract IEnumerable<Spectacleday> GetSpectacleDaysForPerformances(IEnumerable<Performance> performances);

		public abstract IEnumerable<SpectacledayTimeSlot> GetSpectacledayTimeSlotsForPerformances(IEnumerable<Performance> performances);

		public abstract Performance GetPerformance(int id);

		public abstract IEnumerable<Performance> GetPerformancesForVenue(Venue venue);

		public Task<Artist> CreateArtistAsync(Artist artist) {
			return Task.Run(() => CreateArtist(artist));
		}

		public Task<byte[]> CreatePdfScheduleForSpectacleDayAsync(Spectacleday spectacleDay) {
			return Task.Run(() => CreatePdfScheduleForSpectacleDay(spectacleDay));
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

		public Task<bool> LoginAsync(string username, string password) {
			return Task.Run(() => Login(username, password));
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

		public Task<Performance> GetPerformanceAsync(int id) {
			return Task.Run(() => GetPerformance(id));
		}

		public Task<IEnumerable<Performance>> GetPerformancesForArtistAsync(Artist artist) {
			return Task.Run(() => GetPerformancesForArtist(artist));
		}

		public Task<TimeSlot> GetTimeSlotForPerformanceAsync(Performance performance) {
			return Task.Run(() => GetTimeSlotForPerformance(performance));
		}

		Task<IEnumerable<TimeSlot>> IBusinessLogicAsync.GetTimeSlotsForPerformances(IEnumerable<Performance> performances) {
			return Task.Run(() => GetTimeSlotsForPerformances(performances));
		}

		Task<IEnumerable<Artist>> IBusinessLogicAsync.GetArtistsForPerformances(IEnumerable<Performance> performances) {
			return Task.Run(() => GetArtistsForPerformances(performances));
		}

		Task<IEnumerable<Venue>> IBusinessLogicAsync.GetVenuesForPerformances(IEnumerable<Performance> performances) {
			return Task.Run(() => GetVenuesForPerformances(performances));
		}

		Task<IEnumerable<Spectacleday>> IBusinessLogicAsync.GetSpectacleDaysForPerformances(IEnumerable<Performance> performances) {
			return Task.Run(() => GetSpectacleDaysForPerformances(performances));
		}

		Task<IEnumerable<SpectacledayTimeSlot>> IBusinessLogicAsync.GetSpectacledayTimeSlotsForPerformances(IEnumerable<Performance> performances) {
			return Task.Run(() => GetSpectacledayTimeSlotsForPerformances(performances));
		}

		Task<IEnumerable<Performance>> IBusinessLogicAsync.GetPerformancesForVenue(Venue venue) {
			return Task.Run(() => GetPerformancesForVenue(venue));
		}
	}
}