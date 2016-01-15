using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain = UFO.DomainClasses;
using WS = UFO.BL.UFOService;

namespace UFO.BL {

	internal class WebServiceBusinessLogic : AbstractBusinessLogic {
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
			Mapper.CreateMap<Domain.Performance, WS.Performance>();
			Mapper.CreateMap<WS.Spectacleday, Domain.Spectacleday>();
			Mapper.CreateMap<Domain.Spectacleday, WS.Spectacleday>();
			Mapper.CreateMap<Domain.SpectacledayTimeSlot, WS.SpectacledayTimeSlot>();
			Mapper.CreateMap<WS.SpectacledayTimeSlot, Domain.SpectacledayTimeSlot>();
			Mapper.CreateMap<WS.TimeSlot, Domain.TimeSlot>();
			Mapper.CreateMap<WS.Category, Domain.Category>();
			Mapper.CreateMap<Domain.Category, WS.Category>();
			Mapper.CreateMap<WS.Country, Domain.Country>();
			Mapper.CreateMap<Domain.Country, WS.Country>();
		}

		public override Domain.Artist CreateArtist(Domain.Artist artist) {
			return Mapper.Map<WS.Artist, Domain.Artist>(proxy.CreateArtist(Mapper.Map<Domain.Artist, WS.Artist>(artist)));
		}

		public override void CreatePdfScheduleForSpectacleDay(Domain.Spectacleday spectacleDay) {
			proxy.CreatePdfScheduleForSpectacleDay(Mapper.Map<Domain.Spectacleday, WS.Spectacleday>(spectacleDay));
		}

		public override Domain.Venue CreateVenue(Domain.Venue venue) {
			return Mapper.Map<WS.Venue, Domain.Venue>(proxy.CreateVenue(Mapper.Map<Domain.Venue, WS.Venue>(venue)));
		}

		public override void DeleteArtist(Domain.Artist artist) {
			proxy.DeleteArtist(Mapper.Map<Domain.Artist, WS.Artist>(artist));
		}

		public override IEnumerable<Domain.Area> GetAreas() {
			return Mapper.Map<IEnumerable<WS.Area>, IEnumerable<Domain.Area>>(proxy.GetAreas());
		}

		public override Domain.Artist GetArtistById(Domain.Artist artist) {
			return Mapper.Map<WS.Artist, Domain.Artist>(proxy.GetArtistById(Mapper.Map<Domain.Artist, WS.Artist>(artist)));
		}

		public override IEnumerable<Domain.Artist> GetArtists() {
			return Mapper.Map<IEnumerable<WS.Artist>, IEnumerable<Domain.Artist>>(proxy.GetArtists());
		}

		public override IEnumerable<Domain.Artist> GetArtistsForCategory(Domain.Category category) {
			return Mapper.Map<IEnumerable<WS.Artist>, IEnumerable<Domain.Artist>>(proxy.GetArtistsForCategory(Mapper.Map<Domain.Category, WS.Category>(category)));
		}

		public override IEnumerable<Domain.Category> GetCategories() {
			return Mapper.Map<IEnumerable<WS.Category>, IEnumerable<Domain.Category>>(proxy.GetCategories());
		}

		public override Domain.Category GetCategoryById(Domain.Category category) {
			return Mapper.Map<WS.Category, Domain.Category>(proxy.GetCategoryById(Mapper.Map<Domain.Category, WS.Category>(category)));
		}

		public override IEnumerable<Domain.Country> GetCountries() {
			return Mapper.Map<IEnumerable<WS.Country>, IEnumerable<Domain.Country>>(proxy.GetCountries());
		}

		public override Domain.Country GetCountryById(Domain.Country country) {
			return Mapper.Map<WS.Country, Domain.Country>(proxy.GetCountryById(Mapper.Map<Domain.Country, WS.Country>(country)));
		}

		public override IEnumerable<Domain.Performance> GetPerformanesForSpetacleDay(Domain.Spectacleday day) {
			var d = Mapper.Map<Domain.Spectacleday, WS.Spectacleday>(day);
			return Mapper.Map<IEnumerable<WS.Performance>, IEnumerable<Domain.Performance>>(proxy.GetPerformanesForSpetacleDay(d));
		}

		public override IEnumerable<Domain.Spectacleday> GetSpectacleDays() {
			return Mapper.Map<IEnumerable<WS.Spectacleday>, IEnumerable<Domain.Spectacleday>>(proxy.GetSpectacleDays());
		}

		public override IEnumerable<Domain.SpectacledayTimeSlot> GetSpectacleDayTimeSlotsForSpectacleDay(Domain.Spectacleday day) {
			var d = Mapper.Map<Domain.Spectacleday, WS.Spectacleday>(day);
			return Mapper.Map<IEnumerable<WS.SpectacledayTimeSlot>, IEnumerable<Domain.SpectacledayTimeSlot>>(proxy.GetSpectacleDayTimeSlotsForSpectacleDay(Mapper.Map<Domain.Spectacleday, WS.Spectacleday>(day)));
		}

		public override IEnumerable<Domain.TimeSlot> GetTimeSlots() {
			return Mapper.Map<IEnumerable<WS.TimeSlot>, IEnumerable<Domain.TimeSlot>>(proxy.GetTimeSlots());
		}

		public override IEnumerable<Domain.Venue> GetVenues() {
			return Mapper.Map<IEnumerable<WS.Venue>, IEnumerable<Domain.Venue>>(proxy.GetVenues());
		}

		public override IEnumerable<Domain.Venue> GetVenuesForArea(Domain.Area area) {
			var a = Mapper.Map<Domain.Area, WS.Area>(area);
			return Mapper.Map<IEnumerable<WS.Venue>, IEnumerable<Domain.Venue>>(proxy.GetVenuesForArea(a));
		}

		public override bool Login(string username, string password) {
			return proxy.Login(username, password);
		}

		public override Domain.Artist UpdateArtist(Domain.Artist artist) {
			return Mapper.Map<WS.Artist, Domain.Artist>(proxy.UpdateArtist(Mapper.Map<Domain.Artist, WS.Artist>(artist)));
		}

		public override void UpdatePerformances(Domain.Spectacleday spectacleDay, IEnumerable<Domain.Performance> performances) {
			proxy.UpdatePerformances(Mapper.Map<Domain.Spectacleday, WS.Spectacleday>(spectacleDay), Mapper.Map<IEnumerable<Domain.Performance>, IEnumerable<WS.Performance>>(performances).ToArray());
		}

		public override Domain.Venue UpdateVenue(Domain.Venue venue) {
			return Mapper.Map<WS.Venue, Domain.Venue>(proxy.UpdateVenue(Mapper.Map<Domain.Venue, WS.Venue>(venue)));
		}

		public override IEnumerable<Domain.Performance> GetPerformancesForArtist(Domain.Artist artist) {
			return Mapper.Map<IEnumerable<WS.Performance>, IEnumerable<Domain.Performance>>(proxy.GetPerformancesForArtist(Mapper.Map<Domain.Artist, WS.Artist>(artist)));
		}

		public override Domain.TimeSlot GetTimeSlotForPerformance(Domain.Performance performance) {
			return Mapper.Map<WS.TimeSlot, Domain.TimeSlot>(proxy.GetTimeSlotForPerformance(Mapper.Map<Domain.Performance, WS.Performance>(performance)));
		}

		public override IEnumerable<Domain.TimeSlot> GetTimeSlotsForPerformances(IEnumerable<Domain.Performance> performances) {
			return Mapper.Map<IEnumerable<WS.TimeSlot>, IEnumerable<Domain.TimeSlot>>(proxy.GetTimeSlotsForPerformances(Mapper.Map<IEnumerable<Domain.Performance>, IEnumerable<WS.Performance>>(performances).ToArray()));
		}

		public override IEnumerable<Domain.Artist> GetArtistsForPerformances(IEnumerable<Domain.Performance> performances) {
			return Mapper.Map<IEnumerable<WS.Artist>, IEnumerable<Domain.Artist>>(proxy.GetArtistsForPerformances(Mapper.Map<IEnumerable<Domain.Performance>, IEnumerable<WS.Performance>>(performances).ToArray()));
		}

		public override IEnumerable<Domain.Venue> GetVenuesForPerformances(IEnumerable<Domain.Performance> performances) {
			return Mapper.Map<IEnumerable<WS.Venue>, IEnumerable<Domain.Venue>>(proxy.GetVenuesForPerformances(Mapper.Map<IEnumerable<Domain.Performance>, IEnumerable<WS.Performance>>(performances).ToArray()));
		}
	}
}