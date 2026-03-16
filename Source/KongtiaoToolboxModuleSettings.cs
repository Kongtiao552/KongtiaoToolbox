using System;
using Monocle;
using Celeste;
using Celeste.Mod.KongtiaoToolbox.Entities;
using Celeste.Mod.KongtiaoToolbox.Enums;
using Celeste.Mod.KongtiaoToolbox.Menu;
using Microsoft.Xna.Framework;
using KongtiaoToolbox.Menu;
using Microsoft.Xna.Framework.Graphics;

namespace Celeste.Mod.KongtiaoToolbox;

[SettingName("MODOPTION_KONGTIAO_TOOLBOX_MODULE_TITLE")]
public class KongtiaoToolboxModuleSettings : EverestModuleSettings {

    [SettingIgnore]
    public static KongtiaoToolboxModule Module => KongtiaoToolboxModule.Instance;

    [SettingIgnore]
    public bool ShowRealTimeOverlay { get; set; }
    [SettingIgnore]
    public bool ShowTimeZone { get; set; }
    [SettingIgnore]
    public DateFormat DateFormat { get; set; }
    [SettingIgnore]
    public TextOverlayPosition TimeOverlayPosition { get; set; }
    [SettingIgnore]
    public int TimeOverlayXOffset { get; set; }
    [SettingIgnore]
    public int TimeOverlayYOffset { get; set; }
    [SettingIgnore]
    public float Size { get; set; } = 1f;
    [SettingIgnore]
    public Vector2 TimeOverlayOffset => new Vector2(TimeOverlayXOffset, -TimeOverlayYOffset);

    public bool TimeOverlayOption { get; set; }

    public static string IntToString(int v) => v.ToString();
    public static string FloatToPercentString(float v) => v.ToString("P0");

    public void CreateTimeOverlayOptionEntry(TextMenu menu, bool inGame) {
        TextMenuExt.SubMenu subMenu = new TextMenuExt.SubMenu(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_REAL_TIME_OVERLAY"), false);

        TextMenu.OnOff timeOverlayOption = new TextMenu.OnOff(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_SHOW_REAL_TIME_OVERLAY"), ShowRealTimeOverlay);
        TextMenu.OnOff showTimeZoneOption = new TextMenu.OnOff(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_SHOW_TIME_ZONE"), ShowTimeZone);

        float[] sizeOptions = [0.05f, 0.1f, 0.15f, 0.20f, 0.25f, 0.30f, 0.35f, 0.40f, 0.45f, 0.50f, 0.55f, 0.60f, 0.65f, 0.70f, 0.75f, 0.80f, 0.85f, 0.90f, 0.95f, 1.0f];

        CustomFloatSlider sizeOption = new CustomFloatSlider(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TIME_OVERLAY_SIZE"), FloatToPercentString, Size, sizeOptions);
        
        CustomEnumSlider<DateFormat> dateFormatOption = new CustomEnumSlider<DateFormat>(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_DATE_FORMAT"), DateFormat);
        CustomEnumSlider<TextOverlayPosition> positionOption = new CustomEnumSlider<TextOverlayPosition>(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TIME_OVERLAY_POSITION"), TimeOverlayPosition);

        CustomIntSlider timeOverlayXOffsetOption = new CustomIntSlider(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TIME_OVERLAY_X_OFFSET"), IntToString, -500, 500, TimeOverlayXOffset, 20);
        CustomIntSlider timeOverlayYOffsetOption = new CustomIntSlider(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TIME_OVERLAY_Y_OFFSET"), IntToString, -500, 500, TimeOverlayYOffset, 20);

        timeOverlayOption.OnValueChange = value => {
            ShowRealTimeOverlay = value;
            Module.TimeOverlay?.Visible = value;
        };

        showTimeZoneOption.OnValueChange = value => {
            ShowTimeZone = value;
            Module.TimeOverlay?.Position = TimeOverlayPosition.ToVector2(TimeOverlay.dateString);
        };

        dateFormatOption.OnValueChange = value => {
            DateFormat = value;
            Module.TimeOverlay?.Position = TimeOverlayPosition.ToVector2(TimeOverlay.dateString);
        };

        positionOption.OnValueChange = value => {
            TimeOverlayPosition = value;
            Module.TimeOverlay?.Position = TimeOverlayPosition.ToVector2(TimeOverlay.dateString);
        };

        sizeOption.OnValueChange = value => {
            Size = value;
            Module.TimeOverlay?.Scale = value;
            Module.TimeOverlay?.Position = TimeOverlayPosition.ToVector2(TimeOverlay.dateString);
        };

        timeOverlayXOffsetOption.OnValueChange = value => TimeOverlayXOffset = value;
        timeOverlayYOffsetOption.OnValueChange = value => TimeOverlayYOffset = value;

        subMenu.Add(timeOverlayOption);
        subMenu.Add(showTimeZoneOption);
        subMenu.Add(sizeOption);
        subMenu.Add(dateFormatOption);
        subMenu.Add(positionOption);
        subMenu.Add(timeOverlayXOffsetOption);
        subMenu.Add(timeOverlayYOffsetOption);

        menu.Add(subMenu);
    }

    [SettingName("MODOPTION_KONGTIAO_TOOLBOX_TOGGLE_TIME_OVERLAY")]
    public ButtonBinding ToggleTimeOverlay { get; set; }

}