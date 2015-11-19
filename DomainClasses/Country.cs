using System;

namespace UFO.DomainClasses {

	[Serializable]
	public class Country {
		public int Id { get; set; }
		public string Name { get; set; }

		public override string ToString() {
			return "[" + Id.ToString() + "] " + Name;
		}
	}
}