using System.Collections;
using UnityEngine;
using TaskWithCoroutine = MonoBehaviourWithTasks.TaskWithCoroutine;

namespace Centrifuge.Distance.Game
{
	public static class Task
	{
		static Task()
		{
			Status.Initialize();
		}

		public delegate IEnumerator TaskDelegate(Status status);

		private static SteamworksUGC UGC => G.Sys.SteamworksManager_.UGC_;

		private static Status TaskStatus;

		private static int taskCount_ = 0;
		public static int TaskCount
		{
			get => taskCount_;
			private set
			{
				if (value != taskCount_)
				{
					if (value == 0 && taskCount_ > 0)
					{
						UGC.DestroySteamProgressText();
					}
					else if (value > 0 && taskCount_ == 0)
					{
						UGC.SetupSteamProgressText();
					}
				}

				taskCount_ = value;
			}
		}

		public static void Run(TaskDelegate task)
		{
			TaskCount++;
			UGC.AddTask(new TaskWrapper(task(TaskStatus)));
		}

		public static TaskWithCoroutine Wait(float seconds)
		{
			return new WaitTask(seconds);
		}
		#region tasks
		public sealed class TaskWrapper : TaskWithCoroutine
		{
			private readonly IEnumerator coroutine;

			public TaskWrapper(IEnumerator coroutine) : base(UGC)
			{
				this.coroutine = coroutine;
			}

			public override IEnumerator UpdateCoroutine()
			{
				while (coroutine.MoveNext())
				{
					switch (coroutine.Current)
					{
						case IEnumerator enumerator:
							yield return new TaskWrapper(enumerator);
							break;
						default:
							yield return coroutine.Current;
							break;
					}
				}

				TaskCount--;
			}
		}

		private sealed class WaitTask : TaskWithCoroutine
		{
			private readonly float duration;

			public WaitTask(float seconds) : base(null)
			{
				duration = seconds;
			}

			public override IEnumerator UpdateCoroutine()
			{
				float totalTime = Time.deltaTime;

				while (totalTime <= duration && !cancel_)
				{
					totalTime += Time.deltaTime;
					yield return null;
				}
			}
		}
		#endregion

		public sealed class Status
		{
			public static void Initialize()
			{
				if (Equals(TaskStatus, null))
				{
					TaskStatus = new Status();
				}
			}

			private Status()
			{ }

			public void SetText(string value)
			{
				UGC.ProgressTextLabel_.text = value;
			}

			public void SetProgress(uint value, uint max)
			{
				UGC.progressBar_.value = Mathf.Clamp01(value / Mathf.Max(1, max));
			}
		}
	}
}
