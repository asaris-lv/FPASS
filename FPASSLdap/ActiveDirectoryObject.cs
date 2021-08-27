using System;
using System.Collections.Generic;
using System.Text;

namespace Evonik.FPASSLdap
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
        /// Default constructor
        /// </summary>
        public ActiveDirectoryObject()
        { }

        /// <summary>
        /// Constructor accepts attributes
        /// </summary>
        /// <param name="distingName"></param>
        /// <param name="samAccountName"></param>
        /// <param name="givenName"></param>
        /// <param name="surname"></param>
        /// <param name="email"></param>
        /// <param name="telNumber"></param>
        public ActiveDirectoryObject(string distingName,  
                                    string samAccountName, 
                                    string givenName, 
                                    string surname, 
                                    string email,
                                    string telNumber)
        {
            
            this.SamAccountName = samAccountName;
            this.CommonName = givenName;
            this.Surname = surname;
            this.DinstinguishName = distingName;
            this.EmailAddress = email;
            this.TelephoneNumber = telNumber;
        }

        /// <summary>
        /// Distinguished name (i.e. CN=Michael Mustermann,OU=PTA-Mitarbeiter,OU=User,DC=pta,DC=de)
        /// </summary>
        public string DinstinguishName { get; set; }

        /// <summary>
        /// contains the windows id
        /// </summary>
        public string SamAccountName { get; set; }

        /// <summary>
        /// contains the first name
        /// </summary>
        public string CommonName { get; set; }

        /// <summary>
        /// contains the last name
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// E-Mail address from ADS
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Phone number
        /// </summary>
        public string TelephoneNumber { get; set; }

        /// <summary>
        /// Surname, first name
        /// </summary>
        public string ConcatinatedName { get { return Surname + ", " + CommonName + " - " + SamAccountName; } }
    }
}
