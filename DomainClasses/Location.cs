using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.DomainClasses {

    [Serializable]
    public class Location {
        public int Id { get; set; }
        public int AreaId { get; set; }
        public string Name { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }

        public override string ToString() {
            return "[" + Id.ToString() + "," + AreaId.ToString() + "] " + Name;
        }
    }
}
