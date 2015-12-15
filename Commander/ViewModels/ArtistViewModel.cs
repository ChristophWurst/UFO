using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Commander.ViewModels {

	internal class ArtistViewModel : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;

		private Artist artist;

		public ArtistViewModel(Artist artist) {
			this.artist = artist;
		}

		public String Name {
			get { return artist.Name; }
			set {
				if (artist.Name != value) {
					artist.Name = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(artist.Name)));
				}
			}
		}

		public String Image {
			get { return artist.Image; }
			set {
				if (artist.Image != value) {
					artist.Image = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(artist.Image)));
				}
			}
		}

		public String Video {
			get { return artist.Video; }
			set {
				if (artist.Video != value) {
					artist.Video = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(artist.Video)));
				}
			}
		}

		public String Email {
			get { return artist.Email; }
			set {
				if (artist.Email != value) {
					artist.Email = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(artist.Email)));
				}
			}
		}
	}
}