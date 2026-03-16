using System;
using Celeste;
using Celeste.Mod.KongtiaoToolbox.Entities;
using Celeste.Mod.KongtiaoToolbox.Enums;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.KongtiaoToolbox;

public class KongtiaoToolboxModule : EverestModule {
    public static KongtiaoToolboxModule Instance { get; private set; }

    public override Type SettingsType => typeof(KongtiaoToolboxModuleSettings);
    public static KongtiaoToolboxModuleSettings Settings => (KongtiaoToolboxModuleSettings) Instance._Settings;

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
        
        On.Monocle.Engine.Update += Engine_Update;
    }

    public override void Unload() {
        Everest.Events.Level.OnLoadLevel -= OnLoadLevel;

        On.Monocle.Engine.Update -= Engine_Update;
    }

    private void Engine_Update(On.Monocle.Engine.orig_Update orig, Engine self, GameTime gameTime) {
        orig(self, gameTime);

        UpdateHotkeyPresses(self, gameTime);
    }

    private void UpdateHotkeyPresses(Engine self, GameTime gameTime) {
        if (Settings.ToggleTimeOverlay.Pressed) {
            Settings.ShowRealTimeOverlay = !Settings.ShowRealTimeOverlay;
            TimeOverlay?.Visible = Settings.ShowRealTimeOverlay;
        }
    }

    private void OnLoadLevel(Level level, Player.IntroTypes playerIntro, bool isFromLoader) {
        if (isFromLoader) {
            TimeOverlay = new TimeOverlay();
            TimeOverlay.Scale = Settings.Size;
            TimeOverlay.Visible = Settings.ShowRealTimeOverlay;
            level.Add(TimeOverlay);
        }
    }
}