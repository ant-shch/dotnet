using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnumModelBinder.Web.Model
{
    public class CarPartDamage
    {
        public CarPartName Part { get; set; }
        public CarPartCondition Condition { get; set; }
    }
}
