using System;

namespace UFO.DomainClasses {

	[Serializable]
	public class Venue {
		public int Id { get; set; }
		public int AreaId { get; set; }
		public string Description { get; set; }
		public string ShortDescription { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }

		public override string ToString() {
			return "[" + Id.ToString() + "] " + ShortDescription + " " + Description;
		}
	}
}