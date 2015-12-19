using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Commander.ViewModels {

	internal class ScheduleVenueViewModel {
		private Venue venue;

		public ObservableCollection<PerformanceViewModel> Performances { get; set; }

		public ScheduleVenueViewModel(Venue venue, ObservableCollection<PerformanceViewModel> performances) {
			this.venue = venue;
			Performances = performances;
		}

		public string Description {
			get {
				return venue.Description;
			}
		}
	}
}