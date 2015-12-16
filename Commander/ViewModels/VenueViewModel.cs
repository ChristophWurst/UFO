using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UFO.BL;
using UFO.DomainClasses;

namespace UFO.Commander.ViewModels {

	internal class VenueViewModel : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;

		private readonly Venue venue;
		private ICommand saveCommand;
		private IBusinessLogic bl;

		public VenueViewModel(Venue venue, IBusinessLogic bl) {
			this.venue = venue;
			this.bl = bl;
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
			get {
				var culture = CultureInfo.GetCultureInfo("en-GB");
				return venue.Latitude.ToString(culture) + "," + venue.Longitude.ToString(culture);
			}
			set {
				var loc = value.Split(',');
				var culture = CultureInfo.GetCultureInfo("en-GB");
				venue.Latitude = double.Parse(loc.ElementAt(0), culture);
				venue.Longitude = double.Parse(loc.ElementAt(1), culture);
			}
		}

		private void SaveChanges(object obj) {
			if (venue.Id == default(int)) {
				bl.CreateVenue(venue);
			} else {
				bl.UpdateVenue(venue);
			}
		}

		public ICommand SaveCommand {
			get {
				if (saveCommand == null) {
					saveCommand = new RelayCommand(SaveChanges);
				}
				return saveCommand;
			}
		}
	}
}