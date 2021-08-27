using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

using de.pta.Component.DbAccess;
using de.pta.Component.Errorhandling;
using de.pta.Component.ListOfValues;

using Degussa.FPASS.Util;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Bo.Pass;
using Degussa.FPASS.Db;
using Degussa.FPASS.Bo.Search;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// VehicleModel is the model 
	/// of the MVC-triad VehicleModel,
	/// VehicleController and FrmVehicle.
	/// VehicleModel extends from the FPASSBaseModel.
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
	public class VehicleModel : FPASSBaseModel
	{
		#region Members
		
		/// <summary>
		/// Name of database query to select coworker
		/// </summary>
		private const string VEHICLEACCESS_QUERY	= "VehicleAccess";
		/// <summary>
		/// Holds the search parameters from gui
		/// </summary>
		private bool	mVehicleYesPara;
		private bool	mVehicleNoPara;
		private bool   	mVehicleResieveYesPara;
		private bool	mVehicleResieveNoPara;
		private bool	mVehicleShortNoExtended;
		private bool	mVehicleLongNoExtended;
		private bool    mVehicleShortAccepted;
		private bool	mVehicleLongAccepted;
		/// <summary>
		/// Holds coworker value objects
		/// </summary>
		private ArrayList		arlVehicleAccess;
		/// <summary>
		/// Name of SQL statements and parameters for DB update
		/// </summary>
		private const string VEHICLEACCESS_INSERT		= "InsertVehicleAccess";
		private const string VEHICLEACCESS_UPDATE		= "UpdateVehicleAccess";
		private const string SELECTEDVEHICLEACC_UPDATE	= "UpdateSelectedVehicleAccess";
		private const string SELECTEDNOTACEPTVE_UPDATE	= "UpdateSelecNotAceptVehicAccs";
		private const string CWBR_CWR_ID				= ":CWBR_CWR_ID";
		private const string CWBR_BRIEFING_YN			= ":CWBR_BRIEFING_YN";
		private const string CWBR_BRIEFINGDATE			= ":CWBR_BRIEFINGDATE";
		private const string CWBR_INACTIVE_YN			= ":CWBR_INACTIVE_YN";
		private const string CWBR_USER_ID				= ":CWBR_USER_ID";
		private const string CWBR_BRF_ID				= ":CWBR_BRF_ID";
		private const string CWBR_BRFL_ID				= ":CWBR_BRFL_ID";
		/// <summary>
		/// ID of current user (record changes)
		/// </summary>
		private int			 mUserID					= UserManagementControl.getInstance().CurrentUserID;
		/// <summary>
		/// DB commands & transactions 
		/// </summary>
		private IDbCommand		insComm;
		private IDbCommand		updComm;
		private IDbCommand		dummyComm;
		private IDataReader		drTransaction = null;
		private IDbTransaction  trans		  = null;
		/// <summary>
		/// Member attributes
		/// </summary>
		private string		   mVehicleDesireShort;
		private string		   mVehicleDesireLong;
		private string		   mVehicleAcceptedShort;
		private string		   mVehicleAcceptedLong;
		private string		   mVehicleAccepted;
		private DateTime	   mVehicleDateShort;
		private DateTime	   mVehicleDateLong;
		private decimal		   mBrfIDShort;
		private decimal		   mBrfIDLong;
		private decimal		   mBrfID;
		private bool		   mAccessNoShort; 
		private bool		   mAccessNoLong; 
		private bool		   mAcceptedShortFlag = false;
		private bool           mAcceptedLongFlag  = false;
		private bool		   mDesiredShortFlag  = false;
		private bool           mDesiredLongFlag   = false;
		private decimal		   mRecordsFound = 0;
        		
		/// <summary>
		/// Constants
		/// </summary>
		private const string ACCESS_SHORT = "gewünscht";
		private const string ACCESS_LONG  = "gewünscht";
		/// <summary>
		/// For SQL strings
		/// </summary>
		private  String	mwhereClause  = String.Empty;
		private  bool   mDefaultValue = false;
		private  bool   whereSetNotaccept = false;
		private  bool   whereSetNotproc = false;


		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public VehicleModel()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void initialize()
		{
		}	

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Simple getter for view belonging to this model
		/// </summary>
		private FrmVehicle mViewFrmVehicle 
		{
			get 
			{
				return (FrmVehicle) mView;
			}
		}
		
		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// In this case fields at base of form are enabled (reason?)
		/// </summary>
		internal override void PreClose()
		{
			DisableVehicleShortFields();
			DisableVehicleLongFields();
		}

		/// <summary>
		/// PreShow. On the first opening of form, use mDefaultValue to show filtered search results
		/// </summary>
		internal override void PreShow()
		{
			mDefaultValue = true;
			GetVehicleAccess();
		}

		#region EnableDisabledFields


		/// <summary>
		/// Enables all fields at base of form responsible for setting coworker attributes for short vehicle access
		/// </summary>
		private void EnableVehicleLongFields()
		{
			mViewFrmVehicle.RbtVehicleEntranceLongNo.Enabled = true;
			mViewFrmVehicle.RbtVehicleEntranceLongYes.Enabled = true;
			mViewFrmVehicle.DatVehicleEntranceLongReceivedOn.Enabled = true;
			mViewFrmVehicle.BtnSave.Enabled = true;
			//mViewFrmVehicle.BtnAcceptedLong.Enabled = true;
		}
		
		/// <summary>
		/// Enables all fields at base of form responsible for setting coworker attributes for long vehicle access
		/// </summary>
		private void EnableVehicleShortFields()
		{
			mViewFrmVehicle.RbtVehicleEntranceShortReceivedNo.Enabled = true;
			mViewFrmVehicle.RbtVehicleEntranceShortReceivedYes.Enabled = true;
			mViewFrmVehicle.DatVehicleEntranceShortReceivedOn.Enabled = true;
			mViewFrmVehicle.BtnSave.Enabled = true;
			//mViewFrmVehicle.BtnAcceptedShort.Enabled = true;
	}

		/// <summary>
		/// Depending on whether the long access is activated or not 
		/// the fields for short access are activated or deactivated
		/// </summary>
		internal void HandleEnableDisableFieldsForShortAccess()
		{
			//if long access is not allowed the fields for short access are enabled
			if( ((FrmVehicle) mView).RbtVehicleEntranceLongNo.Checked == true )
			{
				EnableVehicleShortFields();
			}
			//if long access is allowed the fields for short access are disabled

			if( ((FrmVehicle) mView).RbtVehicleEntranceLongYes.Checked == true )
			{
				DisableVehicleShortFields();
			}

		}
		
		/// <summary>
		/// Depending on whether the short access is activated or not 
		/// the fields for long access are activated or deactivated
		/// </summary>
		internal void HandleEnableDisableFieldsForLongAccess()
		{
			//if short access is not allowed the fields for long access are enabled
			if( ((FrmVehicle) mView).RbtVehicleEntranceShortReceivedNo.Checked == true )
			{
				EnableVehicleLongFields();
			}
			//if short access is allowed the fields for long access are disabled
			if ( ((FrmVehicle) mView).RbtVehicleEntranceShortReceivedYes.Checked == true )
			{
				DisableVehicleLongFields();
			}		
		}

		#endregion // End of EnableDisabledFields
		
		#region DisableFields

		/// <summary>
		/// Disables all fields at base of form responsible for setting coworker attributes for long vehicle access
		/// </summary>
		internal void DisableVehicleLongFields()
		{
			mViewFrmVehicle.RbtVehicleEntranceLongNo.Enabled = false;
			mViewFrmVehicle.RbtVehicleEntranceLongYes.Enabled = false;
			mViewFrmVehicle.DatVehicleEntranceLongReceivedOn.Enabled = false;
			//mViewFrmVehicle.BtnAcceptedLong.Enabled = false;
		}

		
		/// <summary>
		/// Disables all fields at base of form responsible for setting coworker attributes for short vehicle access
		/// </summary>
		internal void DisableVehicleShortFields()
		{
			mViewFrmVehicle.RbtVehicleEntranceShortReceivedNo.Enabled = false;
			mViewFrmVehicle.RbtVehicleEntranceShortReceivedYes.Enabled = false;
			mViewFrmVehicle.DatVehicleEntranceShortReceivedOn.Enabled = false;
			//mViewFrmVehicle.BtnAcceptedShort.Enabled = false;
		}

		#endregion // End of DisableFields

		#region FillElements

		
		/// <summary>
		/// Get vehicle access BO belonging to current coworker
		/// by looking thru arraylist
		/// </summary>
		/// <param name="pCwrID">PK of current coworker</param>
		/// <returns>veh access BO</returns>
		private VehicleAccessSearch FindBOVehicleAccessByID(decimal pCwrID) 
		{
			foreach ( VehicleAccessSearch vehAcc in arlVehicleAccess ) 
			{
				if ( vehAcc.CwrID == pCwrID ) 
				{
					return vehAcc;
				}
			}
			return null;
		}
		
		
		/// <summary>
		/// If current coworker has veh access type SHORT, load data into fields at base of form for short vehicle access
		/// </summary>
		/// <param name="pCwrID">PK ID of current coworker</param>
		private void FillVehicleShort(decimal pCwrID)
		{
			//Fill short date flag = false
			mAcceptedShortFlag = false;

			// Get (SHORT) veh access data for current coworker
			VehicleAccessSearch VehicleAccess = FindBOVehicleAccessByID(pCwrID);
					
			//Check short entrance is desired
			if (VehicleAccess.VehicleEntrShort == "gewünscht")
			{
				mViewFrmVehicle.RbtVehicleEntranceShort.Checked = true;
				mViewFrmVehicle.BtnAcceptedShort.Enabled = true;

			}
			else
			{
				mViewFrmVehicle.RbtVehicleEntranceShort.Checked = false;
				mViewFrmVehicle.BtnAcceptedShort.Enabled = false;
			}
		
			// check if the short entrance accepted
			if (VehicleAccess.VehicEntrShortRecieve == "akzeptiert")
			{
				mViewFrmVehicle.RbtVehicleEntranceShortReceivedYes.Checked = true;
				mViewFrmVehicle.RbtVehicleEntranceShortReceivedNo.Checked = false;
			}
			else if (VehicleAccess.VehicEntrShortRecieve == "abgelehnt")
			{
				mViewFrmVehicle.RbtVehicleEntranceShortReceivedNo.Checked = true;
				mViewFrmVehicle.RbtVehicleEntranceShortReceivedYes.Checked = false;
				EnableVehicleLongFields();
			}
			else
			{
				mViewFrmVehicle.RbtVehicleEntranceShortReceivedYes.Checked = false;
				mViewFrmVehicle.RbtVehicleEntranceShortReceivedNo.Checked = false;
			}

			// fill the Textelements
			if (VehicleAccess.VehicEntrShortRecDate != null)
			{
				mViewFrmVehicle.DatVehicleEntranceShortReceivedOn.Value = Convert.ToDateTime(VehicleAccess.VehicEntrShortRecDate);
			}
			else
			{
				mViewFrmVehicle.DatVehicleEntranceShortReceivedOn.Value = DateTime.Now;
			}

			if ( ! mViewFrmVehicle.RbtVehicleEntranceShortReceivedYes.Checked && !
				mViewFrmVehicle.RbtVehicleEntranceShortReceivedNo.Checked ) 
			{
				mViewFrmVehicle.TxtVehicleEntranceShortReceivedBy.Text = String.Empty;
			} 
			else 
			{
				mViewFrmVehicle.TxtVehicleEntranceShortReceivedBy.Text = VehicleAccess.VehicEntrShortRecUser.ToString();
			}
			//fill the Boolelements
			if (VehicleAccess.VehicEntrShortRecDate == null)
			{
				mAcceptedShortFlag = false;
			}
			else if (VehicleAccess.VehicEntrShortRecDate != null)
			{
				mAcceptedShortFlag = true;
			}
		}


		/// <summary>
		/// If current coworker has veh access type LONG, load data into fields at base of form for long vehicle access
		/// </summary>
		/// <param name="pCwrID">PK of current CWR</param>
		private void FillVehicleLong(decimal pCwrID)
		{
			//Fill long date flag = false
			mAcceptedLongFlag = false;

			VehicleAccessSearch VehicleAccess = FindBOVehicleAccessByID(pCwrID);

			// Check the long entrance is desired
			if (VehicleAccess.VehicleEntrLong == "gewünscht")
			{
				mViewFrmVehicle.RbtVehicleEntranceLong.Checked = true;
				mViewFrmVehicle.BtnAcceptedLong.Enabled = true;
			}
			else
			{
				mViewFrmVehicle.RbtVehicleEntranceLong.Checked = false;
				mViewFrmVehicle.BtnAcceptedLong.Enabled = false;
			}
			
			//check if the long entrance is accepted
			if (VehicleAccess.VehicEntrLongRecieve == "akzeptiert")
			{
				mViewFrmVehicle.RbtVehicleEntranceLongYes.Checked = true;
				mViewFrmVehicle.RbtVehicleEntranceLongNo.Checked = false;
			}
			else if (VehicleAccess.VehicEntrLongRecieve == "abgelehnt")
			{
				mViewFrmVehicle.RbtVehicleEntranceLongNo.Checked = true;
				mViewFrmVehicle.RbtVehicleEntranceLongYes.Checked = false;
				EnableVehicleShortFields();
			}
			else
			{
				mViewFrmVehicle.RbtVehicleEntranceLongYes.Checked = false;
				mViewFrmVehicle.RbtVehicleEntranceLongNo.Checked = false;
			}

			// fill Text elements
			if (VehicleAccess.VehicEntrLongRecDate != null)
			{
				mViewFrmVehicle.DatVehicleEntranceLongReceivedOn.Value = Convert.ToDateTime(VehicleAccess.VehicEntrLongRecDate);
			}
			else 
			{
				mViewFrmVehicle.DatVehicleEntranceLongReceivedOn.Value = DateTime.Now;
			}

			if ( ! mViewFrmVehicle.RbtVehicleEntranceLongYes.Checked && !
				mViewFrmVehicle.RbtVehicleEntranceLongNo.Checked ) 
			{
				mViewFrmVehicle.TxtVehicleEntranceLongReceivedBy.Text = String.Empty;
			} 
			else 
			{
				mViewFrmVehicle.TxtVehicleEntranceLongReceivedBy.Text = VehicleAccess.VehicEntrLongRecUser;
			}
			
			//fill the Boolelements
			if (VehicleAccess.VehicEntrLongRecDate == null)
			{
				mAcceptedLongFlag = false;
			}
			else if (VehicleAccess.VehicEntrLongRecDate != null)
			{
				mAcceptedLongFlag = true;
			}

		}

		
		/// <summary>
		/// Load individual vehicle access attributes for current coworker
		/// </summary>
		/// <param name="pCwrID">PK ID of current cwr</param>
		internal void FillFields(decimal pCwrID)
		{
			EnableVehicleShortFields();
			EnableVehicleLongFields();
			FillVehicleShort(pCwrID);
			FillVehicleLong(pCwrID);

			if ( mViewFrmVehicle.RbtVehicleEntranceShortReceivedYes.Checked )
			{
				DisableVehicleLongFields();
			}
			else if ( mViewFrmVehicle.RbtVehicleEntranceLongYes.Checked )
			{
				DisableVehicleShortFields();
			}
		}
		
		
		/// <summary>
		/// check witch access is selected. Dependent on the vehicle access, buttons are disabled or enabled 
		/// </summary>
		internal void DisableEnableButton()
		{
			mDesiredLongFlag = false;
			mDesiredShortFlag = false;
			mViewFrmVehicle.BtnAcceptedLong.Enabled  = true;
			mViewFrmVehicle.BtnAcceptedShort.Enabled = true;

			for (int j = 0; j < arlVehicleAccess.Count; j++)
			{
				if ( mViewFrmVehicle.DgrVehicle.IsSelected(j) )
				{
					int currCwrID = Convert.ToInt32(mViewFrmVehicle.DgrVehicle[j, 0].ToString());
					VehicleAccessSearch vehicleaccess = (VehicleAccessSearch)arlVehicleAccess[j];
							
					// Check the long entrance is desired
					if (mViewFrmVehicle.DgrVehicle[j, 6].ToString() == "gewünscht")
					{
						mDesiredLongFlag = true;
					}
					// Check the short entrance is desired
					if (mViewFrmVehicle.DgrVehicle[j, 4].ToString() == "gewünscht")
					{
						mDesiredShortFlag = true;
					}
					
				}
				if ( mDesiredShortFlag )
				{
					mViewFrmVehicle.BtnAcceptedLong.Enabled = false;
				}
				if ( mDesiredLongFlag )
				{
					mViewFrmVehicle.BtnAcceptedShort.Enabled = false;
				}
			}		
		}

		#endregion // End of FillElements

		#region VehicleAccess

		
		/// <summary>
		/// Shows the vehicle access information associated with each coworker.
		/// When the form is first opened (first PreShow, mDefaultValue), 
		/// show only coworkers where long or short access is wished (desired) but not granted, otherwise read search para from gui
		/// </summary>
		internal void GetVehicleAccess() 
		{
			string defaultValWhere = String.Empty;
			mDesiredShortFlag = false;
			mDesiredLongFlag  = false;
			
			((FrmVehicle) mView).RecordsFound		   = 0;   
			((FrmVehicle) mView).DgrVehicle.DataSource = null;

			// DbAccess component: DataProvider, select command & Data Reader
			IProvider mProvider = DBSingleton.GetInstance().DataProvider;
			IDbCommand selComm  = mProvider.CreateCommand(VEHICLEACCESS_QUERY);

			if ( !mDefaultValue )
			{
				this.CopyOutSearchCriteriaVehicleAccess();
				selComm.CommandText = GenerateWhereClauseVehicleAccess( selComm.CommandText );
			}
			else
			{
				defaultValWhere = " WHERE (CWR_MND_ID = "
								+ UserManagementControl.getInstance().CurrentMandatorID.ToString()
								+ ")"
								+ " AND ((( SHORTGATEWAYDESIRE_YN = 'Y')"
								+ " AND ( (SHORTGATEWAY_YN = 'Y' AND SHORTGATEWAYDATE IS NULL) OR (SHORTGATEWAY_YN IS NULL AND SHORTGATEWAYDATE IS NULL) ))" 
								+ " OR  (( LONGGATEWAYDESIRE_YN = 'Y')"
								+ " AND ( (LONGGATEWAY_YN = 'Y' AND LONGGATEWAYDATE IS NULL) OR (LONGGATEWAY_YN IS NULL AND LONGGATEWAYDATE IS NULL) )))"
								+ " ORDER BY CWR_SURNAME";

				selComm.CommandText = selComm.CommandText + defaultValWhere;
				mDefaultValue		= false;
			}

			arlVehicleAccess	= new ArrayList();
			IDataReader mDR		= mProvider.GetReader(selComm);


			DataRow row;
			DataTable displayTable = new DataTable("VehicleTable");
			displayTable.Columns.Add(new DataColumn("CwrID"));
			displayTable.Columns.Add(new DataColumn("SurName"));
			displayTable.Columns.Add(new DataColumn("FirstName"));
			displayTable.Columns.Add(new DataColumn("ExcoName"));
			
			displayTable.Columns.Add(new DataColumn("VehicleShortDesire"));
			displayTable.Columns.Add(new DataColumn("VehicleShortAllowed"));

			displayTable.Columns.Add(new DataColumn("VehicleLongDesire"));
			displayTable.Columns.Add(new DataColumn("VehicleLongAllowed"));

			// Loop thru records and create an ArrayList of VehicleAccess BOs
			while (mDR.Read())
			{
				row = displayTable.NewRow();
				VehicleAccessSearch mBOVehicleAccess = new VehicleAccessSearch();
				 
				mBOVehicleAccess.CwrID				   = Convert.ToDecimal(mDR["CWR_ID"].ToString());
				mBOVehicleAccess.SurName			   = mDR["CWR_SURNAME"].ToString();
				mBOVehicleAccess.FirstName			   = mDR["CWR_FIRSTNAME"].ToString();
				mBOVehicleAccess.ExcoName			   = mDR["EXCO_NAME"].ToString();
				mBOVehicleAccess.VehicleEntrShort	   = mDR["SHORTGATEWAYDESIRE_YN"].ToString();
				mBOVehicleAccess.VehicEntrShortRecieve = mDR["SHORTGATEWAY_YN"].ToString();
				try
				{
					if ( !mDR["SHORTGATEWAYDATE"].Equals(DBNull.Value) )
					{
						mBOVehicleAccess.VehicEntrShortRecDate = Convert.ToDateTime(mDR["SHORTGATEWAYDATE"]).ToString("dd.MM.yyyy");
					}
				}
				catch ( InvalidCastException) 
				{}
				/// new
				/// mBOVehicleAccess.VehicEntrShortRecUser	= mDR["SHORTGATEWAYUSER"].ToString(); 
				/// end new
				mBOVehicleAccess.VehicEntrShortRecUser	= mDR["SHORTUSERNICENAME"].ToString();
				mBOVehicleAccess.VehicleEntrLong		= mDR["LONGGATEWAYDESIRE_YN"].ToString();
				mBOVehicleAccess.VehicEntrLongRecieve	= mDR["LONGGATEWAY_YN"].ToString();
				/// new
				/// mBOVehicleAccess.VehicEntrLongRecUser = mDR["LONGGATEWAYUSER"].ToString();
				/// end new
				mBOVehicleAccess.VehicEntrLongRecUser = mDR["LONGUSERNICENAME"].ToString();
				try
				{
					if ( !mDR["LONGGATEWAYDATE"].Equals(DBNull.Value) )
					{
						mBOVehicleAccess.VehicEntrLongRecDate = Convert.ToDateTime(mDR["LONGGATEWAYDATE"]).ToString("dd.MM.yyyy");
					}
				}
				catch ( InvalidCastException) 
				{}
				

				arlVehicleAccess.Add(mBOVehicleAccess);	
	
				row.ItemArray = new object[8] {mBOVehicleAccess.CwrID,
												   mBOVehicleAccess.SurName,
												   mBOVehicleAccess.FirstName,
												   mBOVehicleAccess.ExcoName,
												   mBOVehicleAccess.VehicleShortDesire,
												   mBOVehicleAccess.VehicleShortAllowed,
												   mBOVehicleAccess.VehicleLongDesire,		
												   mBOVehicleAccess.VehicleLongAllowed};

	

				displayTable.Rows.Add(row);
				
				//Dependent on short desired, the AcceptedShortButton will be enabled or disabled
				if (mBOVehicleAccess.VehicleShortDesire.Equals("gewünscht"))
				{
					((FrmVehicle) mView).BtnAcceptedShort.Enabled = true;
					mDesiredShortFlag = true;
				}
				else if (!mBOVehicleAccess.VehicleShortDesire.Equals("gewünscht")&& mDesiredShortFlag.Equals(false))
				{
					((FrmVehicle) mView).BtnAcceptedShort.Enabled = false;
				}
				
				//Dependent on long desired, the AcceptedLongButton will be enabled or disabled
				if (mBOVehicleAccess.VehicleLongDesire.Equals("gewünscht"))
				{
					((FrmVehicle) mView).BtnAcceptedLong.Enabled = true;
					mDesiredLongFlag = true;
				}
				else if (!mBOVehicleAccess.VehicleLongDesire.Equals("gewünscht")&& mDesiredLongFlag.Equals(false))
				{
					((FrmVehicle) mView).BtnAcceptedLong.Enabled = false;
				}
				mRecordsFound ++;

			}
			mDR.Close();

			// Bind data grid in Form to the arrayList
			if ( arlVehicleAccess.Count > 0 ) 
			{
				// hier muss das grid auf dem usercontrol angegeben werden
				((FrmVehicle) mView).BtnNotAccepted.Enabled	  = true;
				((FrmVehicle) mView).BtnSave.Enabled		  = true;
				((FrmVehicle) mView).DgrVehicle.DataSource	  = displayTable;	
				((FrmVehicle) mView).DgrVehicle.Select(0);
				((FrmVehicle) mView).RecordsFound			  = arlVehicleAccess.Count;

				((FrmVehicle) mView).TableClick();
				ShowMessageInStatusBar("Meldung: " + arlVehicleAccess.Count + " Fremdfirmenmitarbeiter gefunden");				

			} 
			else 
			{
				((FrmVehicle) mView).BtnNotAccepted.Enabled	  = false;
				((FrmVehicle) mView).BtnSave.Enabled		  = false;
				ShowMessageInStatusBar( MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_RESULTS) );
				((FrmVehicle) mView).RecordsFound = 0;
			}
		}
		
		
		/// <summary>
		/// Gets the values of the relevant fields in the GUI to make up the search parameters
		/// </summary>
		/// <exception cref="Degussa.FPASS.Util.Exceptions.UIWarningException">UIWarningException</exception> if no search parameters chosen
		private void CopyOutSearchCriteriaVehicleAccess() 
		{
			int	noSearchCriteria = 0;
			mwhereClause		 = String.Empty;

			this.mVehicleYesPara = ((FrmVehicle) mView).RbtSearchVehicleYes.Checked;
			if ( mVehicleYesPara == false)
			{
				noSearchCriteria ++;
			}

			this.mVehicleNoPara = ((FrmVehicle) mView).RbtSearchVehicleNo.Checked;
			if ( mVehicleNoPara == false )
			{
				noSearchCriteria ++;
			}

			this.mVehicleResieveYesPara = ((FrmVehicle) mView).RbtSearchVehicleNotRecievedYes.Checked;
			if ( mVehicleResieveYesPara == false)
			{
				noSearchCriteria ++;
			}

			this.mVehicleResieveNoPara = ((FrmVehicle) mView).RbtSearchVehicleNotRecievedNo.Checked;
			if ( mVehicleResieveNoPara == false )
			{
				noSearchCriteria ++;
			}
			
			this.mVehicleShortNoExtended = ((FrmVehicle) mView).rbtSearchVehicleShortNoExecuted.Checked;
			if ( mVehicleShortNoExtended == false)
			{
				noSearchCriteria ++;
			}
			
			this.mVehicleLongNoExtended = ((FrmVehicle) mView).rbtSearchVehicleLongNoExecuted.Checked;
			if ( mVehicleLongNoExtended == false)
			{
				noSearchCriteria ++;
			}

			this.mVehicleShortAccepted = ((FrmVehicle) mView).rbtSearchVehicleShortAccepted.Checked;
			if ( mVehicleShortAccepted == false )
			{
				noSearchCriteria ++;
			}
			
			this.mVehicleLongAccepted = ((FrmVehicle) mView).rbtSearchVehicleLongAccepted.Checked;
			if ( mVehicleLongAccepted == false )
			{
				noSearchCriteria ++;
			}



			if ( noSearchCriteria == 8 ) 
			{
				/*throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.NO_SEARCH_CRITERIA));*/	
			} 
			else
			{
				// Return to default
				whereSetNotaccept = false;
                whereSetNotproc = false;
			}			
		}

		/// <summary>
		/// Appends WHERE text onto text of current SELECT statement according to which search paras have been set
		/// </summary>
		/// <param name="pSelect">Original text of SELECT statement</param>
		/// <returns></returns>
		private String GenerateWhereClauseVehicleAccess(String pSelect) 
		{
			string whereClause = String.Empty;
			string orderClause = String.Empty;

			bool		shortgatewaywhere = false;
			bool		vehiclereceivedwhere = false;
			bool		vehicleprocesswhere = false;
			bool		vehicleacceptedwhere = false; 
			bool		DefaultWhere		 = false;


			whereClause = " WHERE CWR_MND_ID = "
							+ UserManagementControl.getInstance().CurrentMandatorID.ToString();

			if ( this.mVehicleYesPara ) 
			{
				whereClause = whereClause + " AND (( SHORTGATEWAYDESIRE_YN = 'Y'"; 
				shortgatewaywhere = true;
				DefaultWhere	  = true;
			}

			if ( this.mVehicleNoPara ) 
			{
				if ( shortgatewaywhere ) 
				{
					whereClause = whereClause + " OR LONGGATEWAYDESIRE_YN = 'Y' "; 
				} 
				else 
				{
					whereClause = whereClause + " AND (( LONGGATEWAYDESIRE_YN = 'Y' "; 
				}
				DefaultWhere	  = true;
			}
			whereClause = this.SetLastBracket(whereClause);

			//falls kurz abgelehnt angeklickt
			if ( this.mVehicleResieveYesPara ) 
			{
				whereClause = whereClause + " AND (( SHORTGATEWAY_YN = 'Y' AND SHORTGATEWAYDATE IS NOT NULL )"; 
				vehiclereceivedwhere = true;
				DefaultWhere	  = true;
			}

			//falls lang abgelehnt angeklickt
			if ( this.mVehicleResieveNoPara ) 
			{

				if ( vehiclereceivedwhere ) 
				{
					whereClause = whereClause + " OR (LONGGATEWAY_YN = 'Y' AND LONGGATEWAYDATE IS NOT NULL)"; 
				} 
				else 
				{
					whereClause = whereClause + " AND (((LONGGATEWAY_YN = 'Y' AND LONGGATEWAYDATE IS NOT NULL)";
				}	
				DefaultWhere	  = true;
			}
			whereClause = this.SetLastBracket(whereClause);

			// if not process
			if ( this.mVehicleShortNoExtended ) 
			{
				whereClause = whereClause + " AND (( SHORTGATEWAYDESIRE_YN = 'Y' AND SHORTGATEWAYDATE IS NULL)";
				vehicleprocesswhere = true;
				DefaultWhere	  = true;
			}

			if ( this.mVehicleLongNoExtended ) 
			{
				if ( vehicleprocesswhere )
				{
					whereClause = whereClause + " OR (LONGGATEWAYDESIRE_YN = 'Y' AND LONGGATEWAYDATE IS NULL)"; 
				}
				else
				{
					whereClause = whereClause + " AND (((LONGGATEWAYDESIRE_YN = 'Y' AND LONGGATEWAYDATE IS NULL)"; 
				}
				DefaultWhere	  = true;
			}

			// if accepted short choose
			if ( this.mVehicleShortAccepted ) 
			{
				whereClause = whereClause + " AND (( SHORTGATEWAY_YN = 'N' AND SHORTGATEWAYDATE IS NOT NULL )"; 
				vehicleacceptedwhere = true;
				DefaultWhere	  = true;
			}

			// if accepted short choose
			if ( this.mVehicleLongAccepted ) 
			{

				if ( vehicleacceptedwhere ) 
				{
					whereClause = whereClause + " OR (LONGGATEWAY_YN = 'N' AND LONGGATEWAYDATE IS NOT NULL)"; 
				} 
				else 
				{
					whereClause = whereClause + " AND (((LONGGATEWAY_YN = 'N' AND LONGGATEWAYDATE IS NOT NULL)";
				}	
				DefaultWhere	  = true;
			}
			
			// If no search criteria where given then use default
			if (!DefaultWhere)
			{
				whereClause = " WHERE (CWR_MND_ID = "
					+ UserManagementControl.getInstance().CurrentMandatorID.ToString()
					+ ")"
					+ " AND ((( SHORTGATEWAYDESIRE_YN = 'Y')"
					+ " AND ( (SHORTGATEWAY_YN = 'Y' AND SHORTGATEWAYDATE IS NULL) OR (SHORTGATEWAY_YN IS NULL AND SHORTGATEWAYDATE IS NULL) ))" 
					+ " OR  (( LONGGATEWAYDESIRE_YN = 'Y')"
					+ " AND ( (LONGGATEWAY_YN = 'Y' AND LONGGATEWAYDATE IS NULL) OR (LONGGATEWAY_YN IS NULL AND LONGGATEWAYDATE IS NULL) )))";
			}
			
			whereClause = this.SetLastBracket(whereClause);

			whereClause = pSelect + whereClause;
			orderClause = " ORDER BY CWR_SURNAME";

			whereClause = this.SetLastBracket(whereClause);

			return whereClause + orderClause;
		}

		
		/// <summary>
		/// Put brackets in the right place in SQL SELECT statement 
		/// </summary>
		/// <param name="sqlString"></param>
		/// <returns></returns>
		private String SetLastBracket(String sqlString) 
		{
			int numberOfOpeningBrackets = 0;
			int numberOfClosingBrackets = 0;

			for ( int i=0; i < sqlString.Length; i++ ) 
			{
				if ( sqlString[i].ToString().Equals("(") )  
				{
					numberOfOpeningBrackets++;
				}

				if ( sqlString[i].ToString().Equals(")") ) 
				{
					numberOfClosingBrackets++;
				}
			}
			if ( numberOfClosingBrackets < numberOfOpeningBrackets ) 
			{
				int dif = numberOfOpeningBrackets - numberOfClosingBrackets;
				for ( int i=0; i < dif; i++ ) 
				{
					sqlString = sqlString + ")";
				}
			}
			return sqlString;

		}

		#endregion // End of VehicleAccess

		#region UpdateVehicleAccess	

		#region UpdateOneSelectedVehicleAccess	

		
		/// <summary>
		/// Update vehicle attributes for current coworker (the case that these were changed directly
		/// in base of form: do not know which type of access was granted or denied, user, date etc)
		/// </summary>
		/// <param name="pCwrID">PK ID of current coworker</param>
		internal void ExtendAccessUpdate(decimal pCwrID)
		{
			CheckForUpdate();
			
			// Case 1: short not desired, short date not existed on DB, accepted short
			// Update existing LONG record in DB: deny short entry wish as cannot have both granted
			// Insert SHORT as there is no record in DB yet
			if (mVehicleDesireShort.Equals("N")&& mAcceptedShortFlag.Equals(false)&& mVehicleAcceptedShort.Equals("N"))
			{
				InsertAccess( pCwrID, mBrfIDShort, mVehicleAcceptedShort, mVehicleDesireShort, mVehicleDateShort );
				UpdateVehicleAccess( pCwrID, mVehicleDesireLong, mVehicleDateLong, mVehicleAcceptedLong, mBrfIDLong );
				base.ExportCwrToZKS(pCwrID);
			}
				// Case 2: long not desired, long date not existed on DB, accepted long
				// Update existing SHORT record in DB: deny LONG entry wish as cannot have both granted
				// Insert LONG as there is no record in DB yet 	
			else if ( mVehicleDesireLong.Equals("N") && mAcceptedLongFlag.Equals(false)&& mVehicleAcceptedLong.Equals ("N"))
			{	
				InsertAccess( pCwrID, mBrfIDLong, mVehicleAcceptedLong, mVehicleDesireLong, mVehicleDateLong );
				UpdateVehicleAccess( pCwrID, mVehicleDesireShort, mVehicleDateShort, mVehicleAcceptedShort, mBrfIDShort );
				base.ExportCwrToZKS(pCwrID);
			}
				// Case 3: short not desired, short date existed on DB
				// Update existing SHORT, as record already in DB
				// Update existing LONG, as record already in DB
				// Not Update, as long not desired, long date not existed on DB, long not accepted
			
			else if (mVehicleDesireShort.Equals("N")&& mAcceptedShortFlag.Equals(true))
			{
				UpdateVehicleAccess( pCwrID, mVehicleDesireShort, mVehicleDateShort, mVehicleAcceptedShort, mBrfIDShort );
				if(!(mVehicleDesireLong.Equals("N")&& mAcceptedLongFlag.Equals(false)&& mVehicleAcceptedLong.Equals("Y")))
				{
					if(!(mViewFrmVehicle.RbtVehicleEntranceLongYes.Checked == false && mViewFrmVehicle.RbtVehicleEntranceLongNo.Checked == false))
					{
						UpdateVehicleAccess( pCwrID, mVehicleDesireLong, mVehicleDateLong, mVehicleAcceptedLong, mBrfIDLong );
					}
				}
				base.ExportCwrToZKS(pCwrID);
			}
				// Case 4: Long not desired, long date existed on DB
				// Update existing LONG, as record already in DB
				// Update existing SHORT, as record already in DB
				// Not Update, as short not desired, short date not existed on DB, short not accepted

			else if (mVehicleDesireLong.Equals("N") && mAcceptedLongFlag.Equals(true))
			{
				UpdateVehicleAccess( pCwrID, mVehicleDesireLong, mVehicleDateLong, mVehicleAcceptedLong, mBrfIDLong );
				if (!(mVehicleDesireShort.Equals("N")&& mAcceptedShortFlag.Equals(false)&& mVehicleAcceptedShort.Equals("Y")))
				{
					if(!(mViewFrmVehicle.RbtVehicleEntranceShortReceivedNo.Checked == false && mViewFrmVehicle.RbtVehicleEntranceShortReceivedYes.Checked == false))
					{
						UpdateVehicleAccess( pCwrID, mVehicleDesireShort, mVehicleDateShort, mVehicleAcceptedShort, mBrfIDShort );
					}
				}
				base.ExportCwrToZKS(pCwrID);
			}
				// Case 5: short desired
				// Update existing SHORT, as record already in DB
				// Update existing LONG, as record already in DB
				// Not Update, as long not desired, long date not existed on DB, long not accepted
			else if (mVehicleDesireShort.Equals("Y"))
			{
				if(!mVehicleAcceptedShort.Equals(""))
				{
					UpdateVehicleAccess( pCwrID, mVehicleDesireShort, mVehicleDateShort, mVehicleAcceptedShort, mBrfIDShort );
				}
				if(!(mVehicleDesireLong.Equals("N")&& mAcceptedLongFlag.Equals(false)&& mVehicleAcceptedLong.Equals("Y")))
				{
					UpdateVehicleAccess( pCwrID, mVehicleDesireLong, mVehicleDateLong, mVehicleAcceptedLong, mBrfIDLong );
				}
				base.ExportCwrToZKS(pCwrID);
			}
				// Case 6: long desired
				// Update existing LONG, as record already in DB
				// Update existing SHORT, as record already in DB
				// Not Update, as short not desired, short date not existed on DB, short not accepted
			else if (mVehicleDesireLong.Equals("Y"))
			{
				if(!mVehicleAcceptedLong.Equals(""))
				{
					UpdateVehicleAccess( pCwrID, mVehicleDesireLong, mVehicleDateLong, mVehicleAcceptedLong, mBrfIDLong);
				}
				if(!(mVehicleDesireShort.Equals("N")&& mAcceptedShortFlag.Equals(false)&& mVehicleAcceptedShort.Equals("Y")))
				{
					UpdateVehicleAccess( pCwrID, mVehicleDesireShort, mVehicleDateShort, mVehicleAcceptedShort, mBrfIDShort );
				}
				base.ExportCwrToZKS(pCwrID);
			}
			else
			{
				ShowMessageInStatusBar( MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_SAVE) );
			}
		}
				
		/// <summary>
		/// Get vehicle access details for current coworker, loaded into fields in lower part of form 
		/// </summary>
		private void CheckForUpdate()
		{
			// Short stuff
			mVehicleDesireShort   = String.Empty;
			mVehicleAcceptedShort = String.Empty;
			mVehicleDateShort	  = DateTime.Now;
			mBrfIDShort			  = -1;

			// Check if short entrance was desired
			if (mViewFrmVehicle.RbtVehicleEntranceShort.Checked == true)
			{
				mVehicleDesireShort = "Y";
			}
			else
			{
				mVehicleDesireShort = "N";
			}
		
			// check if short entrance was accepted
			if ( mViewFrmVehicle.RbtVehicleEntranceShortReceivedYes.Checked )
			{
				mVehicleAcceptedShort = "N";
			}
			else if ( mViewFrmVehicle.RbtVehicleEntranceShortReceivedNo.Checked )
			{
				mVehicleAcceptedShort = "Y";
			}
			else
			{
				mVehicleAcceptedShort = String.Empty;
			}

			// Get ID for short vehicle access briefing from Globals
			mVehicleDateShort = mViewFrmVehicle.DatVehicleEntranceShortReceivedOn.Value;
			mBrfIDShort	 = Globals.GetInstance().BriefVehicleEntranceShortID;
			
			mVehicleDesireLong   = String.Empty;
			mVehicleAcceptedLong = String.Empty;
			mVehicleDateLong	 = DateTime.Now;
			mBrfIDLong			 = -1;
			
			// Check if long entrance is desired
			if ( mViewFrmVehicle.RbtVehicleEntranceLong.Checked )
			{
				mVehicleDesireLong = "Y";
			}
			else
			{
				mVehicleDesireLong = "N";
			}
		
			// check if long entrance is accepted
			if ( mViewFrmVehicle.RbtVehicleEntranceLongYes.Checked )
			{
				mVehicleAcceptedLong = "N";
			}
			else if ( mViewFrmVehicle.RbtVehicleEntranceLongNo.Checked )
			{
				mVehicleAcceptedLong = "Y";
			}
			else
			{
				mVehicleAcceptedLong = String.Empty;
			}

			// Get ID for long vehicle briefing from Globals (normally 49)
			mVehicleDateLong = mViewFrmVehicle.DatVehicleEntranceLongReceivedOn.Value;
			mBrfIDLong       = Globals.GetInstance().BriefVehicleEntranceLongID;			
			
		}
		

		/// <summary>
		/// Updates vehicle access for selected coworker
		/// </summary>
		/// <param name="pCwrID">PK of coworker</param>
		/// <exception cref="System.Data.OracleClient.OracleException">OracleException</exception>
		/// Rollback transaction if a database error occurs
		private void UpdateVehicleAccess(decimal pCwrID, string pVehicleDesire, DateTime pVehicleDate, string pVehicleAccepted, decimal pBrfID )
		{				
			try
			{			
				IProvider mProvider = DBSingleton.GetInstance().DataProvider;

				//Open the transaction using dummy command & connection
				dummyComm		= mProvider.CreateCommand("SequenceDummy");
				drTransaction	= mProvider.GetReader(dummyComm);
				trans			= mProvider.GetTransaction(dummyComm);
				
				updComm			= mProvider.CreateCommand(VEHICLEACCESS_UPDATE);
				mProvider.SetParameter(updComm, ":CWBR_CWR_ID", pCwrID);
				mProvider.SetParameter(updComm, ":CWBR_BRIEFING_YN", pVehicleDesire );
				mProvider.SetParameter(updComm, ":CWBR_BRIEFINGDATE", pVehicleDate);
				mProvider.SetParameter(updComm, ":CWBR_INACTIVE_YN", pVehicleAccepted);
				mProvider.SetParameter(updComm, ":CWBR_USER_ID", mUserID);
				mProvider.SetParameter(updComm, ":CWBR_BRF_ID", pBrfID);
					
				updComm.Connection  = dummyComm.Connection;
				updComm.Transaction = trans;
				updComm.ExecuteNonQuery();

				trans.Commit();
				drTransaction.Close();
				//base.ExportDataToZKS(pCwrID);
			}
			catch (System.Data.OracleClient.OracleException oraex)
			{
				if (trans != null && drTransaction != null)
				{
					trans.Rollback();
					drTransaction.Close();		
					throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.FATAL_DB_ERROR) 
						+ oraex.Message 
						+ " Objekt: " + this.ToString() );
				}
			}
		}

		#endregion //End of UpdateOneSelectedVehicleAccess

		#region UpdateSelectedVehicleAccess
	
		/// <summary>
		/// If short access is desired then grant short access
		/// for each coworker selected in datagrid 
		/// </summary>
		/// <param name="pCwrID">PK ID of current coworker (used?)</param>
		/// <exception cref="de.pta.Component.Errorhandling.UIWarningException">if current coworker has wrong access type</exception>
		internal void UpdateSelectedShortAccess(decimal pCwrID)
		{
			mBrfID			 = -1;
			mAccessNoShort	 = false;		
			mVehicleAccepted = String.Empty;

			for (int j = 0; j < arlVehicleAccess.Count; j++)
			{
				if ( mViewFrmVehicle.DgrVehicle.IsSelected(j) )
				{
					int currCwrID = Convert.ToInt32(mViewFrmVehicle.DgrVehicle[j, 0].ToString());
					string access = mViewFrmVehicle.DgrVehicle[j, 4].ToString();
					string accesslongAll = mViewFrmVehicle.DgrVehicle[j, 7].ToString();
							
					if (access.Equals(ACCESS_SHORT))
					{
						mBrfID = Globals.GetInstance().BriefVehicleEntranceShortID;
						mVehicleAccepted = "N";
						this.UpdateAccess(currCwrID, mBrfID, mVehicleAccepted);
						
						if (accesslongAll.Equals("akzeptiert"))
						{
							mBrfID			 =	Globals.GetInstance().BriefVehicleEntranceLongID;
							mVehicleAccepted = "Y";
							this.UpdateAccess(currCwrID, mBrfID, mVehicleAccepted);
						}
						
						mViewFrmVehicle.DgrVehicle.UnSelect(j);
					}
					else 
					{
						mAccessNoShort=true;
					}
					base.ExportCwrToZKS(currCwrID);
				}
			}		
			if (mAccessNoShort)
			{
				throw new UIWarningException(MessageSingleton.GetInstance().
					GetMessage(MessageSingleton.INVALID_ACCESS_EXTEND));
			}
		}

		
		/// <summary>
		/// If long access is desired then grant long access
		/// for each coworker selected in datagrid 
		/// </summary>
		/// <param name="pCwrID">PK ID of current coworker (used?)</param>
		/// <exception cref="de.pta.Component.Errorhandling.UIWarningException">if current coworker has wrong access type</exception>
		internal void UpdateSelectedLongAccess(decimal pCwrID)
		{
			mBrfID			 = -1;
			mAccessNoLong	 = false;		
			mVehicleAccepted = String.Empty;
	
			for (int j = 0; j < arlVehicleAccess.Count; j++)
			{
				if ( mViewFrmVehicle.DgrVehicle.IsSelected(j) )
				{
					int currCwrID = Convert.ToInt32(mViewFrmVehicle.DgrVehicle[j, 0].ToString());
					string access = mViewFrmVehicle.DgrVehicle[j, 6].ToString();
					string accessshortAll = mViewFrmVehicle.DgrVehicle[j, 5].ToString();
							
					if (access.Equals(ACCESS_LONG))
					{
						mBrfID           = Globals.GetInstance().BriefVehicleEntranceLongID;
						mVehicleAccepted = "N";
						this.UpdateAccess(currCwrID, mBrfID, mVehicleAccepted);
						if (accessshortAll.Equals("akzeptiert"))
						{
							mBrfID			 =	Globals.GetInstance().BriefVehicleEntranceShortID;
							mVehicleAccepted = "Y";
							this.UpdateAccess(currCwrID, mBrfID, mVehicleAccepted);
						}
						mViewFrmVehicle.DgrVehicle.UnSelect(j);
					}
					else 
					{
						mAccessNoLong=true;
					}
					base.ExportCwrToZKS( currCwrID );
				}
			}			
			if (mAccessNoLong)
			{
				throw new UIWarningException(MessageSingleton.GetInstance().
					GetMessage(MessageSingleton.INVALID_ACCESS_EXTEND));
			}
		}

		/// <summary>
		/// If a new vehicle entry record is created (e.g. if an entry that wasn't desired is granted)
		/// perform DB insert.
		/// Update 28.02.2005: new col FPASS_CWRBRIEFING.CWBR_LASTUSER_ID (added 2004) 
		/// was never included here.      
		/// </summary>
		/// <param name="pCwrID"></param>
		/// <param name="pBrfID"></param>
		/// <param name="pVehicleAccepted"></param>
		/// <param name="pVehicleDesire"></param>
		/// <param name="pVehicleDate"></param>
		private void InsertAccess(decimal pCwrID, decimal pBrfID, string pVehicleAccepted, string pVehicleDesire, System.DateTime pVehicleDate)
		{
			try
			{
				/// Get PK value from ORA sequence
				decimal currPKVal = this.GetNextValFromSeq("SEQ_CWRBRIEFING");

				/// Use dummy command and data reader to get an open connection
				IProvider mProvider = DBSingleton.GetInstance().DataProvider;
				dummyComm		= mProvider.CreateCommand("SequenceDummy");
				drTransaction	= mProvider.GetReader(dummyComm);

				insComm = mProvider.CreateCommand(VEHICLEACCESS_INSERT);

				mProvider.SetParameter(insComm, ":CWBR_ID", currPKVal);
				mProvider.SetParameter(insComm, ":CWBR_CWR_ID", pCwrID);
				mProvider.SetParameter(insComm, ":CWBR_BRF_ID", pBrfID);
				mProvider.SetParameter(insComm, ":CWBR_USER_ID", mUserID);
				mProvider.SetParameter(insComm, ":CWBR_BRIEFING_YN", pVehicleDesire);
				mProvider.SetParameter(insComm, ":CWBR_BRIEFINGDATE", pVehicleDate);
				mProvider.SetParameter(insComm, ":CWBR_INACTIVE_YN", pVehicleAccepted);
				/// new
				mProvider.SetParameter(insComm, ":CWBR_LASTUSER_ID", mUserID);

				insComm.Connection  = dummyComm.Connection;
				insComm.ExecuteNonQuery();

				drTransaction.Close();
			}
			catch (System.Data.OracleClient.OracleException oraex)
			{
				if (trans != null && drTransaction != null)
				{				
					trans.Rollback();
					drTransaction.Close();
					throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.FATAL_DB_ERROR) 
						+ oraex.Message );
					
				}
			}
		}
		
		/// <summary>
		/// Build & execute SQL statement to update vehicle access for selected coworker and veh access type
		/// </summary>
		/// <param name="pCwrID">PK of current CWR</param>
		/// <param name="pBrfID">ID of vehicle access (briefing) type, 48 or 49</param>
		/// <param name="pVehicleAccepted">parameter Accepted</param>
		/// <exception cref="System.Data.OracleClient.OracleException">if database returns an error</exception>
		private void UpdateAccess(decimal pCwrID, decimal pBrfID, string pVehicleAccepted)
		{
			this.ClearStatusBar();
			try
			{			
				// Get DataProvider from DbAccess component
				IProvider mProvider = DBSingleton.GetInstance().DataProvider;

				//Open the transaction
				//Open dummy command & connection
				dummyComm		= mProvider.CreateCommand("SequenceDummy");
				drTransaction	= mProvider.GetReader(dummyComm);
				trans			= mProvider.GetTransaction(dummyComm);

				// use the open transaction for update
				updComm = mProvider.CreateCommand(SELECTEDVEHICLEACC_UPDATE);
				
				mProvider.SetParameter(updComm, ":CWBR_CWR_ID", pCwrID);
				mProvider.SetParameter(updComm, ":CWBR_INACTIVE_YN", pVehicleAccepted);
				mProvider.SetParameter(updComm, ":CWBR_USER_ID", mUserID);
				mProvider.SetParameter(updComm, ":CWBR_BRF_ID", pBrfID);
							
				updComm.Connection  = dummyComm.Connection;
				updComm.Transaction = trans;
				updComm.ExecuteNonQuery();

				trans.Commit();
				drTransaction.Close();
				//base.ExportDataToZKS(pCwrID);
			}
			catch (System.Data.OracleClient.OracleException oraex)
			{
				if (trans != null && drTransaction != null)
				{
					trans.Rollback();
					drTransaction.Close();
					throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.FATAL_DB_ERROR) 
						+ oraex.Message 
						+ " Objekt: " + this.ToString() );
				}
			}
		}
		
		#endregion //End of UpdateSelectedVehicleAccess

		#region UpdateSelectedNotAcceptedVehicleAccess
		
		
		/// <summary>
		/// Deny all coworkers selected in datagrid their desired access type (deals with both short and long)
		/// </summary>
		/// <param name="pCwrID">PK ID of current cwr (used?)</param>
		internal void UpdateSelectedAccess(decimal pCwrID)
		{
			mBrfIDLong = -1;
			mBrfID = -1;
			mVehicleAccepted = String.Empty;
	
			for (int j = 0; j < arlVehicleAccess.Count; j++)
			{
				if ( mViewFrmVehicle.DgrVehicle.IsSelected(j) )
				{
					int currCwrID = Convert.ToInt32(mViewFrmVehicle.DgrVehicle[j, 0].ToString());
					mBrfIDLong    = Globals.GetInstance().BriefVehicleEntranceLongID;
					mBrfID	      = Globals.GetInstance().BriefVehicleEntranceShortID;
					mVehicleAccepted = "Y";
					this.UpdateNotAcceptedAccess(currCwrID, mBrfID, mBrfIDLong, mVehicleAccepted);
					mViewFrmVehicle.DgrVehicle.UnSelect(j);
					base.ExportCwrToZKS(currCwrID);
				}
			}
		}

	
		/// <summary>
		/// Builds and executes SQL statement to update coworkers who have been denied their desired vehicle access
		/// </summary>
		/// <param name="pCwrID">PK of current CWR</param>
		/// <param name="pBrfID">ID of vehicle access (briefing) type SHORT (48)</param>
		/// <param name="pBrfIDLong">ID of vehicle access (briefing) type LONG (49)</param>
		/// <param name="pVehicleAccepted">parameter Accepted ('Y')</param>
		/// <exception cref="System.Data.OracleClient.OracleException">if database returns an error</exception>
		private void UpdateNotAcceptedAccess(decimal pCwrID, decimal pBrfID, decimal pBrfIDLong, string pVehicleAccepted)
		{
			try
			{			
				// Get DataProvider from DbAccess component
				IProvider mProvider = DBSingleton.GetInstance().DataProvider;

				//Open the transaction
				//Open dummy command & connection
				dummyComm		= mProvider.CreateCommand("SequenceDummy");
				drTransaction	= mProvider.GetReader(dummyComm);
				trans			= mProvider.GetTransaction(dummyComm);

				// use the open transaction for update
				updComm = mProvider.CreateCommand(SELECTEDNOTACEPTVE_UPDATE);
				
				mProvider.SetParameter(updComm, ":CWBR_CWR_ID", pCwrID);
				mProvider.SetParameter(updComm, ":CWBR_INACTIVE_YN", pVehicleAccepted);
				mProvider.SetParameter(updComm, ":CWBR_USER_ID", mUserID);
				mProvider.SetParameter(updComm, ":CWBR_BRF_ID", pBrfID);
				mProvider.SetParameter(updComm, ":CWBR_BRFL_ID", pBrfIDLong);
							
				updComm.Connection  = dummyComm.Connection;
				updComm.Transaction = trans;
				updComm.ExecuteNonQuery();

				trans.Commit();
				drTransaction.Close();
				//base.ExportDataToZKS(pCwrID);
			}
			catch (System.Data.OracleClient.OracleException oraex)
			{
				if (trans != null && drTransaction != null)
				{
					trans.Rollback();
					drTransaction.Close();
					throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
				}
			}
		}
		
		#endregion //End of UpdateSelectedNotAcceptedVehicleAccess


		#endregion //End of UpdateVehicleAccess

		#endregion // End of Methods

	}
}
