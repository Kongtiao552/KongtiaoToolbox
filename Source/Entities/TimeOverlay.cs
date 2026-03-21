using Microsoft.Xna.Framework;
using Celeste.Mod.KongtiaoToolbox.Entities;
using System;
using Celeste.Mod.KongtiaoToolbox.Enums;
using System.Reflection;
using Celeste.Mod.KongtiaoToolbox.Misc;

namespace Celeste.Mod.KongtiaoToolbox.Entities;
public class TimeOverlay : TextOverlay {
    
    public TimeOverlay() : base(GetDateString()) {
        UpdateSettings();
    }

    public static string GetDateString() {
        string rawDateString = ModSettings.DateFormat == DateFormat.LONG ? Utils.GetLongDateString() : Utils.GetShortDateString();
        return ModSettings.ShowTimeZone ? rawDateString + Utils.timeZoneInfo : rawDateString;
    }


    public void UpdateSettings() {
        UpdateSettings(
            ModSettings.TimeOverlaySize,
            ModSettings.ShowRealTimeOverlay,
            ModSettings.TimeOverlayColor,
            ModSettings.TimeOverlayOutline,
            ModSettings.TimeOverlayOutlineColor,
            ModSettings.TimeOverlayTransparency,
            GetDateString()
        );
    }

    public override void Update() {
        if (Visible) {
            Text = GetDateString();
        }
    }
}