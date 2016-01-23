using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.BL {

	/// <summary>
	/// Schedule pdf maker interface
	/// </summary>
	public interface IPdfMaker {

		/// <summary>
		/// Create a pdf that shows the schedule of the given timeslots
		/// </summary>
		/// <param name="spectacleDayTimeSlots"></param>
		/// <param name="performances"></param>
		/// <param name="areas"></param>
		/// <param name="venues"></param>
		/// <param name="timeSlots"></param>
		/// <param name="artists"></param>
		/// <returns>pdf-file as byte-array</returns>
		byte[] MakeSpectacleSchedule(IEnumerable<SpectacledayTimeSlot> spectacleDayTimeSlots,
									IEnumerable<Performance> performances,
									IEnumerable<Area> areas,
									IEnumerable<Venue> venues,
									IEnumerable<TimeSlot> timeSlots,
									IEnumerable<Artist> artists);
	}
}