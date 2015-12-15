using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Commander.ViewModels {

	internal class CategoryCollectionViewModel : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;

		public CategoryViewModel currCategory;

		public CategoryCollectionViewModel() {
			Categories = new ObservableCollection<CategoryViewModel>();
		}

		public ObservableCollection<CategoryViewModel> Categories { get; set; }

		public CategoryViewModel CurrCategory {
			get { return currCategory; }
			set {
				if (currCategory != value) {
					currCategory = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrCategory)));
				}
			}
		}

		public async void LoadCategories() {
			Categories.Add(new CategoryViewModel(new Category() { Id = 1, Description = "BDSM" }));
			Categories.Add(new CategoryViewModel(new Category() { Id = 2, Description = "SM" }));
			Categories.Add(new CategoryViewModel(new Category() { Id = 3, Description = "GONZO" }));
		}
	}
}