﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.DomainClasses {

	/// <summary>
	/// Domain class for the entity 'Spectacleday'
	/// </summary>
	[Serializable]
	public class Spectacleday {
		public int Id { get; set; }
		public DateTime Day { get; set; }

		public override string ToString() {
			return "[" + Id.ToString() + "] " + Day;
		}
	}
}