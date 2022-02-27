using HarmonyLib;
using Centrifuge.Distance.Notifications.Scripts;

namespace Centrifuge.Distance.Notifications.Harmony
{
	//[HarmonyPatch(typeof(NotificationBox), "Awake")]
	internal static class NotificationBox__Awake
	{
		[HarmonyPostfix]
		internal static void Postfix(NotificationBox __instance)
		{
			__instance.gameObject.AddComponent<NotificationPanel>();
		}
	}
}
