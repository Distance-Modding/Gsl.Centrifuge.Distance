using HarmonyLib;
using UnityEngine;

namespace Centrifuge.Distance.Systems.EditorInspector.Harmony
{
    [HarmonyPatch(typeof(NGUIObjectInspectorTabAbstract), "CreateComponentInspectorsOnObject")]
    internal class NGUIObjectInspectorTabAbstract__CreateComponentInspectorsOnObject
    {
        [HarmonyPrefix]
        internal static void Prefix(GameObject obj)
        {
            Group group = obj.GetComponent<Group>();

            if (group)
            {
                group.inspectChildren_ = Group.InspectChildrenType.None;
            }
        }
    }
}
