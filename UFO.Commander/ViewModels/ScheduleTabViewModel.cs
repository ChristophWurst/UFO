﻿using System;
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

	/// <summary>
	/// ViewModel for the schele-tab
	/// </summary>
	internal class ScheduleTabViewModel : INotifyPropertyChanged {
		private IBusinessLogic bl;
		private IBusinessLogicAsync blAync;

		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<SpectacledayViewModel> SpectacleDays { get; set; }
		public ObservableCollection<ScheduleArtistViewModel> Artists { get; set; }
		public SpectacledayViewModel activeSpectacleDay;

		public SpectacledayViewModel ActiveSpectacleDay {
			get { return activeSpectacleDay; }
			set {
				if (value != activeSpectacleDay) {
					activeSpectacleDay = value;
					activeSpectacleDay?.LoadPerformances();
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActiveSpectacleDay)));
				}
			}
		}

		public ScheduleTabViewModel() {
			bl = BusinessLogicFactory.GetBusinessLogic();
			blAync = BusinessLogicFactory.GetBusinessLogicAsync();

			SpectacleDays = new ObservableCollection<SpectacledayViewModel>();
			Artists = new ObservableCollection<ScheduleArtistViewModel>();

			LoadSpectacleDays();
			LoadArtists();
			isSelected = true;
		}

		private async void LoadSpectacleDays() {
			SpectacleDays.Clear();
			var days = await blAync.GetSpectacleDaysAsync();
			foreach (var sd in days.OrderByDescending(d => d.Day)) {
				SpectacleDays.Add(new SpectacledayViewModel(sd, bl, blAync));
			}
			ActiveSpectacleDay = SpectacleDays.FirstOrDefault();
		}

		public async void LoadArtists() {
			var categories = await blAync.GetCategoriesAsync();

			Artists.Clear();
			Artists.Add(new ScheduleArtistViewModel(new Artist { Name = "-" }, null));
			var artists = await blAync.GetArtistsAsync();
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

		private bool isSelected;

		public bool IsSelected {
			get {
				return isSelected;
			}

			set {
				ActiveSpectacleDay?.LoadPerformances();
				if (value != isSelected) {
					isSelected = value;
					if (value) {
						// Unfortunately, this can not be done async :(
						//LoadArtists();
						//ActiveSpectacleDay?.LoadPerformances();
					}
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(isSelected)));
				}
			}
		}
	}
}