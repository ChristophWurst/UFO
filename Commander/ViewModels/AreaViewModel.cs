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

			Venues = new ObservableCollection<VenueViewModel>();
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

		internal void LoadVenues() {
			Venues.Clear();
			Venues.Add(new VenueViewModel(new Venue { ShortDescription = "V1", Description = "Venue 1", Latitude = 48.301, Longitude = 14.297 }));
			Venues.Add(new VenueViewModel(new Venue { ShortDescription = "V2", Description = "Venue 2", Latitude = 48.302, Longitude = 14.296 }));
			Venues.Add(new VenueViewModel(new Venue { ShortDescription = "V3", Description = "Venue 3", Latitude = 48.303, Longitude = 14.291 }));
			CurrentVenue = Venues.FirstOrDefault();
		}
	}
}