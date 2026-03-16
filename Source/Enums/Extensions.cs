using System;
using Celeste;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.KongtiaoToolbox.Enums;
public static class Extensions
{

    public static KongtiaoToolboxModuleSettings Settings => KongtiaoToolboxModule.Settings;
    
    public static string ToLocalizedString(this Enum format) => Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_" + format.GetType().Name.ToUpper() + "_" + format.ToString());

    public static Vector2 ToVector2(this TextOverlayPosition position, string text) {
        return position switch {
            TextOverlayPosition.TopLeft => new Vector2(20, 20),
            TextOverlayPosition.TopRight => new Vector2(Engine.Width - ActiveFont.Measure(text).X * Settings.Size - 20, 20),
            TextOverlayPosition.BottomLeft => new Vector2(20, Engine.Height - ActiveFont.Measure(text).Y * Settings.Size - 20),
            TextOverlayPosition.BottomRight => new Vector2(Engine.Width - ActiveFont.Measure(text).X * Settings.Size - 20, Engine.Height - ActiveFont.Measure(text).Y * Settings.Size - 20),
            TextOverlayPosition.TopMiddle => new Vector2(Engine.Width / 2 - ActiveFont.Measure(text).X / 2 * Settings.Size, 20),
            TextOverlayPosition.BottomMiddle => new Vector2(Engine.Width / 2 - ActiveFont.Measure(text).X / 2 * Settings.Size, Engine.Height - ActiveFont.Measure(text).Y * Settings.Size - 20),
            _ => new Vector2(20, 20)
        };
    }
}