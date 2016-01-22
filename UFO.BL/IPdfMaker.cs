using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.BL {

	public interface IPdfMaker {

		byte[] MakeSpectacleSchedule(IEnumerable<SpectacledayTimeSlot> spectacleDayTimeSlots,
									IEnumerable<Performance> performances,
									IEnumerable<Area> areas,
									IEnumerable<Venue> venues,
									IEnumerable<TimeSlot> timeSlots,
									IEnumerable<Artist> artists);
	}
}