using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractRdlImages.Platform
{
    public class FileAssociationAction
    {
        /// <summary>
        /// Gets or sets the name of the action.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the text that will be displayed in the file context menu.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when invoking the action. 
        /// Use %1 as placeholder for the file path.
        /// </summary>
        public string Command { get; set; }
    }
}
