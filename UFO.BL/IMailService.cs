using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.BL {

	public interface IMailService {

		void MailToArtists(IEnumerable<Artist> artists, Spectacleday day, string attPath, string attName);
	}
}