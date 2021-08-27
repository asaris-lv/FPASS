using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evonik.FPASSMail.Model
{
    /// <summary>
    /// Value business object to hold attributes for FPASS Coordinator.
    /// </summary>
    class Coordinator
    {
        internal Coordinator()
        {
            ExcoDict = new Dictionary<decimal, ExternalContractor>();
        }

        #region Members

        internal string FirstName { get; set; }

        internal string Surname { get; set; }

        /// <summary>
        /// UserId is the KonzernId
        /// </summary>
        internal string UserId { get; set; }

        internal string EMailAddress { get; set; }

        /// <summary>
        /// Dictionary of assigned external contractors
        /// </summary>
        //internal List<ExternalContractor> ExcoList { get; set; }
        internal Dictionary<decimal, ExternalContractor> ExcoDict { get; set; }

        ///// <summary>
        ///// List of assigned coworkers
        ///// </summary>
        //internal List<CoWorker> CoWorkerList { get; set; }

        #endregion
    }
}
