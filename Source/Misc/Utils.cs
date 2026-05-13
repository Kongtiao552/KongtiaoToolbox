using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using Celeste;
using Celeste.Mod.KongtiaoToolbox.Entities;
using Celeste.Mod.KongtiaoToolbox.Enums;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.KongtiaoToolbox.Misc;

public static class Utils {

    public static KongtiaoToolboxModuleSettings ModSettings => KongtiaoToolboxModule.ModSettings;

    public static void Log(string message, LogLevel level = LogLevel.Info) => Logger.Log(level, "KongtiaoToolbox", message);

    public static CultureInfo CultureInfo = CultureInfo.CurrentCulture;

    public static string timeZoneInfo = "";

    public static string GetLongDateTimeString() => DateTime.Now.ToString("D", CultureInfo) + " " + DateTime.Now.ToString("T", CultureInfo);
    public static string GetShortDateTimeString() => DateTime.Now.ToString("d", CultureInfo) + " " + DateTime.Now.ToString("t", CultureInfo);

    public static void SetInGameMusicVolume(int volume) {
        Settings.Instance.MusicVolume = volume;
        Settings.Instance.ApplyMusicVolume();

        Log($"Changed in-game music volume to {volume}");
    }

    [DllImport("user32.dll")]
    private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

    private const byte VK_MEDIA_PLAY_PAUSE = 0xB3;
    private const int KEYEVENTF_KEYUP = 0x0002;

    public static bool IsWindows => OperatingSystem.IsWindows();

    public static void ToggleExternalMediaPlayers()
    {
        if (!IsWindows) {
            Tooltip.Show(Dialog.Get("MODOPTION_KONGTIAO_TOOLBOX_TOGGLE_EXTERNAL_MEDIA_PLAYERS_NOTE4"));
            Log("External media player toggling is only supported on Windows", LogLevel.Warn);
            return;
        }

        try {
            keybd_event(VK_MEDIA_PLAY_PAUSE, 0, 0, 0);
            keybd_event(VK_MEDIA_PLAY_PAUSE, 0, KEYEVENTF_KEYUP, 0);

            Tooltip.Show(Dialog.Get("MODOPTION_KONGTIAO_TOOLBOX_TOGGLE_EXTERNAL_MEDIA_PLAYERS_NOTE3"));
            Log("External media players toggled");
        } catch (DllNotFoundException) {
            Tooltip.Show(Dialog.Get("MODOPTION_KONGTIAO_TOOLBOX_TOGGLE_EXTERNAL_MEDIA_PLAYERS_NOTE1"));
            Log("Failed to toggle external media players: user32.dll not found", LogLevel.Error);
        } catch (Exception e) {
            Tooltip.Show(string.Format(Dialog.Get("MODOPTION_KONGTIAO_TOOLBOX_TOGGLE_EXTERNAL_MEDIA_PLAYERS_NOTE2"), e.Message));
            Log($"Failed to toggle external media players: {e.Message}", LogLevel.Error);
        }
    }

    public static bool TryParseColor(string hex, out Color color) {
        color = default;

        if (string.IsNullOrEmpty(hex)) {
            return false;
        }

        hex = hex.Replace("#", "").Trim();

        if (hex.Length < 6) {
            return false;
        }

        bool flag = int.TryParse(hex.Substring(0, 2), NumberStyles.HexNumber, null, out int r);
        bool flag1 = int.TryParse(hex.Substring(2, 2), NumberStyles.HexNumber, null, out int g);
        bool flag2 = int.TryParse(hex.Substring(4, 2), NumberStyles.HexNumber, null, out int b);

        if (flag && flag1 && flag2) {
            color = new Color(r, g, b);
            return true;
        }

        return false;
    } 

    public static string ColorToHex(Color color) {
        return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
    }

}