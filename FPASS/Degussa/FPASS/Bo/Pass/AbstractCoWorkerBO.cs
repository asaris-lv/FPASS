using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Windows.Forms;

using de.pta.Component.DbAccess;
using de.pta.Component.DbAccess.Exceptions;

using de.pta.Component.Errorhandling;

using Degussa.FPASS.Bo;
using Degussa.FPASS.Gui;
using Degussa.FPASS.Gui.Dialog.Pass;

namespace Degussa.FPASS.Bo.Pass
{
	/// <summary>
	/// Base class of all real bo's in fpass application.
	/// Defines minimal interface a bo must provide. Holds references to the model and the
	/// view the bo belongs to.
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
	public abstract class AbstractCoWorkerBO
	{
		#region Members

		/// <summary>
		/// model of the bo
		/// </summary>
		protected		CoWorkerModel			mCoWorkerModel;
		
		/// <summary>
		/// provides db access
		/// </summary>
		protected		IProvider				mProvider;

		/// <summary>
		/// view (form) of the bo
		/// </summary>
		protected		FrmCoWorker				mViewCoWorker;

		/// <summary>
		/// flag indicating whether the data a bo holds was chaged by a user
		/// </summary>
		protected bool mChanged;


		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Simple Constructor
		/// </summary>
		/// <param name="pModel">model the bo belongs to</param>
		public AbstractCoWorkerBO(CoWorkerModel pCoWorkerModel)
		{
			mChanged = false;
			mCoWorkerModel = pCoWorkerModel;
			initialize();
		}

		internal void RegisterView(BaseView pBaseView) 
		{
			mViewCoWorker = (FrmCoWorker)pBaseView;

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
		/// simple accessor
		/// </summary>
		public bool Changed 
		{
			get 
			{
				return mChanged;
			}
			set 
			{
				mChanged = value;
			}
		}

		

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Is called before a dialog is closed. empty implementation because subclasses
		/// have to implement their individual logic
		/// </summary>
		internal virtual void PreClose()
		{

		}

		/// <summary>
		/// Is called before a dialog is destroyed. empty implementation because subclasses
		/// have to implement their individual logic to free all ressoucers tey hold
		/// </summary>
		internal virtual void PreDestroy() 
		{

		}


		/// <summary>
		/// Is called before a dialog is displayed. empty implementation because subclasses
		/// have to implement their individual logic
		/// </summary>
		internal virtual void PreShow() 
		{

		}

		/// <summary>
		/// Is called before a dialog is hidden/covered. empty implementation because subclasses
		/// have to implement their individual logic
		/// </summary>
		internal virtual void PreHide() 
		{

		}

		/// <summary>
		/// Initializes the bo. Empty implementation because subclasses have to implement 
		/// their individual logic.
		/// </summary>
		internal virtual void InitializeBO() 
		{

		}

		/// <summary>
		/// Shows data of the bo in the gui. Data is "copied in the gui".
		/// Empty implementation because subclasses have to implement 
		/// their individual logic.
		/// </summary>
		internal virtual void CopyIn() 
		{

		}

		/// <summary>
		/// Data is "copied out of the gui" in the bo.
		/// Empty implementation because subclasses have to implement 
		/// their individual logic.
		/// </summary>
		internal virtual void CopyOut() 
		{

		}

		/// <summary>
		/// Validates the bo. Empty implementation because subclasses have to implement 
		/// their individual logic.
		/// </summary>
		internal virtual void Validate()
		{

		}

		/// <summary>
		/// Persists the bo. Empty implementation because subclasses have to implement 
		/// their individual logic.
		/// </summary>
		internal virtual void Save()
		{

		}

        /// <summary>
        /// Converts DBNull format from database into empty field (number 0)
        /// </summary>
        /// <param name="pDRCol"></param>
        /// <returns></returns>
        protected decimal FormatReaderNumsForDisplay(object pDRCol)
        {
            decimal currDRNum;

            if (pDRCol.Equals(DBNull.Value))
            {
                currDRNum = 0;
            }
            else
            {
                currDRNum = Convert.ToDecimal(pDRCol);
            }
            return currDRNum;
        }


        /// <summary>
        /// Converts DBNull format from database into empty field (empty string)
        /// </summary>
        /// <param name="pDRCol"></param>
        /// <returns></returns>
        protected string FormatReaderStringsForDisplay(object pDRCol)
        {
            string currDRString;

            if (pDRCol.Equals(DBNull.Value))
            {
                currDRString = String.Empty;
            }
            else
            {
                currDRString = Convert.ToString(pDRCol);
            }
            return currDRString;
        }


        /// <summary>
        /// Converts empty fields (number 0) to DBNull format for database
        /// </summary>
        /// <param name="pCurrCommand"></param>
        /// <param name="pParaName"></param>
        /// <param name="pDecValue"></param>
        protected void FormatCopyOutNumsForSave(IDbCommand pCurrCommand, string pParaName, decimal pDecValue)
        {
            if (pDecValue == 0)
            {
                mProvider.SetParameter(pCurrCommand, pParaName, DBNull.Value);
            }
            else
            {
                mProvider.SetParameter(pCurrCommand, pParaName, pDecValue);
            }
        }


        /// <summary>
        /// Converts empty strings to DBNull format for database
        /// </summary>
        /// <param name="pCurrCommand"></param>
        /// <param name="pParaName"></param>
        /// <param name="pStrValue"></param>
        protected void FormatCopyOutStringsForSave(IDbCommand pCurrCommand, string pParaName, string pStrValue)
        {
            if (pStrValue.Equals(String.Empty))
            {
                mProvider.SetParameter(pCurrCommand, pParaName, DBNull.Value);
            }
            else
            {
                mProvider.SetParameter(pCurrCommand, pParaName, pStrValue);
            }
        }
		
		#endregion // End of Methods


	}
}
