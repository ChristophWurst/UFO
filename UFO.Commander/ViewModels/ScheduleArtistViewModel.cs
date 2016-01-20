using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Commander.ViewModels {

	internal class ScheduleArtistViewModel {
		private Artist artist;

		public ScheduleArtistViewModel(Artist artist) {
			this.artist = artist;
		}

		public int Id {
			get {
				return artist.Id;
			}
		}

		public string Name {
			get {
				return artist.Name;
			}
		}
	}
}