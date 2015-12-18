using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.BL {

	internal interface IMailService {

		void MailArtistsNewProgramm(IEnumerable<Artist> artists);
	}
}