using System;

namespace de.pta.Component.Common
{
	/// <summary>
	/// Interface for component specific configuration processors.
	/// Needed to call method ReadConfig from de.pta.Component.Common.ConfigReader
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> U.Fretz, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>

	#region Members
	#endregion //End of Members

	#region Constructors

	/// <summary>
	/// Interface definition.
	/// </summary>
	public interface IConfigProcessor
	{
		void  ProcessConfigItem(ConfigNode cNode);
		void  ProcessConfigBlockBegin(ConfigNode cNode);
		void  ProcessConfigBlockEnd(ConfigNode cNode);
	}

	#endregion //End of Constructors

	#region Initialization
	#endregion //End of Initialization

	#region Accessors 
	#endregion //End of Accessors

	#region Methods
	#endregion //Methods


}
