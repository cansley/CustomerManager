using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerWebMgrPOC.Enums
{
    /// <summary>
    /// Used to set how to handle duplicate values in collections
    /// </summary>
    public enum AllowDuplicates
    {
        /// <summary>
        /// If a duplicate entry exists, it will be replaced with the new entry.
        /// </summary>
        Weak,
        /// <summary>
        /// If a duplicate entry exists, it will throw an error.
        /// </summary>
        Strong,
        /// <summary>
        /// No duplicate checks will be run, new entries will just be added.
        /// </summary>
        Allow
    }
}
