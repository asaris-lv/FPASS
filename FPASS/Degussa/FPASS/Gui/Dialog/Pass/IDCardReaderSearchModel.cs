using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Degussa.FPASS.Util.Messages;
using de.pta.Component.Errorhandling;
using de.pta.Component.DbAccess;
using Degussa.FPASS.Db;
using System.Data;
using Degussa.FPASS.Util;
using Degussa.FPASS.Util.UserManagement;
using System.Collections;
using Degussa.FPASS.Bo.Administration;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
    public class IDCardReaderSearchModel : FPASSBaseModel
	{
        #region Members

        /// <summary>
        /// Database query
        /// </summary>
        private const string IDCARDREADER_QUERY = "IDCardReaders";

        /// <summary>
        /// holds number of search values elected in GUI
        /// </summary>
        private string mIdCardReaderParameter;

        #endregion //End of Members
        
        #region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
        public IDCardReaderSearchModel()
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
            ((FrmSearchIDCardReader)mView).DgrIDCardReader.DataSource = null;
            this.ClearStatusBar();
        }

        /// <summary>
        /// Executes ID card reader search: creates SQL query
        /// Build arraylist of ExContractor value objects, bind arraylist to datagrid (no sorting)
        /// The attributes are displayed in the datagrid on the form.
        /// <param name="pIDCardReaderType">current Id card reader type</param>
        /// </summary>
        /// <exception cref="de.pta.Component.Errorhandling.UIWarningException">thrown when query returns no results </exception>
        public void GetIDCardReaders(string pIDCardReaderType)
        {
            FrmSearchIDCardReader mIdCardReadView = (FrmSearchIDCardReader)mView;

            mIdCardReadView.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            mIdCardReadView.DgrIDCardReader.DataSource = null;
            this.ClearStatusBar();

            // Gets DataProvider from DbAccess component and create select command
            // Where clause with id card reader type
            IProvider mProvider = DBSingleton.GetInstance().DataProvider;
            IDbCommand selComm = mProvider.CreateCommand(IDCARDREADER_QUERY);
            selComm.CommandText += " WHERE IDR_READER_TYPE ='" + pIDCardReaderType + "'"; 

     
            ArrayList arlAusweisleser = new ArrayList();
            BOIDCardReader idCardReaderBO;

            // Open data reader and loop thru records to create an ArrayList of BOs
            IDataReader mDR = mProvider.GetReader(selComm);

            while (mDR.Read())
            {
                idCardReaderBO = new BOIDCardReader();
                idCardReaderBO.ReaderNumber = Convert.ToDecimal(mDR["IDR_READER_NO"]);
                idCardReaderBO.ReaderType = mDR["IDR_READER_TYPE"].ToString();
                idCardReaderBO.Description = mDR["IDR_DESCRIPTION"].ToString();

                arlAusweisleser.Add(idCardReaderBO);
            }
            mDR.Close();

            // Enable button "Übernehmen" or not
            mIdCardReadView.BtnGetReaderNum.Enabled = (arlAusweisleser.Count > 0);
            mIdCardReadView.Cursor = System.Windows.Forms.Cursors.Default;

            if (arlAusweisleser.Count > 0)
            {
                //Bind data grid in Form to arrayList
                mIdCardReadView.DgrIDCardReader.DataSource = arlAusweisleser;
                ShowMessageInStatusBar("Meldung: " + arlAusweisleser.Count + " Ausweisleser gefunden.");
            }
            else
            {
                ShowMessageInStatusBar(MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_RESULTS));
            }
        }

       
        #endregion 
	}
}
