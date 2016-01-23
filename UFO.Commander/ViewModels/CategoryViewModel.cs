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

	/// <summary>
	/// ViewModel for listing categories on the venue-tab
	/// </summary>
	internal class CategoryViewModel : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;

		private IBusinessLogic bl;

		internal Category Category { get; private set; }

		public CategoryViewModel(Category category) {
			this.Category = category;
			bl = BusinessLogicFactory.GetBusinessLogic();
		}

		internal int Id {
			get { return Category.Id; }
		}

		public String Description {
			get { return Category.Description; }
			set {
				if (Category.Description != value) {
					Category.Description = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Category.Description)));
				}
			}
		}
	}
}