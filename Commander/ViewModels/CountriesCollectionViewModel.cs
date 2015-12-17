using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.BL;

namespace UFO.Commander.ViewModels {

	internal class CountriesCollectionViewModel : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<CountryViewModel> Countries;

		private IBusinessLogic bl;

		public CountriesCollectionViewModel() {
			bl = BusinessLogicFactory.GetBusinessLogic();
			Countries = new ObservableCollection<CountryViewModel>();
		}

		internal async void LoadCountries() {
			Countries.Clear();
			var tmpCategories = await Task.Factory.StartNew(() => bl.GetCountries().Select(country => new CountryViewModel(country)));
			tmpCategories.ToList().ForEach(country => Countries.Add(country));
		}
	}
}