using System;
using System.Collections.Generic;
using System.Linq;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Synthesis;
using Mutagen.Bethesda.Skyrim;
using System.Threading.Tasks;
using Poverty.util;

namespace Poverty
{
    public class Program
    {
        private static Lazy<Settings> _lazySettings = null!;
        private static Settings Settings => _lazySettings.Value;

        private static bool same_as<T1, T2>()
        {
            return typeof(T1).Equals(typeof(T2));
        }

        internal static ISkyrimMajorRecordGetter Process<T>(IPatcherState<ISkyrimMod, ISkyrimModGetter> state, T record) where T : ISkyrimMajorRecordGetter
        {
            if (same_as<T, Ingestible>()) // INGESTIBLE
            {
                Ingestible alch = (Ingestible)record.DeepCopy();
                if (alch.Keywords == null)
                    return alch;
                if (CheckKeywords.HasAny(alch.Keywords, new()
                {
                    Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.Keyword.VendorItemPotion,
                    Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.Keyword.VendorItemPoison,
                    Mutagen.Bethesda.FormKeys.SkyrimSE.Skyrim.Keyword.VendorItemPotion,
                    Mutagen.Bethesda.FormKeys.SkyrimSE.Skyrim.Keyword.VendorItemPoison,
                }))
                {
                    alch.Name = "DummyPotion";
                }
                else if (alch.ConsumeSound.Equals(
                    Mutagen.Bethesda.FormKeys.SkyrimSE.Skyrim.SoundDescriptor.ITMPotionUse)
                    || alch.ConsumeSound.Equals(Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.SoundDescriptor.ITMPotionUse)
                    )
                {
                    alch.Name = "DummyDrink";
                }
                else
                {
                    alch.Name = "DummyFood";
                }
                return alch;
            }
            else if (same_as<T, Ammunition>()) // AMMUNITION
            {
                Ammunition ammo = (Ammunition)record.DeepCopy();
                ammo.Name = "DummyArrow";
                return ammo;
            }
            else if (same_as<T, Armor>()) // ARMOR
            {
                Armor armo = (Armor)record.DeepCopy();
                if (CheckKeywords.HasAny(armo.Keywords, new()
                {
                    Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.Keyword.ArmorBoots,
                    Mutagen.Bethesda.FormKeys.SkyrimSE.Skyrim.Keyword.ArmorBoots,
                    Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.Keyword.ClothingFeet,
                    Mutagen.Bethesda.FormKeys.SkyrimSE.Skyrim.Keyword.ClothingFeet
                }))
                {
                    armo.Name = "DummyBoots";
                }
                else if (CheckKeywords.HasAny(armo.Keywords, new()
                {
                    Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.Keyword.ArmorCuirass,
                    Mutagen.Bethesda.FormKeys.SkyrimSE.Skyrim.Keyword.ArmorCuirass,
                    Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.Keyword.ClothingBody,
                    Mutagen.Bethesda.FormKeys.SkyrimSE.Skyrim.Keyword.ClothingBody
                }))
                {
                    armo.Name = "DummyCuirass";
                }
                else if (CheckKeywords.HasAny(armo.Keywords, new()
                {
                    Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.Keyword.ArmorGauntlets,
                    Mutagen.Bethesda.FormKeys.SkyrimSE.Skyrim.Keyword.ArmorGauntlets,
                    Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.Keyword.ClothingHands,
                    Mutagen.Bethesda.FormKeys.SkyrimSE.Skyrim.Keyword.ClothingHands
                }))
                {
                    armo.Name = "DummyGauntlets";
                }
                else if (CheckKeywords.HasAny(armo.Keywords, new()
                {
                    Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.Keyword.ArmorHelmet,
                    Mutagen.Bethesda.FormKeys.SkyrimSE.Skyrim.Keyword.ArmorHelmet,
                    Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.Keyword.ClothingHead,
                    Mutagen.Bethesda.FormKeys.SkyrimSE.Skyrim.Keyword.ClothingHead
                }))
                {
                    armo.Name = "DummyHelmet";
                }
                else if (CheckKeywords.HasAny(armo.Keywords, new()
                {
                    Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.Keyword.ArmorShield,
                    Mutagen.Bethesda.FormKeys.SkyrimSE.Skyrim.Keyword.ArmorShield
                }))
                {
                    armo.Name = "DummyShield";
                }
                else if (CheckKeywords.HasAny(armo.Keywords, new()
                {
                    Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.Keyword.ClothingCirclet,
                    Mutagen.Bethesda.FormKeys.SkyrimSE.Skyrim.Keyword.ClothingCirclet
                }))
                {
                    armo.Name = "DummyCirclet";
                }
                else if (CheckKeywords.HasAny(armo.Keywords, new()
                {
                    Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.Keyword.ClothingRing,
                    Mutagen.Bethesda.FormKeys.SkyrimSE.Skyrim.Keyword.ClothingRing
                }))
                {
                    armo.Name = "DummyRing";
                }
                else if (CheckKeywords.HasAny(armo.Keywords, new()
                {
                    Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.Keyword.ClothingNecklace,
                    Mutagen.Bethesda.FormKeys.SkyrimSE.Skyrim.Keyword.ClothingNecklace
                }))
                {
                    armo.Name = "DummyNecklace";
                }
                return armo;
            }
            else if (same_as<T, Book>())
            {
                Book book = (Book)record.DeepCopy();
                book.Name = "DummyBook";
                return book;
            }
            else if (same_as<T, Ingredient>())
            {
                Ingredient ingr = (Ingredient)record.DeepCopy();
                ingr.Name = "DummyIngredient";
                return ingr;
            }
            else if (same_as<T, MiscItem>())
            {
                MiscItem misc = (MiscItem)record.DeepCopy();
                if (misc.EditorID == null)
                    return misc;
                if (misc.Equals(Mutagen.Bethesda.FormKeys.SkyrimSE.Skyrim.MiscItem.Gold001) || misc.Equals(Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.MiscItem.Gold001))
                {
                    misc.Name = "DummySeptim";
                }
                else // referenced by a COBJ (constructible object) record
                {
                    misc.AsLink().;
                    foreach (var refer in state.LinkCache.ResolveAllContexts<MiscItem, IMiscItemGetter>(misc.FormKey))
                    {

                    }
                    misc.Name = "DummyResource";


                    {
                        misc.Name = "DummyClutter";
                    }
                }
                return misc;
            }
            return record;
        }

        public static async Task<int> Main(string[] args)
        {
            return await SynthesisPipeline.Instance
                .AddPatch<ISkyrimMod, ISkyrimModGetter>(RunPatch)
                .SetAutogeneratedSettings("Configuration", "settings.json", out _lazySettings)
                .SetTypicalOpen(GameRelease.SkyrimSE, "PovertyPatch.esp")
                .Run(args);
        }

        public static void RunPatch(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
        {
            int count = 0;

            // handle leveled items - LVLI records
            if (Settings.IsEnabled<LeveledItem>())
            {
                foreach (var lvli in state.LoadOrder.PriorityOrder.LeveledItem().WinningOverrides())
                {
                    if (!Settings.Filter[lvli])
                        continue;

                    var copy = lvli.DeepCopy();
                    int copyCount = count;

                    // do stuff

                    if (copyCount <= count)
                        continue;

                    state.PatchMod.LeveledItems.Set(copy);
                }
            }
            // handle items in containers - CONT records
            if (Settings.IsEnabled<ContainerEntry>())
            {
                foreach (var cont in state.LoadOrder.PriorityOrder.Container().WinningOverrides())
                {
                    if (cont.Items == null || cont.Items.Count == 0 || !Settings.Filter[cont])
                        continue;

                    int changeCount = 0;
                    Container copy = cont.DeepCopy();

                    foreach (var entry in copy.Items!)
                    {
                        if (Settings.Filter.CheckAndResolve(entry, state.LinkCache, out var link))
                        {
                            copy.Items.Remove(entry);
                            changeCount++;
                        }
                    }

                    if (changeCount == 0) // if the counter didn't increase, don't add to patch
                        continue;

                    state.PatchMod.Containers.Set(copy);

                    if (Settings.LogAll)
                        Console.WriteLine($"[{++count}]\t Finished processing items in container: \"{cont.EditorID ?? ""}\"");
                }
            }

            Console.WriteLine($"Processed {count} record{(count > 1 ? "s" : "")}.");
        }
    }
}
