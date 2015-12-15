using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Commander.ViewModels {

	internal class CountryViewModel : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;

		private Country country;

		public CountryViewModel(Country country) {
			this.country = country;
		}

		public String Name {
			get { return country.Name; }
			set {
				if (country.Name != value) {
					country.Name = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(country.Name)));
				}
			}
		}
	}
}