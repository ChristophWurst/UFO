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

	internal class ScheduleAreaViewModel : INotifyPropertyChanged {
		private Area area;
		private ObservableCollection<ScheduleVenueViewModel> venues;
		private IBusinessLogic bl;

		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<ScheduleVenueViewModel> Venues { get; set; }

		public ScheduleAreaViewModel(Area area, IBusinessLogic bl) {
			this.area = area;
			this.bl = bl;

			Venues = new ObservableCollection<ScheduleVenueViewModel>();

			LoadVenues();
		}

		private void LoadVenues() {
			Venues.Clear();
			var venues = bl.GetVenuesForArea(area);
			foreach (var v in venues) {
				Venues.Add(new ScheduleVenueViewModel(v));
			}
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