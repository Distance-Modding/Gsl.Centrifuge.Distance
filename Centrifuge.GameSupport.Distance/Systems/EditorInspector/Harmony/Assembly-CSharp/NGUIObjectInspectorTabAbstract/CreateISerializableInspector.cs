using HarmonyLib;
using System;

namespace Centrifuge.Distance.Systems.EditorInspector.Harmony
{
	[HarmonyPatch(typeof(NGUIObjectInspectorTabAbstract), "CreateISerializableInspector", new Type[2] { typeof(ISerializable), typeof(bool) })]
    internal static class NGUIObjectInspectorTabAbstract__CreateISerializableInspector
    {
        [HarmonyPrefix]
        internal static bool Prefix(ISerializable serializable)
        {
            if (serializable is Group group && group.gameObject.GetChildren().Length == 0)
            {
                group.inspectChildren_ = Group.InspectChildrenType.None;

                return false;
            }

            return true;
        }
    }
}
