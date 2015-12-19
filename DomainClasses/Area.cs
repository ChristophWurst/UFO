using System;
using System.Collections.Generic;

namespace UFO.DomainClasses {

	[Serializable]
	public class Area {
		public int Id { get; set; }
		public string Name { get; set; }

		public override string ToString() {
			return "[" + Id.ToString() + "] " + Name;
		}
	}
}