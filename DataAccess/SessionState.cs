using System;
using System.Text;

namespace de.pta.Component.DataAccess.Internal
{
	/// <summary>
	/// Encapsulates a session state in the DataAccess component.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/09/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class SessionState
	{
		#region Members

		private String		sessionId;
		private String		user;
		private String		xmlSessionPath;
		private bool		sessionActive;
		private bool		excelFinished;
		private DateTime	lastModified;

		#endregion //End of Members
		
		#region Constructors

		/// <summary>
		/// Default construction of the object.
		/// </summary>
		public SessionState()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
			sessionId		= String.Empty;;
			user			= String.Empty;;
			xmlSessionPath	= String.Empty;
			sessionActive	= false;
			excelFinished	= true;
			lastModified	= DateTime.Now;
		}	

		#endregion //End of Initialization
		
		#region Accessors 

		/// <summary>
		/// Gets or sets the session id.
		/// </summary>
		public String SessionId
		{
			get
			{
				return sessionId;
			}
			set
			{
				sessionId = value;
			}
		}

		/// <summary>
		/// Gets or sets the user.
		/// </summary>
		public String User
		{
			get
			{
				return user;
			}
			set
			{
				user = value;
			}
		}

		/// <summary>
		/// Gets or sets the xml data session path.
		/// </summary>
		public String XmlSessionPath
		{
			get
			{
				return xmlSessionPath;
			}
			set
			{
				xmlSessionPath = value;
			}
		}

		/// <summary>
		/// Gets or sets if the session is active or not.
		/// </summary>
		public bool	SessionActive
		{
			get
			{
				return sessionActive;
			}
			set
			{
				sessionActive = value;
			}
		}

		/// <summary>
		/// Gets or sets if the excel processing is finished or not.
		/// </summary>
		public bool	ExcelFinished
		{
			get
			{
				return excelFinished;
			}
			set
			{
				excelFinished = value;
			}
		}

		/// <summary>
		/// Gets or stes the last modified state of the session.
		/// </summary>
		public DateTime	LastModified
		{
			get
			{
				return lastModified;
			}
			set
			{
				lastModified = value;
			}
		}

		#endregion //End of Accessors

		#region Methods
		#endregion // End Methods
	}
}
