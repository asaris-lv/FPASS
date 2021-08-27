using System;
using System.IO;
using System.Windows.Forms;

using de.pta.Component.Errorhandling;
using de.pta.Component.ListOfValues;
using de.pta.Component.Common;
using de.pta.Component.DataAccess;
using de.pta.Component.DbAccess.Exceptions;
using de.pta.Component.N_UserManagement;
using de.pta.Component.N_UserManagement.Internal;
using de.pta.Component.N_UserManagement.Exceptions;

using Degussa.FPASS.Util.ErrorHandling;
using Degussa.FPASS.Util;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.FPASSApplication;


namespace Degussa.FPASS
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

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public FPASSStart()
		{
			
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void Initialize()
		{
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Initializes all parts of fpass application, and starts the gui.
		/// </summary>
		internal void Start() 
		{
			try 
			{
				this.ReadConfigurations();

				// checks if user is auhrorized to use fpass application
				if ( FPASSControllSingleton.GetInstance().CheckUser() ) 
				{
					// Get Global values from database
					Globals.GetInstance().GetValuesFromDatabase();
					
					// Do checks
					this.CheckFPASSVersions();
					this.CheckWritePaths();
					this.ClearReportsDirectory();

					// userid and mandatorid are available, so initilaize mandator dependent lov
					LovSingleton.GetInstance().SetSqlRestriction(UserManagementControl.getInstance().CurrentMandatorID.ToString());
					LovSingleton.GetInstance().ReadConfiguration();
					FPASSControllSingleton.GetInstance().ShowFPASS();	
				}
				this.ClearReportsDirectory();
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
				ConfigReader.GetInstance().ApplicationRootPath = Application.StartupPath;
				DataAccessManager.GetInstance().ReadConfiguration();
				UserManagementConfiguration.getInstance().initializeUserManagementConfiguration();
			} 
			catch (UserManagementException ume)
			{
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.INITIALIZATION_ERROR), ume);
			}
			catch (Exception e)
			{
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.INITIALIZATION_ERROR), e);
			}	
		}

		/// <summary>
		/// Ensures FPASS version no. stored in database is same as that stored
		/// in application (Assembly version)
		/// </summary>
		/// <exception cref="UIFatalException">if version nrs do not match</exception>
		private void CheckFPASSVersions()
		{
            string currFPASSVersion = Globals.GetInstance().FPASSApplicationVersion.Trim();
            string currDBVersion = Globals.GetInstance().FPASSDatabaseVersion.Trim();

            if (Globals.GetInstance().VersionCheckActive)
            {
                if (!currFPASSVersion.Equals(currDBVersion))
                    throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_VERSION_ERROR));
                else
                    Globals.GetInstance().Log.Info("CheckFPASSVersions(): Version check OK. " + currFPASSVersion);

            }
            else
            {
                Globals.GetInstance().Log.Warn("CheckFPASSVersions(): Version check currently deactivated. Database has: " + currDBVersion + ". FPASS has: " + currFPASSVersion);
            }
		}

		/// <summary>
		/// Tests if fpass report directories exists.
		/// Paths are held in<see cref="Degussa.FPASS.Util"><code>Globals</code></see> object.
		/// </summary>
		/// <exception cref="de.pta.Component.Errorhandling.UIFatalException">Is thrown, if the 
		/// directories don't exist</exception>
		private void CheckWritePaths() 
		{
            string dirError = MessageSingleton.GetInstance().GetMessage(MessageSingleton.REPORT_DIRECTORY_ERROR);

			if ( !Directory.Exists(Globals.GetInstance().ReportsBasePath))
			{
                throw new UIFatalException(String.Format(dirError, Globals.GetInstance().ReportsBasePath));
			}		
			else if ( !Directory.Exists(Globals.GetInstance().ReportsDataPath))
			{
                throw new UIFatalException(String.Format(dirError, Globals.GetInstance().ReportsDataPath));
			}
			else if ( !Directory.Exists(Globals.GetInstance().ReportsTemplatePath))
			{
                throw new UIFatalException(String.Format(dirError, Globals.GetInstance().ReportsTemplatePath));
			}
		}

		/// <summary>
		/// Deletes the temp files in the report directories.
		/// </summary>
		private void ClearReportsDirectory() 
		{
			try 
			{
                string[] files = Directory.GetFiles(Globals.GetInstance().ReportsDataPath);
				
                for ( int i = 0; i< files.Length; i++ ) 
				{
					File.Delete(files[i]);
				}
			} 
			catch ( Exception e ) 
			{
				Globals.GetInstance().Log.Fatal(e.Message);
			}
		}


		#endregion Methods
	}
}
