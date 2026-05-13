using Microsoft.Xna.Framework;
using System;
using Celeste.Mod.KongtiaoToolbox.Enums;
using Monocle;
using Celeste.Mod.KongtiaoToolbox.Misc;

namespace Celeste.Mod.KongtiaoToolbox.Entities;
public class TimeOverlay : Entity {
    
    private static KongtiaoToolboxModuleSettings ModSettings => KongtiaoToolboxModule.ModSettings;
    private static KongtiaoToolboxModule Module => KongtiaoToolboxModule.Instance;

    private string Text;
    private Vector2 Justify;

    public bool Outline = true;
    public float Scale;
    public float Opacity;

    public Color Color;
    public Color OutlineColor = Color.Black;
    public float OutlineThickness = 2f;

    public TimeOverlay() {
        Depth = -200;
        Tag = Tags.Global | Tags.HUD | Tags.FrozenUpdate | Tags.PauseUpdate | Tags.TransitionUpdate;

        ApplyModSettings();
    }

    private void ApplyModSettings() {
        Color = ModSettings.TimeOverlayColor;
        OutlineColor = ModSettings.TimeOverlayOutlineColor;
        Outline = ModSettings.TimeOverlayOutline;
        Scale = ModSettings.TimeOverlaySize;
        Opacity = ModSettings.TimeOverlayOpacity;

        SetPosition(ModSettings.TimeOverlayPosition, ModSettings.TimeOverlayXOffset, ModSettings.TimeOverlayYOffset);
    }

    public void SetPosition(TimeOverlayPosition position, float xOffset, float yOffset) {
        switch (position) {
            case TimeOverlayPosition.TopLeft:
                Justify = new Vector2(0f, 0f);
                Position = new Vector2(xOffset, yOffset);
                break;

            case TimeOverlayPosition.TopMiddle:
                Justify = new Vector2(0.5f, 0f);
                Position = new Vector2(Engine.Width / 2f + xOffset, yOffset);
                break;      

            case TimeOverlayPosition.TopRight:
                Justify = new Vector2(1f, 0f);
                Position = new Vector2(Engine.Width - xOffset, yOffset);
                break;      

            case TimeOverlayPosition.BottomLeft:
                Justify = new Vector2(0f, 1f);
                Position = new Vector2(xOffset, Engine.Height - yOffset);
                break;

            case TimeOverlayPosition.BottomMiddle:
                Justify = new Vector2(0.5f, 1f);
                Position = new Vector2(Engine.Width / 2f + xOffset, Engine.Height - yOffset);
                break;      

            case TimeOverlayPosition.BottomRight:
                Justify = new Vector2(1f, 1f);
                Position = new Vector2(Engine.Width - xOffset, Engine.Height - yOffset);
                break;     
        }
    }

    public override void Render() {
        if (Outline) {
            ActiveFont.DrawOutline(Text, Position, Justify, Vector2.One * Scale, Color * Opacity, OutlineThickness, OutlineColor * Opacity);
        } else {
            ActiveFont.Draw(Text, Position, Justify, Vector2.One * Scale, Color * Opacity);
        }
        
    }

    public override void Update() {
        if (!Visible) {
            return;
        }

        Text = GetDateTimeString();
    }

    private static string GetDateTimeString() {
        string rawDateString = ModSettings.DateFormat == DateFormat.LONG ? Utils.GetLongDateTimeString() : Utils.GetShortDateTimeString();
        return ModSettings.ShowTimeZone ? rawDateString + Utils.timeZoneInfo : rawDateString;
    }
}