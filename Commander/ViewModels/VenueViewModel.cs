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

		public string Description {
			get { return venue.Description; }
			set {
				if (value != venue.Description) {
					venue.Description = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
				}
			}
		}
	}
}