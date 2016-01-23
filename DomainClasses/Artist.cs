using System;

namespace UFO.DomainClasses {

	/// <summary>
	/// Domain class for the entity 'Artist'
	/// </summary>
	[Serializable]
	public class Artist {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Image { get; set; }
		public string Video { get; set; }
		public int CategoryId { get; set; }
		public int CountryId { get; set; }
		public string Email { get; set; }

		public override string ToString() {
			return "[" + Id.ToString() + "] " + Name;
		}
	}
}