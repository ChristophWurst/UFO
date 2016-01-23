using System;

namespace UFO.DomainClasses {

	/// <summary>
	/// Domain class for the entity 'Category'
	/// </summary>
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