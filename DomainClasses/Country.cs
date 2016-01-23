using System;

namespace UFO.DomainClasses {

	/// <summary>
	/// Domain class for the entity 'Country'
	/// </summary>
	[Serializable]
	public class Country {
		public int Id { get; set; }
		public string Name { get; set; }

		public override string ToString() {
			return "[" + Id.ToString() + "] " + Name;
		}
	}
}