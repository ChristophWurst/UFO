﻿using System;
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

	/// <summary>
	/// ViewModel for the artist-tab
	/// </summary>
	internal class ArtistTabViewModel : INotifyPropertyChanged {
		private IBusinessLogicAsync bl;

		public ArtistTabViewModel() {
			bl = BusinessLogicFactory.GetBusinessLogicAsync();
			Artists = new ObservableCollection<ArtistViewModel>();
			Categories = new ObservableCollection<CategoryViewModel>();
			Countries = new ObservableCollection<CountryViewModel>();
			LoadCategories();
			LoadCountries();
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
					NameInput = currArtist.Name;
					ImageInput = currArtist.Image;
					VideoInput = currArtist.Video;
					EmailInput = currArtist.Email;
					SelCategory = Categories.Where(category => category.Id == CurrArtist.CategoryId).FirstOrDefault();
					SelCountry = Countries.Where(country => country.Id == CurrArtist.CountryId).FirstOrDefault();
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrArtist)));
				}
			}
		}

		private string nameInput;

		public string NameInput {
			get { return nameInput; }
			set {
				if (nameInput != value) {
					nameInput = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NameInput)));
				}
			}
		}

		private string imageInput;

		public string ImageInput {
			get { return imageInput; }
			set {
				if (imageInput != value) {
					imageInput = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageInput)));
				}
			}
		}

		private string videoInput;

		public string VideoInput {
			get { return videoInput; }
			set {
				if (videoInput != value) {
					videoInput = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VideoInput)));
				}
			}
		}

		private string emailInput;

		public string EmailInput {
			get { return emailInput; }
			set {
				if (emailInput != value) {
					if (value?.Trim() == "") {
						emailInput = null;
					} else {
						emailInput = value;
					}
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EmailInput)));
				}
			}
		}

		private CategoryViewModel currCategory;

		public CategoryViewModel CurrCategory {
			get { return currCategory; }
			set {
				if (currCategory != value && value != null) {
					currCategory = value;
					LoadArtistsForCategory(currCategory);
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
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelCategory)));
				}
			}
		}

		private async void LoadCategories() {
			Categories.Clear();
			var tmpCategories = (await bl.GetCategoriesAsync()).Select(category => new CategoryViewModel(category));
			tmpCategories.ToList().ForEach(category => Categories.Add(category));
			CurrCategory = Categories.FirstOrDefault();
		}

		private async void LoadCountries() {
			Countries.Clear();
			var tmpCountries = (await bl.GetCountriesAsync()).Select(country => new CountryViewModel(country));
			tmpCountries.ToList().ForEach(country => Countries.Add(country));
		}

		private async void LoadArtistsForCategory(CategoryViewModel category) {
			Artists.Clear();
			var tmpArtists = (await bl.GetArtistsForCategoryAsync(category.Category)).Select(artist => new ArtistViewModel(artist));
			tmpArtists.ToList().ForEach(artist => Artists.Add(artist));
			CurrArtist = Artists.FirstOrDefault();
		}

		private ICommand addCommand;

		public ICommand AddCommand {
			get {
				if (addCommand == null) {
					addCommand = new RelayCommand(param => ResetArtist());
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
					saveCommand = new RelayCommand(param => SaveArtist());
				}
				return saveCommand;
			}
		}

		private void SaveArtist() {
			CurrArtist.Name = NameInput;
			CurrArtist.Image = ImageInput;
			CurrArtist.Video = VideoInput;
			CurrArtist.Email = EmailInput;
			CurrArtist.CategoryId = SelCategory.Id;
			CurrArtist.CountryId = SelCountry.Id;
			CurrArtist.SaveArtist();
			LoadArtistsForCategory(currCategory);
			ResetArtist();
		}

		private void ResetArtist() {
			CurrArtist = new ArtistViewModel(new Artist() {
				CategoryId = Categories.FirstOrDefault().Id,
				CountryId = Categories.FirstOrDefault().Id
			});
		}

		private void RemoveArtist() {
			if (currArtist != null) {
				Artists.Remove(CurrArtist);
				CurrArtist.DeleteArtist();
				ResetArtist();
			}
		}
	}
}