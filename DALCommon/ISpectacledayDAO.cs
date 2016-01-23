using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.DAL.Common {

	/// <summary>
	/// Data-access-object for the entity 'Spectacleday'
	/// </summary>
	public interface ISpectacledayDAO {

		/// <summary>
		/// Get spectacleday by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Spectacleday GetById(int id);

		/// <summary>
		/// Get all spectacledays
		/// </summary>
		/// <returns></returns>
		IEnumerable<Spectacleday> GetAll();

		/// <summary>
		/// Get the spectacleday of the given performance
		/// </summary>
		/// <param name="performance"></param>
		/// <returns></returns>
		Spectacleday GetForPerformance(Performance performance);

		/// <summary>
		/// Get spectacledays of the given performances
		/// </summary>
		/// <param name="performances"></param>
		/// <returns></returns>
		IEnumerable<Spectacleday> GetForPerformances(IEnumerable<Performance> performances);
	}
}