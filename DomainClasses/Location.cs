using System;

namespace UFO.DomainClasses {

    [Serializable]
    public class Location {
        public int Id { get; set; }
        public int AreaId { get; set; }
        public string Desc { get; set; }
        public string ShortDesc { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }

        public override string ToString() {
            return "[" + Id.ToString() + "] " + ShortDesc + " " + Desc;
        }
    }
}