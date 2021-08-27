using System;


namespace de.pta.Component.N_UserManagement.Exceptions
{
	/// <summary>
	/// Exception for error handling concerning password verification
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Aug/14/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class PasswordVerificationException : UserManagementException
	{

		#region Constructors

		public PasswordVerificationException () : base()
		{
		}

		public PasswordVerificationException (String pMessage) : base(pMessage)
		{
		}

		public PasswordVerificationException (String pMessage, Exception pInnerException) : base(pMessage, pInnerException)
		{
		}

		#endregion //End of Constructors
	}
}
