using System;

namespace de.pta.Component.ListOfValues
{
	/// <summary>
	/// Interface that provides language specific information.
	/// A client that uses the list of values, must implement
	/// this interface and provide so the necessary information.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Sept/23/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public interface ILocalization
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
		/// Gets a localized text.
		/// The implementation of the method must localize the text provided
		/// as parameter. Localization can be done by a localization manager
		/// or by a database and so on.
		/// If no localization is needed, the incoming text can only be returned.
		/// </summary>
		/// <param name="text">Text to localize.</param>
		/// <returns>The localized text.</returns>
		String GetLocalizedText(String text);

		#endregion //Methods
	}







}
