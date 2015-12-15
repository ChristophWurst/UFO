using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Commander.ViewModels {

	internal class TimeslotViewModel : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;

		private Timeslot timeslot;

		public TimeslotViewModel(Timeslot timeslot) {
			this.timeslot = timeslot;
		}

		public TimeSpan Start {
			get { return timeslot.Start; }
			set {
				if (timeslot.Start != value) {
					timeslot.Start = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(timeslot.Start)));
				}
			}
		}

		public TimeSpan End {
			get { return timeslot.End; }
			set {
				if (timeslot.End != value) {
					timeslot.End = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(timeslot.End)));
				}
			}
		}
	}
}