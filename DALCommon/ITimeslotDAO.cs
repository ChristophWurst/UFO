using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.DAL.Common {

	/// <summary>
	/// Data-access-object for the entity 'Timeslot'
	/// </summary>
	public interface ITimeSlotDAO {

		/// <summary>
		/// Get timeslot by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		TimeSlot GetById(int id);

		/// <summary>
		/// Get all timeslots
		/// </summary>
		/// <returns></returns>
		IEnumerable<TimeSlot> GetAll();

		/// <summary>
		/// Get the timeslot of the given performance
		/// </summary>
		/// <param name="performance"></param>
		/// <returns></returns>
		TimeSlot GetForPerformance(Performance performance);

		/// <summary>
		/// Get the timslots of the given performances
		/// </summary>
		/// <param name="performances"></param>
		/// <returns></returns>
		IEnumerable<TimeSlot> GetForPerformances(IEnumerable<Performance> performances);
	}
}