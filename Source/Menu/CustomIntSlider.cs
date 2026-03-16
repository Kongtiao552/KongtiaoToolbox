using Celeste.Mod.UI;
using Celeste;
using System;
using Monocle;
using System.Collections.Generic;

namespace KongtiaoToolbox.Menu
{
    public class CustomIntSlider : TextMenu.Option<int>
    {
        public CustomIntSlider(string label, Func<int, string> values, int min, int max, int value = -1, int step = 1) : base(label) {
            for (int i = min; i <= max; i += step)
            {
                Add(values(i), i, value == i);
            }
        }
    }
}