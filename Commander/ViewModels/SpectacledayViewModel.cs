using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Commander.ViewModels {

	internal class SpectacledayViewModel : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;

		private Spectacleday spectacleday;

		public SpectacledayViewModel(Spectacleday spectacleday) {
			this.spectacleday = spectacleday;
		}

		public DateTime Day {
			get { return spectacleday.Day; }
			set {
				if (spectacleday.Day != value) {
					spectacleday.Day = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(spectacleday.Day)));
				}
			}
		}
	}
}