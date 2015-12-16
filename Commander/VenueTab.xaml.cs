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
using UFO.Commander.ViewModels;
using UFO.DomainClasses;

namespace UFO.Commander {

	/// <summary>
	/// Interaktionslogik für VenueTab.xaml
	/// </summary>
	public partial class VenueTab : TabItem {

		public VenueTab() {
			InitializeComponent();

			var area1 = new Area { Name = "Area 1" };
			var area2 = new Area { Name = "Area 2" };

			var venue1 = new Venue { ShortDescription = "V1", Description = "Venue 1", Latitude = 48.301, Longitude = 14.297 };
			var venue2 = new Venue { ShortDescription = "V2", Description = "Venue 2", Latitude = 48.302, Longitude = 14.296 };
			var venue3 = new Venue { ShortDescription = "V3", Description = "Venue 3", Latitude = 48.303, Longitude = 14.291 };
			var venue4 = new Venue { ShortDescription = "V4", Description = "Venue 4", Latitude = 48.204, Longitude = 14.292 };
			var venue5 = new Venue { ShortDescription = "V5", Description = "Venue 5", Latitude = 48.205, Longitude = 14.292 };
			var venue6 = new Venue { ShortDescription = "V6", Description = "Venue 6", Latitude = 48.206, Longitude = 14.30 };

			Loaded += (s, e) => {
				DataContext = new VenueTabViewModel {
					Areas = new ObservableCollection<AreaViewModel> {
						new AreaViewModel(area1) {
							Venues = new ObservableCollection<VenueViewModel> {
								new VenueViewModel(venue1),
								new VenueViewModel(venue2),
								new VenueViewModel(venue3)
							}
						},
						new AreaViewModel(area2) {
							Venues = new ObservableCollection<VenueViewModel> {
								new VenueViewModel(venue4),
								new VenueViewModel(venue5),
								new VenueViewModel(venue6)
							}
						}
					}
				};
			};
		}
	}
}