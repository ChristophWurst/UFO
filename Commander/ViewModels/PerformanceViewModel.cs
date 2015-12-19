using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Commander.ViewModels {

	internal class PerformanceViewModel {
		private Performance performance;

		public PerformanceViewModel(Performance performance) {
			this.performance = performance;
		}

		public string Name {
			get { return performance.ArtistId.ToString(); }
		}
	}
}