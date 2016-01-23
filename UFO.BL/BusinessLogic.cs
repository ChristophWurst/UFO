using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using UFO.BL.execptions;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace UFO.BL {

	/// <summary>
	/// Implementation of the IBusinessLogic interface
	///
	/// Use the BusinessLogicFactory to get an instance of this class
	/// </summary>
	internal class BusinessLogic : ABusinessLogic {
		private const string SALT = "H4g3nb3Rg";

		private DALFactory dalFactory;
		private IDatabase db;
		private IMailService ms;
		private IPdfMaker pdf;
		private string pdfPath;
		private string pdfName;

		public BusinessLogic(DALFactory dalFactory, IMailService ms, IPdfMaker pdf) {
			this.db = dalFactory.CreateDatabase();
			this.dalFactory = dalFactory;
			this.ms = ms;
			this.pdf = pdf;
		}

		public BusinessLogic(DALFactory dalFactory) {
			this.db = dalFactory.CreateDatabase();
			this.dalFactory = dalFactory;
			var appSettings = ConfigurationManager.AppSettings;
			var smtpServer = appSettings["smtpServer"];
			var mailAddress = new MailAddress(appSettings["mailAddress"], appSettings["sender"]);
			var user = appSettings["user"];
			var pwd = appSettings["pwd"];
			var port = int.Parse(appSettings["port"]);
			ms = new MailService(smtpServer, port, user, pwd, mailAddress);
			pdfPath = appSettings["pdfPath"];
			pdfName = appSettings["pdfName"];
			pdf = new PdfMaker(pdfPath, pdfName);
		}

		public override Artist CreateArtist(Artist artist) {
			try {
				return dalFactory.CreateArtistDAO(db).Create(artist);
			} catch {
				throw new BusinessLogicException($"Could not create Artist {artist.Name}.");
			}
		}

		public override void DeleteArtist(Artist artist) {
			try {
				dalFactory.CreateArtistDAO(db).Delete(artist);
				var performanceDAO = dalFactory.CreatePerformanceDAO(db);
				var performancesOfArtist = performanceDAO.GetForArtist(artist);
				var futureSpectacleDays = dalFactory.CreateSpectacledayDAO(db).GetAll().Where(day => day.Day >= DateTime.Today);
				var futureTimeSlots = dalFactory.CreateTimeSlotDAO(db).GetAll().Where(timeslot => timeslot.Start >= DateTime.Now.Hour);
				var futureSpectacleDayTimeslots = dalFactory.CreateSpectacledayTimeSlotDAO(db).GetAll().Where(t => futureSpectacleDays.Select(d => d.Id).Contains(t.SpectacledayId) && futureTimeSlots.Select(o => o.Id).Contains(t.TimeSlotId));
				var performanceToDelete = performancesOfArtist.Where(performance => futureSpectacleDayTimeslots.Select(fsdt => fsdt.Id).Contains(performance.SpectacledayTimeSlot));
				using (new TransactionScope()) {
					performanceToDelete.ToList().ForEach(performance => performanceDAO.Delete(performance));
				}
			} catch (EntityNotFoundException) {
				throw new BusinessLogicException($"Could not delete Artist {artist.Name}, Artist {artist.Name} not found.");
			} catch {
				throw new BusinessLogicException($"Could not delete Artist {artist.Name}.");
			}
		}

		public override IEnumerable<Area> GetAreas() {
			return dalFactory.CreateAreaDAO(db).GetAll();
		}

		public override IEnumerable<Artist> GetArtists() {
			return dalFactory.CreateArtistDAO(db).GetAll();
		}

		public override IEnumerable<Artist> GetArtistsForCategory(Category category) {
			return dalFactory.CreateArtistDAO(db).GetForCategory(category);
		}

		public override IEnumerable<Category> GetCategories() {
			return dalFactory.CreateCategoryDAO(db).GetAll();
		}

		public override IEnumerable<Venue> GetVenuesForArea(Area area) {
			return dalFactory.CreateVenueDAO(db).GetForArea(area);
		}

		public override Venue CreateVenue(Venue venue) {
			return dalFactory.CreateVenueDAO(db).Create(venue);
		}

		public override Venue UpdateVenue(Venue venue) {
			return dalFactory.CreateVenueDAO(db).Update(venue);
		}

		public override Artist UpdateArtist(Artist artist) {
			try {
				return dalFactory.CreateArtistDAO(db).Update(artist);
			} catch {
				throw new BusinessLogicException($"Could not update Artist {artist.Name}.");
			}
		}

		public override IEnumerable<TimeSlot> GetTimeSlots() {
			return dalFactory.CreateTimeSlotDAO(db).GetAll();
		}

		public override IEnumerable<Country> GetCountries() {
			return dalFactory.CreateCountryDAO(db).GetAll();
		}

		public override Category GetCategoryById(Category category) {
			return dalFactory.CreateCategoryDAO(db).GetById(category.Id);
		}

		public override Country GetCountryById(Country country) {
			return dalFactory.CreateCountryDAO(db).GetById(country.Id);
		}

		public override Artist GetArtistById(Artist artist) {
			return dalFactory.CreateArtistDAO(db).GetById(artist.Id);
		}

		public override IEnumerable<Spectacleday> GetSpectacleDays() {
			return dalFactory.CreateSpectacledayDAO(db).GetAll();
		}

		public override IEnumerable<SpectacledayTimeSlot> GetSpectacleDayTimeSlotsForSpectacleDay(Spectacleday day) {
			var spectacleTS = dalFactory.CreateSpectacledayTimeSlotDAO(db).GetForSpectacleDay(day);
			var timeSlots = dalFactory.CreateTimeSlotDAO(db).GetAll();
			foreach (var ts in spectacleTS) {
				ts.TimeSlot = timeSlots.Where(x => x.Id == ts.TimeSlotId).First();
			}
			return spectacleTS;
		}

		public override IEnumerable<Performance> GetPerformanesForSpetacleDay(Spectacleday day) {
			return dalFactory.CreatePerformanceDAO(db).GetForSpectacleDay(day);
		}

		public override void UpdatePerformances(Spectacleday spectacleDay, IEnumerable<Performance> performances) {
			performances = performances.Where(p => p.Id != default(int) || p.ArtistId != default(int));
			if (performances.Count() <= 0) return;
			var oldPerformances = dalFactory.CreatePerformanceDAO(db).GetForSpectacleDay(spectacleDay);
			var performancesToDelete = performances.Where(p => p.ArtistId == default(int));
			var performancesToCreate = performances.Where(p => p.Id == default(int));
			var performancesToUpdate = performances.Where(p => p.Id != default(int) && p.ArtistId != default(int));
			var tmpPerformances = MergePerformances(oldPerformances, performancesToDelete, performancesToCreate, performancesToUpdate);

			var valid = ValidateArtist(tmpPerformances);
			if (valid != null) throw new BusinessLogicException($"Artist {dalFactory.CreateArtistDAO(db).GetById(valid.ArtistId).Name} has already a performance scheduled.");
			valid = ValidateVenue(tmpPerformances);
			if (valid != null) throw new BusinessLogicException($"Venue {dalFactory.CreateArtistDAO(db).GetById(valid.ArtistId).Name} already scheduled.");
			valid = ValidatePause(tmpPerformances);
			if (valid != null) throw new BusinessLogicException($"Artist {dalFactory.CreateArtistDAO(db).GetById(valid.ArtistId).Name} has no time for a pause.");

			Update(performancesToDelete, performancesToCreate, performancesToUpdate);
			var artists = GetArtistsToNotify(oldPerformances, performancesToDelete, performancesToCreate, performancesToUpdate);
			MailPerformanceChangesToArtists(artists, performances, spectacleDay);
			performancesToDelete.ToList().ForEach(p => p.Id = default(int));
		}

		private IEnumerable<Artist> GetArtistsToNotify(IEnumerable<Performance> oldPerformances, IEnumerable<Performance> performancesToDelete, IEnumerable<Performance> performancesToCreate, IEnumerable<Performance> performancesToUpdate) {
			var artistIds = new HashSet<int>();
			oldPerformances.Where(p => performancesToDelete.Where(d => d.Id == p.Id).Any() ||
									   performancesToUpdate.Where(d => d.Id == p.Id && d.ArtistId != p.ArtistId ||
																	   d.Id == p.Id && d.VenueId != p.VenueId).Any()).ToList().ForEach(p => artistIds.Add(p.ArtistId));
			performancesToCreate.ToList().ForEach(p => artistIds.Add(p.ArtistId));
			performancesToUpdate.ToList().ForEach(p => artistIds.Add(p.ArtistId));
			var dao = dalFactory.CreateArtistDAO(db);
			var artists = new List<Artist>();
			artistIds.ToList().ForEach(a => artists.Add(dao.GetById(a)));
			return artists;
		}

		private void Update(IEnumerable<Performance> performancesToDelete, IEnumerable<Performance> performancesToCreate, IEnumerable<Performance> performancesToUpdate) {
			using (TransactionScope trans = new TransactionScope()) {
				try {
					var dao = dalFactory.CreatePerformanceDAO(db);
					performancesToDelete.ToList().ForEach(p => dao.Delete(p));
					performancesToCreate.ToList().ForEach(p => dao.Create(p));
					performancesToUpdate.ToList().ForEach(p => dao.Update(p));
					trans.Complete();
				} catch (Exception e) {
					throw; // new BusinessLogicException($"Could not Update DataBase.");
				}
			}
		}

		private Performance ValidatePause(IEnumerable<Performance> tmpPerformances) {
			bool valid = true;
			var it = tmpPerformances.GetEnumerator();
			while (valid && it.MoveNext()) {
				valid = !tmpPerformances.Where(s => it.Current.Id != s.Id &&
													it.Current.ArtistId == s.ArtistId &&
													(it.Current.SpectacledayTimeSlot == s.SpectacledayTimeSlot + 1 ||
													it.Current.SpectacledayTimeSlot == s.SpectacledayTimeSlot - 1)).Any();
			}
			return valid ? null : it.Current;
		}

		private Performance ValidateVenue(IEnumerable<Performance> tmpPerformances) {
			bool valid = true;
			var it = tmpPerformances.GetEnumerator();
			while (valid && it.MoveNext()) {
				valid = !tmpPerformances.Where(s => it.Current.Id != s.Id &&
													it.Current.VenueId == s.VenueId &&
													it.Current.SpectacledayTimeSlot == s.SpectacledayTimeSlot).Any();
			}
			return valid ? null : it.Current;
		}

		private Performance ValidateArtist(IEnumerable<Performance> tmpPerformances) {
			bool valid = true;
			var it = tmpPerformances.GetEnumerator();
			while (valid && it.MoveNext()) {
				valid = !tmpPerformances.Where(s => it.Current.Id != s.Id &&
													it.Current.ArtistId == s.ArtistId &&
													it.Current.SpectacledayTimeSlot == s.SpectacledayTimeSlot).Any();
			}
			return valid ? null : it.Current;
		}

		private IEnumerable<Performance> MergePerformances(IEnumerable<Performance> oldPerformances, IEnumerable<Performance> performancesToDelete, IEnumerable<Performance> performancesToCreate, IEnumerable<Performance> performancesToUpdate) {
			List<Performance> dummy = oldPerformances.Where(old => !performancesToDelete.Where(p => p.Id == old.Id).Any()).ToList();
			dummy = dummy.Where(d => !performancesToUpdate.Where(p => p.Id == d.Id).Any()).ToList();
			dummy.AddRange(performancesToUpdate);
			dummy.AddRange(performancesToCreate);
			return dummy;
		}

		public void MailPerformanceChangesToArtists(IEnumerable<Artist> artists, IEnumerable<Performance> performances, Spectacleday day) {
			var file = CreatePdfScheduleForSpectacleDay(day);
			ms.MailToArtists(artists, day, file);
		}

		public override IEnumerable<Venue> GetVenues() {
			return dalFactory.CreateVenueDAO(db).GetAll();
		}

		public override byte[] CreatePdfScheduleForSpectacleDay(Spectacleday spectacleDay) {
			return pdf.MakeSpectacleSchedule(GetSpectacleDayTimeSlotsForSpectacleDay(spectacleDay), GetPerformanesForSpetacleDay(spectacleDay), GetAreas(), GetVenues(), GetTimeSlots(), GetArtists());
		}

		public override bool Login(string username, string password) {
			try {
				User user = dalFactory.CreateUserDAO(db).GetByName(username);
				SHA1 sha = new SHA1CryptoServiceProvider();
				var shaPwd = sha.ComputeHash(Encoding.ASCII.GetBytes(SALT + password + username));
				var sb = new StringBuilder();

				foreach (byte b in shaPwd) {
					sb.AppendFormat("{0:x2}", b);
				}

				password = sb.ToString();
				return password == user.Password;
			} catch (EntityNotFoundException) {
				return false;
			}
		}

		public override Performance GetPerformance(int id) {
			return dalFactory.CreatePerformanceDAO(db).GetById(id);
		}

		public override IEnumerable<Performance> GetPerformancesForArtist(Artist artist) {
			return dalFactory.CreatePerformanceDAO(db).GetForArtist(artist);
		}

		public override TimeSlot GetTimeSlotForPerformance(Performance performance) {
			return dalFactory.CreateTimeSlotDAO(db).GetForPerformance(performance);
		}

		public override IEnumerable<TimeSlot> GetTimeSlotsForPerformances(IEnumerable<Performance> performances) {
			return dalFactory.CreateTimeSlotDAO(db).GetForPerformances(performances);
		}

		public override IEnumerable<Artist> GetArtistsForPerformances(IEnumerable<Performance> performances) {
			return dalFactory.CreateArtistDAO(db).GetForPerformances(performances);
		}

		public override IEnumerable<Venue> GetVenuesForPerformances(IEnumerable<Performance> performances) {
			return dalFactory.CreateVenueDAO(db).GetForPerformances(performances);
		}

		public override IEnumerable<Spectacleday> GetSpectacleDaysForPerformances(IEnumerable<Performance> performances) {
			return dalFactory.CreateSpectacledayDAO(db).GetForPerformances(performances);
		}

		public override IEnumerable<SpectacledayTimeSlot> GetSpectacledayTimeSlotsForPerformances(IEnumerable<Performance> performances) {
			return dalFactory.CreateSpectacledayTimeSlotDAO(db).GetForPerformances(performances);
		}

		public override IEnumerable<Performance> GetPerformancesForVenue(Venue venue) {
			return dalFactory.CreatePerformanceDAO(db).GetForVenue(venue);
		}
	}
}