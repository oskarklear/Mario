using System;
using System.ComponentModel;
using System.Reflection;
using Microsoft.Xna.Framework.Input;

namespace Mario.Controller
{
    class Input
    {
        public enum ControllerType
        {
            [Description("Undefined")] Undefined,
            [Description("Keyboard")] Keyboard,
            [Description("Gamepad")] Gamepad
        }

        public ControllerType Controller { get; set; }

        public int Key { get; set; }

        public Input()
        {
            Controller = ControllerType.Undefined;
        }

        public override string ToString()
        {
            string key = (Controller == ControllerType.Keyboard) ? ((Keys)Key).ToString() : ((Buttons)Key).ToString();
            return base.ToString() + ": " + Controller.GetDescription() + " - " + key;
        }
    }

    // Reflection Helper - For getting String from enumeration
    public static class StringHelper
    {
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                        Attribute.GetCustomAttribute(field,
                            typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }
    }
}
