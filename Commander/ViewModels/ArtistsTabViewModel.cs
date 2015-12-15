using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.Commander.ViewModels {

	internal class ArtistsTabViewModel : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;

		private ArtistCollectionViewModel artistsCollection;
		private CategoryCollectionViewModel categoriesCollection;

		public ObservableCollection<ArtistViewModel> Artists { get; set; }
		public ObservableCollection<CategoryViewModel> Categories { get; set; }

		public CategoryViewModel CurrCategory {
			get { return categoriesCollection.currCategory; }
			set {
				if (categoriesCollection.currCategory != value) {
					categoriesCollection.currCategory = value;
					artistsCollection.LoadArtistsForCategory(CurrCategory);
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrCategory)));
				}
			}
		}

		public ArtistViewModel CurrArtist { get; set; }

		public ArtistsTabViewModel() {
			artistsCollection = new ArtistCollectionViewModel();
			categoriesCollection = new CategoryCollectionViewModel();
			CurrArtist = artistsCollection.CurrArtist;
			Artists = artistsCollection.Artists;
			Categories = categoriesCollection.Categories;
		}
	}
}