using System;

namespace UFO.DomainClasses {

	[Serializable]
	public class Performance {
		public int Id { get; set; }
		public int SpectacledayTimeSlot { get; set; }
		public int ArtistId { get; set; }
		public int VenueId { get; set; }

		public override string ToString() {
			return "[" + Id.ToString() + "]";
		}
	}
}