using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mutagen.Bethesda.WPF.Reflection.Attributes;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Plugins.Cache;

namespace Poverty.util
{
    public class Settings
    {
        [MaintainOrder]
        public bool LogAll = true;
        public DynamicFilter Filter = new();

        public bool IsEnabled<T>()
        {
            return Filter.IsEnabled<T>();
        }
    }
}
