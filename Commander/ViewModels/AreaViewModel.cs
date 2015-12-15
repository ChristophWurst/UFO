using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Commander.ViewModels {

	internal class AreaViewModel : INotifyPropertyChanged {
		private readonly Area area;

		public ObservableCollection<VenueViewModel> Venues { get; set; }

		private VenueViewModel currentVenue;

		public VenueViewModel CurrentVenue {
			get { return currentVenue; }
			set {
				if (value != currentVenue) {
					currentVenue = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentVenue)));
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public AreaViewModel(Area area) {
			this.area = area;
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