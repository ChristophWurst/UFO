using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.DomainClasses {

    [Serializable]
    public class Category {
        public int Id { get; set; }
        public string Description { get; set; }
        public override string ToString() {
            return "[" + Id.ToString() + "] " + Description;
        }
    }
}
