using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

using de.pta.Component.DbAccess;
using de.pta.Component.ListOfValues;
using de.pta.Component.Errorhandling;

using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Bo.Administration;
using Degussa.FPASS.Db;
using Degussa.FPASS.Util;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// An ExternalContractorSearchModel is the model 
	/// of the MVC-triad ExternalContractorSearchModel,
	/// ExternalContractorSearchController and FrmExternalSearchContractor.
	/// ExternalContractorSearchModel extends from the AbstractModel.
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
	public class ExternalContractorSearchModel  : FPASSBaseModel
	{
		#region Members

		/// <summary>
		/// Database queries
		/// </summary>
		private const string EXCONTRACTOR_QUERY		= "ExternalContractor";
		
		/// <summary>
		/// holds number of search values elected in GUI
		/// </summary>
		private string		 mExContractorParameter;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public ExternalContractorSearchModel()
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

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Empties datagrid & textbox for search 
		/// </summary>
		internal override void PreClose()
		{
			((FrmSearchExternalContractor) mView).DgrExternalContractor.DataSource = null;
			((FrmSearchExternalContractor) mView).TxtExternalContractor.Text = String.Empty;
			this.ClearStatusBar();
		}


		#region ExContractor

		/// <summary>
		/// Selects ExContractor/s which match the given search criteria,
		/// Build arraylist of ExContractor value objects, bind arraylist to datagrid (no sorting)
		/// The attributes are displayed in the datagrid on the form.
		/// </summary>
		/// <exception cref="de.pta.Component.Errorhandling.UIWarningException">thrown when query returns no results </exception>
		internal void GetExContractor() 
		{
			((FrmSearchExternalContractor) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
			((FrmSearchExternalContractor) mView).DgrExternalContractor.DataSource = null;
			this.ClearStatusBar();

			// read search criteria from gui
			this.CopyOutSearchCriteriaExContractor();

			// Get DataProvider from DbAccess component and create select command
			IProvider mProvider = DBSingleton.GetInstance().DataProvider;
			IDbCommand selComm  = mProvider.CreateCommand(EXCONTRACTOR_QUERY);
			selComm.CommandText = GenerateWhereClauseExContractor(selComm.CommandText );

			// Instantiate Arraylist
			ArrayList arlExContractor = new ArrayList();

			// Open data reader and loop thru records to create an ArrayList of ExContractor BOs
			IDataReader mDR = mProvider.GetReader(selComm);

			// 
			while (mDR.Read())
			{
				BOExternalContractor mBOExternalContractor		= new BOExternalContractor();

				mBOExternalContractor.PropAdminBOID			= Convert.ToDecimal(mDR["EXCO_ID"]);
				mBOExternalContractor.PropAdminBOName		= mDR["EXCO_NAME"].ToString();
				mBOExternalContractor.PropexcoCity			= mDR["EXCO_CITY"].ToString();
				mBOExternalContractor.PropexcoCountry		= mDR["EXCO_COUNTRY"].ToString();
				mBOExternalContractor.PropexcoPostcode		= mDR["EXCO_POSTCODE"].ToString();
				mBOExternalContractor.PropexcoStreet		= mDR["EXCO_STREET"].ToString();

				arlExContractor.Add(mBOExternalContractor);			
			}
			mDR.Close();
		
			if ( arlExContractor.Count > 0 ) 
			{
				// Bind data grid in Form to arrayList
				((FrmSearchExternalContractor) mView).Cursor = System.Windows.Forms.Cursors.Default;
				((FrmSearchExternalContractor) mView).DgrExternalContractor.DataSource = arlExContractor;	
				this.ShowMessageInStatusBar( "Meldung: " + arlExContractor.Count + " Fremdfirmen gefunden." );
			} 
			else 
			{
				((FrmSearchExternalContractor) mView).Cursor = System.Windows.Forms.Cursors.Default;
				((FrmSearchExternalContractor) mView).BtnAssume.Enabled = false;	
				this.ShowMessageInStatusBar( MessageSingleton.GetInstance().GetMessage
											(MessageSingleton.NO_RESULTS));
			}
		}

		
		/// <summary>
		/// Generates WHERE clause of SQL statement text dynamically according to what search parameters set
		/// Mandator ID is always set: excos are mandator-dependent
		/// </summary>
		/// <param name="pSelect">first part of SQL text</param>
		/// <returns>complete SQL text, including WHERE clause, to be executed</returns>
		private String GenerateWhereClauseExContractor(String pSelect) 
		{
			String	whereClause = "";

			// Changed: mandant ID is a number and does not have to be enclosed in ''
			whereClause = " WHERE EXCO_MND_ID = " + UserManagementControl.getInstance().CurrentMandatorID;

			if ( this.mExContractorParameter.Length > 0 ) 
			{
				whereClause = whereClause 
							+ " AND UPPER(EXCO_NAME) LIKE '" 
							+ this.mExContractorParameter.ToUpper() 
							+ "' "; 
			}

			// filter out invalid excos
			whereClause = whereClause 
						+ "AND (EXCO_STATUS = '"
						+ Globals.STATUS_VALID
						+ "' "
						+ "OR EXCO_STATUS IS NULL)"
						+ " ORDER BY EXCO_NAME";
			
			return pSelect + whereClause;
		}

		/// <summary>
		/// Reads search parameter value (in this case 1 textbox) from GUI
		/// </summary>
		/// <exception cref="de.pta.Component.Errorhandling.UIWarningException">thrown when no value geiven in textbox</exception>		
		private void CopyOutSearchCriteriaExContractor() 
		{
			int			noSearchCriteria = 0;

			this.mExContractorParameter = ((FrmSearchExternalContractor) mView).TxtExternalContractor.Text.Trim().Replace("*", "%");
			if ( mExContractorParameter.Length < 1 ) 
			{
				noSearchCriteria ++;
			}

			if ( noSearchCriteria == 1 ) 
			{
				((FrmSearchExternalContractor) mView).Cursor = System.Windows.Forms.Cursors.Default;
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.NO_SEARCH_CRITERIA));
			} 
		}

		#endregion // End of ExContractor

		
		#endregion // End of Methods

	}
}
