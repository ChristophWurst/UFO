using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.BL {

	public interface IBusinessLogicAsync {

		Task<IEnumerable<Area>> GetAreasAsync();

		Task<IEnumerable<Category>> GetCategoriesAsync();

		Task<IEnumerable<Venue>> GetVenuesForAreaAsync(Area area);

		Task<Venue> CreateVenueAsync(Venue venue);

		Task<Venue> UpdateVenueAsync(Venue venue);

		Task<IEnumerable<Artist>> GetArtistsAsync();

		Task<IEnumerable<Artist>> GetArtistsForCategoryAsync(Category category);

		Task<Artist> UpdateArtistAsync(Artist artist);

		Task<Artist> CreateArtistAsync(Artist artist);

		void DeleteArtistAsync(Artist artist);

		Task<IEnumerable<TimeSlot>> GetTimeSlotsAsync();

		Task<IEnumerable<Country>> GetCountriesAsync();

		Task<Category> GetCategoryByIdAsync(Category category);

		Task<Country> GetCountryByIdAsync(Country country);

		Task<Artist> GetArtistByIdAsync(Artist artist);

		Task<IEnumerable<Spectacleday>> GetSpectacleDaysAsync();

		Task<IEnumerable<SpectacledayTimeSlot>> GetSpectacleDayTimeSlotsForSpectacleDayAsync(Spectacleday day);

		Task<IEnumerable<Performance>> GetPerformanesForSpetacleDayAsync(Spectacleday day);

		void UpdatePerformancesAsync(Spectacleday spectacleDay, IEnumerable<Performance> performances);

		Task<IEnumerable<Venue>> GetVenuesAsync();

		void CreatePdfScheduleForSpectacleDayAsync(Spectacleday spectacleDay);

		Task<bool> LoginAsync(string username, string password);

		Task<IEnumerable<Performance>> GetPerformancesForArtistAsync(Artist artist);

		Task<TimeSlot> GetTimeSlotForPerformanceAsync(Performance performance);

		Task<IEnumerable<TimeSlot>> GetTimeSlotsForPerformances(IEnumerable<Performance> performances);

		Task<IEnumerable<Artist>> GetArtistsForPerformances(IEnumerable<Performance> performances);

		Task<IEnumerable<Venue>> GetVenuesForPerformances(IEnumerable<Performance> performances);
	}
}