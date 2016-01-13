using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using UFO.BL;
using UFO.DomainClasses;

namespace UFO.WebService {

	/// <summary>
	/// Zusammenfassungsbeschreibung für UFO
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

		[WebMethod]
		public Artist CreateArtist(Artist artist) {
			return bl.CreateArtist(artist);
		}

		[WebMethod]
		public void CreatePdfScheduleForSpectacleDay(Spectacleday spectacleDay) {
			bl.CreatePdfScheduleForSpectacleDay(spectacleDay);
		}

		[WebMethod]
		public Venue CreateVenue(Venue venue) {
			return bl.CreateVenue(venue);
		}

		[WebMethod]
		public void DeleteArtist(Artist artist) {
			bl.DeleteArtist(artist);
		}

		[WebMethod]
		public List<Area> GetAreas() {
			return bl.GetAreas().ToList();
		}

		[WebMethod]
		public Artist GetArtistById(Artist artist) {
			return bl.GetArtistById(artist);
		}

		[WebMethod]
		public List<Artist> GetArtists() {
			return bl.GetArtists().ToList();
		}

		[WebMethod]
		public List<Artist> GetArtistsForCategory(Category category) {
			return bl.GetArtistsForCategory(category).ToList();
		}

		[WebMethod]
		public List<Category> GetCategories() {
			return bl.GetCategories().ToList();
		}

		[WebMethod]
		public Category GetCategoryById(Category category) {
			return bl.GetCategoryById(category);
		}

		[WebMethod]
		public List<Country> GetCountries() {
			return bl.GetCountries().ToList();
		}

		[WebMethod]
		public Country GetCountryById(Country country) {
			return bl.GetCountryById(country);
		}

		[WebMethod]
		public List<Performance> GetPerformanesForSpetacleDay(Spectacleday day) {
			return bl.GetPerformanesForSpetacleDay(day).ToList();
		}

		[WebMethod]
		public List<Spectacleday> GetSpectacleDays() {
			return bl.GetSpectacleDays().ToList();
		}

		[WebMethod]
		public List<SpectacledayTimeSlot> GetSpectacleDayTimeSlotsForSpectacleDay(Spectacleday day) {
			return bl.GetSpectacleDayTimeSlotsForSpectacleDay(day).ToList();
		}

		[WebMethod]
		public List<TimeSlot> GetTimeSlots() {
			return bl.GetTimeSlots().ToList();
		}

		[WebMethod]
		public List<Venue> GetVenues() {
			return bl.GetVenues().ToList();
		}

		[WebMethod]
		public List<Venue> GetVenuesForArea(Area area) {
			return bl.GetVenuesForArea(area).ToList();
		}

		[WebMethod]
		public bool Login(string username, string password) {
			return bl.Login(username, password);
		}

		[WebMethod]
		public Artist UpdateArtist(Artist artist) {
			return bl.UpdateArtist(artist);
		}

		[WebMethod]
		public void UpdatePerformances(Spectacleday spectacleDay, List<Performance> performances) {
			bl.UpdatePerformances(spectacleDay, performances);
		}

		[WebMethod]
		public Venue UpdateVenue(Venue venue) {
			return bl.UpdateVenue(venue);
		}

		[WebMethod]
		public List<Performance> GetPerformancesForArtist(Artist artist) {
			return bl.GetPerformancesForArtist(artist).ToList();
		}
	}
}