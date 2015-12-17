using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UFO.BL;

namespace UFO.Commander.ViewModels {

	internal class ArtistCollectionViewModel : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<ArtistViewModel> Artists { get; set; }

		private IBusinessLogic bl;

		public ArtistCollectionViewModel() {
			bl = BusinessLogicFactory.GetBusinessLogic();
			Artists = new ObservableCollection<ArtistViewModel>();
		}

		public async void LoadArtistForCategory(CategoryViewModel category) {
			Artists.Clear();
			var tmpArtist = await Task.Factory.StartNew(() => bl.GetArtistsForCategory(category.Category).Select(artist => new ArtistViewModel(artist)));
			tmpArtist.ToList().ForEach(artist => Artists.Add(artist));
		}
	}
}