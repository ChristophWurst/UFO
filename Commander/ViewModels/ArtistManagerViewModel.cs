using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.Commander.ViewModels {

	internal class ArtistManagerViewModel : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;

		public ArtistManagerViewModel() {
			artistsCollection = new ArtistCollectionViewModel();
			categoriesCollection = new CategoriesCollectionViewModel();
			countriesCollection = new CountriesCollectionViewModel();
			CategoriesCollection.LoadCategories();
			CountriesCollection.LoadCountries();
		}

		private ArtistCollectionViewModel artistsCollection;

		public ArtistCollectionViewModel ArtistsCollection {
			get { return artistsCollection; }
			set {
				if (artistsCollection != value) {
					artistsCollection = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ArtistsCollection)));
				}
			}
		}

		private CategoriesCollectionViewModel categoriesCollection;

		public CategoriesCollectionViewModel CategoriesCollection {
			get { return categoriesCollection; }
			set {
				if (categoriesCollection != value) {
					categoriesCollection = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CategoriesCollection)));
				}
			}
		}

		private CountriesCollectionViewModel countriesCollection;

		public CountriesCollectionViewModel CountriesCollection {
			get { return countriesCollection; }
			set {
				if (countriesCollection != value) {
					countriesCollection = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CountriesCollection)));
				}
			}
		}

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

		private CategoryViewModel currCategory;

		public CategoryViewModel CurrCategory {
			get { return currCategory; }
			set {
				if (currCategory != value) {
					currCategory = value;
					ArtistsCollection.LoadArtistForCategory(currCategory);
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrCategory)));
				}
			}
		}

		private CategoryViewModel currCountry;

		public CategoryViewModel CurrCountry {
			get { return currCategory; }
			set {
				if (currCountry != value) {
					currCountry = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrCountry)));
				}
			}
		}
	}
}