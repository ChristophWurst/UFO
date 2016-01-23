﻿using System;
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
			if (performances.Count() == 0) {
				return;
			}

			var performanceDAO = dalFactory.CreatePerformanceDAO(db);
			ValidatePerformanceChanges(performances, spectacleDay);

			// old performances needed for ids of artists which had their performances changed
			var oldPerformances = performanceDAO.GetForSpectacleDay(spectacleDay);
			HashSet<int> artistIds = new HashSet<int>();

			try {
				// To prevent duplicate db entries which result in database exceptions,
				// all changed performances are deleted before they are iserted again.

				// All of that is done in one transaction to ensure data consistency
				using (TransactionScope trans = new TransactionScope()) {
					try {
						// Delete old entries
						// Id == 0 => new timeslot - nothing old to delete
						foreach (var p in performances.Where(p => p.Id != default(int))) {
							performanceDAO.Delete(p);
							p.Id = default(int);
							int aId = oldPerformances.Where(old => old.Id == p.Id).Select(old => old.ArtistId).FirstOrDefault();
							if (aId != default(int)) {
								artistIds.Add(aId);
							}
						}

						// Create new entries
						// ArtistId == 0 => new empty timeslot - nothing to create in the db
						foreach (var p in performances.Where(p => p.ArtistId != default(int))) {
							performanceDAO.Create(p);
							artistIds.Add(p.ArtistId);
						}
					} catch (Exception e) {
						throw new BusinessLogicException("Error while saving performance changes to the database: " + e.Message);
					}
					try {
						oldPerformances.ToList().ForEach(old => {
							if (performances.Where(p => p.Id == old.Id && p.ArtistId != old.ArtistId).Count() != 0) artistIds.Add(old.ArtistId);
						});
						var artists = new List<Artist>();
						var artistDAO = dalFactory.CreateArtistDAO(db);
						foreach (var id in artistIds) {
							artists.Add(artistDAO.GetById(id));
						}
						if (artists.Count() > 0) {
							var file = CreatePdfScheduleForSpectacleDay(spectacleDay);
							ms.MailToArtists(artists, spectacleDay, file);
						}
					} catch (Exception e) {
						throw new BusinessLogicException("Error while sending PDF to artists. " + e.Message);
					}

					trans.Complete();
				}
			} catch (Exception e) {
				if (e is BusinessLogicException) {
					throw e;
				}
				throw new BusinessLogicException("Error while updating performances");
			}
		}

		private void ValidatePerformanceChanges(IEnumerable<Performance> performances, Spectacleday spectacleDay) {
			var allPerformances = dalFactory.CreatePerformanceDAO(db).GetForSpectacleDay(spectacleDay).ToDictionary(p => p.Id, p => p);

			// Merge dirty data with DB data
			foreach (var p in performances) {
				allPerformances[p.Id] = p;
			}

			// Run checks
			ValidateUniqueArtistPerTimeSlot(allPerformances);
			ValidateArtistBreakAfterPerformance(allPerformances);
		}

		private void ValidateUniqueArtistPerTimeSlot(Dictionary<int, Performance> allPerformances) {
			foreach (var p in allPerformances.Values) {
				if (p.ArtistId == default(int)) {
					// Empty slots may occur multiple times
					continue;
				}
				if (allPerformances.Values.Where(pi => pi.Id != p.Id) // not the same performance
										  .Where(pi => pi.SpectacledayTimeSlot == p.SpectacledayTimeSlot) // same timeslot
										  .Where(pi => pi.ArtistId == p.ArtistId).Count() > 1) { // same artist
					throw new BusinessLogicException("Artist can only perform once per timeslot");
				}
			}
		}

		private void ValidateArtistBreakAfterPerformance(Dictionary<int, Performance> allPerformances) {
			foreach (var p in allPerformances.Values) {
				int timeslotId = p.SpectacledayTimeSlot;
				int nextTimeslotId = timeslotId + 1;

				foreach (var pi in allPerformances.Values) {
					// Check if there are any performances of the same artist in the next timeslot
					if (pi.SpectacledayTimeSlot == timeslotId
						&& pi.ArtistId == p.ArtistId) {
						throw new BusinessLogicException("Artists have to take one hour break after performing");
					}
				}
			}
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