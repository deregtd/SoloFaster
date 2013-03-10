using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SoloFaster
{
    public class OpenedSession
    {
        public RecordedSession session;
        public Color color;

        public OpenedSession(RecordedSession run)
        {
            this.session = run;
        }
    }
}
