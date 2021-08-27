using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evonik.FPASSMail.Model
{
    /// <summary>
    /// Value business object to hold attributes for FPASS external contractor.
    /// </summary>
    class ExternalContractor
    {
        internal ExternalContractor()
        {
            //CoWorkerList = new List<CoWorker>();
            CoWorkerDict = new Dictionary<decimal, CoWorker>();
        }

        #region Members

        internal decimal ExcoId { get; set; }

        internal string Name { get; set; }

       
        /// <summary>
        /// List of assigned coworkers
        /// </summary>
        internal Dictionary<decimal, CoWorker> CoWorkerDict { get; set; }

        #endregion
    }
}
