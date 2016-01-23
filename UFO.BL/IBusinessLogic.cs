using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.BL {

	/// <summary>
	/// Synchronous business logic interface
	/// </summary>
	public interface IBusinessLogic {

		/// <summary>
		/// Get all areas
		/// </summary>
		/// <returns></returns>
		IEnumerable<Area> GetAreas();

		/// <summary>
		/// Get all categories
		/// </summary>
		/// <returns></returns>
		IEnumerable<Category> GetCategories();

		/// <summary>
		/// Get venues for the given area
		/// </summary>
		/// <param name="area"></param>
		/// <returns></returns>
		IEnumerable<Venue> GetVenuesForArea(Area area);

		/// <summary>
		/// Create a new venue
		/// </summary>
		/// <param name="venue"></param>
		/// <returns></returns>
		Venue CreateVenue(Venue venue);

		/// <summary>
		/// Update an existing venue
		/// </summary>
		/// <param name="venue"></param>
		/// <returns></returns>
		Venue UpdateVenue(Venue venue);

		/// <summary>
		/// Get all artists
		/// </summary>
		/// <returns></returns>
		IEnumerable<Artist> GetArtists();

		/// <summary>
		/// Get artists for the given category
		/// </summary>
		/// <param name="category"></param>
		/// <returns></returns>
		IEnumerable<Artist> GetArtistsForCategory(Category category);

		/// <summary>
		/// Update an existing artist
		/// </summary>
		/// <param name="artist"></param>
		/// <returns></returns>
		Artist UpdateArtist(Artist artist);

		/// <summary>
		/// Creat a new artist
		/// </summary>
		/// <param name="artist"></param>
		/// <returns></returns>
		Artist CreateArtist(Artist artist);

		/// <summary>
		/// Delete an artist
		/// </summary>
		/// <param name="artist"></param>
		void DeleteArtist(Artist artist);

		/// <summary>
		/// Get all timeslots
		/// </summary>
		/// <returns></returns>
		IEnumerable<TimeSlot> GetTimeSlots();

		/// <summary>
		/// Get all countries
		/// </summary>
		/// <returns></returns>
		IEnumerable<Country> GetCountries();

		/// <summary>
		/// Get category by id
		/// </summary>
		/// <param name="category"></param>
		/// <returns></returns>
		Category GetCategoryById(Category category);

		/// <summary>
		/// Get country by id
		/// </summary>
		/// <param name="country"></param>
		/// <returns></returns>
		Country GetCountryById(Country country);

		/// <summary>
		/// Get artist by id
		/// </summary>
		/// <param name="artist"></param>
		/// <returns></returns>
		Artist GetArtistById(Artist artist);

		/// <summary>
		/// Get all spectacledays
		/// </summary>
		/// <returns></returns>
		IEnumerable<Spectacleday> GetSpectacleDays();

		/// <summary>
		/// Get spectacledaytimeslots for the given spectacleday
		/// </summary>
		/// <param name="day"></param>
		/// <returns></returns>
		IEnumerable<SpectacledayTimeSlot> GetSpectacleDayTimeSlotsForSpectacleDay(Spectacleday day);

		/// <summary>
		/// Get performacnes for the given spectacleday
		/// </summary>
		/// <param name="day"></param>
		/// <returns></returns>
		IEnumerable<Performance> GetPerformanesForSpetacleDay(Spectacleday day);

		/// <summary>
		/// Update the given performances
		/// </summary>
		/// <param name="spectacleDay"></param>
		/// <param name="performances"></param>
		void UpdatePerformances(Spectacleday spectacleDay, IEnumerable<Performance> performances);

		/// <summary>
		/// Get all venues
		/// </summary>
		/// <returns></returns>
		IEnumerable<Venue> GetVenues();

		/// <summary>
		/// Create schedule pdf for the given spectacleday
		/// </summary>
		/// <param name="spectacleDay"></param>
		/// <returns>byte representation of the file</returns>
		byte[] CreatePdfScheduleForSpectacleDay(Spectacleday spectacleDay);

		/// <summary>
		/// Try to log a user in by the given credentials
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		bool Login(string username, string password);

		/// <summary>
		/// Get performance by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Performance GetPerformance(int id);

		/// <summary>
		/// Ger all performances of the given artist
		/// </summary>
		/// <param name="artist"></param>
		/// <returns></returns>
		IEnumerable<Performance> GetPerformancesForArtist(Artist artist);

		/// <summary>
		/// Get timeslot for the given performance
		/// </summary>
		/// <param name="performance"></param>
		/// <returns></returns>
		TimeSlot GetTimeSlotForPerformance(Performance performance);

		/// <summary>
		/// get timeslots for the given performances
		/// </summary>
		/// <param name="performances"></param>
		/// <returns></returns>
		IEnumerable<TimeSlot> GetTimeSlotsForPerformances(IEnumerable<Performance> performances);

		/// <summary>
		/// Get artists for the given performances
		/// </summary>
		/// <param name="performances"></param>
		/// <returns></returns>
		IEnumerable<Artist> GetArtistsForPerformances(IEnumerable<Performance> performances);

		/// <summary>
		/// Get venues for the given performances
		/// </summary>
		/// <param name="performances"></param>
		/// <returns></returns>
		IEnumerable<Venue> GetVenuesForPerformances(IEnumerable<Performance> performances);

		/// <summary>
		/// Get spectacledays for the given performancedays
		/// </summary>
		/// <param name="performances"></param>
		/// <returns></returns>
		IEnumerable<Spectacleday> GetSpectacleDaysForPerformances(IEnumerable<Performance> performances);

		/// <summary>
		/// Get spectalceday timeslots for the given performances
		/// </summary>
		/// <param name="performances"></param>
		/// <returns></returns>
		IEnumerable<SpectacledayTimeSlot> GetSpectacledayTimeSlotsForPerformances(IEnumerable<Performance> performances);

		/// <summary>
		///  Get performances of the given venue
		/// </summary>
		/// <param name="venue"></param>
		/// <returns></returns>
		IEnumerable<Performance> GetPerformancesForVenue(Venue venue);
	}
}