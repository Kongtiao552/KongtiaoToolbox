using Celeste;
using Celeste.Mod.KongtiaoToolbox.Enums;
using System;

namespace Celeste.Mod.KongtiaoToolbox.Menu {
    public class CustomEnumSlider<T> : TextMenu.Option<T> where T : Enum {

        public CustomEnumSlider(string label = null, T startValue = default(T))
            : base(label ?? typeof(T).Name)
        {
            foreach (T value2 in Enum.GetValues(typeof(T)))
            {
                string label2 = value2.ToLocalizedString();
                T value = value2;
                object obj = startValue;
                Add(label2, value, value2.Equals(obj));
            }
        }
    }
}