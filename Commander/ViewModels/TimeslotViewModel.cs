using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Commander.ViewModels {

	internal class TimeSlotViewModel {
		private SpectacledayTimeSlot timeslot;

		public TimeSlotViewModel(SpectacledayTimeSlot timeslot) {
			this.timeslot = timeslot;
		}

		public string Label {
			get {
				return timeslot.TimeSlot.Start.Hours.ToString() + " – " + timeslot.TimeSlot.End.Hours.ToString();
			}
		}
	}
}