using System;
using System.Collections;
using System.Text;


namespace de.pta.Component.Common
{
	/// <summary>
	/// Message that holds any parameters.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Paprotté, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class MessageParameterized
	{
	#region Members

		private ArrayList paramList;
		private String messageText;

	#endregion //End of Members

	#region Constructors
		
		/// <summary>
		/// Default Constructor
		/// </summary>
		public MessageParameterized()
		{
			initialize();
		}
		/// <summary>
		/// Constructor is used to initialize with localized text
		/// </summary>
		public MessageParameterized(String message)
		{
			initialize();
			messageText = message;
		}
	#endregion //End of Constructors

	#region Initialization

		private void initialize()
		{
			// Initializes the members.
			messageText = "";
		}	

	#endregion //End of Initialization

	#region Accessors 

	#endregion //End of Accessors

	#region Methods 

		/// <summary>
		/// Adds texts to the ParamList used for placeholders.
		/// </summary>
		/// <param name="Parameter"></param>
		public void AddParameter(String Parameter)
		{
			if (null == paramList)
			{
				paramList = new ArrayList();
			}
			paramList.Add(Parameter);
		}

		/// <summary>
		/// Creates a message while recognizing assigned parameters.
		/// </summary>
		/// <param name="localizedMessageText">already localized text to be filled with parameters</param>
		/// <returns> complete message text including replaced parameters</returns>
		public String MessageWithParam()
		{

			StringBuilder textContainer = new StringBuilder("");
			int parPos = 0;
			String processingMessage = messageText;

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
									// append current parameter to string container
									textContainer.Append(paramList[parPos]);
								}
							}
							pos += 2;
						}
					}
				}
					// append current character to string container
				else
				{
					textContainer.Append(processingMessage[pos]);
				}
			}
			return Convert.ToString(textContainer);
		}


	#endregion // End of Methods
	}
}

