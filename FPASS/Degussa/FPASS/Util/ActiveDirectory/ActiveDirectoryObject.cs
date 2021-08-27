using System;
using System.Collections.Generic;
using System.Text;

namespace Degussa.FPASS.Util.ActiveDirectory
{
    /// <summary>
    /// This class describes objects in an active directory. This class was designed for use with FPASS v5. 
    /// Therefore only the needed Attributes will included.
    /// </summary>
    /// <date>23.07.2014</date>
    /// <author>Clemens Bergthaller</author>
    /// <company>PTA GmbH</company>
    public class ActiveDirectoryObject
    {
        /// <summary>
        /// standard constructor
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="samAccountName"></param>
        /// <param name="givenName"></param>
        /// <param name="surname"></param>
        /// <param name="distinguishedName"></param>
        public ActiveDirectoryObject(string displayName,  
            string samAccountName, 
            string givenName, 
            string surname, 
            string distinguishedName,
            string TelephoneNumber)
        {
            this.DisplayName    = displayName;
            this.SamAccountName = samAccountName;
            this.CommonName = givenName;
            this.Surname = surname;
            this.DistinguishedName = distinguishedName;
            this.TelephoneNumber = TelephoneNumber;
        }

        public string ConcatinatedName
        {
            get { return DisplayName + ", " + SamAccountName; }
        }

        /// <summary>
        /// contains both first name and last name
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// contains the windows id
        /// </summary>
        public string SamAccountName { get; private set; }

        /// <summary>
        /// contains the first name
        /// </summary>
        public string CommonName { get; private set; }

        /// <summary>
        /// contains the last name
        /// </summary>
        public string Surname { get; private set; }

        /// <summary>
        /// contains the distinguished name (i.e. CN=Michael Mustermann,OU=PTA-Mitarbeiter,OU=User,DC=pta,DC=de)
        /// </summary>
        public string DistinguishedName { get; private set; }

        /// <summary>
        /// contains the phone number
        /// </summary>
        public string TelephoneNumber { get; private set; }
    }
}
