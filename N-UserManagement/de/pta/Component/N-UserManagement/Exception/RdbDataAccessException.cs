using System;

namespace de.pta.Component.N_UserManagement.Exceptions
{
	/// <summary>
	/// Exception for error handling concerning data access to database
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Sep/15/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class RdbDataAccessException : DataAccessException 
	{

		#region Constructors

		public RdbDataAccessException() : base()
		{
		}

		public RdbDataAccessException(String pMessage) : base(pMessage)
		{
		}

		public RdbDataAccessException(String pMessage, Exception pInnerException) : base(pMessage, pInnerException)
		{
		}

		#endregion //End of Constructors
	
	}//end RdbDataAccessException
}//end namespace Exceptions