using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using System.Collections.Generic;

namespace Poverty.util
{
    public enum FilterType
    {
        WHITELIST,
        BLACKLIST,
    }
    public class Filter<T> where T : SkyrimMajorRecord
    {
        public Filter(FilterType ty, List<FormLink<T>> list)
        {
            type = ty;
            List = list;
        }
        public Filter(FilterType ty = FilterType.WHITELIST)
        {
            type = ty;
            List = new();
        }

        private readonly FilterType type;
        public List<FormLink<T>> List;

        virtual public bool ShouldSkip(T record)
        {
            return List.Contains(record)
                ? type == FilterType.BLACKLIST // return true when entry is present and this is a blacklist
                : type == FilterType.WHITELIST;// return true when entry isn't present and this is a whitelist
        }
    }
    public class DualFilter<T> where T : SkyrimMajorRecord
    {
        public DualFilter(Filter<T> whitelist, Filter<T> blacklist)
        {
            Whitelist = whitelist;
            Blacklist = blacklist;
        }
        public DualFilter()
        {
            Whitelist = new();
            Blacklist = new();
        }

        public Filter<T> Whitelist;
        public Filter<T> Blacklist;

        // Checks if the given record is invalid
        public bool ShouldSkip(T record)
        {
            return Whitelist.ShouldSkip(record) || Blacklist.ShouldSkip(record);
        }

        // Checks if the given record (passed with operator[]) is valid
        public bool this[T record]
        {
            get
            {
                return record.EditorID != null && !ShouldSkip(record);
            }
        }
    }
}
