using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UFO.BL;
using UFO.BL.execptions;
using UFO.DomainClasses;

namespace UFO.Commander.ViewModels {

	internal class SpectacledayViewModel {
		private const int NO_PERFORMANCE_ID = -1;
		private Spectacleday spectacleDay;
		private IBusinessLogic bl;

		public ObservableCollection<ScheduleAreaViewModel> Areas { get; set; }

		public SpectacledayViewModel(Spectacleday spectacleday, IBusinessLogic bl) {
			this.spectacleDay = spectacleday;
			this.bl = bl;

			Areas = new ObservableCollection<ScheduleAreaViewModel>();
		}

		public async void LoadPerformances() {
			var areas = await Task.Factory.StartNew(() => bl.GetAreas());
			var performances = await Task.Factory.StartNew(() => bl.GetPerformanesForSpetacleDay(spectacleDay));
			var timeSlots = await Task.Factory.StartNew(() => bl.GetSpectacleDayTimeSlotsForSpectacleDay(spectacleDay));

			ObservableCollection<TimeSlotViewModel> timeSlotViewModels = new ObservableCollection<TimeSlotViewModel>();
			foreach (var ts in timeSlots) {
				timeSlotViewModels.Add(new TimeSlotViewModel(ts));
			}

			Areas.Clear();
			foreach (var area in areas) {
				ObservableCollection<ScheduleVenueViewModel> venueViewModels = new ObservableCollection<ScheduleVenueViewModel>();
				var venues = await Task.Factory.StartNew(() => bl.GetVenuesForArea(area));
				foreach (var v in venues) {
					ObservableCollection<PerformanceViewModel> performanceViewModels = new ObservableCollection<PerformanceViewModel>();
					foreach (var ts in timeSlots) {
						var performance = performances
							.Where(p => p.VenueId == v.Id)
							.Where(p => p.SpectacledayTimeSlot == ts.Id)
							.FirstOrDefault();
						if (performance == null) {
							// Insert new performance as placeholder
							performance = new Performance {
								VenueId = v.Id,
								SpectacledayTimeSlot = ts.Id
							};
						}
						performanceViewModels.Add(new PerformanceViewModel(performance));
					}

					ScheduleVenueViewModel venueViewModel = new ScheduleVenueViewModel(v, performanceViewModels);
					venueViewModels.Add(venueViewModel);
				}
				var areaViewModel = new ScheduleAreaViewModel(area, timeSlotViewModels, venueViewModels, bl);
				Areas.Add(areaViewModel);
			}
		}

		internal async void SaveChanges() {
			try {
				List<Performance> performances = new List<Performance>();

				foreach (var a in Areas) {
					foreach (var v in a.Venues) {
						foreach (var p in v.Performances.Where(p => p.IsDirty)) {
							var performance = p.Performance;

							performances.Add(performance);
						}
					}
				}

				if (performances.Count() == 0) {
					MessageBox.Show("No changes to save");
					return;
				}
				bl.UpdatePerformances(spectacleDay, performances);

				foreach (var a in Areas) {
					foreach (var v in a.Venues) {
						foreach (var p in v.Performances.Where(p => p.IsDirty)) {
							p.IsDirty = false;
						}
					}
				}

				MessageBox.Show("Changes saves successfully");
			} catch (BusinessLogicException ble) {
				MessageBox.Show(ble.Message);
			}
		}

		public string Label {
			get { return spectacleDay.Day.ToString(); }
		}
	}
}