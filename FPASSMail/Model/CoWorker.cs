using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evonik.FPASSMail.Model
{
    /// <summary>
    /// Value business object to hold attributes for FPASS Coworker (Fremdfirmenmitarbeiter).
    /// </summary>
    class CoWorker
    {
        /// <summary>
        /// Coworker's PK id from database (just for reference)
        /// </summary>
        internal decimal CrwId { get; set; }

        /// <summary>
        /// Coordinator's userId, so whe know who Cwr is assigned to.
        /// </summary>
        internal string UserIdCoord { get; set; }

        /// <summary>
        /// PK Id of externalcontractor that this Cwr belongs to.
        /// </summary>
        internal decimal ExcoId { get; set; }

        internal string Firstname { get; set; }

        internal string Surname { get; set; }

        internal string IdCardNumber { get; set; }

        internal DateTime ValidUntil { get; set; }

        /// <summary>
        /// PK Id of Coordinator that this Cwr belongs to.
        /// </summary>
        internal decimal CoordId { get; set; }

        internal string CoordFirstname { get; set; }
        
        internal string CoordSurname { get; set; }
        
        internal string CoordTelNo { get; set; }
    }
}
