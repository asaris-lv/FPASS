using System;

namespace de.pta.Component.N_UserManagement.Exceptions
{
	/// <summary>
	/// Exception for error handling concerning data access to xml file
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Sep/15/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class XmlDataAccessException : DataAccessException 
	{

		#region Constructors

		public XmlDataAccessException() : base()
		{
		}

		public XmlDataAccessException(String pMessage) : base(pMessage)
		{
		}

		public XmlDataAccessException(String pMessage, Exception pInnerException) : base(pMessage, pInnerException)
		{
		}

		#endregion //End of Constructors
	
	}//end XmlDataAccessException
}//end namespace Exceptions