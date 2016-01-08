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

			IBusinessLogic bl = BusinessLogicFactory.GetBusinessLogic(BLType.WebService);

			Loaded += (s, e) => {
				DataContext = new VenueTabViewModel(bl);
			};
		}

		private bool draggingMapPin = false;
		private Vector mouseToMarker;

		private void MapPinMouseDown(object sender, MouseButtonEventArgs e) {
			e.Handled = true;

			draggingMapPin = true;
			mouseToMarker = Point.Subtract(Map.LocationToViewportPoint(MapPin.Location), e.GetPosition(Map));
		}

		private void MapPinMouseMove(object sender, MouseEventArgs e) {
			if (e.LeftButton == MouseButtonState.Pressed) {
				if (draggingMapPin && MapPin != null) {
					MapPin.Location = Map.ViewportPointToLocation(Point.Add(e.GetPosition(Map), mouseToMarker));
					e.Handled = true;
				}
			}
		}
	}
}