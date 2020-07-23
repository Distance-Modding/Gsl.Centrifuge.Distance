using Reactor.API.Attributes;
using Reactor.API.Interfaces.Systems;
using Reactor.API.Runtime.Patching;
using Reactor.API.Logging;
using System;
using UnityEngine;
using Centrifuge.Distance.Systems.ExportedTypes;
using LevelEditorTools;
using Centrifuge.Distance.GUI.Data;
using Centrifuge.Distance.GUI.Menu;

namespace Centrifuge.Distance
{
    [GameSupportLibraryEntryPoint("com.github.reherc/Centrifuge.Distance")]
    internal sealed class GameAPI : MonoBehaviour
    {
        internal static GameAPI Instance { get; set; }

        internal IManager manager_;

        internal Log logger_;

        public void Initialize(IManager manager)
        {
            DontDestroyOnLoad(gameObject);

            logger_ = LogManager.GetForCurrentAssembly();

            Instance = this;

            manager_ = manager;

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
            TypeExportManager.Register<LevelEditorTool>();
            TypeExportManager.Register<AddedComponent>();
        }

        private void CreateSettingsMenu()
        {
            MenuTree settingsMenu = new MenuTree("menu.gsl.distance", InternalResources.Strings.Settings.Gsl.MenuTitle);

            settingsMenu.CheckBox(MenuDisplayMode.Both, "setting:show_version_info", InternalResources.Strings.Settings.Gsl.ShowVersionInfo, () => true, (_) => { }, true, InternalResources.Strings.Settings.Gsl.ShowVersionInfoDescription);

            MenuSystem.MenuTree.SubmenuButton(MenuDisplayMode.Both, "navigate:menu.gsl.distance", InternalResources.Strings.Settings.Gsl.MenuTitle.ToUpper(), settingsMenu, InternalResources.Strings.Settings.Gsl.MenuDescription);
        }
    }
}