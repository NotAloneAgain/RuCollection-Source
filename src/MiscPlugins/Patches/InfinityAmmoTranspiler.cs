using System.Collections.Generic;
using HarmonyLib;
using InventorySystem;
using System.Reflection.Emit;
using NorthwoodLib.Pools;

namespace MiscPlugins.Patches
{
    [HarmonyPatch(typeof(InventoryExtensions), nameof(InventoryExtensions.ServerDropAmmo))]
    internal static class InfinityAmmoTranspiler
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instr)
        {
            var instructions = ListPool<CodeInstruction>.Shared.Rent(instr);

            const int index = 0;

            instructions.InsertRange(index, new CodeInstruction[2]
            {
                new CodeInstruction(OpCodes.Ldc_I4_0),
                new CodeInstruction(OpCodes.Ret)
            });

            for (int i = 0; i < instructions.Count; i++)
            {
                yield return instructions[i];
            }

            ListPool<CodeInstruction>.Shared.Return(instructions);
        }
    }
}