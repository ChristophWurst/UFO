using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.BL;
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

			LoadPerformances();
		}

		public void LoadPerformances() {
			var areas = bl.GetAreas();
			var performances = bl.GetPerformanesForSpetacleDay(spectacleDay);

			Areas.Clear();
			foreach (var area in areas) {
				ObservableCollection<TimeSlotViewModel> timeSlotViewModels = new ObservableCollection<TimeSlotViewModel>();
				foreach (var ts in bl.GetTimeSlots()) {
					timeSlotViewModels.Add(new TimeSlotViewModel(ts));
				}

				ObservableCollection<ScheduleVenueViewModel> venueViewModels = new ObservableCollection<ScheduleVenueViewModel>();
				foreach (var v in bl.GetVenuesForArea(area)) {
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

		public string Label {
			get { return spectacleDay.Day.ToString(); }
		}
	}
}