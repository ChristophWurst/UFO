using System;

namespace UFO.DomainClasses {

    [Serializable]
    public class Artist {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Country { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public int CategoryId { get; set; }

        public override string ToString() {
            return "[" + Id.ToString() + "]" + Firstname + " " + Lastname;
        }
    }
}