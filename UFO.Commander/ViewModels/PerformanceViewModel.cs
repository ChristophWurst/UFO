using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Commander.ViewModels {

	/// <summary>
	/// ViewModel to show performances on the schedule-tab schedule
	/// </summary>
	internal class PerformanceViewModel : INotifyPropertyChanged {
		private Performance performance;
		private bool dirty;

		public event PropertyChangedEventHandler PropertyChanged;

		public PerformanceViewModel(Performance performance) {
			this.performance = performance;
			dirty = false;
		}

		public int ArtistId {
			get { return performance.ArtistId; }
			set {
				if (value != performance.ArtistId) {
					performance.ArtistId = value;
					dirty = true;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ArtistId)));
				}
			}
		}

		public Performance Performance {
			get { return performance; }
		}

		public bool IsDirty {
			get { return dirty; }
			set { dirty = value; }
		}
	}
}