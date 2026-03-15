using System;
using Celeste.Mod.KongtiaoToolbox;

namespace Celeste.Mod.KongtiaoToolbox;

public class KongtiaoToolboxModModule : EverestModule {
    public static KongtiaoToolboxModModule Instance { get; private set; }

    public override Type SettingsType => typeof(KongtiaoToolboxModModuleSettings);
    public static KongtiaoToolboxModModuleSettings Settings => (KongtiaoToolboxModModuleSettings) Instance._Settings;

    public override Type SessionType => typeof(KongtiaoToolboxModuleSession);
    public static KongtiaoToolboxModuleSession Session => (KongtiaoToolboxModuleSession) Instance._Session;

    public override Type SaveDataType => typeof(KongtiaoToolboxModuleSaveData);
    public static KongtiaoToolboxModuleSaveData SaveData => (KongtiaoToolboxModuleSaveData) Instance._SaveData;

    public KongtiaoToolboxModModule() {
        Instance = this;
#if DEBUG
        // debug builds use verbose logging
        Logger.SetLogLevel(nameof(KongtiaoToolboxModModule), LogLevel.Verbose);
#else
        // release builds use info logging to reduce spam in log files
        Logger.SetLogLevel(nameof(KongtiaoToolboxModModule), LogLevel.Info);
#endif
    }

    public override void Load() {
        
    }

    public override void Unload() {
        
    }
}