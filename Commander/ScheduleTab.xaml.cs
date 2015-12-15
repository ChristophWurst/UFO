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
using UFO.DomainClasses;

namespace UFO.Commander {

	/// <summary>
	/// Interaktionslogik für ScheduleTab.xaml
	/// </summary>
	public partial class ScheduleTab : TabItem {
		private IList<Venue> performances;

		public ScheduleTab() {
			InitializeComponent();

			performances = new List<Venue> {
				new Venue() { ShortDescription = "AAA" },
				new Venue() { ShortDescription = "BBB" }
			};
		}
	}
}