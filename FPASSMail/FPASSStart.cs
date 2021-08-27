using System;
using System.IO;

using de.pta.Component.Errorhandling;
using de.pta.Component.Common;
using de.pta.Component.DataAccess;
using de.pta.Component.DbAccess.Exceptions;
using de.pta.Component.DbAccess;
using Evonik.FPASSMail.Db;
using System.Globalization;
using System.Threading;
using Evonik.FPASSMail.Util;
using Evonik.FPASSMail.Util.Messages;

namespace Evonik.FPASSMail
{
	/// <summary>
	/// Controls the start of the application
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">PTA GmbH</th>
	///			<th width="20%">12/01/2003</th>
	///			<th width="60%">Remarks</th>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class FPASSStart
	{
		#region Members

		#endregion 

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public FPASSStart()
		{
            Globals.GetInstance().Log.Fatal("FPASSMail is starting....");
		}

		#endregion

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void Initialize()
		{
		}	

		#endregion 

		#region Methods 

		/// <summary>
		/// Initializes all parts of fpass mail application.
		/// </summary>
		internal void Start() 
		{
			try 
			{
				ReadConfigurations();
                 
                // Need this so that dates and times
                CultureInfo ci = new CultureInfo("de-DE");
                Thread.CurrentThread.CurrentCulture = ci;

                CheckFPASSVersions();
			} 
			catch ( UIFatalException uie ) 
			{
				ExceptionProcessor.GetInstance().Process(uie);
			}
		}
		

		/// <summary>
		/// Reads the configuration of the pta components dataaccess and user management.
		/// Config for lov is done later cause we need a unique mandator when we read lov 
		/// compoment.
		/// </summary>
		private void ReadConfigurations() 
		{
			try 
			{
                var dir = AppDomain.CurrentDomain.BaseDirectory;
                ConfigReader.GetInstance().ApplicationRootPath = dir;

                // Get DataProvider from DbAccess component
                IProvider mProvider = DBSingleton.GetInstance().DataProvider;

			} 			
			catch (Exception e)
			{
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.INITIALIZATION_ERROR), e);
			}	
		}

		/// <summary>
		/// Checks and logs version no. stored in database and ver no. stored in application (Assembly version).
        /// For logging purposes only, no error thrown.
		/// </summary>
		/// <exception cref="UIFatalException">if version nrs do not match</exception>
		private void CheckFPASSVersions()
		{
            string currFPASSVersion = Globals.GetInstance().FPASSApplicationVersion.Trim();
            string currDBVersion = Globals.GetInstance().FPASSDatabaseVersion.Trim();

            Globals.GetInstance().Log.Warn("Checked FPASS versions. Database has: " + currDBVersion + ". FPASSMail has: " + currFPASSVersion);       
		}

		#endregion Methods
	}
}
