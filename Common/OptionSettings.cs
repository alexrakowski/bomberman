using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman
{
    class OptionsSettings
    {
        public bool Arrows { get; private set; }
        public string Toggle(OptionType optionType)
        {
            switch (optionType)
            {
                case OptionType.Controls:
                    this.Arrows = !this.Arrows;
                    return this.Arrows ? "Arrows" : "WSAD";
                default:
                    throw new BombermanException("Unknown Settings type: " + optionType);
            }
        }
        public string GetOptionValue(OptionType optionType)
        {
            Toggle(optionType);
            return Toggle(optionType);
        }
        public OptionsSettings()
        {
            this.Arrows = true;
        }
    }
}
