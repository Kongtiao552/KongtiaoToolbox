using System;
using Celeste;
using Monocle;
using Microsoft.Xna.Framework;
using Celeste.Mod.KongtiaoToolbox.Enums;
using System.Reflection;

namespace Celeste.Mod.KongtiaoToolbox.Entities;

public class TextOverlay : Entity {

    public static KongtiaoToolboxModuleSettings ModSettings => KongtiaoToolboxModule.ModSettings;
    public KongtiaoToolboxModule Module => KongtiaoToolboxModule.Instance;
    public string Text;
    public bool Outline = true;
    public float Scale;
    public float Transparency = 1f;

    public Color Color = Color.White;
    public Color OutlineColor = Color.Black;
    public int OutlineThickness = 2;

    public TextOverlay(string text = "", float scale = 1f) {
        this.Text = text;
        this.Scale = scale;
        Depth = -200;

        Tag = Tags.Global | Tags.HUD | Tags.FrozenUpdate | Tags.PauseUpdate | Tags.TransitionUpdate;

        Position = ModSettings.TimeOverlayPosition.ToVector2(Text);
    }

    public override void Render() {
        if (Outline) {
            ActiveFont.DrawOutline(Text, Position + ModSettings.TimeOverlayOffset, Vector2.Zero, Vector2.One * Scale, Color * Transparency, OutlineThickness, OutlineColor * Transparency);
        } else {
            ActiveFont.Draw(Text, Position + ModSettings.TimeOverlayOffset, Vector2.Zero, Vector2.One * Scale, Color * Transparency);
        }
        
    }

    public void UpdateSettings(float scale, bool visible, Color color, bool outline, Color outlineColor, float transparency, string text) {
        Scale = ModSettings.TimeOverlaySize;
        Visible = ModSettings.ShowRealTimeOverlay;
        Color = ModSettings.TimeOverlayColor;
        Outline = ModSettings.TimeOverlayOutline;
        OutlineColor = ModSettings.TimeOverlayOutlineColor;
        Transparency = ModSettings.TimeOverlayTransparency;

        Position = ModSettings.TimeOverlayPosition.ToVector2(text);
    }

}
