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

	internal class ScheduleTabViewModel : INotifyPropertyChanged {
		private IBusinessLogicAsync bl;

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

		public ScheduleTabViewModel(IBusinessLogicAsync bl) {
			this.bl = bl;

			SpectacleDays = new ObservableCollection<SpectacledayViewModel>();
			Artists = new ObservableCollection<ScheduleArtistViewModel>();

			LoadSpectacleDays();
			LoadArtists();
		}

		private async void LoadSpectacleDays() {
			SpectacleDays.Clear();
			var days = await bl.GetSpectacleDaysAsync();
			foreach (var sd in days) {
				SpectacleDays.Add(new SpectacledayViewModel(sd, bl));
			}
			ActiveSpectacleDay = SpectacleDays.FirstOrDefault();
		}

		private async void LoadArtists() {
			var categories = await bl.GetCategoriesAsync();

			Artists.Clear();
			Artists.Add(new ScheduleArtistViewModel(new Artist { Name = "-" }, null));
			var artists = await bl.GetArtistsAsync();
			foreach (var a in artists) {
				var cat = categories.Where(c => c.Id == a.CategoryId).FirstOrDefault();
				Artists.Add(new ScheduleArtistViewModel(a, cat));
			}
		}

		public ICommand SaveCommand {
			get {
				return new RelayCommand(param => ActiveSpectacleDay?.SaveChanges());
			}
		}

		public ICommand SaveAsPdfCommand {
			get {
				return new RelayCommand(param => ActiveSpectacleDay?.SaveAsPdf());
			}
		}
	}
}