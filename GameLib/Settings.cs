using System;
using System.Windows.Forms;

namespace GameLib
{
    [Serializable]
    public class Settings
    {
        public int Window_Width;
        public int Window_Height;

        public Keys UsageKey;
        public Settings() 
        {
            Window_Height = 760;
            Window_Width = 1024;
            UsageKey = Keys.E;
        }
    }
}
