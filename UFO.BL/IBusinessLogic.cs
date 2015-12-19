﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.BL {

	public interface IBusinessLogic {

		IEnumerable<Area> GetAreas();

		IEnumerable<Category> GetCategories();

		IEnumerable<Venue> GetVenuesForArea(Area area);

		Venue CreateVenue(Venue venue);

		Venue UpdateVenue(Venue venue);

		IEnumerable<Artist> GetArtistsForCategory(Category category);

		Artist UpdateArtist(Artist artist);

		Artist CreateArtist(Artist artist);

		void DeleteArtist(Artist artist);

		IEnumerable<TimeSlot> GetTimeSlots();

		IEnumerable<Country> GetCountries();

		Category GetCategoryById(Category category);

		Country GetCountryById(Country country);

		Artist GetArtistById(Artist artist);

		IEnumerable<Spectacleday> GetSpectacleDays();

		IEnumerable<SpectacledayTimeSlot> GetSpectacleDayTimeSlotsForSpectacleDay(Spectacleday day);

		IEnumerable<Performance> GetPerformanesForSpetacleDay(Spectacleday day);

		void MailPerformanceChangesToArtists(IEnumerable<Artist> artists, IEnumerable<Performance> performances);
	}
}