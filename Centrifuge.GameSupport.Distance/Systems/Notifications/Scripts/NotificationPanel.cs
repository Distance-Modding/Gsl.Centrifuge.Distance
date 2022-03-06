using Centrifuge.Distance.Notifications;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Centrifuge.Distance.Notifications.Scripts
{
	internal class NotificationPanel : MonoBehaviour
	{
		public static NotificationPanel Instance { get; protected set; }
		#region Fields and Properties
		protected NotificationPrefabData prefabData;
		#region Notification Queue
		internal static Queue<NotificationBase> allNotifications = new Queue<NotificationBase>();
		internal static Queue<NotificationBase> menuNotifications = new Queue<NotificationBase>();
		internal static NotificationBase currentNotification;
		#endregion
		#region Script Fields
		protected float hideTime = -1f;
		protected static bool ReadMenuNotifications => GameManager.IsInMainMenuScene_;
		#endregion
		#endregion

		public void Show(NotificationBase notification, bool menuOnly = true)
		{
			if (IsReadyToShow(menuOnly))
			{
				Display(notification);
			}
			else
			{
				(menuOnly ? menuNotifications : allNotifications).Enqueue(notification);
			}
		}

		protected internal void Display(NotificationBase notification)
		{
			hideTime = Time.unscaledTime + notification.Duration;
			currentNotification = notification;
			gameObject.SetActive(true);
			notification.Prepare(prefabData);
			notification.Display(prefabData);
		}

		internal static bool IsReadyToShow(bool mustRead = false)
		{
			if (!Instance || !Instance.gameObject.activeSelf) return false;
			return !mustRead || ReadMenuNotifications;
		}

		protected internal void ShowNextOrHide()
		{
			if (ReadMenuNotifications && menuNotifications.Count > 0)
			{
				HandleNotification(menuNotifications);
			}
			else if (allNotifications.Count > 0)
			{
				HandleNotification(allNotifications);
			}
			else
			{
				gameObject.SetActive(false);
			}
		}

		protected void HandleNotification(Queue<NotificationBase> notificationQueue)
		{
			currentNotification = notificationQueue.Dequeue();

			if (currentNotification is VanillaNotification notification && notification.Type == NotificationType.Levels)
			{
				List<NotificationBase> levelNotifications = notificationQueue
					.OfType<VanillaNotification>()
					.Where(n => n.Type == NotificationType.Levels)
					.Cast<NotificationBase>()
					.ToList();

				if (levelNotifications.Count > 0)
				{
					notification.Title = "Levels Unlocked";
					notification.Description = $"{notification.Description} [88](and {levelNotifications.Count} more)[-]";
					notificationQueue.RemoveAll(n => !levelNotifications.Contains(n));
				}
			}

			Display(currentNotification);
		}

		protected void Setup(NotificationBox baseScript)
		{
			prefabData = new NotificationPrefabData
			{
				PrefabObject = baseScript.panel_.gameObject,
				Panel = baseScript?.panel_,
				Icon = baseScript?.icon_,
				Title = baseScript?.title_,
				Description = baseScript?.description_
			};
		}

		#region Unity Scripting Events 
		public void Awake()
		{
			if (Instance && Instance.isActiveAndEnabled)
			{
				Destroy(this);
				return;
			}

			Setup(gameObject.GetComponent<NotificationBox>());
			Instance = this;
		}

		public void Update()
		{
			prefabData.Panel.alpha = 1;

			float alpha = Mathf.Clamp01(hideTime - Time.unscaledTime);
			prefabData.Panel.alpha = alpha;

			if (alpha > 0f) return;

			currentNotification?.Reset(prefabData);
			currentNotification = null;

			ShowNextOrHide();
		}
		#endregion
	}
}
