using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Commander.ViewModels {

	/// <summary>
	/// VieModel for the combobox for selecting performance artists
	/// </summary>
	internal class ScheduleArtistViewModel {
		private Artist artist;
		private Category category;

		public ScheduleArtistViewModel(Artist artist, Category category) {
			this.artist = artist;
			this.category = category;
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

		public string Color {
			get {
				return category?.Color;
			}
		}
	}
}