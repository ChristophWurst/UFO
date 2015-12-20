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

			Areas.Clear();
			foreach (var area in areas) {
				ObservableCollection<TimeSlotViewModel> timeSlotViewModels = new ObservableCollection<TimeSlotViewModel>();
				var timeSlots = await Task.Factory.StartNew(() => bl.GetTimeSlots());
				foreach (var ts in timeSlots) {
					timeSlotViewModels.Add(new TimeSlotViewModel(ts));
				}

				ObservableCollection<ScheduleVenueViewModel> venueViewModels = new ObservableCollection<ScheduleVenueViewModel>();
				var venues = await Task.Factory.StartNew(() => bl.GetVenuesForArea(area));
				foreach (var v in venues) {
					ObservableCollection<PerformanceViewModel> performanceViewModels = new ObservableCollection<PerformanceViewModel>();
					var x = performances.Where(p => p.VenueId == v.Id).Count();
					foreach (var p in performances.Where(p => p.VenueId == v.Id)) {
						performanceViewModels.Add(new PerformanceViewModel(p));
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

				bl.UpdatePerformances(performances);
			} catch (BusinessLogicException ble) {
				MessageBox.Show(ble.Message);
			}
		}

		public string Label {
			get { return spectacleDay.Day.ToString(); }
		}
	}
}