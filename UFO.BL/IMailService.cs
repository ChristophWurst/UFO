using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.BL {

	/// <summary>
	/// Mail service abstraction for sending notifications about schedule changes to artists
	/// </summary>
	public interface IMailService {

		/// <summary>
		/// Send mail to the given artists about the given spectacleday
		/// </summary>
		/// <param name="artists"></param>
		/// <param name="day"></param>
		/// <param name="file">the schedule as pdf-file as byte-array</param>
		void MailToArtists(IEnumerable<Artist> artists, Spectacleday day, byte[] file);
	}
}