using System;
using de.pta.Component.PdfGenerator.Exceptions.Internal.Configuration;

namespace de.pta.Component.PdfGenerator.Internal.Configuration
{
	/// <summary>
	/// ConfigFactory provides a factory method to get a concrete configuration factory 
	/// depending on a specified configuration type and ensures that any extending factory 
	/// provides a method to get a filled XML2PDFConfiguration.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M. Mehlem, PTA GmbH
	/// <b>Date:</b> Aug/28/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal abstract class ConfigFactory
	{
		#region Members
		#endregion //End of Members

		#region Constructors
		#endregion //End of Constructors

		#region Initialization
		#endregion //End of Initialization

		#region Accessors 
		#endregion //End of Accessors

		#region Methods 

		
		/// <summary>
		/// Returns the concrete factory to analyse a configuration depending on the specified 
		/// configuration type. Currently only an XMLConfigFactory is implemented.
		/// </summary>
		/// <param name="pType">The type of configuration a factory is asked for.</param>
		/// <returns>A ConfigFactory extending concrete factory (Currently only XMLConfigFactory).
		/// </returns>
		public static ConfigFactory GetFactory(ConfigurationTypes pType) 
		{
			switch(pType) 
			{
				case ConfigurationTypes.Xml:
					return new XMLConfigFactory();
				case ConfigurationTypes.RdbSqlServer:
					throw new ConfigurationException("PdfGenerator configuration failed: Invalid config type(RdbSqlServer)");
				case ConfigurationTypes.RdbOracle:
					throw new ConfigurationException("PdfGenerator configuration failed: Invalid config type(RdbOracle)");
				default:
					throw new ConfigurationException("PdfGenerator configuration failed: Invalid config type(unknown)");
			}
		}

		/// <summary>
		/// Should return an XML2PDFConfiguration filled by a ConfigProcessor that analyses 
		/// the specified configuration source data.
		/// </summary>
		/// <returns>A filled XML2PDFConfiguration.</returns>
		public abstract XML2PDFConfiguration GetConfiguration();

		#endregion // End of Methods


	}
}
