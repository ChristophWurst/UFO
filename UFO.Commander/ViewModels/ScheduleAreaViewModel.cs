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

	/// <summary>
	/// ViewModel to show a spectacleday schedule on the schedule-tab
	/// </summary>
	internal class ScheduleAreaViewModel : INotifyPropertyChanged {
		private Area area;
		private IBusinessLogicAsync bl;

		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<TimeSlotViewModel> TimeSlots { get; set; }
		public ObservableCollection<ScheduleVenueViewModel> Venues { get; set; }

		public ScheduleAreaViewModel(Area area,
									 ObservableCollection<TimeSlotViewModel> timeslots,
									 ObservableCollection<ScheduleVenueViewModel> venues,
									 IBusinessLogicAsync bl) {
			this.area = area;
			TimeSlots = timeslots;
			Venues = venues;
			this.bl = bl;
		}

		public string Name {
			get { return area.Name; }
			set {
				if (value != area.Name) {
					area.Name = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
				}
			}
		}
	}
}