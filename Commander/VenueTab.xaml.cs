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

			var venue1 = new Venue { Description = "Venue 1" };
			var venue2 = new Venue { Description = "Venue 2" };
			var venue3 = new Venue { Description = "Venue 3" };
			var venue4 = new Venue { Description = "Venue 4" };
			var venue5 = new Venue { Description = "Venue 5" };
			var venue6 = new Venue { Description = "Venue 6" };

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