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
	/// ViewModel for the venue-tab
	/// </summary>
	internal class VenueTabViewModel : INotifyPropertyChanged {
		private AreaViewModel currentArea;
		private IBusinessLogicAsync bl;

		private async void LoadAreas() {
			Areas = new ObservableCollection<AreaViewModel>();
			(await bl.GetAreasAsync()).Select(a => new AreaViewModel(a, bl)).ToList().ForEach(a => Areas.Add(a));
			CurrentArea = Areas.FirstOrDefault();
		}

		public VenueTabViewModel() {
			bl = BusinessLogicFactory.GetBusinessLogicAsync();

			LoadAreas();
		}

		public ObservableCollection<AreaViewModel> Areas { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		public AreaViewModel CurrentArea {
			get { return currentArea; }
			set {
				if (value != currentArea) {
					currentArea = value;
					currentArea.LoadVenues();
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentArea)));
				}
			}
		}
	}
}