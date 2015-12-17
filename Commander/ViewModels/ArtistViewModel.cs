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

	internal class ArtistViewModel : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;

		private IBusinessLogic bl;

		internal Artist Artist { get; private set; }

		public ArtistViewModel(Artist artist) {
			this.Artist = artist;
			bl = BusinessLogicFactory.GetBusinessLogic();
		}

		public String Name {
			get { return Artist.Name; }
			set {
				if (Artist.Name != value) {
					Artist.Name = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Artist.Name)));
				}
			}
		}

		public String Image {
			get { return Artist.Image; }
			set {
				if (Artist.Image != value) {
					Artist.Image = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Artist.Image)));
				}
			}
		}

		public String Video {
			get { return Artist.Video; }
			set {
				if (Artist.Video != value) {
					Artist.Video = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Artist.Video)));
				}
			}
		}

		public String Email {
			get { return Artist.Email; }
			set {
				if (Artist.Email != value) {
					Artist.Email = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Artist.Email)));
				}
			}
		}

		private CategoryViewModel category;

		public CategoryViewModel Category {
			get {
				if (category == null) {
					LoadCategory();
				}
				return category;
			}
			set {
				if (category != value) {
					category = value;
					Artist.CategoryId = category.Id;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Category)));
				}
			}
		}

		private CountryViewModel country;

		public CountryViewModel Country {
			get {
				if (country == null) {
					LoadCountry();
				}
				return country;
			}
			set {
				if (country != value) {
					country = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Country)));
				}
			}
		}

		private ICommand saveCommand;

		public ICommand SaveCommand {
			get {
				if (saveCommand == null) {
					saveCommand = new RelayCommand(param => SaveArtist());
				}
				return saveCommand;
			}
		}

		private void SaveArtist() {
			if (Artist.Id == 0) {
				Artist = bl.CreateArtist(Artist);
			} else {
				bl.UpdateArtist(Artist);
			}
		}

		private ICommand deleteCommand;

		public ICommand DeleteCommand {
			get {
				if (deleteCommand == null) {
					deleteCommand = new RelayCommand(param => bl.DeleteArtist(Artist));
				}
				return deleteCommand;
			}
		}

		private async void LoadCategory() {
			Category tmpCategory = new Category() { Id = Artist.CategoryId };
			Category = new CategoryViewModel(await Task.Factory.StartNew(() => bl.GetCategoryById(tmpCategory)));
		}

		private async void LoadCountry() {
			Country tmpCountry = new Country() { Id = Artist.CountryId };
			Country = new CountryViewModel(await Task.Factory.StartNew(() => bl.GetCountryById(tmpCountry)));
		}
	}
}