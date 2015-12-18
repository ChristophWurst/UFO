using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UFO.Commander.ViewModels;

namespace UFO.Commander {

	/// <summary>
	/// Interaktionslogik für ArtistTab.xaml
	/// </summary>
	public partial class ArtistTab : TabItem {

		public ArtistTab() {
			InitializeComponent();
			this.Loaded += (s, e) => {
				//this.DataContext = new ArtistManagerViewModel(new ArtistCollectionViewModel(), new CategoriesCollectionViewModel(), new CountriesCollectionViewModel());
				this.DataContext = new ArtistTabViewModel();
			};
		}
	}
}