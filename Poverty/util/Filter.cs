using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Plugins.Records.Internals;
using Mutagen.Bethesda.Skyrim;
using System.Collections.Generic;
using System.Linq;

namespace Poverty.util
{
    public enum FilterType
    {
        WHITELIST,
        BLACKLIST,
    }
    public class LinkFilter<T> where T : class, IMajorRecordGetter
    {
        public LinkFilter(FilterType ty, List<FormLink<T>> list)
        {
            type = ty;
            List = list;
        }
        public LinkFilter(FilterType ty = FilterType.WHITELIST)
        {
            type = ty;
            List = new();
        }

        private readonly FilterType type;
        public List<FormLink<T>> List;

        virtual public bool Allows(FormLink<T> record)
        {
            return List.Contains(record)
                ? type == FilterType.WHITELIST // return true when entry isn't present and this is a whitelist
                : type == FilterType.BLACKLIST;// return true when entry is present and this is a blacklist
        }
        virtual public bool Denies(FormLink<T> record)
        {
            return List.Contains(record)
                ? type == FilterType.BLACKLIST // return true when entry is present and this is a blacklist
                : type == FilterType.WHITELIST;// return true when entry isn't present and this is a whitelist
        }

        // Checks if the filter allows the object
        public bool this[FormLink<T> link] { get { return Allows(link); } }

        public bool this[T record] { get { return Allows(record); } }
    }
    public class IDFilter
    {
        public IDFilter(FilterType ty, List<string> list)
        {
            type = ty;
            List = list;
        }
        public IDFilter(FilterType ty)
        {
            type = ty;
            List = new();
        }

        private readonly FilterType type;
        public List<string> List;

        virtual public bool Allows(string editorID)
        {
            return List.Any(id => id.Equals(editorID, System.StringComparison.OrdinalIgnoreCase))
                ? type == FilterType.WHITELIST
                : type == FilterType.BLACKLIST;
        }
        virtual public bool Denies(string editorID)
        {
            return List.Any(id => id.Equals(editorID, System.StringComparison.OrdinalIgnoreCase))
                ? type == FilterType.BLACKLIST
                : type == FilterType.WHITELIST;
        }

        virtual public bool Allows(SkyrimMajorRecord record)
        {
            if (record.EditorID != null)
            {
                foreach (var entry in List)
                { // return true when record is present and this is a whitelist, false if this is a blacklist
                    if (entry.Equals(record.EditorID, System.StringComparison.OrdinalIgnoreCase))
                        return type == FilterType.WHITELIST;
                }
            }
            return false;
        }
        virtual public bool Allows(ISkyrimMajorRecordGetter record)
        {
            if (record.EditorID != null)
            {
                foreach (var entry in List)
                { // return true when record is present and this is a whitelist, false if this is a blacklist
                    if (entry.Equals(record.EditorID, System.StringComparison.OrdinalIgnoreCase))
                        return type == FilterType.WHITELIST;
                }
            }
            return false;
        }

        virtual public bool Denies(SkyrimMajorRecord record)
        {
            if (record.EditorID != null)
            {
                foreach (var entry in List)
                { // return true when record is present and this is a blacklist, false if this is a whitelist
                    if (entry.Equals(record.EditorID, System.StringComparison.OrdinalIgnoreCase))
                        return type == FilterType.BLACKLIST;
                }
            }
            return false;
        }
        virtual public bool Denies(ISkyrimMajorRecordGetter record)
        {
            if (record.EditorID != null)
            {
                foreach (var entry in List)
                { // return true when record is present and this is a blacklist, false if this is a whitelist
                    if (entry.Equals(record.EditorID, System.StringComparison.OrdinalIgnoreCase))
                        return type == FilterType.BLACKLIST;
                }
            }
            return false;
        }

        // Checks if the filter allows the object
        public bool this[string editorID] { get { return Allows(editorID); } }
        public bool this[SkyrimMajorRecord record] { get { return Allows(record); } }
        public bool this[ISkyrimMajorRecordGetter record] { get { return Allows(record); } }
    }
    public class DualIDFilter
    {
        public DualIDFilter(List<string> whitelist, List<string> blacklist)
        {
            Enable = whitelist.Count > 0 && blacklist.Count > 0;
            Whitelist = new(FilterType.WHITELIST, whitelist);
            Blacklist = new(FilterType.BLACKLIST, blacklist);
        }

        public bool Enable;
        public bool ExclusiveWhitelist = false;
        public IDFilter Whitelist;
        public IDFilter Blacklist;

        public bool Allows(string EditorID)
        {
            return Enable && (ExclusiveWhitelist
                ? (Whitelist[EditorID] && Blacklist[EditorID])
                : (Whitelist[EditorID] || Blacklist[EditorID]));
        }
        public bool Denies(string EditorID)
        {
            return Enable && !Allows(EditorID);
        }
        public bool this[string EditorID] { get { return Allows(EditorID); } }
    }
    public class DualLinkFilter<T> where T : class, IMajorRecordGetter
    {
        public DualLinkFilter(List<FormLink<T>> whitelist, List<FormLink<T>> blacklist)
        {
            Enable = whitelist.Count > 0 && blacklist.Count > 0;
            Whitelist = new(FilterType.WHITELIST, whitelist);
            Blacklist = new(FilterType.BLACKLIST, blacklist);
        }

        public bool Enable;
        public bool ExclusiveWhitelist = false;
        public LinkFilter<T> Whitelist;
        public LinkFilter<T> Blacklist;

        public bool Allows(T record)
        {
            return Enable && (ExclusiveWhitelist
                ? (Whitelist[record] && Blacklist[record])
                : (Whitelist[record] || Blacklist[record])
            );
        }
        public bool Allows(FormLink<T> link)
        {
            return Enable && (ExclusiveWhitelist
                ? (Whitelist[link] && Blacklist[link])
                : (Whitelist[link] || Blacklist[link])
            );
        }
        public bool Denies(T record)
        {
            return Enable && !Allows(record);
        }
        public bool Denies(FormLink<T> link)
        {
            return Enable && !Allows(link);
        }
        public bool this[T record] { get { return Allows(record); } }
        public bool this[FormLink<T> link] { get { return Allows(link); } }
    }
}
