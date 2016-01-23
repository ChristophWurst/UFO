using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.DAL.Common {

	/// <summary>
	/// Data-access-object for the entity 'SpectacledayTimeslot'
	/// </summary>
	public interface ISpectacledayTimeSlotDAO {

		/// <summary>
		/// Get spectacledaytimeslot by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		SpectacledayTimeSlot GetById(int id);

		/// <summary>
		/// Get all spectacledaytimeslots
		/// </summary>
		/// <returns></returns>
		IEnumerable<SpectacledayTimeSlot> GetAll();

		/// <summary>
		/// Get spectacledaytimeslots of the given spectacleday
		/// </summary>
		/// <param name="spectacleDay"></param>
		/// <returns></returns>
		IEnumerable<SpectacledayTimeSlot> GetForSpectacleDay(Spectacleday spectacleDay);

		/// <summary>
		/// Get spectacledaytimeslots of the given performances
		/// </summary>
		/// <param name="performances"></param>
		/// <returns></returns>
		IEnumerable<SpectacledayTimeSlot> GetForPerformances(IEnumerable<Performance> performances);
	}
}