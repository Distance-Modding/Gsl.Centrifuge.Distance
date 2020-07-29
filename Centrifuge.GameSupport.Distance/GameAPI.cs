using Centrifuge.Distance.Configuration;
using Centrifuge.Distance.EditorScripts.Attributes;
using Centrifuge.Distance.EditorTools.Attributes;
using Centrifuge.Distance.GUI.Data;
using Centrifuge.Distance.GUI.Menu;
using Centrifuge.Distance.Systems.ExportedTypes;
using LevelEditorTools;
using Reactor.API.Attributes;
using Reactor.API.Interfaces.Systems;
using Reactor.API.Logging;
using Reactor.API.Runtime.Patching;
using System;
using UnityEngine;

namespace Centrifuge.Distance
{
    [GameSupportLibraryEntryPoint(GSL_ID)]
    internal sealed class GameAPI : MonoBehaviour
    {
        public const string GSL_ID = "com.github.reherc/Centrifuge.Distance";

        internal static GameAPI Instance { get; set; }

        internal IManager manager_;

        internal Log logger_;

        internal Config config_;

        public void Initialize(IManager manager)
        {
            DontDestroyOnLoad(gameObject);

            logger_ = LogManager.GetForCurrentAssembly();

            Instance = this;

            manager_ = manager;

            config_ = new Config();

            RegisterExportedTypes();
            CreateSettingsMenu();

            try
            {
                RuntimePatcher.AutoPatch();
                RuntimePatcher.RunTranspilers();
            }
            catch (Exception e)
            {
                logger_.Error("Failed to initialize harmony. Mods will still be loaded, but may not function correctly.");
                logger_.Exception(e);
            }
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

            settingsMenu.CheckBox(MenuDisplayMode.Both, "setting:show_version_info", InternalResources.Strings.Settings.Gsl.ShowVersionInfo, () => config_.ShowVersionInfo, (value) => config_.ShowVersionInfo = value, InternalResources.Strings.Settings.Gsl.ShowVersionInfoDescription);

            MenuSystem.MenuTree.SubmenuButton(MenuDisplayMode.Both, "navigate:menu.gsl.distance", InternalResources.Strings.Settings.Gsl.MenuTitle.ToUpper(), settingsMenu, InternalResources.Strings.Settings.Gsl.MenuDescription);
        }
    }
}