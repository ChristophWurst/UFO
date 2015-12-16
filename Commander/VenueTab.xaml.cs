using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using UFO.BL;
using UFO.Commander.ViewModels;
using UFO.DomainClasses;

namespace UFO.Commander {

	/// <summary>
	/// Interaktionslogik für VenueTab.xaml
	/// </summary>
	public partial class VenueTab : TabItem {

		public VenueTab() {
			InitializeComponent();

			IBusinessLogic bl = BusinessLogicFactory.GetBusinessLogic();

			Loaded += (s, e) => {
				DataContext = new VenueTabViewModel(bl);
			};
		}
	}
}