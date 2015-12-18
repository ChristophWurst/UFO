using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Commander.ViewModels {

	internal class TimeSlotViewModel {
		private TimeSlot timeslot;

		public TimeSlotViewModel(TimeSlot timeslot) {
			this.timeslot = timeslot;
		}

		public string Label {
			get {
				return timeslot.Start.Hours.ToString() + " – " + timeslot.End.Hours.ToString();
			}
		}
	}
}