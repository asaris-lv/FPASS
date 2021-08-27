using System;
using System.Collections;
using System.Data;
using System.Data.OracleClient;
using de.pta.Component.DbAccess;
using de.pta.Component.DbAccess.Exceptions;
using de.pta.Component.Errorhandling;
using Degussa.FPASS.Db;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Gui.Dialog.Pass;
using Degussa.FPASS.Util;
using Degussa.FPASS.Util.Messages;

namespace Degussa.FPASS.Bo.Pass
{
	/// <summary>
	/// Implementation of factory for all authorizations. Provides methods to build
	/// instances of all types of authorizations. Acts like a container for authorizations.
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
	public class AuthorizationFactory
	{
		#region Members

		/// <summary>
		/// used to hold all created authorizations
		/// </summary>
		private				Hashtable					mAllAuthorizations;

		/// <summary>
		/// reference to the model, which instantiated the factory
		/// </summary>
		private				CoWorkerModel				mCoWorkerModel;
		/// <summary>
		/// provides db access
		/// </summary>
		private				IProvider					mProvider;

		// constants for select statements
		private				String						SELECT_AUTHORIZATION = "SelectAuthorization";
		private				String						SELECT_AUTHORIZATION_ARCHIVE = "SelectAuthorizationArchive";
		private				String						AUTHOR_CWR_PARAM = ":RATH_CWR_ID";
		

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Simple constructor.
		/// </summary>
		public AuthorizationFactory(CoWorkerModel pModel)
		{
			mCoWorkerModel = pModel;
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void initialize()
		{
			// Get DataProvider from DbAccess component
			mProvider = DBSingleton.GetInstance().DataProvider;
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Gets all authorizations needed for a coworker. Checks what type of authorization 
		/// were saved to db and reads the appropriate data from db. Authorizations which were
		/// not granted once and as result are not in db are created as new authorizations.
		/// </summary>
		/// <returns>List containing all Authorizations</returns>
		internal ArrayList GetAuthorizations() 
		{
			mAllAuthorizations = new Hashtable();

			//SignatureAuthorization  signatureAuthorization = new SignatureAuthorization(mCoWorkerModel);
			AccessAuthorization		accessAuthorization = new AccessAuthorization(mCoWorkerModel);
			SafetyAuhorization		safetyAuhorization	= new SafetyAuhorization(mCoWorkerModel);
			IndustrialSafetyAuthorization  indSafetyAuthorization = new IndustrialSafetyAuthorization(mCoWorkerModel);
			ParkingExternalAuthorization parkingAuthorization = new ParkingExternalAuthorization(mCoWorkerModel);

			//mAllAuthorizations.Add(signatureAuthorization.AuthorizationTypeID, signatureAuthorization);
			mAllAuthorizations.Add(accessAuthorization.AuthorizationTypeID, accessAuthorization);
			mAllAuthorizations.Add(safetyAuhorization.AuthorizationTypeID, safetyAuhorization);
			mAllAuthorizations.Add(indSafetyAuthorization.AuthorizationTypeID, indSafetyAuthorization);
			mAllAuthorizations.Add(parkingAuthorization.AuthorizationTypeID, parkingAuthorization);
		
			ArrayList AuthorizationsList = new ArrayList();

			ReadAuthorizationFromDB();

			AuthorizationsList.AddRange(mAllAuthorizations.Values);

			return AuthorizationsList;
		}


		/// <summary>
		/// Gets a authorization. Checks given pTypeID to decide which 
		/// subclass of AbstractAuthoriztaion is requested.
		/// </summary>
		/// <param name="pTypeID">id which identifies type of authorization</param>
		/// <returns>requested authorization</returns>
		internal AbstractAuthorization GetAuthorization(decimal pTypeID) 
		{
			if ( pTypeID.Equals(Globals.GetInstance().AccessAuthorSiteSecurityID ) ) 
			{
				AbstractAuthorization authorization = (AbstractAuthorization)mAllAuthorizations[Globals.GetInstance().AccessAuthorID];

				authorization.AuthorizationTypeID = Globals.GetInstance().AccessAuthorSiteSecurityID;
				return authorization;
			}
			return (AbstractAuthorization)mAllAuthorizations[pTypeID];
		}


		/// <summary>
		/// Gets list of authorizations for current coworker from db.
		/// <exception cref="de.pta.Component.Errorhandling.UIFatalException">
		/// Is thrown if select statement fails.
		/// </exception>
		/// </summary>
		private void ReadAuthorizationFromDB() 
		{
            try
            {
                AbstractAuthorization authorization;
                IDbCommand mSelComm = null;

                // Create the select command & fill Data Reader 
                if (!mCoWorkerModel.Mode.Equals(AllFPASSDialogs.CWR_DIALOG_MODE_ARCHIVE))
                {
                    mSelComm = mProvider.CreateCommand(SELECT_AUTHORIZATION);
                }
                else
                {
                    mSelComm = mProvider.CreateCommand(SELECT_AUTHORIZATION_ARCHIVE);
                }
                mProvider.SetParameter(mSelComm, AUTHOR_CWR_PARAM, mCoWorkerModel.CoWorkerId);


                // Open data reader to get ExContractor data
                IDataReader mDR = mProvider.GetReader(mSelComm);

                int numrecs = 0;
            
                while (mDR.Read())
                {
                    authorization = this.GetAuthorization(Convert.ToDecimal(mDR["RATH_RATT_ID"]));
                    authorization.IsInsert = false;
                    authorization.AuthorizationID = Convert.ToDecimal(mDR["RATH_ID"]);

                    try
                    {
                        authorization.AuthorizationDate = Convert.ToDateTime(mDR["RATH_RECEPTAUTHODATE"]);
                    }
                    catch (InvalidCastException)
                    {
                        authorization.AuthorizationDate = DateTime.Now;
                    }
                    authorization.Comment = mDR["RATH_COMMENT"].ToString();

                    /// swap user OS name for nice name in display
                    /// UserID (the PK) saved in database
                    authorization.UserName = mDR["USERNICENAME"].ToString();
                    authorization.UserID = Convert.ToDecimal(mDR["RATH_USER_ID"]);

                    String boolStringYN = mDR["RATH_RECEPTAUTHO_YN"].ToString();
                    if (boolStringYN.Equals("Y"))
                    {
                        authorization.AuthorizationExecuted = true;
                    }
                    numrecs++;
                }

                // Close reader
                mDR.Close();
            }
            catch (OracleException oraex)
            {
                throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + oraex.Message);
            }
            catch (DbAccessException dba)
            {
                throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + dba.Message);
            }
		}

		#endregion // End of Methods
	}
}
