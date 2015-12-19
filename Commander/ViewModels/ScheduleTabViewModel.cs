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

	internal class ScheduleTabViewModel : INotifyPropertyChanged {
		private IBusinessLogic bl;

		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<SpectacledayViewModel> SpectacleDays { get; set; }
		public ObservableCollection<ScheduleArtistViewModel> Artists { get; set; }
		public SpectacledayViewModel activeSpectacleDay;

		public SpectacledayViewModel ActiveSpectacleDay {
			get { return activeSpectacleDay; }
			set {
				if (value != activeSpectacleDay) {
					activeSpectacleDay = value;
					activeSpectacleDay.LoadPerformances();
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActiveSpectacleDay)));
				}
			}
		}

		public ScheduleTabViewModel(IBusinessLogic bl) {
			this.bl = bl;

			SpectacleDays = new ObservableCollection<SpectacledayViewModel>();
			Artists = new ObservableCollection<ScheduleArtistViewModel>();

			LoadSpectacleDays();
			LoadArtists();
		}

		private void LoadSpectacleDays() {
			SpectacleDays.Clear();
			var days = bl.GetSpectacleDays();
			foreach (var sd in days) {
				SpectacleDays.Add(new SpectacledayViewModel(sd, bl));
			}
			ActiveSpectacleDay = SpectacleDays.FirstOrDefault();
		}

		private void LoadArtists() {
			Artists.Clear();
			var artists = bl.GetArtists();
			foreach (var a in artists) {
				Artists.Add(new ScheduleArtistViewModel(a));
			}
		}
	}
}