using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Commander.ViewModels {

	internal class ArtistCollectionViewModel : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<ArtistViewModel> Artists { get; set; }

		private ArtistViewModel currArtist;
		private CategoryViewModel category;

		public ArtistViewModel CurrArtist {
			get { return currArtist; }
			set {
				if (currArtist != value) {
					currArtist = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrArtist)));
				}
			}
		}

		public CategoryViewModel Category {
			get { return category; }
			set {
				if (category != value) {
					category = value;
					LoadArtists();
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Category)));
				}
			}
		}

		public ArtistCollectionViewModel() {
			Artists = new ObservableCollection<ArtistViewModel>();
		}

		public async void LoadArtists() {
			Artists.Clear();
			Artists.Add(new ArtistViewModel(new Artist() { Id = 1, Name = "Christooph", Image = "bidl", Email = "yay@yay.com", Video = "videoooo" }));
			Artists.Add(new ArtistViewModel(new Artist() { Id = 2, Name = "Stefaaaan" }));
		}
	}
}