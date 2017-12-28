using ExtractRdlImages.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExtractRdlImages.Forms
{
    internal static class ButtonExtensions
    {
        public static void SetShield(this ButtonBase button, bool showShield)
        {
            button.FlatStyle = FlatStyle.System;
            NativeMethods.SendMessage(button.Handle, NativeMethods.BCM_SETSHIELD, 0, showShield ? 1 : 0);
        }
    }
}
