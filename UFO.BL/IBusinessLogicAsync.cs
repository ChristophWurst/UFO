using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.BL {

	/// <summary>
	/// Asynchronous business logic interface
	/// </summary>
	public interface IBusinessLogicAsync {

		/// <summary>
		/// Get all areas
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<Area>> GetAreasAsync();

		/// <summary>
		/// Get all categories
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<Category>> GetCategoriesAsync();

		/// <summary>
		/// Get venues for the given area
		/// </summary>
		/// <param name="area"></param>
		/// <returns></returns>
		Task<IEnumerable<Venue>> GetVenuesForAreaAsync(Area area);

		/// <summary>
		/// Create a new venue
		/// </summary>
		/// <param name="venue"></param>
		/// <returns></returns>
		Task<Venue> CreateVenueAsync(Venue venue);

		/// <summary>
		/// Update an existing venue
		/// </summary>
		/// <param name="venue"></param>
		/// <returns></returns>
		Task<Venue> UpdateVenueAsync(Venue venue);

		/// <summary>
		/// Get all artists
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<Artist>> GetArtistsAsync();

		/// <summary>
		/// Get artists for the given category
		/// </summary>
		/// <param name="category"></param>
		/// <returns></returns>
		Task<IEnumerable<Artist>> GetArtistsForCategoryAsync(Category category);

		/// <summary>
		/// Update an existing artist
		/// </summary>
		/// <param name="artist"></param>
		/// <returns></returns>
		Task<Artist> UpdateArtistAsync(Artist artist);

		/// <summary>
		/// Creat a new artist
		/// </summary>
		/// <param name="artist"></param>
		/// <returns></returns>
		Task<Artist> CreateArtistAsync(Artist artist);

		/// <summary>
		/// Delete an artist
		/// </summary>
		/// <param name="artist"></param>
		void DeleteArtistAsync(Artist artist);

		/// <summary>
		/// Get all timeslots
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<TimeSlot>> GetTimeSlotsAsync();

		/// <summary>
		/// Get all countries
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<Country>> GetCountriesAsync();

		/// <summary>
		/// Get category by id
		/// </summary>
		/// <param name="category"></param>
		/// <returns></returns>
		Task<Category> GetCategoryByIdAsync(Category category);

		/// <summary>
		/// Get country by id
		/// </summary>
		/// <param name="country"></param>
		/// <returns></returns>
		Task<Country> GetCountryByIdAsync(Country country);

		/// <summary>
		/// Get artist by id
		/// </summary>
		/// <param name="artist"></param>
		/// <returns></returns>
		Task<Artist> GetArtistByIdAsync(Artist artist);

		/// <summary>
		/// Get all spectacledays
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<Spectacleday>> GetSpectacleDaysAsync();

		/// <summary>
		/// Get spectacledaytimeslots for the given spectacleday
		/// </summary>
		/// <param name="day"></param>
		/// <returns></returns>
		Task<IEnumerable<SpectacledayTimeSlot>> GetSpectacleDayTimeSlotsForSpectacleDayAsync(Spectacleday day);

		/// <summary>
		/// Get performacnes for the given spectacleday
		/// </summary>
		/// <param name="day"></param>
		/// <returns></returns>
		Task<IEnumerable<Performance>> GetPerformanesForSpetacleDayAsync(Spectacleday day);

		/// <summary>
		/// Update the given performances
		/// </summary>
		/// <param name="spectacleDay"></param>
		/// <param name="performances"></param>
		void UpdatePerformancesAsync(Spectacleday spectacleDay, IEnumerable<Performance> performances);

		/// <summary>
		/// Get all venues
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<Venue>> GetVenuesAsync();

		/// <summary>
		/// Create schedule pdf for the given spectacleday
		/// </summary>
		/// <param name="spectacleDay"></param>
		/// <returns>byte representation of the file</returns>
		Task<byte[]> CreatePdfScheduleForSpectacleDayAsync(Spectacleday spectacleDay);

		/// <summary>
		/// Try to log a user in by the given credentials
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		Task<bool> LoginAsync(string username, string password);

		/// <summary>
		/// Ger all performances of the given artist
		/// </summary>
		/// <param name="artist"></param>
		/// <returns></returns>
		Task<IEnumerable<Performance>> GetPerformancesForArtistAsync(Artist artist);

		/// <summary>
		/// Get timeslot for the given performance
		/// </summary>
		/// <param name="performance"></param>
		/// <returns></returns>
		Task<TimeSlot> GetTimeSlotForPerformanceAsync(Performance performance);

		/// <summary>
		/// get timeslots for the given performances
		/// </summary>
		/// <param name="performances"></param>
		/// <returns></returns>
		Task<IEnumerable<TimeSlot>> GetTimeSlotsForPerformances(IEnumerable<Performance> performances);

		/// <summary>
		/// Get artists for the given performances
		/// </summary>
		/// <param name="performances"></param>
		/// <returns></returns>
		Task<IEnumerable<Artist>> GetArtistsForPerformances(IEnumerable<Performance> performances);

		/// <summary>
		/// Get venues for the given performances
		/// </summary>
		/// <param name="performances"></param>
		/// <returns></returns>
		Task<IEnumerable<Venue>> GetVenuesForPerformances(IEnumerable<Performance> performances);

		/// <summary>
		/// Get spectacledays for the given performancedays
		/// </summary>
		/// <param name="performances"></param>
		/// <returns></returns>
		Task<IEnumerable<Spectacleday>> GetSpectacleDaysForPerformances(IEnumerable<Performance> performances);

		/// <summary>
		/// Get spectalceday timeslots for the given performances
		/// </summary>
		/// <param name="performances"></param>
		/// <returns></returns>
		Task<IEnumerable<SpectacledayTimeSlot>> GetSpectacledayTimeSlotsForPerformances(IEnumerable<Performance> performances);

		/// <summary>
		///  Get performances of the given venue
		/// </summary>
		/// <param name="venue"></param>
		/// <returns></returns>
		Task<IEnumerable<Performance>> GetPerformancesForVenue(Venue venue);
	}
}