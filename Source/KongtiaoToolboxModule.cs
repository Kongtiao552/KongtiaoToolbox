using System;
using System.Collections.Generic;
using System.Globalization;
using Celeste;
using Celeste.Mod.KongtiaoToolbox.Entities;
using Celeste.Mod.KongtiaoToolbox.Enums;
using Celeste.Mod.KongtiaoToolbox.Misc;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.KongtiaoToolbox;

public class KongtiaoToolboxModule : EverestModule {
    public static KongtiaoToolboxModule Instance { get; private set; }

    public override Type SettingsType => typeof(KongtiaoToolboxModuleSettings);
    public static KongtiaoToolboxModuleSettings ModSettings => (KongtiaoToolboxModuleSettings) Instance._Settings;

    public TimeOverlay TimeOverlay { get; set; }

    public KongtiaoToolboxModule() {
        Instance = this;
#if DEBUG
        // debug builds use verbose logging
        Logger.SetLogLevel(nameof(KongtiaoToolboxModule), LogLevel.Verbose);
#else
        // release builds use info logging to reduce spam in log files
        Logger.SetLogLevel(nameof(KongtiaoToolboxModule), LogLevel.Info);
#endif
    }

    public override void Load() {
        Everest.Events.Level.OnLoadLevel += OnLoadLevel;
        Everest.Events.MainMenu.OnCreateButtons += MainMenu_OnCreateButtons;
        
        On.Monocle.Engine.Update += Engine_Update;
    }

    public override void Unload() {
        Everest.Events.Level.OnLoadLevel -= OnLoadLevel;
        Everest.Events.MainMenu.OnCreateButtons -= MainMenu_OnCreateButtons;

        On.Monocle.Engine.Update -= Engine_Update;
    }

    private void Engine_Update(On.Monocle.Engine.orig_Update orig, Engine self, GameTime gameTime) {
        orig(self, gameTime);

        if (self.scene is Level level && !level.Paused) {
            UpdateHotkeyPresses(self, gameTime);
        }
    }

    private void UpdateHotkeyPresses(Engine self, GameTime gameTime) {
        if (ModSettings.ToggleTimeOverlay.Pressed) {
            ModSettings.ShowRealTimeOverlay = !ModSettings.ShowRealTimeOverlay;
            TimeOverlay.UpdateSettings();
        }

        if (ModSettings.ToggleInGameMusic.Pressed) {
            if (Settings.Instance.MusicVolume == 0) {
                Utils.ChangeInGameMusicVolume(ModSettings.DefaultInGameMusicVolume);
            } else {
                Utils.ChangeInGameMusicVolume(0);
            }

            if (ModSettings.ToggleExternalMediaPlayers) Utils.ToggleExternalMediaPlayers();
        }
    }

    private void MainMenu_OnCreateButtons(OuiMainMenu menu, List<MenuButton> buttons) {
        Utils.CultureInfo = new CultureInfo("KONGTIAO_TOOLBOX_CULTURE_CODE".DialogCleanOrNull() ?? "en-us");

        Utils.Log($"CultureInfo Initialized: {Utils.CultureInfo.Name}");
    }

    private void OnLoadLevel(Level level, Player.IntroTypes playerIntro, bool isFromLoader) {
        if (isFromLoader) {
            TimeOverlay = new TimeOverlay();
            level.Add(TimeOverlay);
            Utils.Log("Time overlay added");
        }
    }
}