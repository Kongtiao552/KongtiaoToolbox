using System.Collections;
using System.Linq;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.KongtiaoToolbox.Entities;

// Copied from https://github.com/DemoJameson/Celeste.SpeedrunTool/blob/master/SpeedrunTool/Source/Message/Tooltip.cs

[Tracked]
public class Tooltip : Entity {
    private readonly string Message;
    private float Alpha;
    private float UnEasedAlpha;
    private readonly float Duration;
    private readonly Vector2 Justify;
    private readonly Vector2 Scale;

    private Tooltip(string message, float duration = 1f) {
        Message = message;
        Duration = duration;
        Justify = new Vector2(0.5f, 1f);
        Scale = Vector2.One * 0.5f;
        Position = new(Engine.Width / 2f, Engine.Height - 20f);
        Tag = Tags.HUD | Tags.Global | Tags.FrozenUpdate | Tags.PauseUpdate | Tags.TransitionUpdate;
        Add(new Coroutine(Show()));
    }

    private IEnumerator Show() {
        while (Alpha < 1f) {
            UnEasedAlpha = Calc.Approach(UnEasedAlpha, 1f, Engine.RawDeltaTime * 5f);
            Alpha = Ease.SineOut(UnEasedAlpha);
            yield return null;
        }

        yield return Dismiss();
    }

    private IEnumerator Dismiss() {
        yield return Duration;
        while (Alpha > 0f) {
            UnEasedAlpha = Calc.Approach(UnEasedAlpha, 0f, Engine.RawDeltaTime * 5f);
            Alpha = Ease.SineIn(UnEasedAlpha);
            yield return null;
        }

        RemoveSelf();
    }

    public override void Render() {
        base.Render();
        ActiveFont.DrawOutline(Message, Position, Justify, Scale, Color.White * Alpha, 2,
            Color.Black * Alpha * Alpha * Alpha);
    }

    public static void Show(string message, float duration = 1f) {
        if (KongtiaoToolboxModule.ModSettings.EnableTooltips && Engine.Scene is Level level) {
            if (!level.Tracker.Entities.TryGetValue(typeof(Tooltip), out var tooltips)) {
                tooltips = level.Entities.FindAll<Tooltip>().Cast<Entity>().ToList();
            }

            tooltips.ForEach(entity => entity.RemoveSelf());
            level.Add(new Tooltip(message, duration));
        }
    }
}