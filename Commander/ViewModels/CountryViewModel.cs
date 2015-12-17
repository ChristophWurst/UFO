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

		internal Country Country { get; private set; }

		public CountryViewModel(Country country) {
			this.Country = country;
		}

		internal int Id {
			get { return Country.Id; }
		}

		public String Name {
			get { return Country.Name; }
			set {
				if (Country.Name != value) {
					Country.Name = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Country.Name)));
				}
			}
		}
	}
}