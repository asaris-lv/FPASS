using System;
using System.Windows.Forms;
using System.Threading;

using de.pta.Component.Errorhandling;

using Degussa.FPASS.Util.ErrorHandling;
using Degussa.FPASS.Util;


namespace Degussa.FPASS
{
	/// <summary>
	/// Startup class for FPASS. Contains only the main-method and 
	/// overwrites runtime exception handling.
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
	public class FPASSMain
	{
		#region Members

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Default Constructor
		/// </summary>
		public FPASSMain()
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
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			
			Globals.GetInstance().Log.Fatal("FPASS gestartet");

			UIFatalPublisher fatalPub = new UIFatalPublisher();
			UIFatalDelegate.GetInstance().Publisher = fatalPub;

			UIErrorPublisher errorPub = new UIErrorPublisher();
			UIErrorDelegate.GetInstance().Publisher = errorPub;

			UIWarningPublisher warningPub = new UIWarningPublisher();
			UIWarningDelegate.GetInstance().Publisher = warningPub;

			AppDomain currentDomain = AppDomain.CurrentDomain;
			currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);

			Application.ThreadException += new ThreadExceptionEventHandler(MyThreadHandler);

            Application.EnableVisualStyles();

			FPASSStart StartUp =  new FPASSStart();
			StartUp.Start();
		}


		/// <summary>
		/// overwrites runtime exception handling
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		static void MyHandler(object sender, UnhandledExceptionEventArgs args) 
		{
			try 
			{
				Exception e = (Exception) args.ExceptionObject;
				throw new UIFatalException("Fehler in FPASS", e);
			} 
			catch ( UIFatalException ufe ) 
			{
				ExceptionProcessor.GetInstance().Process(ufe);
			}
			Application.Exit();
		}

		/// <summary>
		/// overwrites runtime exception handling
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		static void MyThreadHandler(object sender, System.Threading.ThreadExceptionEventArgs args) 
		{
			try 
			{
				Exception e = (Exception) args.Exception;
				throw new UIFatalException("Fehler in FPASS", e);
			} 
			catch ( UIFatalException ufe ) 
			{
				ExceptionProcessor.GetInstance().Process(ufe);
			}
			Application.Exit();
		}



		#endregion // End of Methods


	}
}
