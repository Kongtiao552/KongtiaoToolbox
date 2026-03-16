using Celeste.Mod.UI;
using Celeste;
using System;
using Monocle;
using System.Collections.Generic;

namespace KongtiaoToolbox.Menu
{
    public class CustomFloatSlider : TextMenu.Option<float>
    {
        public CustomFloatSlider(string label, Func<float, string> values, float min, float max, float value, float step = 0.05f) : base(label) {
            for (float i = min; i <= max; i += step)
            {
                Add(values(i), i, value == i);
            }
        }

        public CustomFloatSlider(string label, Func<float, string> values, float value, float[] options) : base(label) {
            foreach (float option in options)
            {
                Add(values(option), option, value == option);
            }
        }
    }
}