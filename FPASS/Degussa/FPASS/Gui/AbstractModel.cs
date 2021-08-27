using System;

namespace Degussa.FPASS.Gui
{
	/// <summary>
	/// The AbstractModel is the parent Model of all other types of model. It is the
	/// model of the MVC-triad AbstractController, BaseView and AbstractModel.
	/// Provides empty implementations of the three virtual methods PreClose(), PreShow() 
	/// and PreHide(). These  are always called when a dialog is closed, shown
	/// or hidden (kind of a trigger logic).
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
	public abstract class AbstractModel
	{
		#region Members

		/// <summary>
		/// used to hold the view of the mcv triad
		/// </summary>
		protected		BaseView		mView;

		#endregion //End of Members
				
		#region Constructors
		
		/// <summary>
		/// simple constructor
		/// </summary>
		public AbstractModel()
		{
			
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

		internal BaseView View
		{
			get 
			{
				return mView;
			}
		}

		#endregion //End of Accessors

		#region Methods 


		/// <summary>
		/// registers the view on this model
		/// </summary>
		/// <param name="view">view to register</param>
		public void registerView(BaseView view) 
		{
			mView = view;

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
		/// is called before a dialog is displayed. empty implementation because subclasses
		/// have to implement their individual logic
		/// </summary>
		internal virtual void PreHide() 
		{

		}


		




		#endregion // End of Methods

		
	}
}
