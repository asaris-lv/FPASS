using System;
using System.Collections;
using System.Windows.Forms;
using de.pta.Component.ListOfValues;
using Degussa.FPASS.Util;
using Degussa.FPASS.Util.ListOfValues;
using System.IO;
using de.pta.Component.Errorhandling;

namespace Degussa.FPASS.Reports
{
    /// <summary>
    /// Popup provides a bigger checklistbox control to select multiple excos
    /// to search for in attendance times reports
    /// </summary>
    /// <remarks>
    /// <para><b>History</b></para>
    /// <div class="tablediv">
    /// <table class="dtTABLE" cellspacing="0">
    ///		<tr>
    ///			<th width="20%">Author</th>
    ///			<th width="20%">Date</th>
    ///			<th width="60%">Remarks</th>
    ///		</tr>
    ///		<tr>
    ///			<td width="20%">N. Mundy, PTA GmbH</td>
    ///			<td width="20%">11.07.2008</td>
    ///			<td width="60%">Creation</td>
    ///		</tr>
    /// </table>
    /// </div>
    /// </remarks>
	partial class FrmPopupSearchExco : Form
    {
        #region Members

        /// <summary>
        /// Controller instance
        /// </summary>
        private ReportsController mReportsController;

        /// <summary>
        /// Id of currently selected ext contractor (used in checkboxlist Mehrfach FF)
        /// </summary>
        private int mSelectedExcoId = 0;

        /// <summary>
        /// Should all items in checklistbox be checked when button pressed or not?
        /// </summary>
        private bool mCheckAll = true;

        /// <summary>
        /// Name of file with exco list
        /// </summary>
        private string mFileName = Globals.GetInstance().ReportsBasePath + Globals.REPORTS_MYEXCO_FILE;

        #endregion

        #region Constructors

        /// <summary>
		/// Default constructor
		/// </summary>
	    public FrmPopupSearchExco()
		{
			InitializeComponent();
            InitView();
		}

        #endregion

        #region Accessors

        /// <summary>
        /// Gets or sets current Controller instance
	    /// </summary>
        public ReportsController ReportsController
	    {
            get { return mReportsController; }
            set { mReportsController = value; }
	    }

        /// <summary>
        /// Gets or sets name of file with external contractor list
        /// </summary>
        public string FileName
        {
            get { return mFileName; }
            set { mFileName = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize input fields.
        /// </summary>
        private void InitView()
        {
            FillExContractor();

            openFileExco.FileName = Globals.GetInstance().ReportsBasePath + Globals.REPORTS_MYEXCO_FILE;
            saveFileExco.FileName = Globals.GetInstance().ReportsBasePath + Globals.REPORTS_MYEXCO_FILE;
            tsLblAction.Text = "";
        }


        /// <summary>
        /// Fills external contractor  checklistbox
        /// </summary>
        private void FillExContractor()
        {
            ArrayList exContractors1 = LovSingleton.GetInstance().GetRootList(null, "FPASS_EXCONTRACTOR", "EXCO_NAME");

            // List of non-fpass excontractors for checklistbox
            exContractors1.AddRange(FPASSLovsSingleton.GetInstance().NonFPASSContractors.Values);
            exContractors1.Add(new LovItem("0", Globals.PLACEHOLDER_EMPTY));
            
            ClbAttExContractor.DataSource = exContractors1;
            ClbAttExContractor.DisplayMember = "ItemValue";
            ClbAttExContractor.ValueMember = "Id";
        }

        /// <summary>
        /// Checks or unchecks all items in checkboxlist 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectDeselectAll()
        {
            for (int j = 0; j < ClbAttExContractor.Items.Count; j++)
            {
                ClbAttExContractor.SetItemChecked(j, mCheckAll);
            }

            mCheckAll = !mCheckAll;
        }

        #endregion

        #region Events

        /// <summary>
	    /// User clicks on "OK"
	    /// </summary>
	    /// <param name="sender"></param>
	    /// <param name="e"></param>
	    private void btnOK_Click(object sender, EventArgs e)
        {
            mReportsController.HandleSynchroniseExcoLists(this);
	        this.Close();
        }

 
        /// <summary>
        /// FPASS V5: 
        /// This and the following two event handlers are a big workround for when user presses spacebar to tick an item (its checkbox)
        /// Without the workaround the cursor always jumps back to the first item in the list (item 0), 
        /// some kind of re-load but have not been able to find out when or why this fires.
        /// This event gets the index of the item selected when the user pressed the spacebar.
        /// Both PreviewKeyDown and KeyUp required
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClbAttExContractor_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            mSelectedExcoId = ClbAttExContractor.SelectedIndex;
            if (e.KeyCode == Keys.Space)
            {
                ClbAttExContractor.SelectedIndex = mSelectedExcoId;
            }
        }

        /// <summary>
        /// FPASS V5: 
        /// This and the following two event handlers are a big workround for when user presses spacebar to tick an item (its checkbox)
        /// Without the workaround the cursor always jumps back to the first item in the list (item 0), 
        /// some kind of re-load but have not been able to find out when or why this fires.
        /// This event gets the index of the item selected when the user pressed the spacebar.
        /// Both PreviewKeyDown and KeyUp required
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClbAttExContractor_KeyUp(object sender, KeyEventArgs e)
        {
            mSelectedExcoId = ClbAttExContractor.SelectedIndex;
            if (e.KeyCode == Keys.Space)
            {
                ClbAttExContractor.SelectedIndex = mSelectedExcoId;
            }
        }

        /// <summary>
        /// Raised when item's check state is changed. 
        /// If cursor has jumped back to item 0 then put it back where it was when user pressed space
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClbAttExContractor_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (ClbAttExContractor.SelectedIndex == 0)
            {
                // This does not change item 0's checkstate
                // so commented out
                //this.ClbAttExContractor.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.ClbAttExContractor_ItemCheck);
                //ClbAttExContractor.SetItemCheckState(0, CheckState.Unchecked);
                //this.ClbAttExContractor.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ClbAttExContractor_ItemCheck);
                ClbAttExContractor.SelectedIndex = mSelectedExcoId;
            }
        }

        /// <summary>
        /// Checks or unchecks all items in checkboxlist when button pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            SelectDeselectAll();
        }

        /// <summary>
        /// Raised when user clicks on "LIste öffnen":
        /// loads a list of previously selected ext contractors from a textfile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpenList_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileExco.ShowDialog();
            tsLblAction.Text = "";

            try
            {
                if (result == DialogResult.OK)
                {
                    this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                    mFileName = openFileExco.FileName;

                    // deselect all
                    mCheckAll = false;
                    SelectDeselectAll();

                    // loads ext contr
                    mReportsController.HandleOpenExcoList(this);

                    if (ClbAttExContractor.SelectedItems.Count > 0)
                        tsLblAction.Text = "Datei " + mFileName + " erfolgreich geladen.";
                    else tsLblAction.Text = "Keine selektierten Fremdfirmenen in der Datei " + mFileName + " gefunden.";
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
            catch (Exception iex)
            {
                ExceptionProcessor.GetInstance().Process(new UIErrorException("Die Datei " + mFileName + " konnte nicht geöffnet werden. \n" + iex.Message, iex));
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        /// <summary>
        /// Shows FileSave dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveList_Click(object sender, EventArgs e)
        {
            tsLblAction.Text = "";
            saveFileExco.ShowDialog();
        }

      
        /// <summary>
        /// Raised when user clicks OK on FileSave dialog:
        /// saves currently selectd excos to given file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveFileExco_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Get file name.
            mFileName = saveFileExco.FileName;

            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                // calls Save method in Model
                mReportsController.HandleSaveExcoList(this);

                tsLblAction.Text = "Datei " + mFileName + " erfolgreich gespeichert.";
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
            catch (Exception iex)
            {
                ExceptionProcessor.GetInstance().Process(new UIErrorException("Die Datei " + mFileName + " konnte nicht gespeichert werden. \n" + iex.Message, iex));
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        #endregion
    }
}
