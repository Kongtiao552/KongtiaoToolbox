using System;
using Microsoft.Xna.Framework;
using Monocle;
using Celeste.Mod.KongtiaoToolbox.Misc;
using static Celeste.TextMenu;

namespace Celeste.Mod.KongtiaoToolbox.Entities.Menu;

public class ColorOption : Item {

    private static string ConfirmSfx = "event:/ui/main/button_select";

    private string _Label;

    public string Label { get; set; }

    public Color Color { get; set; }

    public Action<Color> OnValueChange { get; set; }

    public ColorOption(string label, Color color) {
        Selectable = true;
        Color = color;
        _Label = label;

        Label = GetLabel(_Label, Color);
    }

    public override float LeftWidth() {
        return ActiveFont.Measure(Label).X;
    }

    public override float Height() {
        return ActiveFont.LineHeight;
    }

    public override void ConfirmPressed() {
        Audio.Play(ConfirmSfx);

        if (Utils.TryParseColor(TextInput.GetClipboardText(), out Color color)) {
            Color = color;
            Label = GetLabel(_Label, Color);
            OnValueChange?.Invoke(color);
        }
    }

    public override void Render(Vector2 position, bool highlighted) {
        // Stolen from Celeste.TextMenu
        float alpha = Container.Alpha;
        Color color = Disabled ? Color.DarkSlateGray : ((highlighted ? Container.HighlightColor : Color.White) * alpha);
        Color strokeColor = Color.Black * (alpha * alpha * alpha);
        ActiveFont.DrawOutline(Label, position, Vector2.UnitY / 2f, Vector2.One, color, 2f, strokeColor);

        // Render the color box
        Vector2 measure = ActiveFont.Measure(Label);
        Vector2 colorBoxPosition = position + new Vector2(measure.X + 10f, -20f);
        Draw.Rect(colorBoxPosition, 40f, 40f, Color);
        Draw.HollowRect(colorBoxPosition, 40f, 40f, Color.White);
    }

    private static string GetLabel(string label, Color color) {
        return $"{label}: {Utils.ColorToHex(color)}";
    }

}