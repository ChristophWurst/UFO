using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using UFO.BL;
using UFO.DomainClasses;

namespace UFO.WebService {

	/// <summary>
	/// Web service that exports the UFO.BL business logic interface
	///
	/// This class acts as proxy. All method calls are delegated to a business logic implementation
	/// </summary>
	[WebService(Namespace = "http://ufo.fh-hagenberg.at/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// Wenn der Aufruf dieses Webdiensts aus einem Skript zulässig sein soll, heben Sie mithilfe von ASP.NET AJAX die Kommentarmarkierung für die folgende Zeile auf.
	// [System.Web.Script.Services.ScriptService]
	public class UFO : System.Web.Services.WebService {
		private IBusinessLogic bl;

		public UFO() {
			bl = BusinessLogicFactory.GetBusinessLogic();
		}

		/// <summary>
		/// Creat a new artist
		/// </summary>
		/// <param name="artist"></param>
		/// <returns></returns>
		[WebMethod]
		public Artist CreateArtist(Artist artist) {
			return bl.CreateArtist(artist);
		}

		/// <summary>
		/// Create schedule pdf for the given spectacleday
		/// </summary>
		/// <param name="spectacleDay"></param>
		/// <returns>byte representation of the file</returns>
		[WebMethod]
		public byte[] CreatePdfScheduleForSpectacleDay(Spectacleday spectacleDay) {
			return bl.CreatePdfScheduleForSpectacleDay(spectacleDay);
		}

		/// <summary>
		/// Create a new venue
		/// </summary>
		/// <param name="venue"></param>
		/// <returns></returns>
		[WebMethod]
		public Venue CreateVenue(Venue venue) {
			return bl.CreateVenue(venue);
		}

		/// <summary>
		/// Delete an artist
		/// </summary>
		/// <param name="artist"></param>
		[WebMethod]
		public void DeleteArtist(Artist artist) {
			bl.DeleteArtist(artist);
		}

		/// <summary>
		/// Get all areas
		/// </summary>
		/// <returns></returns>
		[WebMethod]
		public List<Area> GetAreas() {
			return bl.GetAreas().ToList();
		}

		/// <summary>
		/// Get artist by id
		/// </summary>
		/// <param name="artist"></param>
		/// <returns></returns>
		[WebMethod]
		public Artist GetArtistById(Artist artist) {
			return bl.GetArtistById(artist);
		}

		/// <summary>
		/// Get all artists
		/// </summary>
		/// <returns></returns>
		[WebMethod]
		public List<Artist> GetArtists() {
			return bl.GetArtists().ToList();
		}

		/// <summary>
		/// Get artists for the given category
		/// </summary>
		/// <param name="category"></param>
		/// <returns></returns>
		[WebMethod]
		public List<Artist> GetArtistsForCategory(Category category) {
			return bl.GetArtistsForCategory(category).ToList();
		}

		/// <summary>
		/// Get all categories
		/// </summary>
		/// <returns></returns>
		[WebMethod]
		public List<Category> GetCategories() {
			return bl.GetCategories().ToList();
		}

		/// <summary>
		/// Get category by id
		/// </summary>
		/// <param name="category"></param>
		/// <returns></returns>
		[WebMethod]
		public Category GetCategoryById(Category category) {
			return bl.GetCategoryById(category);
		}

		/// <summary>
		/// Get all countries
		/// </summary>
		/// <returns></returns>
		[WebMethod]
		public List<Country> GetCountries() {
			return bl.GetCountries().ToList();
		}

		/// <summary>
		/// Get country by id
		/// </summary>
		/// <param name="country"></param>
		/// <returns></returns>
		[WebMethod]
		public Country GetCountryById(Country country) {
			return bl.GetCountryById(country);
		}

		/// <summary>
		/// Get performacnes for the given spectacleday
		/// </summary>
		/// <param name="day"></param>
		/// <returns></returns>
		[WebMethod]
		public List<Performance> GetPerformanesForSpetacleDay(Spectacleday day) {
			return bl.GetPerformanesForSpetacleDay(day).ToList();
		}

		/// <summary>
		/// Get all spectacledays
		/// </summary>
		/// <returns></returns>
		[WebMethod]
		public List<Spectacleday> GetSpectacleDays() {
			return bl.GetSpectacleDays().ToList();
		}

		/// <summary>
		/// Get spectacledaytimeslots for the given spectacleday
		/// </summary>
		/// <param name="day"></param>
		/// <returns></returns>
		[WebMethod]
		public List<SpectacledayTimeSlot> GetSpectacleDayTimeSlotsForSpectacleDay(Spectacleday day) {
			return bl.GetSpectacleDayTimeSlotsForSpectacleDay(day).ToList();
		}

		/// <summary>
		/// Get all timeslots
		/// </summary>
		/// <returns></returns>
		[WebMethod]
		public List<TimeSlot> GetTimeSlots() {
			return bl.GetTimeSlots().ToList();
		}

		/// <summary>
		/// Get all venues
		/// </summary>
		/// <returns></returns>
		[WebMethod]
		public List<Venue> GetVenues() {
			return bl.GetVenues().ToList();
		}

		/// <summary>
		/// Get venues for the given area
		/// </summary>
		/// <param name="area"></param>
		/// <returns></returns>
		[WebMethod]
		public List<Venue> GetVenuesForArea(Area area) {
			return bl.GetVenuesForArea(area).ToList();
		}

		/// <summary>
		/// Try to log a user in by the given credentials
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		[WebMethod]
		public bool Login(string username, string password) {
			return bl.Login(username, password);
		}

		/// <summary>
		/// Update an existing artist
		/// </summary>
		/// <param name="artist"></param>
		/// <returns></returns>
		[WebMethod]
		public Artist UpdateArtist(Artist artist) {
			return bl.UpdateArtist(artist);
		}

		/// <summary>
		/// Update the given performances
		/// </summary>
		/// <param name="spectacleDay"></param>
		/// <param name="performances"></param>
		[WebMethod]
		public void UpdatePerformances(Spectacleday spectacleDay, List<Performance> performances) {
			bl.UpdatePerformances(spectacleDay, performances);
		}

		/// <summary>
		/// Update an existing venue
		/// </summary>
		/// <param name="venue"></param>
		/// <returns></returns>
		[WebMethod]
		public Venue UpdateVenue(Venue venue) {
			return bl.UpdateVenue(venue);
		}

		/// <summary>
		/// Get performance by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[WebMethod]
		public Performance GetPerformance(int id) {
			return bl.GetPerformance(id);
		}

		/// <summary>
		/// Ger all performances of the given artist
		/// </summary>
		/// <param name="artist"></param>
		/// <returns></returns>
		[WebMethod]
		public List<Performance> GetPerformancesForArtist(Artist artist) {
			return bl.GetPerformancesForArtist(artist).ToList();
		}

		/// <summary>
		/// Get timeslot for the given performance
		/// </summary>
		/// <param name="performance"></param>
		/// <returns></returns>
		[WebMethod]
		public TimeSlot GetTimeSlotForPerformance(Performance performance) {
			return bl.GetTimeSlotForPerformance(performance);
		}

		/// <summary>
		/// get timeslots for the given performances
		/// </summary>
		/// <param name="performances"></param>
		/// <returns></returns>
		[WebMethod]
		public List<TimeSlot> GetTimeSlotsForPerformances(List<Performance> performances) {
			return bl.GetTimeSlotsForPerformances(performances).ToList();
		}

		/// <summary>
		/// Get artists for the given performances
		/// </summary>
		/// <param name="performances"></param>
		/// <returns></returns>
		[WebMethod]
		public List<Artist> GetArtistsForPerformances(List<Performance> performances) {
			return bl.GetArtistsForPerformances(performances).ToList();
		}

		/// <summary>
		/// Get venues for the given performances
		/// </summary>
		/// <param name="performances"></param>
		/// <returns></returns>
		[WebMethod]
		public List<Venue> GetVenuesForPerformances(List<Performance> performances) {
			return bl.GetVenuesForPerformances(performances).ToList();
		}

		/// <summary>
		/// Get spectacledays for the given performancedays
		/// </summary>
		/// <param name="performances"></param>
		/// <returns></returns>
		[WebMethod]
		public List<Spectacleday> GetSpectacleDaysForPerformances(List<Performance> performances) {
			return bl.GetSpectacleDaysForPerformances(performances).ToList();
		}

		/// <summary>
		/// Get spectalceday timeslots for the given performances
		/// </summary>
		/// <param name="performances"></param>
		/// <returns></returns>
		[WebMethod]
		public List<SpectacledayTimeSlot> GetSpectacledayTimeSlotsForPerformances(List<Performance> performances) {
			return bl.GetSpectacledayTimeSlotsForPerformances(performances).ToList();
		}

		/// <summary>
		///  Get performances of the given venue
		/// </summary>
		/// <param name="venue"></param>
		/// <returns></returns>
		[WebMethod]
		public List<Performance> GetPerformancesForVenue(Venue venue) {
			return bl.GetPerformancesForVenue(venue).ToList();
		}
	}
}