using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UFO.BL;
using UFO.BL.execptions;
using UFO.DomainClasses;

namespace UFO.Commander.ViewModels {

	internal class SpectacledayViewModel {
		private const int NO_PERFORMANCE_ID = -1;
		private Spectacleday spectacleDay;
		private IBusinessLogicAsync bl;

		public ObservableCollection<ScheduleAreaViewModel> Areas { get; set; }

		public SpectacledayViewModel(Spectacleday spectacleday, IBusinessLogicAsync bl) {
			this.spectacleDay = spectacleday;
			this.bl = bl;

			Areas = new ObservableCollection<ScheduleAreaViewModel>();
		}

		public async void LoadPerformances() {
			var areas = await bl.GetAreasAsync();
			var performances = await bl.GetPerformanesForSpetacleDayAsync(spectacleDay);
			var timeSlots = await bl.GetSpectacleDayTimeSlotsForSpectacleDayAsync(spectacleDay);

			ObservableCollection<TimeSlotViewModel> timeSlotViewModels = new ObservableCollection<TimeSlotViewModel>();
			foreach (var ts in timeSlots) {
				timeSlotViewModels.Add(new TimeSlotViewModel(ts));
			}

			Areas.Clear();
			foreach (var area in areas) {
				ObservableCollection<ScheduleVenueViewModel> venueViewModels = new ObservableCollection<ScheduleVenueViewModel>();
				var venues = await bl.GetVenuesForAreaAsync(area);
				foreach (var v in venues) {
					ObservableCollection<PerformanceViewModel> performanceViewModels = new ObservableCollection<PerformanceViewModel>();
					foreach (var ts in timeSlots) {
						var performance = performances
							.Where(p => p.VenueId == v.Id)
							.Where(p => p.SpectacledayTimeSlot == ts.Id)
							.FirstOrDefault();
						if (performance == null) {
							// Insert new performance as placeholder
							performance = new Performance {
								VenueId = v.Id,
								SpectacledayTimeSlot = ts.Id
							};
						}
						var vm = new PerformanceViewModel(performance);
						vm.PropertyChanged += PerformancePropertyChanged;
						performanceViewModels.Add(vm);
					}

					ScheduleVenueViewModel venueViewModel = new ScheduleVenueViewModel(v, performanceViewModels);
					venueViewModels.Add(venueViewModel);
				}
				var areaViewModel = new ScheduleAreaViewModel(area, timeSlotViewModels, venueViewModels, bl);
				Areas.Add(areaViewModel);
			}
		}

		private void PerformancePropertyChanged(object sender, PropertyChangedEventArgs e) {
			if (sender is PerformanceViewModel && e.PropertyName == nameof(PerformanceViewModel.ArtistId)) {
				var performance = (PerformanceViewModel)sender;
				if (performance.ArtistId == default(int)) {
					// New artist is the placeholder, nothing to do then
					return;
				}
				foreach (var a in Areas) {
					foreach (var v in a.Venues) {
						foreach (var p in v.Performances) {
							if (p != performance && p.ArtistId == performance.ArtistId) {
								p.ArtistId = default(int);
							}
						}
					}
				}
			}
		}

		internal void SaveChanges() {
			try {
				List<Performance> performances = new List<Performance>();

				foreach (var a in Areas) {
					foreach (var v in a.Venues) {
						foreach (var p in v.Performances.Where(p => p.IsDirty)) {
							var performance = p.Performance;

							performances.Add(performance);
						}
					}
				}

				if (performances.Count() == 0) {
					MessageBox.Show("No changes to save");
					return;
				}
				bl.UpdatePerformancesAsync(spectacleDay, performances);

				foreach (var a in Areas) {
					foreach (var v in a.Venues) {
						foreach (var p in v.Performances.Where(p => p.IsDirty)) {
							p.IsDirty = false;
						}
					}
				}

				MessageBox.Show("Changes saves successfully");
			} catch (BusinessLogicException ble) {
				MessageBox.Show(ble.Message);
			}
		}

		internal async void SaveAsPdf() {
			Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

			dlg.DefaultExt = "pdf";
			dlg.FileName = spectacleDay.Day.ToString().Replace(":", "-") + ".pdf";
			dlg.AddExtension = true;

			bool? selected = dlg.ShowDialog();
			if (selected == true) {
				string fileName = dlg.SafeFileName;
				var file = await bl.CreatePdfScheduleForSpectacleDayAsync(spectacleDay);
				File.WriteAllBytes(fileName, file);
			}
		}

		public string Label {
			get { return spectacleDay.Day.ToString(); }
		}
	}
}