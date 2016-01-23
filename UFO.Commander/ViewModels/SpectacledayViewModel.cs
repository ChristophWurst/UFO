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

	/// <summary>
	/// ViewModel for listing and selecting the current spectacleday on of the schedule-tab
	/// </summary>
	internal class SpectacledayViewModel {
		private Spectacleday spectacleDay;
		private IBusinessLogic bl;
		private IBusinessLogicAsync blAsync;

		public ObservableCollection<ScheduleAreaViewModel> Areas { get; set; }

		public SpectacledayViewModel(Spectacleday spectacleday, IBusinessLogic bl, IBusinessLogicAsync blAsync) {
			this.spectacleDay = spectacleday;
			this.blAsync = blAsync;
			this.bl = bl;

			Areas = new ObservableCollection<ScheduleAreaViewModel>();
		}

		public async void LoadPerformances() {
			var areas = await blAsync.GetAreasAsync();
			var performances = await blAsync.GetPerformanesForSpetacleDayAsync(spectacleDay);
			var timeSlots = await blAsync.GetSpectacleDayTimeSlotsForSpectacleDayAsync(spectacleDay);

			ObservableCollection<TimeSlotViewModel> timeSlotViewModels = new ObservableCollection<TimeSlotViewModel>();
			foreach (var ts in timeSlots) {
				timeSlotViewModels.Add(new TimeSlotViewModel(ts));
			}

			Areas.Clear();
			foreach (var area in areas) {
				ObservableCollection<ScheduleVenueViewModel> venueViewModels = new ObservableCollection<ScheduleVenueViewModel>();
				var venues = await blAsync.GetVenuesForAreaAsync(area);
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
				var areaViewModel = new ScheduleAreaViewModel(area, timeSlotViewModels, venueViewModels, blAsync);
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

				// An artist was selected -> remove all performances of the next timeslot
				int timeSlotId = performance.Performance.SpectacledayTimeSlot;
				int nextTimeSlotId = timeSlotId + 1;
				foreach (var a in Areas) {
					foreach (var v in a.Venues) {
						foreach (var p in v.Performances) {
							if (p != performance && p.ArtistId == performance.ArtistId) {
								// Another performance, but same artist
								if (p.Performance.SpectacledayTimeSlot == timeSlotId) {
									//|| p.Performance.SpectacledayTimeSlot == nextTimeSlotId) {
									// Same or next timeslot
									p.ArtistId = default(int);
								}
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

				bl.UpdatePerformances(spectacleDay, performances);

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

		internal void SaveAsPdf() {
			Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

			dlg.DefaultExt = "pdf";
			dlg.FileName = spectacleDay.Day.ToString().Replace(":", "-") + ".pdf";
			dlg.AddExtension = true;

			bool? selected = dlg.ShowDialog();
			if (selected == true) {
				string fileName = dlg.FileName;
				var file = bl.CreatePdfScheduleForSpectacleDay(spectacleDay);
				File.WriteAllBytes(fileName, file);
				MessageBox.Show($"PDF-File saved successfully as {fileName}");
			} else {
				MessageBox.Show("Invalid path selected");
			}
		}

		public string Label {
			get { return spectacleDay.Day.ToString(); }
		}
	}
}