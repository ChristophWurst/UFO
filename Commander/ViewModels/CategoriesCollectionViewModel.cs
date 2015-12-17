using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.BL;

namespace UFO.Commander.ViewModels {

	internal class CategoriesCollectionViewModel : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<CategoryViewModel> Categories { get; set; }

		private IBusinessLogic bl;

		public CategoriesCollectionViewModel() {
			Categories = new ObservableCollection<CategoryViewModel>();
			bl = BusinessLogicFactory.GetBusinessLogic();
		}

		public async void LoadCategories() {
			Categories.Clear();
			var tmpCategories = await Task.Factory.StartNew(() => bl.GetCategories().Select(category => new CategoryViewModel(category)));
			tmpCategories.ToList().ForEach(category => Categories.Add(category));
		}
	}
}