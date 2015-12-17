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

	internal class ArtistsTabViewModel : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<CategoryViewModel> Categories { get; set; }

		private CategoryViewModel currCategory;

		public CategoryViewModel CurrCategory {
			get { return currCategory; }
			set {
				if (currCategory != value) {
					currCategory = value;
					currCategory.LoadArtists();
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrCategory)));
				}
			}
		}

		private IBusinessLogic bl;

		public ArtistsTabViewModel() {
			bl = BusinessLogicFactory.GetBusinessLogic();
			Categories = new ObservableCollection<CategoryViewModel>();
			LoadCategories();
		}

		internal async void LoadCategories() {
			Categories.Clear();
			var tmpCategories = await Task.Factory.StartNew(() => bl.GetCategories().Select(category => new CategoryViewModel(category)));
			tmpCategories.ToList().ForEach(category => Categories.Add(category));
		}
	}
}