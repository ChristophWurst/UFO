using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UFO.BL;
using UFO.DomainClasses;

namespace UFO.Commander.ViewModels {

	internal class AreaViewModel : INotifyPropertyChanged {
		private readonly Area area;

		public ObservableCollection<VenueViewModel> Venues { get; set; }

		private VenueViewModel currentVenue;
		private IBusinessLogicAsync bl;
		private RelayCommand relayCommand;

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

		public AreaViewModel(Area area, IBusinessLogicAsync bl) {
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

		internal async void LoadVenues() {
			Venues.Clear();
			(await bl.GetVenuesForAreaAsync(area)).ToList().ForEach(v => Venues.Add(new VenueViewModel(v, bl)));

			CurrentVenue = Venues.FirstOrDefault();
		}

		private void AddVenue(object obj) {
			var newVenue = new Venue { AreaId = area.Id };
			var vm = new VenueViewModel(newVenue, bl);
			Venues.Add(vm);
			CurrentVenue = vm;
		}

		public ICommand AddVenueCommand {
			get {
				if (relayCommand == null) {
					relayCommand = new RelayCommand(AddVenue);
				}
				return relayCommand;
			}
		}
	}
}