using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.DomainClasses {

    [Serializable]
    public class Performance {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int ArtistId { get; set; }
        public int LocationId { get; set; }
        public int LocationAreaId { get; set; }

        public override string ToString() {
            return "[" + Id.ToString() + "] " + Start.ToString() + "-" + End.ToString();
        }
    }
}
