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

	internal class ScheduleTabViewModel : INotifyPropertyChanged {
		private IBusinessLogic bl;

		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<SpectacledayViewModel> SpectacleDays { get; set; }
		public SpectacledayViewModel activeSpectacleDay;

		public SpectacledayViewModel ActiveSpectacleDay {
			get { return activeSpectacleDay; }
			set {
				if (value != activeSpectacleDay) {
					activeSpectacleDay = value;
					activeSpectacleDay.LoadPerformances();
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActiveSpectacleDay)));
				}
			}
		}

		public ObservableCollection<TimeSlotViewModel> TimeSlots2 { get; set; }
		public ObservableCollection<ScheduleAreaViewModel> Areas2 { get; set; }

		public ScheduleTabViewModel(IBusinessLogic bl) {
			this.bl = bl;

			SpectacleDays = new ObservableCollection<SpectacledayViewModel>();
			TimeSlots2 = new ObservableCollection<TimeSlotViewModel>();
			Areas2 = new ObservableCollection<ScheduleAreaViewModel>();

			LoadSpectacleDays();
			//LoadTimeSlots();
			//LoadAreas();
		}

		private void LoadSpectacleDays() {
			SpectacleDays.Clear();
			var days = bl.GetSpectacleDays();
			foreach (var sd in days) {
				SpectacleDays.Add(new SpectacledayViewModel(sd, bl));
			}
			ActiveSpectacleDay = SpectacleDays.FirstOrDefault();
		}

		private void LoadAreas() {
			Areas2.Clear();
			var areas = bl.GetAreas();
			foreach (var a in areas) {
				//Areas2.Add(new ScheduleAreaViewModel(a, bl));
			}
		}

		private void LoadTimeSlots() {
			TimeSlots2.Clear();
			var timeSlots = bl.GetTimeSlots();
			foreach (var ts in timeSlots) {
				TimeSlots2.Add(new TimeSlotViewModel(ts));
			}
		}
	}
}