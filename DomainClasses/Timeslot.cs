using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.DomainClasses {

	[Serializable]
	public class Timeslot {
		public int Id { get; set; }
		public TimeSpan Start { get; set; }
		public TimeSpan End { get; set; }

		public override string ToString() {
			return "[" + Id.ToString() + "] " + Start + " " + End;
		}
	}
}