using System;

namespace UFO.DomainClasses {

	[Serializable]
	public class Category {
		public int Id { get; set; }
		public string Description { get; set; }
		public string Color { get; set; }

		public override string ToString() {
			return "[" + Id.ToString() + "] " + Description;
		}
	}
}