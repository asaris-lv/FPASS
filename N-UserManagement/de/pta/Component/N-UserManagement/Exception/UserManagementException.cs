using System;
using de.pta.Component.Errorhandling;


namespace de.pta.Component.N_UserManagement.Exceptions
{
	/// <summary>
	/// Exception for error handling in component UserManagement
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Aug/12/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class UserManagementException : BaseComponentException
	{

		#region Constructors

		public UserManagementException() : base()
		{
		}

		public UserManagementException(String pMessage) : base(pMessage)
		{
		}

		public UserManagementException(String pMessage, Exception pInnerException) : base(pMessage, pInnerException)
		{
		}

		#endregion //End of Constructors
	}
}
