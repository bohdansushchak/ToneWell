using System;

namespace ToneWell.Helpers
{
    public class PlayerArgs : EventArgs
    {
        public string CurrentPositionSec { get; set; }
        public string LeftProgressSec { get; set; }

        public double ProgressDegree { get; set; }

    }
}
