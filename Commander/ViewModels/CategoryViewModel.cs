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

	internal class CategoryViewModel : INotifyPropertyChanged {

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

		private Category category;

		private IBusinessLogic bl;

		public CategoryViewModel(Category category) {
			this.category = category;
			bl = BusinessLogicFactory.GetBusinessLogic();
			Artists = new ObservableCollection<ArtistViewModel>();
		}

		public String Description {
			get { return category.Description; }
			set {
				if (category.Description != value) {
					category.Description = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(category.Description)));
				}
			}
		}

		internal async void LoadArtists() {
			Artists.Clear();
			var tmpArtist = await Task.Factory.StartNew(() => bl.GetArtistsForCategory(category).Select(artist => new ArtistViewModel(artist)));
			tmpArtist.ToList().ForEach(artist => Artists.Add(artist));
		}
	}
}