using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Commander.ViewModels {

	internal class VenueViewModel : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;

		private readonly Venue venue;

		public VenueViewModel(Venue venue) {
			this.venue = venue;
		}

		public string ShortDescription {
			get { return venue.ShortDescription; }
			set {
				if (value != venue.ShortDescription) {
					venue.ShortDescription = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShortDescription)));
				}
			}
		}

		public string Description {
			get { return venue.Description; }
			set {
				if (value != venue.Description) {
					venue.Description = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
				}
			}
		}

		public string Location {
			get { return venue.Latitude + "," + venue.Longitude; }
		}
	}
}