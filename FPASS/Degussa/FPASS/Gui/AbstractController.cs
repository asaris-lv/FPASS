using System;
using System.Collections;

using de.pta.Component.Errorhandling;

using Degussa.FPASS.FPASSApplication;

namespace Degussa.FPASS.Gui
{
	/// <summary>
	/// The AbstractController is the parent Controller of all other types of controller. It is the
	/// Controller of the MVC-triad Abstract Controller, BaseView and AbstractModel.
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
	public abstract class AbstractController
	{
		#region Members

		/// <summary>
		/// used to hold the model of this triad
		/// </summary>
		internal		AbstractModel				mModel;
		
		/// <summary>
		/// used to hold the view of this triad
		/// </summary>
		internal	    BaseView     				mView;

		/// <summary>
		/// used to hold the unique ID of this dialog-triad
		/// </summary>
		protected		int							mDialogId;

		/// <summary>
		/// used to hold parent controller ( calling controller ) of this controller
		/// </summary>
		protected		AbstractController			mParent;

		/// <summary>
		/// flag indicating if the dialog is closed controlled or uncontrolled
		/// </summary>
		protected		bool						mControlledClose;
		

		#endregion //End of Members
		
		#region Constructors

		/// <summary>
		/// simpleConstructor
		/// </summary>
		public AbstractController()
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
		/// gets the unique dialog id of this dialog. 
		/// Unique id's are held in the class AllFPASSDialogs
		/// </summary>
		public int DialogId 
		{
			get 
			{
				return mDialogId;
			}
		}

		/// <summary>
		/// gets or sets the parent controller of this dialog
		/// </summary>
		public AbstractController Parent 
		{
			get 
			{
				return mParent;
			}
			set 
			{
				mParent = value;
			}
		}

		#endregion

		#region Methods 


		/// <summary>
		/// Shows this dialog in a controlled way. 
		/// Calls PreShow()-Method of this triad ( kind of a trigger logic ) 
		/// and then shows the view of this triad as modal view
		/// </summary>
		internal void ShowDialog() 
		{
			try 
			{
				this.PreShow();
				mView.ShowDialog();
			} 
			catch ( UIWarningException uwe ) 
			{
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		/// <summary>
		/// Closes this dialog in controlled way. 
		/// Calls PreClose()-Methods of this triad ( kind of a trigger logic ) 
		/// and then closes the view of this triad
		/// </summary>
		internal void Close() 
		{
			try {
				mControlledClose = true;
				mModel.PreClose();
				mView.PreClose();
				mView.Close();
			} 
			catch ( UIWarningException uwe ) 
			{
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		/// <summary>
		/// Destroys this dialog in controlled way and frees all resources of this dialog. 
		/// Calls PreDestroy()-Methods of this triad ( kind of a trigger logic ) 
		/// and then closes the view of this triad
		/// </summary>
		internal void Destroy() 
		{
			try 
			{
				mView.RegisterController(null);
				mView.PreDestroy();
				mView.Dispose();
				mModel.PreDestroy();
				mModel.registerView(null);
				mView = null;
				mModel = null;
			} 
			catch ( UIWarningException uwe ) 
			{
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		/// <summary>
		/// Hides this dialog in a controlled way
		/// Calls PreHide()-Methods of this triad ( kind of a trigger logic ) 
		/// and then hides the view of this triad
		/// </summary>
		internal void Hide() 
		{
			try {
				mModel.PreHide();
				mView.PreHide();
			} 
			catch ( UIWarningException uwe ) 
			{
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		/// <summary>
		/// calls PreShow of the model and the view of this dialog ( mvc-triad )
		/// </summary>
		internal void PreShow() 
		{
			mControlledClose = false;
			mModel.PreShow();
			mView.PreShow();
		}

		/// <summary>
		/// closes this dialog and recursive forces its parent to close
		/// is called for all open shown/hidden/covered dialogs when the application is 
		/// closed
		/// </summary>
		internal void Exit() 
		{	
			this.Close();
			if ( null != mParent ) 
			{
				mParent.Exit();
			}
		}

		// <summary>
		/// Fills all the lists of this view. empty implementation because subclasses
		/// have to implement their individual logic
		/// </summary>
		internal void FillLists() 
		{
			mView.FillLists();
		}

		/// <summary>
		/// Handles closing-events of all dialogs in FPASS.
		/// Forces an controlled close by calling the Close()-Method 
		/// of the current triad if it's an uncontrolled close. 
		/// An uncontrolled close of a form can be fired by a user-click 
		/// on the exit-button on the top-right of a window.
		/// </summary>
		internal virtual void HandleEventUnControlledClose() 
		{
			if ( ! mControlledClose ) 
			{
				this.Close();
			}
		}


		/// <summary>
		/// For the forms from which the External Contractor search (FrmSearchExternalContractor) is called,
		/// need to know which external contractor was selected
		/// Call method to refill exco combobox in view from which (FrmSearchExternalContractor) was called
		/// </summary>
		/// <param name="pContractorID"></param>
		internal void SetSelectedContractorID(String pContractorID) 
		{
			mView.ReFillContractorList(pContractorID);
		}


        /// <summary>
        /// Call methode in BaseView to fill IDCardReader Textbox. 
        /// </summary>
        /// <param name="pIDCardReaderNo"></param>
        internal void SetIDCardReaderNumber(int pIDCardReaderNo, string mIDCardReaderType)
        {
            mView.ReFillIDCardReader(pIDCardReaderNo, mIDCardReaderType);
        }


        /// <summary>
        /// Can be used to implement some condition and show dialog (the View instance)
        /// only when this condition is true
        /// </summary>
        /// <returns></returns>
        internal virtual bool ConditionIsMet()
        {
            return true;
        }

        /// <summary>
        /// Offers a way of re-triggering some method in the Model, 
        /// for example re-executing a query before the View is shown
        /// </summary>
        /// <returns></returns>
        internal virtual void RequeryModel()
        {            
        }


		#endregion // End of Methods
	}
}
