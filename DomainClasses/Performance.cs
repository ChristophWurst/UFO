using System;

namespace UFO.DomainClasses {

	[Serializable]
	public class Performance {
		public int Id { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public int ArtistId { get; set; }
		public int VenueId { get; set; }

		public override string ToString() {
			return "[" + Id.ToString() + "]";
		}
	}
}