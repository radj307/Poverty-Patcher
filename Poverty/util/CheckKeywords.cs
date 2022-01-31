using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Noggog;
using System.Collections.Generic;
using System.Linq;

namespace Poverty.util
{
    public struct CheckKeywords
    {
        public static bool HasAny(ExtendedList<IFormLinkGetter<IKeywordGetter>>? kwda, List<FormLink<IKeywordGetter>> keywords)
        {
            return kwda != null && keywords.Any(kywd => kwda.Any(k => k.FormKey == kywd.FormKey));
        }
    }
}
