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

		public ArtistViewModel CurrArtist {
			get { return currArtist; }
			set {
				if (currArtist != value) {
					currArtist = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrArtist)));
				}
			}
		}

		public ArtistCollectionViewModel() {
			Artists = new ObservableCollection<ArtistViewModel>();
		}

		public async void LoadArtistsForCategory(CategoryViewModel category) {
			Artists.Add(new ArtistViewModel(new Artist() { Id = 1, Name = "Christooph" }));
			Artists.Add(new ArtistViewModel(new Artist() { Id = 2, Name = "Stefaaaan" }));
		}
	}
}