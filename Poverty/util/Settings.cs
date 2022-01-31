using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mutagen.Bethesda.WPF.Reflection.Attributes;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;

namespace Poverty.util
{
    public class Category<T> : DualFilter<T> where T : SkyrimMajorRecord
    {
        public Category(bool enabled, Filter<T> whitelist, Filter<T> blacklist)
        {
            Enable = enabled;
            Whitelist = whitelist;
            Blacklist = blacklist;
        }
        public Category(bool enabled = true)
        {
            Enable = enabled;
        }
        [Tooltip("When this is checked, this record type will be processed.")]
        public bool Enable;
    }
    public class Settings
    {
        [MaintainOrder]
        public Category<LeveledItem> LVLI = new(); // LVLI - Leveled Item
        public Category<Container> CONT = new(); // CONT - Container
        public Category<Flora> FLOR = new(); // FLOR - Flora
        public Category<Tree> TREE = new(); // TREE - Trees
        public Category<Npc> NPC = new(); // NPC_ - Non-Player Characters

        // Check the filters of all categories, return true if record is allowed to be processed
        public bool IsValidTarget(SkyrimMajorRecord record)
        {
            if (record.GetType() == typeof(LeveledItem))
                return LVLI[(LeveledItem)record];
            if (record.GetType() == typeof(Container))
                return CONT[(Container)record];
            if (record.GetType() == typeof(Flora))
                return FLOR[(Flora)record];
            if (record.GetType() == typeof(Tree))
                return TREE[(Tree)record];
            if (record.GetType() == typeof(Npc))
                return NPC[(Npc)record];
            return false;
        }
        public bool IsValidTarget(ISkyrimMajorRecordGetter record)
        {
            return IsValidTarget(record);
        }
    }
}
