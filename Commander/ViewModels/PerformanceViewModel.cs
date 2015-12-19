﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Commander.ViewModels {

	internal class PerformanceViewModel : INotifyPropertyChanged {
		private Performance performance;

		public event PropertyChangedEventHandler PropertyChanged;

		public PerformanceViewModel(Performance performance) {
			this.performance = performance;
		}

		public int ArtistId {
			get { return performance.ArtistId; }
			set {
				if (value != performance.ArtistId) {
					performance.ArtistId = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ArtistId)));
				}
			}
		}
	}
}