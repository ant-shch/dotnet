using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace EnumModelBinder.Web.Model
{
    public enum CarPartName : int
    {
        [Description("SUSPENSION")]
        Suspension = 0,
        [Description("TIER")]
        Tier = 1,
        [Description("ENGINE")]
        Engine = 2
    }
}
