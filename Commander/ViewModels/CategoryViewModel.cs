using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Commander.ViewModels {

	internal class CategoryViewModel : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;

		private Category category;

		public CategoryViewModel(Category category) {
			this.category = category;
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
	}
}