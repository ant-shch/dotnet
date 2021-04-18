using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace EnumModelBinder.Web.Model
{
    public enum CarPartCondition : int
    {
        [Description("NEW")]
        New = 0,
        [Description("OLD")]
        Old = 1
    }
}
