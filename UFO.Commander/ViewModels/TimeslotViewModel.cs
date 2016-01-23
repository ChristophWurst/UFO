using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Commander.ViewModels {

	/// <summary>
	/// ViewModel to listing timeslot-columns on the schedule-tab
	/// </summary>
	internal class TimeSlotViewModel {
		private SpectacledayTimeSlot timeslot;

		public TimeSlotViewModel(SpectacledayTimeSlot timeslot) {
			this.timeslot = timeslot;
		}

		public string Label {
			get {
				return timeslot.TimeSlot.Start.ToString() + " – " + timeslot.TimeSlot.End.ToString();
			}
		}
	}
}