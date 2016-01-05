using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.DomainClasses {

	[Serializable]
	public class SpectacledayTimeSlot {
		public int Id { get; set; }
		public int TimeSlotId { get; set; }
		public int SpectacledayId { get; set; }
		public TimeSlot TimeSlot { get; set; }

		public override string ToString() {
			return "[" + Id.ToString() + "] TimeSlotId: [" + TimeSlotId + "] SpectacledayId: [" + SpectacledayId + "]";
		}
	}
}