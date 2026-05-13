using System;
using Monocle;
using Celeste.Mod.KongtiaoToolbox.Enums;
using Celeste.Mod.KongtiaoToolbox.Entities.Menu;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using YamlDotNet.Serialization;
using Celeste.Mod.KongtiaoToolbox.Entities;

namespace Celeste.Mod.KongtiaoToolbox;

[SettingName("MODOPTION_KONGTIAO_TOOLBOX_MODULE_TITLE")]
public class KongtiaoToolboxModuleSettings : EverestModuleSettings {

    public static TimeOverlay TimeOverlay => KongtiaoToolboxModule.TimeOverlay;

    [SettingIgnore] 
    public bool ShowRealTimeOverlay { get; set; } = false;

    [SettingIgnore] 
    public bool ShowTimeZone { get; set; } = true;

    [SettingIgnore] 
    public DateFormat DateFormat { get; set; } = DateFormat.LONG;

    [SettingIgnore] 
    public Color TimeOverlayColor { get; set; } = Color.White;

    [SettingIgnore] 
    public bool TimeOverlayOutline { get; set; } = true;

    [SettingIgnore] 
    public Color TimeOverlayOutlineColor { get; set; } = Color.Black;

    [SettingIgnore] 
    public float TimeOverlayOpacity { get; set; } = 1f;

    [SettingIgnore]
    public TimeOverlayPosition TimeOverlayPosition { get; set; } = TimeOverlayPosition.TopMiddle;

    [SettingIgnore] 
    public int TimeOverlayXOffset { get; set; } = 0;

    [SettingIgnore] 
    public int TimeOverlayYOffset { get; set; } = 0;

    [SettingIgnore]
    public float TimeOverlaySize { get; set; } = 0.75f;

    [SettingIgnore]
    public int DefaultInGameMusicVolume { get; set; } = 5;

    [SettingName("MODOPTION_KONGTIAO_TOOLBOX_ENABLE_TOOLTIP")]
    public bool EnableTooltips { get; set; } = true;

    public void CreateDefaultInGameMusicVolumeEntry(TextMenu menu, bool inGame) {
        TextMenu.Slider defaultInGameMusicVolumeOption = new TextMenu.Slider(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_DEFAULT_IN_GAME_MUSIC_VOLUME"), v => v.ToString(), 0, 10, DefaultInGameMusicVolume) {
            OnValueChange = v => DefaultInGameMusicVolume = v
        };

        menu.Add(defaultInGameMusicVolumeOption);

        defaultInGameMusicVolumeOption.AddDescription(menu, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_DEFAULT_IN_GAME_MUSIC_VOLUME_DESCRIPTION"));
    }
    
    [SettingIgnore]
    public bool ToggleExternalMediaPlayers { get; set; } = false;

    public void CreateToggleExternalMediaPlayersEntry(TextMenu menu, bool inGame) {
        TextMenu.OnOff toggleExternalMediaPlayersOption = new TextMenu.OnOff(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TOGGLE_EXTERNAL_MEDIA_PLAYERS"), ToggleExternalMediaPlayers) {
            OnValueChange = value => ToggleExternalMediaPlayers = value
        };

        menu.Add(toggleExternalMediaPlayersOption);

        toggleExternalMediaPlayersOption.AddDescription(menu, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TOGGLE_EXTERNAL_MEDIA_PLAYERS_DESCRIPTION"));
    }

    [YamlIgnore]
    public bool TimeOverlayOption { get; set; }

    public void CreateTimeOverlayOptionEntry(TextMenu menu, bool inGame) {
        if (!inGame) {
            return;
        }

        string colorOptionDesc = Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_COLOR_OPTION_DESC");

        TextMenuExt.SubMenu subMenu = new TextMenuExt.SubMenu(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_REAL_TIME_OVERLAY"), false);

        List<KeyValuePair<float, string>> percentOptions = new List<KeyValuePair<float, string>>() {
            new KeyValuePair<float, string>(0f, "0%"),
            new KeyValuePair<float, string>(0.05f, "5%"),
            new KeyValuePair<float, string>(0.1f, "10%"),
            new KeyValuePair<float, string>(0.15f, "15%"),
            new KeyValuePair<float, string>(0.2f, "20%"),
            new KeyValuePair<float, string>(0.25f, "25%"),
            new KeyValuePair<float, string>(0.3f, "30%"),
            new KeyValuePair<float, string>(0.35f, "35%"),
            new KeyValuePair<float, string>(0.4f, "40%"),
            new KeyValuePair<float, string>(0.45f, "45%"),
            new KeyValuePair<float, string>(0.5f, "50%"),
            new KeyValuePair<float, string>(0.55f, "55%"),
            new KeyValuePair<float, string>(0.6f, "60%"),
            new KeyValuePair<float, string>(0.65f, "65%"),
            new KeyValuePair<float, string>(0.7f, "70%"),
            new KeyValuePair<float, string>(0.75f, "75%"),
            new KeyValuePair<float, string>(0.8f, "80%"),
            new KeyValuePair<float, string>(0.85f, "85%"),
            new KeyValuePair<float, string>(0.9f, "90%"),
            new KeyValuePair<float, string>(0.95f, "95%"),
            new KeyValuePair<float, string>(1f, "100%"),
        };

        TextMenu.OnOff timeOverlayOption = new TextMenu.OnOff(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_SHOW_REAL_TIME_OVERLAY"), ShowRealTimeOverlay) {
            OnValueChange = v => {
                ShowRealTimeOverlay = v;
                TimeOverlay.Visible = ShowRealTimeOverlay;
            }
        };
        
        TextMenu.OnOff showTimeZoneOption = new TextMenu.OnOff(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_SHOW_TIME_ZONE"), ShowTimeZone) {
            OnValueChange = v => ShowTimeZone = v
        };

        TextMenu.OnOff timeOverlayOutlineOption = new TextMenu.OnOff(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TIME_OVERLAY_OUTLINE"), TimeOverlayOutline) {
            OnValueChange = v => {
                TimeOverlayOutline = v;
                TimeOverlay.Outline = TimeOverlayOutline;
            }
        };

        ColorOption timeOverlayColorOption = new ColorOption(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TIME_OVERLAY_COLOR"), TimeOverlayColor) {
            OnValueChange = v => {
                TimeOverlayColor = v;
                TimeOverlay.Color = v;
            }
        };     
        ColorOption timeOverlayOutlineColorOption = new ColorOption(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TIME_OVERLAY_OUTLINE_COLOR"), TimeOverlayOutlineColor) {
            OnValueChange = v => {
                TimeOverlayOutlineColor = v;
                TimeOverlay.OutlineColor = TimeOverlayOutlineColor;
            }
        }; 

        TextMenuExt.EnumerableSlider<float> sizeOption = new TextMenuExt.EnumerableSlider<float>(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TIME_OVERLAY_SIZE"), percentOptions, TimeOverlaySize) {
            OnValueChange = v => {
                TimeOverlaySize = v;
                TimeOverlay.Scale = TimeOverlaySize;
            }
        };
        TextMenuExt.EnumerableSlider<float> timeOverlayOpacityOption = new TextMenuExt.EnumerableSlider<float>(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TIME_OVERLAY_OPACITY"), percentOptions, TimeOverlayOpacity) {
            OnValueChange = v => {
                TimeOverlayOpacity = v;
                TimeOverlay.Opacity = TimeOverlayOpacity;
            }
        };
        
        TextMenuExt.EnumerableSlider<DateFormat> dateFormatOption = new TextMenuExt.EnumerableSlider<DateFormat>(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_DATE_FORMAT"), new List<KeyValuePair<DateFormat, string>>() {
            new KeyValuePair<DateFormat, string>(DateFormat.LONG, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_DATEFORMAT_LONG")),
            new KeyValuePair<DateFormat, string>(DateFormat.SHORT, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_DATEFORMAT_SHORT"))
        }, DateFormat) {
            OnValueChange = v => DateFormat = v
        };
        
        TextMenuExt.EnumerableSlider<TimeOverlayPosition> positionOption = new TextMenuExt.EnumerableSlider<TimeOverlayPosition>(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TIME_OVERLAY_POSITION"), new List<KeyValuePair<TimeOverlayPosition, string>>() {
            new KeyValuePair<TimeOverlayPosition, string>(TimeOverlayPosition.TopLeft, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TEXTOVERLAYPOSITION_TOPLEFT")),
            new KeyValuePair<TimeOverlayPosition, string>(TimeOverlayPosition.TopMiddle, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TEXTOVERLAYPOSITION_TOPMIDDLE")),
            new KeyValuePair<TimeOverlayPosition, string>(TimeOverlayPosition.TopRight, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TEXTOVERLAYPOSITION_TOPRIGHT")),
            new KeyValuePair<TimeOverlayPosition, string>(TimeOverlayPosition.BottomLeft, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TEXTOVERLAYPOSITION_BOTTOMLEFT")),
            new KeyValuePair<TimeOverlayPosition, string>(TimeOverlayPosition.BottomMiddle, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TEXTOVERLAYPOSITION_BOTTOMMIDDLE")),
            new KeyValuePair<TimeOverlayPosition, string>(TimeOverlayPosition.BottomRight, Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TEXTOVERLAYPOSITION_BOTTOMRIGHT"))
        }, TimeOverlayPosition) {
            OnValueChange = v => {
                TimeOverlayPosition = v;
                TimeOverlay.SetPosition(TimeOverlayPosition, TimeOverlayXOffset, TimeOverlayYOffset);
            }
        };

        TextMenuExt.IntSlider timeOverlayXOffsetOption = new TextMenuExt.IntSlider(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TIME_OVERLAY_X_OFFSET"), -2000, 2000, TimeOverlayXOffset) {
            OnValueChange = v => {
                TimeOverlayXOffset = v;
                TimeOverlay.SetPosition(TimeOverlayPosition, TimeOverlayXOffset, TimeOverlayYOffset);
            }
        };
        TextMenuExt.IntSlider timeOverlayYOffsetOption = new TextMenuExt.IntSlider(Dialog.Clean("MODOPTION_KONGTIAO_TOOLBOX_TIME_OVERLAY_Y_OFFSET"), -2000, 2000, TimeOverlayYOffset) {
            OnValueChange = v => {
                TimeOverlayYOffset = v;
                TimeOverlay.SetPosition(TimeOverlayPosition, TimeOverlayXOffset, TimeOverlayYOffset);
            }
        };

        subMenu.Add(timeOverlayOption);
        subMenu.Add(showTimeZoneOption);
        subMenu.Add(timeOverlayOpacityOption);
        subMenu.Add(timeOverlayColorOption);
        timeOverlayColorOption.AddDescription(subMenu, menu, colorOptionDesc);
        subMenu.Add(timeOverlayOutlineOption);
        subMenu.Add(timeOverlayOutlineColorOption);
        timeOverlayOutlineColorOption.AddDescription(subMenu, menu, colorOptionDesc);
        subMenu.Add(sizeOption);
        subMenu.Add(dateFormatOption);
        subMenu.Add(positionOption);
        subMenu.Add(timeOverlayXOffsetOption);
        subMenu.Add(timeOverlayYOffsetOption);

        menu.Add(subMenu);
    }

    [SettingName("MODOPTION_KONGTIAO_TOOLBOX_TOGGLE_TIME_OVERLAY")]
    public ButtonBinding ToggleTimeOverlay { get; set; }

    [SettingName("MODOPTION_KONGTIAO_TOOLBOX_TOGGLE_IN_GAME_MUSIC")]
    public ButtonBinding ToggleInGameMusic { get; set; }

}