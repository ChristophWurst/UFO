using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.Commander.ViewModels {

	internal class VenueTabViewModel : INotifyPropertyChanged {
		private AreaViewModel currentArea;

		public ObservableCollection<AreaViewModel> Areas { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		public AreaViewModel CurrentArea {
			get { return currentArea; }
			set {
				if (value != currentArea) {
					currentArea = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentArea)));
				}
			}
		}
	}
}