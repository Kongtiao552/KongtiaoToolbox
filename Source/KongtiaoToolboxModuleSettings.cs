using System;
using Monocle;
using Celeste;
using Celeste.Mod.KongtiaoToolbox.Entities;
using Celeste.Mod.KongtiaoToolbox.Enums;
using Celeste.Mod.KongtiaoToolbox.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Celeste.Mod.KongtiaoToolbox.Misc;

namespace Celeste.Mod.KongtiaoToolbox;

[SettingName("MODOPTION_KONGTIAO_TOOLBOX_MODULE_TITLE")]
public class KongtiaoToolboxModuleSettings : EverestModuleSettings {

    [SettingIgnore]
    public static KongtiaoToolboxModule Module => KongtiaoToolboxModule.Instance;

    [SettingIgnore] public bool ShowRealTimeOverlay { get; set; } = false;
    [SettingIgnore] public bool ShowTimeZone { get; set; } = true;
    [SettingIgnore] public DateFormat DateFormat { get; set; } = DateFormat.LONG;
    [SettingIgnore] public Color TimeOverlayColor { get; set; } = Color.White;
    [SettingIgnore] public bool TimeOverlayOutline { get; set; } = true;
    [SettingIgnore] public Color TimeOverlayOutlineColor { get; set; } = Color.Black;
    [SettingIgnore] public float TimeOverlayTransparency { get; set; } = 1f;

    [SettingIgnore]
    public TextOverlayPosition TimeOverlayPosition { get; set; } = TextOverlayPosition.TopMiddle;

    [SettingIgnore] public int TimeOverlayXOffset { get; set; } = 0;
    [SettingIgnore] public int TimeOverlayYOffset { get; set; } = 0;
    [SettingIgnore]
    public float TimeOverlaySize { get; set; } = 0.75f;
    [SettingIgnore]
    public Vector2 TimeOverlayOffset => new Vector2(TimeOverlayXOffset, -TimeOverlayYOffset);

    [SettingIgnore]
    public int DefaultInGameMusicVolume { get; set; } = 5;

    [SettingName("MODOPTION_KONGTIAO_TOOLBOX_ENABLE_TOOLTIP")]
    public bool EnableTooltips { get; set; } = true;

    public void CreateDefaultInGameMusicVolumeEntry(TextMenu menu, bool inGame) {
        TextMenu.Slider defaultInGameMusicVolumeOption = new TextMenu.Slider(
            Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_DEFAULT_IN_GAME_MUSIC_VOLUME"), IntToString, 0, 10, DefaultInGameMusicVolume
        );

        defaultInGameMusicVolumeOption.OnValueChange = value => DefaultInGameMusicVolume = value;

        menu.Add(defaultInGameMusicVolumeOption);

        defaultInGameMusicVolumeOption.AddDescription(menu, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_DEFAULT_IN_GAME_MUSIC_VOLUME_DESCRIPTION"));
    }
    
    [SettingIgnore]
    public bool ToggleExternalMediaPlayers { get; set; } = false;

    public void CreateToggleExternalMediaPlayersEntry(TextMenu menu, bool inGame) {
        TextMenu.OnOff toggleExternalMediaPlayersOption = new TextMenu.OnOff(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TOGGLE_EXTERNAL_MEDIA_PLAYERS"), ToggleExternalMediaPlayers);

        toggleExternalMediaPlayersOption.OnValueChange = value => ToggleExternalMediaPlayers = value;
        toggleExternalMediaPlayersOption.Disabled = !Utils.IsWindows;

        menu.Add(toggleExternalMediaPlayersOption);

        toggleExternalMediaPlayersOption.AddDescription(menu, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TOGGLE_EXTERNAL_MEDIA_PLAYERS_DESCRIPTION"));
    }

    public bool TimeOverlayOption { get; set; }

    public static string IntToString(int v) => v.ToString();
    public static string FloatToPercentString(float v) => v.ToString("P0");

    public void CreateTimeOverlayOptionEntry(TextMenu menu, bool inGame) {
        TextMenuExt.SubMenu subMenu = new TextMenuExt.SubMenu(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_REAL_TIME_OVERLAY"), false);

        TextMenu.OnOff timeOverlayOption = new TextMenu.OnOff(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_SHOW_REAL_TIME_OVERLAY"), ShowRealTimeOverlay);

        TextMenu.OnOff showTimeZoneOption = new TextMenu.OnOff(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_SHOW_TIME_ZONE"), ShowTimeZone);

        TextMenu.OnOff timeOverlayOutlineOption = new TextMenu.OnOff(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TIME_OVERLAY_OUTLINE"), TimeOverlayOutline);

        Dictionary<Color, string> colorList = new Dictionary<Color, string> {
            { Color.Black, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_COLOR_BLACK") },
            { Color.White, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_COLOR_WHITE") },
            { Color.Red, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_COLOR_RED") },
            { Color.Green, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_COLOR_GREEN") },
            { Color.Blue, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_COLOR_BLUE") },
            { Color.LightBlue, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_COLOR_LIGHT_BLUE") },
            { Color.SkyBlue, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_COLOR_SKY_BLUE") },
            { Color.Pink, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_COLOR_PINK") },
            { Color.Aquamarine, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_COLOR_AQUAMARINE") },
            { Color.Gold, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_COLOR_GOLD") },
            { Color.Orange, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_COLOR_ORANGE") },
            { Color.Purple, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_COLOR_PURPLE") },
            { Color.Brown, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_COLOR_BROWN") },
            { Color.Gray, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_COLOR_GRAY") },
            { Color.HotPink, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_COLOR_HOT_PINK") },
            { Color.Yellow, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_COLOR_YELLOW") },
            { Color.Magenta, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_COLOR_MAGENTA") },
            { Color.Cyan, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_COLOR_CYAN") }
        };  

        TextMenuExt.EnumerableSlider<Color> timeOverlayColorOption = new TextMenuExt.EnumerableSlider<Color>(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TIME_OVERLAY_COLOR"), colorList, TimeOverlayColor);

        TextMenuExt.EnumerableSlider<Color> timeOverlayOutlineColorOption = new TextMenuExt.EnumerableSlider<Color>(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TIME_OVERLAY_OUTLINE_COLOR"), colorList, TimeOverlayOutlineColor);

        float[] sizeOptions = [0.05f, 0.1f, 0.15f, 0.20f, 0.25f, 0.30f, 0.35f, 0.40f, 0.45f, 0.50f, 0.55f, 0.60f, 0.65f, 0.70f, 0.75f, 0.80f, 0.85f, 0.90f, 0.95f, 1.0f];

        CustomFloatSlider sizeOption = new CustomFloatSlider(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TIME_OVERLAY_SIZE"), FloatToPercentString, TimeOverlaySize, sizeOptions);
        CustomFloatSlider timeOverlayTransparencyOption = new CustomFloatSlider(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TIME_OVERLAY_TRANSPARENCY"), FloatToPercentString, TimeOverlayTransparency, sizeOptions);
        
        CustomEnumSlider<DateFormat> dateFormatOption = new CustomEnumSlider<DateFormat>(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_DATE_FORMAT"), DateFormat);
        CustomEnumSlider<TextOverlayPosition> positionOption = new CustomEnumSlider<TextOverlayPosition>(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TIME_OVERLAY_POSITION"), TimeOverlayPosition);

        CustomIntSlider timeOverlayXOffsetOption = new CustomIntSlider(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TIME_OVERLAY_X_OFFSET"), IntToString, -500, 500, TimeOverlayXOffset, 20);
        CustomIntSlider timeOverlayYOffsetOption = new CustomIntSlider(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TIME_OVERLAY_Y_OFFSET"), IntToString, -500, 500, TimeOverlayYOffset, 20);

        timeOverlayOption.OnValueChange = value => {
            ShowRealTimeOverlay = value;
            Module.TimeOverlay?.Visible = ShowRealTimeOverlay;

            showTimeZoneOption.Disabled = !ShowRealTimeOverlay;
            timeOverlayOutlineOption.Disabled = !ShowRealTimeOverlay;
            sizeOption.Disabled = !ShowRealTimeOverlay;
            dateFormatOption.Disabled = !ShowRealTimeOverlay;
            positionOption.Disabled = !ShowRealTimeOverlay;
            timeOverlayXOffsetOption.Disabled = !ShowRealTimeOverlay;
            timeOverlayYOffsetOption.Disabled = !ShowRealTimeOverlay;
            timeOverlayColorOption.Disabled = !ShowRealTimeOverlay;
            timeOverlayTransparencyOption.Disabled = !ShowRealTimeOverlay;

            timeOverlayOutlineColorOption.Disabled = !ShowRealTimeOverlay || !TimeOverlayOutline;
        };

        showTimeZoneOption.OnValueChange = value => {
            ShowTimeZone = value;
            Module.TimeOverlay?.UpdateSettings();
        };

        dateFormatOption.OnValueChange = value => {
            DateFormat = value;
            Module.TimeOverlay?.UpdateSettings();
        };

        positionOption.OnValueChange = value => {
            TimeOverlayPosition = value;
            Module.TimeOverlay?.UpdateSettings();
        };

        sizeOption.OnValueChange = value => {
            TimeOverlaySize = value;
            Module.TimeOverlay?.UpdateSettings();
        };

        timeOverlayColorOption.OnValueChange = value => {
            TimeOverlayColor = value;
            Module.TimeOverlay?.UpdateSettings();
        };

        timeOverlayOutlineOption.OnValueChange = value => {
            TimeOverlayOutline = value;
            Module.TimeOverlay?.UpdateSettings();

            timeOverlayOutlineColorOption.Disabled = !ShowRealTimeOverlay || !TimeOverlayOutline;
        };

        timeOverlayOutlineColorOption.OnValueChange = value => {
            TimeOverlayOutlineColor = value;
            Module.TimeOverlay?.UpdateSettings();
        }; 

        timeOverlayTransparencyOption.OnValueChange = value => {
            TimeOverlayTransparency = value;
            Module.TimeOverlay?.UpdateSettings();
        };

        timeOverlayXOffsetOption.OnValueChange = value => TimeOverlayXOffset = value;
        timeOverlayYOffsetOption.OnValueChange = value => TimeOverlayYOffset = value;

        showTimeZoneOption.Disabled = !ShowRealTimeOverlay;
        timeOverlayOutlineOption.Disabled = !ShowRealTimeOverlay;
        timeOverlayColorOption.Disabled = !ShowRealTimeOverlay;
        sizeOption.Disabled = !ShowRealTimeOverlay;
        dateFormatOption.Disabled = !ShowRealTimeOverlay;
        positionOption.Disabled = !ShowRealTimeOverlay;
        timeOverlayXOffsetOption.Disabled = !ShowRealTimeOverlay;
        timeOverlayYOffsetOption.Disabled = !ShowRealTimeOverlay;
        timeOverlayTransparencyOption.Disabled = !ShowRealTimeOverlay;

        timeOverlayOutlineColorOption.Disabled = !ShowRealTimeOverlay || !TimeOverlayOutline;

        subMenu.Add(timeOverlayOption);
        subMenu.Add(showTimeZoneOption);
        subMenu.Add(timeOverlayTransparencyOption);
        subMenu.Add(timeOverlayColorOption);
        subMenu.Add(timeOverlayOutlineOption);
        subMenu.Add(timeOverlayOutlineColorOption);
        subMenu.Add(sizeOption);
        subMenu.Add(dateFormatOption);
        subMenu.Add(positionOption);
        subMenu.Add(timeOverlayXOffsetOption);
        subMenu.Add(timeOverlayYOffsetOption);

        menu.Add(subMenu);

        Utils.Log("Time overlay options added");
    }

    [SettingName("MODOPTION_KONGTIAO_TOOLBOX_TOGGLE_TIME_OVERLAY")]
    public ButtonBinding ToggleTimeOverlay { get; set; }

    [SettingName("MODOPTION_KONGTIAO_TOOLBOX_TOGGLE_IN_GAME_MUSIC")]
    public ButtonBinding ToggleInGameMusic { get; set; }

}