using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;



namespace Degussa.FPASS.Gui
{
	/// <summary>
	/// BaseView is the parent view of all other types of forms. It is the
	/// view of the MVC-triad AbstractController, BaseView and AbstractModel.
	/// It's not abstract by definition because the designer can't show forms 
	/// when they are abstract or have an abstract superclass and a form must
	/// extend from System.Windows.Forms.Form.
	/// Provides empty implementations of the three virtual methods PreClose(), PreShow() 
	/// and PreHide(). These  are always called when a dialog is closed, shown
	/// or hidden (kind of a trigger logic).
	/// Catches the Closing-Event and delgates it to AbstractController to disable 
	/// uncontrolled closing of a form. 
	/// 
	///
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
	/// </remarks
	public class BaseView : System.Windows.Forms.Form
	{
		#region Members

		/// <summary>
		/// used to hold the Controller of this triad
		/// </summary>
		internal	AbstractController						mController;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;


		#endregion //End of Members

		#region Constructors

		public BaseView()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			InitView();
		}

		#endregion //End of Constructors

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		public virtual void RegisterController(AbstractController pAbstractController) 
		{
			this.mController = pAbstractController;

		}

		
		/// <summary>
		/// is called before a dialog is closed. empty implementation because subclasses
		/// have to implement their individual logic
		/// </summary>
		internal virtual void PreClose()
		{

		}

		/// <summary>
		/// is called before a dialog is destroyed. empty implementation because subclasses
		/// have to implement their individual logic to free all ressoucers tey hold
		/// </summary>
		internal virtual void PreDestroy() 
		{

		}


		/// <summary>
		/// is called before a dialog is displayed. empty implementation because subclasses
		/// have to implement their individual logic
		/// </summary>
		internal virtual void PreShow() 
		{

		}

		/// <summary>
		/// is called before a dialog is hidden/covered. empty implementation because subclasses
		/// have to implement their individual logic
		/// </summary>
		internal virtual void PreHide() 
		{

		}

		/// <summary>
		/// Fills all the lists of this view. empty implementation because subclasses
		/// have to implement their individual logic
		/// </summary>
		internal virtual void FillLists() 
		{
			
		}

		/// <summary>
		/// Implemented as necessary by each form: refill Combobox Excontractor 
		/// set selected value to the Excontractor with the current ID
		/// </summary>
		/// <param name="pContractorID">current Exco ID</param>
		internal virtual void ReFillContractorList(String pContractorID) 
		{
			

		}



        /// <summary>
        /// Can be filled by the CoWorker Form. The textbox for the IDCardReader can be set.
        /// </summary>
        /// <param name="pIDCardReaderNo">current ID CardReader No</param>
        internal virtual void ReFillIDCardReader(int pIDCardReaderNo, string mIDCardReaderType)
        {
        }


		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Initialize input fields.
		/// </summary>
		private void InitView()
		{
			
		}

		#endregion // End of Methods

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{ 
            this.SuspendLayout();
            // 
            // BaseView
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1272, 952);
            this.HelpButton = true;
            this.MaximumSize = new System.Drawing.Size(1280, 980);
            this.Name = "BaseView";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.BaseView_Closing);
            this.Load += new System.EventHandler(this.BaseView_Load);
            this.ResumeLayout(false);

		}
		#endregion

		#region Events

        /// <summary>
        /// Raised when any FPASS form is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void BaseView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if ( null != mController ) 
			{
				mController.HandleEventUnControlledClose();
			}
		}

		#endregion Events

        private void BaseView_Load(object sender, EventArgs e)
        {

        }



	}
}
