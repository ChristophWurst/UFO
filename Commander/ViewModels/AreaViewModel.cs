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

	internal class AreaViewModel : INotifyPropertyChanged {
		private readonly Area area;

		public ObservableCollection<VenueViewModel> Venues { get; set; }

		private VenueViewModel currentVenue;
		private IBusinessLogic bl;

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

		public AreaViewModel(Area area, IBusinessLogic bl) {
			this.area = area;
			this.bl = bl;

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
			bl.GetVenuesForArea(area).ToList().ForEach(v => Venues.Add(new VenueViewModel(v, bl)));

			CurrentVenue = Venues.FirstOrDefault();
		}
	}
}