using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.Test.DomainClasses {

	[Serializable]
	public class SpectacledayTimeslot {
		public int Id { get; set; }
		public int TimeslotId { get; set; }
		public int SpectacledayId { get; set; }

		public override string ToString() {
			return "[" + Id.ToString() + "] TimeslotId: " + TimeslotId + "SpectacledayId: " + SpectacledayId;
		}
	}
}