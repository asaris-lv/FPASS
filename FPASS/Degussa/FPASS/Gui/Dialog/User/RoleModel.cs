using System;
using System.Data;

using Degussa.FPASS.Db;
using Degussa.FPASS.Db.DataSets;
using Degussa.FPASS.Gui;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Util;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.UserManagement;

using de.pta.Component.DbAccess;
using de.pta.Component.ListOfValues;
using de.pta.Component.DbAccess.Exceptions;
using de.pta.Component.Errorhandling;

namespace Degussa.FPASS.Gui.Dialog.User
{
	/// <summary>
	/// Contains the functionality required to show all users for a particular role
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
	public class RoleModel : FPASSBaseModel
	{
		#region Members

		// DataProvider from PTA DbAccess component
		private IProvider mProvider;
		private IDbDataAdapter mDataAdapterUser;

		// ID of adaptor as defined in Config.xml
		private const string USER_ADA_ID = "DSUserByRole";
		// Name of the "table" (actually a view)
		private const string VIEW_USERBYROLE = "VW_FPASS_USERBYROLE";

		// Name of the query parameters as defined in Config.xml
		private const string USER_MAND_PARA   = ":MND_ID";
		private const string ROLE_ID_PARA	  = ":RO_ID";

		// Typified DataSet used to load Roles. Model, View & Controller operate on the same instance of the typified dataset
		private DSUser mDSUser;

		private string mRoleIDSearchParameter;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public RoleModel()
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
			// Instantiate DataAdpators
			mProvider = DBSingleton.GetInstance().DataProvider;
			mDataAdapterUser = mProvider.CreateDataAdapter(USER_ADA_ID);
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		
		internal void RegisterUserDataSet(DSUser pDSUser) 
		{
			// The typified dataset as a form attribute is registered with the model.
			this.mDSUser = pDSUser;
		}


		public void ClearTextFields()
		{
			((FrmRole) mView).DgrUserHaveRole.DataBindings.Clear();
			((FrmRole) mView).DSUser.Clear();
			((FrmRole) mView).CboRole.Text = "";		
		}

		/// <summary>
		/// Analogous to the AdminModel, the results of the search are loaded into a dataset.
		/// this is then bound to the datagrid in the GUI.
		/// </summary>
		internal void GetUsers()
		{
			int numrecs;

			// Clear datagrid
			this.mDSUser.Clear();
			((FrmRole) mView).DgrUserHaveRole.DataBindings.Clear();

			// Fill parameters & bind to data adaptor
			this.SetUserSearchCriteria();
			mProvider.SetParameter(mDataAdapterUser, USER_MAND_PARA, UserManagementControl.getInstance().CurrentMandatorID.ToString());
			mProvider.SetParameter(mDataAdapterUser, ROLE_ID_PARA, this.mRoleIDSearchParameter);

			try
			{
				numrecs = mProvider.FillDataSet(USER_ADA_ID, mDataAdapterUser, mDSUser);
				// Bind dataset to grid
				((FrmRole) mView).DgrUserHaveRole.DataSource = mDSUser;
				((FrmRole) mView).DgrUserHaveRole.DataMember = VIEW_USERBYROLE;
			}
			catch (System.Data.OracleClient.OracleException oraex)
			{
				Globals.GetInstance().Log.Fatal( (MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.FATAL_DB_ERROR)) + oraex.Message );
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.FATAL_DB_ERROR) + " " + oraex.Message );
			}
			catch (DbAccessException)
			{
				Globals.GetInstance().Log.Fatal( (MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.FATAL_DB_ERROR)) );
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.FATAL_DB_ERROR) );
			}

			if ( numrecs < 1 ) 
			{
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.NO_RESULTS));
			}
		}


		/// <summary>
		/// Analogous to the AdminModel: the search criteria as chosen in the GUI combobox is stored in a variable
		/// </summary>
		private void SetUserSearchCriteria() 
		{
			if ( ((FrmRole)mView).CboRole.SelectedItem != null )
			{
				this.mRoleIDSearchParameter = this.GetSelectedIDFromCbo( ((FrmRole)mView).CboRole );
			}
			else
			{
				this.mRoleIDSearchParameter = String.Empty;
			}
				
			if ( mRoleIDSearchParameter.Length < 1 ) 
			{
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.NO_SEARCH_CRITERIA));
			}
		}

		#endregion // End of Methods

	}
}
