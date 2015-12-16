using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UFO.Commander.ViewModels {

	internal class ArtistsTabViewModel : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;

		private ArtistCollectionViewModel artistsCollection;
		private CategoryCollectionViewModel categoriesCollection;

		public ObservableCollection<ArtistViewModel> Artists { get; set; }
		public ObservableCollection<CategoryViewModel> Categories { get; set; }

		public CategoryViewModel CurrCategory {
			get { return categoriesCollection.CurrCategory; }
			set {
				if (categoriesCollection.CurrCategory != value) {
					categoriesCollection.currCategory = value;
					artistsCollection.Category = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrCategory)));
				}
			}
		}

		public ArtistViewModel CurrArtist {
			get { return artistsCollection.CurrArtist; }
			set {
				if (artistsCollection.CurrArtist != value) {
					artistsCollection.CurrArtist = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrArtist)));
				}
			}
		}

		private bool editable;

		public bool Editable {
			get { return editable; }
			set {
				if (editable != value) {
					editable = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Editable)));
				}
			}
		}

		public ArtistsTabViewModel() {
			artistsCollection = new ArtistCollectionViewModel();
			categoriesCollection = new CategoryCollectionViewModel();
			CurrArtist = artistsCollection.CurrArtist;
			CurrCategory = categoriesCollection.CurrCategory;
			Artists = artistsCollection.Artists;
			Categories = categoriesCollection.Categories;
		}

		private ICommand editCommand;

		public ICommand EditCommand {
			get {
				if (editCommand == null) {
					editCommand = new RelayCommand(param => Editable = true);
				}
				return editCommand;
			}
		}

		private ICommand saveCommand;

		public ICommand SaveCommand {
			get {
				if (saveCommand == null) {
					saveCommand = new RelayCommand(param => Editable = false);
				}
				return saveCommand;
			}
		}
	}
}