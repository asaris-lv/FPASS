using System;
using System.Collections;
using System.Text;

namespace de.pta.Component.Errorhandling 
{
	/// <summary>
	/// All exceptions throwed by components should derive from this class
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> D.Hassloecher, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class BaseApplicationException : System.ApplicationException
	{
		#region Members

		private ArrayList paramList;

		#endregion //End of Members

		#region Constructors
 
		/// <summary>
		/// Standard constructor.
		/// </summary>
		public BaseApplicationException() : base()
		{
		}

		/// <summary>
		/// Instantiating while defining a error message.
		/// </summary>
		/// <param name="message">message associated with the exception</param>
		public BaseApplicationException(string message) : base(message)
		{
		}

		/// <summary>
		/// Instantiating with a defined message a associated inner exception.
		/// </summary>
		/// <param name="message">message associated with the exception</param>
		/// <param name="innerException">exception which caused this exception</param>
		public BaseApplicationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		#endregion //End of Constructors

		#region Initialization
		#endregion //End of Initialization

		#region Accessors 
		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Adds texts to the ParamList used for Placeholders
		/// </summary>
		/// <param name="Parameter"></param>
		public void AddParameter(string Parameter)
		{
			if (null == paramList)
			{
				paramList = new ArrayList();
			}
			paramList.Add(Parameter);

		}

		/// <summary>
		/// Creates a errormessage while recognizing assigned parameters.
		/// </summary>
		/// <param name="localizedMessageText">already localized text to be filled with parameters</param>
		/// <returns> complete message text including replaced parameters</returns>
		public string MessageWithParam(string localizedMessageText)
		{

			StringBuilder erg = new StringBuilder("");
			int parPos = 0;
			String processingMessage = localizedMessageText;

			
			for (int pos = 0;pos < processingMessage.Length; pos ++)
			{
				if (processingMessage[pos] == '#')
				{
					if (pos+1 < processingMessage.Length && processingMessage[pos+1] == '$')
					{
						if (pos+2 < processingMessage.Length && 
							processingMessage[pos+2] >= '0' && 
							processingMessage[pos+2] <= '9')
						{  
							//parPos = Convert.ToInt32(processingMessage[pos+2]);
							parPos = Int32.Parse(processingMessage[pos+2].ToString());
				
							if (null != paramList)
							{
								if (paramList.Count > parPos)
								{
									erg.Append(paramList[parPos]);
								}
							}
							pos += 2;
						}
					}
				}
				else
				{
					erg.Append(processingMessage[pos]);
				}

			}

			return Convert.ToString(erg);

		}

		#endregion
	}
}