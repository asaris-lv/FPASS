using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Degussa.FPASS.FPASSApplication;
using de.pta.Component.Errorhandling;
using Degussa.FPASS.Util.Messages;
using System.Windows.Forms;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
    class IDCardReaderSearchController : FPASSBaseController
	{

        /// <summary>
        /// used to hold the model of this triad. hold for convenience to avoid casting
        /// </summary>
        private IDCardReaderSearchModel mIDCardReaderModel;

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
        public IDCardReaderSearchController()
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
            mDialogId = AllFPASSDialogs.SEARCH_IDCARD_READER_DIALOG;
            mView = new FrmSearchIDCardReader();
            mView.RegisterController(this);

            mModel = new IDCardReaderSearchModel();
            mModel.registerView(mView);

            mIDCardReaderModel = (IDCardReaderSearchModel)mModel;
        }

        #endregion //End of Initialization

        #region Methods

        /// <summary>
        /// Executes ID card reader search
        /// </summary>
        /// <param name="pIDCardReaderType">current Id card reader type</param>
        public void SearchIDCardReaders(string pIDCardReaderType)
        {
            try
            {
                try
                {
                    mIDCardReaderModel.GetIDCardReaders(pIDCardReaderType);
                }
                catch (System.Data.OracleClient.OracleException oraex)
                {
                    throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + oraex.Message);
                }
            }
            catch (UIWarningException uwe)
            {
                ((FrmSearchIDCardReader)mView).BtnGetReaderNum.Enabled = false;
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }


        /// <summary>
        /// Give selected IDCardReader back to controller of view from which the IDCard Reader search was opened
        /// </summary>
        /// <param name="pIDCardReaderNo">ID of selected IDCard Reader</param>
        internal void HandleEventGiveBackIDCardReader(int pIDCardReaderNo, string mIDCardReaderType)
        {
            mParent.SetIDCardReaderNumber(pIDCardReaderNo, mIDCardReaderType);
            this.HandleCloseDialog();
        }


        #endregion 
    }
}
