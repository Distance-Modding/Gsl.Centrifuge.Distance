using HarmonyLib;
using System;
using System.Linq;

namespace Centrifuge.Distance.Systems.EditorInspector.Harmony
{
    [HarmonyPatch(typeof(NGUIObjectInspectorTabAbstract), "CreateISerializableInspector", new Type[2] { typeof(ISerializable), typeof(bool) })]
    internal class NGUIObjectInspectorTabAbstract__CreateISerializableInspector
    {
        [HarmonyPrefix]
        internal static bool Prefix(ISerializable serializable)
        {
            if (serializable is Group group && !group.gameObject.GetChildren().Any())
            {
                group.inspectChildren_ = Group.InspectChildrenType.None;

                return false;
            }

            return true;
        }
    }
}
