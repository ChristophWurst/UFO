﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.BL {

	internal interface IMailService {

		void MailToArtists(IEnumerable<Artist> artists, string body);
	}
}