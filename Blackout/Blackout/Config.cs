namespace Blackout
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Exiled.API.Features;
    using Exiled.API.Interfaces;

    public sealed class Config : IConfig
    {
        private int timeBetweenBlackouts = 30;
        private int blackoutDuration = 20;

        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; }

        [Description("Time Between Blackouts.")]
        public int TimeBetweenBlackouts
        {
            get => timeBetweenBlackouts; set { timeBetweenBlackouts = value; }
        }

        [Description("The Duration Of The Blackout.")]
        public int BlackoutDuration
        {
            get => blackoutDuration; set { blackoutDuration = value; }
        }
    }
}