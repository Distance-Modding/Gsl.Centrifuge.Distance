using HarmonyLib;
using System.Linq;
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

            if (group && !group.gameObject.GetChildren().Any())
            {
                group.inspectChildren_ = Group.InspectChildrenType.None;
            }
        }
    }
}
