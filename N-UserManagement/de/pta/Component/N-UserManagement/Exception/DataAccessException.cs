using System;

namespace de.pta.Component.N_UserManagement.Exceptions
{
	/// <summary>
	/// Common exception for error handling concerning data access
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Sep/15/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class DataAccessException : UserManagementException 
	{

		#region Constructors

		public DataAccessException() : base()
		{
		}

		public DataAccessException(String pMessage) : base(pMessage)
		{
		}

		public DataAccessException(String pMessage, Exception pInnerException) : base(pMessage, pInnerException)
		{
		}

		#endregion //End of Constructors
	
	}//end DataAccessException
}//end namespace Exceptions