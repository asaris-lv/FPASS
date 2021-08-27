using System;

namespace de.pta.Component.N_UserManagement.Exceptions
{
	/// <summary>
	/// Exception for error handling concerning configiration errors
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Aug/18/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class UserManagementConfigurationException  : UserManagementException
	{

		#region Constructors
		public UserManagementConfigurationException() : base()
		{
		}

		public UserManagementConfigurationException(String pMessage) : base(pMessage)
		{
		}

		public UserManagementConfigurationException(String pMessage, Exception pInnerException) : base(pMessage, pInnerException)
		{
		}

		#endregion //End of Constructors

	}

}
