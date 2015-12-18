using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Commander.ViewModels {

	internal class ScheduleVenueViewModel {
		private Venue venue;

		public ScheduleVenueViewModel(Venue venue) {
			this.venue = venue;
		}

		public string Description {
			get {
				return venue.Description;
			}
		}
	}
}