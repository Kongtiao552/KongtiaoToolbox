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
    public float Scale;

    public Color color = Color.White;
    public Color outlineColor = Color.Black;

    public TextOverlay(string text = "", float scale = 1f) {
        this.Text = text;
        this.Scale = scale;
        Depth = -200;

        Tag = Tags.Global | Tags.HUD | Tags.PauseUpdate | Tags.TransitionUpdate;

        Position = Settings.TimeOverlayPosition.ToVector2(Text);
    }

    public override void Render() {
        ActiveFont.DrawOutline(Text, Position + Settings.TimeOverlayOffset, Vector2.Zero, Vector2.One * Scale, color, 2, outlineColor);
    }

}
