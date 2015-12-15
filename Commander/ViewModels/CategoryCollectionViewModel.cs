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

		public CategoryCollectionViewModel() {
			Items = new ObservableCollection<CategoryViewModel>();
			Items.Add(new CategoryViewModel(new Category() { Id = 1, Description = "BDSM" }));
			Items.Add(new CategoryViewModel(new Category() { Id = 1, Description = "SM" }));
			Items.Add(new CategoryViewModel(new Category() { Id = 1, Description = "GONZO" }));
		}

		public ObservableCollection<CategoryViewModel> Items { get; set; }

		public CategoryViewModel CurrentItem {
			get { return CurrentItem; }
			set {
				if (CurrentItem != value) {
					CurrentItem = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentItem)));
				}
			}
		}
	}
}