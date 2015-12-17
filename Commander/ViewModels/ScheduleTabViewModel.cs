using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.BL;

namespace UFO.Commander.ViewModels {

	internal class ScheduleTabViewModel : INotifyPropertyChanged {
		private IBusinessLogic bl;

		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<TimeSlotViewModel> TimeSlots { get; set; }

		private ObservableCollection<ScheduleAreaViewModel> a;

		public ObservableCollection<ScheduleAreaViewModel> Areas {
			get {
				return a;
			}
			set { a = value; }
		}

		public ScheduleTabViewModel(IBusinessLogic bl) {
			this.bl = bl;

			TimeSlots = new ObservableCollection<TimeSlotViewModel>();
			Areas = new ObservableCollection<ScheduleAreaViewModel>();

			LoadTimeSlots();
			LoadAreas();
		}

		private void LoadAreas() {
			Areas.Clear();
			var areas = bl.GetAreas();
			foreach (var a in areas) {
				Areas.Add(new ScheduleAreaViewModel(a, bl));
			}
		}

		private void LoadTimeSlots() {
			TimeSlots.Clear();
			var timeSlots = bl.GetTimeSlots();
			foreach (var ts in timeSlots) {
				TimeSlots.Add(new TimeSlotViewModel(ts));
			}
		}
	}
}