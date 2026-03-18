using Microsoft.Xna.Framework;
using Celeste.Mod.KongtiaoToolbox.Entities;
using System;
using Celeste.Mod.KongtiaoToolbox.Enums;
using System.Reflection;

namespace Celeste.Mod.KongtiaoToolbox.Entities;
public class TimeOverlay : TextOverlay {

    public static string timeZoneInfo = $" (UTC+{TimeZoneInfo.Local.BaseUtcOffset})";

    public static string dateString {
        get {
            string rawDateString = Settings.DateFormat == DateFormat.LONG ? DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() : DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            return KongtiaoToolboxModule.Settings.ShowTimeZone ? rawDateString + timeZoneInfo : rawDateString;
        }
    }
    
    public TimeOverlay() : base(dateString) {
    }

    public override void Update() {
        if (Visible) {
            Text = dateString;
        }
    }
}