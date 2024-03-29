﻿#pragma warning disable IDE0051, RCS1213
using System;
using Centrifuge.Distance.Configuration;
using Centrifuge.Distance.EditorScripts.Attributes;
using Centrifuge.Distance.EditorTools.Attributes;
using Centrifuge.Distance.Game;
using Centrifuge.Distance.GUI.Data;
using Centrifuge.Distance.GUI.Menu;
using Centrifuge.Distance.Notifications;
using Centrifuge.Distance.Systems.ExportedTypes;
using LevelEditorTools;
using Reactor.API.Attributes;
using Reactor.API.Interfaces.Systems;
using Reactor.API.Logging;
using Reactor.API.Runtime.Patching;
using UnityEngine;

namespace Centrifuge.Distance
{
	[GameSupportLibraryEntryPoint(GSL_ID)]
	internal sealed class GameAPI : MonoBehaviour
	{
		public const string GSL_ID = "com.github.reherc/Centrifuge.Distance";

		internal static GameAPI Instance { get; set; }

		internal IManager Manager;

		internal Log Logger;

		internal Config Config;

		public void Initialize(IManager manager)
		{
			DontDestroyOnLoad(gameObject);

			Logger = LogManager.GetForCurrentAssembly();

			Instance = this;

			Manager = manager;

			Config = new Config();

			RegisterExportedTypes();
			CreateSettingsMenu();

			try
			{
				RuntimePatcher.AutoPatch();
				RuntimePatcher.RunTranspilers();
			}
			catch (Exception e)
			{
				Logger.Error("Failed to initialize harmony. Mods will still be loaded, but may not function correctly.");
				Logger.Exception(e);
			}
		}

		private void FixedUpdate()
		{
			Task.FixedUpdate();
		}

		private void RegisterExportedTypes()
		{
			TypeExportManager.Register<ISerializable>();
			TypeExportManager.Register<LevelEditorTool>((type) => type.HasAttribute<EditorToolAttribute>());
			TypeExportManager.Register<AddedComponent>((type) => type.HasAttribute<EditorScriptAttribute>());
		}

		private void CreateSettingsMenu()
		{
			MenuTree settingsMenu = new MenuTree("menu.gsl.distance", InternalResources.Strings.Settings.Gsl.MenuTitle);

			settingsMenu.CheckBox(MenuDisplayMode.Both, "setting:show_version_info", InternalResources.Strings.Settings.Gsl.ShowVersionInfo, () => Config.ShowVersionInfo, (value) => Config.ShowVersionInfo = value, InternalResources.Strings.Settings.Gsl.ShowVersionInfoDescription);

			MenuSystem.MenuTree.SubmenuButton(MenuDisplayMode.Both, "navigate:menu.gsl.distance", InternalResources.Strings.Settings.Gsl.MenuTitle.ToUpper(), settingsMenu, InternalResources.Strings.Settings.Gsl.MenuDescription);
		}
	}
}