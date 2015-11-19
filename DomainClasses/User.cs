using System;

namespace UFO.DomainClasses {

	[Serializable]
	public class User {
		public int Id { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

		public override string ToString() {
			return "[" + Id.ToString() + "] " + Email;
		}
	}
}