using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain = UFO.DomainClasses;
using WS = UFO.BL.UFOService;

namespace UFO.BL {

	internal class WebServiceBusinessLogic : IBusinessLogic {
		private WS.UFO proxy;

		public WebServiceBusinessLogic() {
			proxy = new WS.UFO();
		}

		static WebServiceBusinessLogic() {
			Mapper.CreateMap<WS.Artist, Domain.Artist>();
			Mapper.CreateMap<Domain.Artist, WS.Artist>();
			Mapper.CreateMap<Domain.Spectacleday, WS.Spectacleday>();
			Mapper.CreateMap<WS.Venue, Domain.Venue>();
			Mapper.CreateMap<Domain.Venue, WS.Venue>();
			Mapper.CreateMap<WS.Area, Domain.Area>();
			Mapper.CreateMap<Domain.Area, WS.Area>();
			Mapper.CreateMap<WS.Artist, Domain.Artist>();
			Mapper.CreateMap<WS.Performance, Domain.Performance>();
			Mapper.CreateMap<WS.Spectacleday, Domain.Spectacleday>();
			Mapper.CreateMap<Domain.Spectacleday, WS.Spectacleday>();
			Mapper.CreateMap<Domain.SpectacledayTimeSlot, WS.SpectacledayTimeSlot>();
			Mapper.CreateMap<WS.TimeSlot, Domain.TimeSlot>();
		}

		public Domain.Artist CreateArtist(Domain.Artist artist) {
			return Mapper.Map<WS.Artist, Domain.Artist>(proxy.CreateArtist(Mapper.Map<Domain.Artist, WS.Artist>(artist)));
		}

		public void CreatePdfScheduleForSpectacleDay(Domain.Spectacleday spectacleDay) {
			proxy.CreatePdfScheduleForSpectacleDay(Mapper.Map<Domain.Spectacleday, WS.Spectacleday>(spectacleDay));
		}

		public Domain.Venue CreateVenue(Domain.Venue venue) {
			return Mapper.Map<WS.Venue, Domain.Venue>(proxy.CreateVenue(Mapper.Map<Domain.Venue, WS.Venue>(venue)));
		}

		public void DeleteArtist(Domain.Artist artist) {
			throw new NotImplementedException();
		}

		public IEnumerable<Domain.Area> GetAreas() {
			return Mapper.Map<IEnumerable<WS.Area>, IEnumerable<Domain.Area>>(proxy.GetAreas());
		}

		public Domain.Artist GetArtistById(Domain.Artist artist) {
			throw new NotImplementedException();
		}

		public IEnumerable<Domain.Artist> GetArtists() {
			return Mapper.Map<IEnumerable<WS.Artist>, IEnumerable<Domain.Artist>>(proxy.GetArtists());
		}

		public IEnumerable<Domain.Artist> GetArtistsForCategory(Domain.Category category) {
			throw new NotImplementedException();
		}

		public IEnumerable<Domain.Category> GetCategories() {
			throw new NotImplementedException();
		}

		public Domain.Category GetCategoryById(Domain.Category category) {
			throw new NotImplementedException();
		}

		public IEnumerable<Domain.Country> GetCountries() {
			throw new NotImplementedException();
		}

		public Domain.Country GetCountryById(Domain.Country country) {
			throw new NotImplementedException();
		}

		public IEnumerable<Domain.Performance> GetPerformanesForSpetacleDay(Domain.Spectacleday day) {
			var d = Mapper.Map<Domain.Spectacleday, WS.Spectacleday>(day);
			return Mapper.Map<IEnumerable<WS.Performance>, IEnumerable<Domain.Performance>>(proxy.GetPerformanesForSpetacleDay(d));
		}

		public IEnumerable<Domain.Spectacleday> GetSpectacleDays() {
			return Mapper.Map<IEnumerable<WS.Spectacleday>, IEnumerable<Domain.Spectacleday>>(proxy.GetSpectacleDays());
		}

		public IEnumerable<Domain.SpectacledayTimeSlot> GetSpectacleDayTimeSlotsForSpectacleDay(Domain.Spectacleday day) {
			var d = Mapper.Map<Domain.Spectacleday, WS.Spectacleday>(day);
			return Mapper.Map<IEnumerable<WS.SpectacledayTimeSlot>, IEnumerable<Domain.SpectacledayTimeSlot>>(proxy.GetSpectacleDayTimeSlotsForSpectacleDay(d));
		}

		public IEnumerable<Domain.TimeSlot> GetTimeSlots() {
			return Mapper.Map<IEnumerable<WS.TimeSlot>, IEnumerable<Domain.TimeSlot>>(proxy.GetTimeSlots());
		}

		public IEnumerable<Domain.Venue> GetVenues() {
			throw new NotImplementedException();
		}

		public IEnumerable<Domain.Venue> GetVenuesForArea(Domain.Area area) {
			var a = Mapper.Map<Domain.Area, WS.Area>(area);
			return Mapper.Map<IEnumerable<WS.Venue>, IEnumerable<Domain.Venue>>(proxy.GetVenuesForArea(a));
		}

		public void Login(string username, string password) {
			proxy.Login(username, password);
		}

		public Domain.Artist UpdateArtist(Domain.Artist artist) {
			throw new NotImplementedException();
		}

		public void UpdatePerformances(Domain.Spectacleday spectacleDay, IEnumerable<Domain.Performance> performances) {
			throw new NotImplementedException();
		}

		public Domain.Venue UpdateVenue(Domain.Venue venue) {
			throw new NotImplementedException();
		}
	}
}