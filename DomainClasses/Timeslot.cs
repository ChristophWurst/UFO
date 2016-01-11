using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.DomainClasses {

	[Serializable]
	public class TimeSlot {
		public int Id { get; set; }
		public int Start { get; set; }
		public int End { get; set; }

		public override string ToString() {
			return "[" + Id.ToString() + "] " + Start + " " + End;
		}
	}
}