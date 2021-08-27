using System;

namespace Degussa.FPASS.Bo.Administration
{
    /// <summary>
    /// Simple BO holds Id card reader fields
    /// </summary>
    /// <remarks>
    /// <para><b>History</b></para>
    /// <div class="tablediv">
    /// <table class="dtTABLE" cellspacing="0">
    ///		<tr>
    ///			<th width="20%">PTA GmbH</th>
    ///			<th width="20%">03.12.2014</th>
    ///			<th width="60%">Remarks</th>
    ///		</tr>
    /// </table>
    /// </div>
    /// </remarks>
    public class BOIDCardReader : AbstractAdminBO
    {
        #region Members

    
        /// <summary>
        /// Gets or sets id card reader number
        /// </summary>
        public decimal ReaderNumber { get; set; }
            
        /// <summary>
        /// Gets or sets id card reader type
        /// </summary>
        public string ReaderType { get; set; }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        public string Description { get; set; }

        #endregion 

        #region Constructors

        /// <summary>
        /// Standard constructor.
        /// </summary>
        public BOIDCardReader()
        {
            initialize();
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the members.
        /// </summary>
        private void initialize()
        {

        }

        #endregion //End of Initialization


        #region Accessors

        #endregion

        #region Methods

        #endregion
    }
}
