using System;
using Celeste;
using Monocle;
using Microsoft.Xna.Framework;
using Celeste.Mod.KongtiaoToolbox.Enums;
using System.Reflection;

namespace Celeste.Mod.KongtiaoToolbox.Entities;

public class TextOverlay : Entity {

    public static KongtiaoToolboxModuleSettings Settings => KongtiaoToolboxModule.Settings;
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

        Tag = Tags.Global | Tags.HUD | Tags.PauseUpdate | Tags.TransitionUpdate;

        Position = Settings.TimeOverlayPosition.ToVector2(Text);
    }

    public override void Render() {
        if (Outline) {
            ActiveFont.DrawOutline(Text, Position + Settings.TimeOverlayOffset, Vector2.Zero, Vector2.One * Scale, Color * Transparency, OutlineThickness, OutlineColor * Transparency);
        } else {
            ActiveFont.Draw(Text, Position + Settings.TimeOverlayOffset, Vector2.Zero, Vector2.One * Scale, Color * Transparency);
        }
    }

}
