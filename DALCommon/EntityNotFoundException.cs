using System;

namespace UFO.DAL.Common {

	public class EntityNotFoundException : Exception {

		public EntityNotFoundException() {
		}

		public EntityNotFoundException(string msg) : base(msg) {
		}
	}
}