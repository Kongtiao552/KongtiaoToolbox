using System;
using System.Collections.Generic;
using System.Globalization;
using Celeste;
using Celeste.Mod.KongtiaoToolbox.Entities;
using Celeste.Mod.KongtiaoToolbox.Misc;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.KongtiaoToolbox;

public class KongtiaoToolboxModule : EverestModule {

    public static KongtiaoToolboxModule Instance { get; private set; }

    public override Type SettingsType => typeof(KongtiaoToolboxModuleSettings);
    public static KongtiaoToolboxModuleSettings ModSettings => (KongtiaoToolboxModuleSettings) Instance._Settings;

    public static TimeOverlay TimeOverlay { get; private set; }

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

    public void InitializeConstants() {
        // Initialize time zone info
        TimeZoneInfo localZone = TimeZoneInfo.Local;
        TimeSpan offset = localZone.BaseUtcOffset;
        double offsetHours = offset.TotalHours;
        string sign = offsetHours == 0d ? "" : offsetHours > 0d ? "+" : "-";
        offset = offset.Duration();
        Utils.timeZoneInfo = offsetHours == 0d ? $" (UTC)" : $" (UTC{sign}{offset})";
    }

    public override void Load() {
        InitializeConstants();

        Everest.Events.Level.OnLoadLevel += OnLoadLevel;
        Everest.Events.MainMenu.OnCreateButtons += MainMenu_OnCreateButtons;
        
        On.Monocle.Engine.Update += Engine_Update;

 
    }

    public override void Unload() {
        Everest.Events.Level.OnLoadLevel -= OnLoadLevel;
        Everest.Events.MainMenu.OnCreateButtons -= MainMenu_OnCreateButtons;

        On.Monocle.Engine.Update -= Engine_Update;
    }

    private static void Engine_Update(On.Monocle.Engine.orig_Update orig, Engine self, GameTime gameTime) {
        orig(self, gameTime);

        UpdateHotkeyPresses(self, gameTime);
    }

    private static void UpdateHotkeyPresses(Engine self, GameTime gameTime) {
        if (ModSettings.ToggleInGameMusic.Pressed) {
            if (Settings.Instance.MusicVolume == 0) {
                Utils.SetInGameMusicVolume(ModSettings.DefaultInGameMusicVolume);
            } else {
                Utils.SetInGameMusicVolume(0);
            }

            if (ModSettings.ToggleExternalMediaPlayers) {
                Utils.ToggleExternalMediaPlayers();
            }
        }

        if (Engine.Scene is not Level) {
            return;
        }

        if (ModSettings.ToggleTimeOverlay.Pressed) {
            ModSettings.ShowRealTimeOverlay = !ModSettings.ShowRealTimeOverlay;
            TimeOverlay.Visible = ModSettings.ShowRealTimeOverlay;
        }
    }

    private static void MainMenu_OnCreateButtons(OuiMainMenu menu, List<MenuButton> buttons) {
        Utils.CultureInfo = new CultureInfo("KONGTIAO_TOOLBOX_CULTURE_CODE".DialogCleanOrNull() ?? "en-us");

        Utils.Log($"CultureInfo Initialized: {Utils.CultureInfo.Name}");
    }

    private static void OnLoadLevel(Level level, Player.IntroTypes playerIntro, bool isFromLoader) {
        if (isFromLoader) {
            TimeOverlay = new TimeOverlay();
            level.Add(TimeOverlay);
            Utils.Log("Time overlay added");
        }
    }
}