using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UFO.BL;
using UFO.DomainClasses;

namespace UFO.Commander.ViewModels {

	internal class ArtistTabViewModel : INotifyPropertyChanged {
		private IBusinessLogic bl;

		public ArtistTabViewModel() {
			bl = BusinessLogicFactory.GetBusinessLogic();
			Artists = new ObservableCollection<ArtistViewModel>();
			Categories = new ObservableCollection<CategoryViewModel>();
			Countries = new ObservableCollection<CountryViewModel>();
			LoadCategories();
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<ArtistViewModel> Artists { get; set; }
		public ObservableCollection<CategoryViewModel> Categories { get; set; }
		public ObservableCollection<CountryViewModel> Countries { get; set; }

		private ArtistViewModel currArtist;

		public ArtistViewModel CurrArtist {
			get { return currArtist; }
			set {
				if (currArtist != value && value != null) {
					currArtist = value;
					SelCategory = Categories.Where(category => category.Id == CurrArtist.CategoryId).FirstOrDefault();
					SelCountry = Countries.Where(country => country.Id == CurrArtist.CountryId).FirstOrDefault();
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrArtist)));
				}
			}
		}

		private CategoryViewModel currCategory;

		public CategoryViewModel CurrCategory {
			get { return currCategory; }
			set {
				if (currCategory != value && value != null) {
					currCategory = value;
					LoadArtistForCategory(currCategory);
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

		private CountryViewModel selCountry;

		public CountryViewModel SelCountry {
			get { return selCountry; }
			set {
				if (selCountry != value && value != null) {
					selCountry = value;
					CurrArtist.CountryId = selCategory.Id;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelCountry)));
				}
			}
		}

		private CategoryViewModel selCategory;

		public CategoryViewModel SelCategory {
			get { return selCategory; }
			set {
				if (selCategory != value && value != null) {
					selCategory = value;
					CurrArtist.CategoryId = SelCategory.Id;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelCategory)));
				}
			}
		}

		public async void LoadCategories() {
			Categories.Clear();
			var tmpCategories = await Task.Factory.StartNew(() => bl.GetCategories().Select(category => new CategoryViewModel(category)));
			tmpCategories.ToList().ForEach(category => Categories.Add(category));
		}

		public async void LoadArtistForCategory(CategoryViewModel category) {
			Artists.Clear();
			var tmpArtist = await Task.Factory.StartNew(() => bl.GetArtistsForCategory(category.Category).Select(artist => new ArtistViewModel(artist)));
			tmpArtist.ToList().ForEach(artist => Artists.Add(artist));
		}

		private ICommand addCommand;

		public ICommand AddCommand {
			get {
				if (addCommand == null) {
					addCommand = new RelayCommand(param => AddArtist());
				}
				return addCommand;
			}
		}

		private ICommand removeCommand;

		public ICommand RemoveCommand {
			get {
				if (removeCommand == null) {
					removeCommand = new RelayCommand(param => RemoveArtist());
				}
				return removeCommand;
			}
		}

		private ICommand saveCommand;

		public ICommand SaveCommand {
			get {
				if (saveCommand == null) {
					saveCommand = new RelayCommand(param => CurrArtist.SaveArtist());
				}
				return saveCommand;
			}
		}

		private void AddArtist() {
			CurrArtist = new ArtistViewModel(new Artist() {
				Name = "New User",
				CategoryId = Categories.FirstOrDefault().Id,
				CountryId = Categories.FirstOrDefault().Id
			});
			Artists.Add(CurrArtist);
		}

		private void RemoveArtist() {
			Artists.Remove(CurrArtist);
			CurrArtist.DeleteArtist();
		}
	}
}