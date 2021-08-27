using System;

namespace de.pta.Component.Errorhandling
{
	/// <summary>
	/// Primary singleton class for handling any kinds of exceptions.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> D.Hassloecher, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class ExceptionProcessor
	{
		#region Members

		private static ExceptionProcessor instance;

		#endregion //End of Members

		#region Constructors

		private ExceptionProcessor()
		{
			// Constructor is not public for singleton reasons
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Returns the one and only instance
		/// </summary>
		/// <returns>instance of ExceptionProcessor</returns>
		public static ExceptionProcessor GetInstance() 
		{
			if ( null == instance ) 
			{
				instance = new ExceptionProcessor();
			}
			return instance;
		}

		#endregion //End of Initialization

		#region Accessors 
		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Does the processing for a given exception.
		/// </summary>
		/// <param name="e"> exception to be processed </param>
		public void Process(Exception e) 
		{
		}

		/// <summary>
		/// Does the processing for a given exception.
		/// </summary>
		/// <param name="e"> exception to be processed </param>
		public void Process(BaseUIException e) 
		{
			e.UIDelegate.Publish(e);
		}

		#endregion // Methods
	}
}