using System;
using System.Collections;

namespace de.pta.Component.ListOfValues.Internal
{
	/// <summary>
	/// Abstact base class for the demands.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal abstract class ListDemand
	{
		#region Members

		private ILocalization localization;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Constructs the object.
		/// </summary>
		public ListDemand()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
			// Initializes the members.
			localization = null;
		}

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Sets and gets the localization interface.
		/// </summary>
		public ILocalization Localization
		{
			get
			{
				return localization;
			}
			set
			{
				localization = value;
			}
		}

		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Retrieves the language dependent text from the localization interface.
		/// When no localization interface is set, the input text is returned.
		/// </summary>
		/// <param name="text">A String that contains the text to translate.</param>
		/// <returns>Returns a string that contains the translated text.</returns>
		public String GetLanguageText(String text)
		{
			String localized = String.Empty;

			// If no interface is set, use the incoming text.
			if ( null == localization )
			{
				localized = text;
			}
			else
			{
				// Get the localized text and return it.
				localized = localization.GetLocalizedText(text);
			}

			// Return result.
			return localized;
		}

		#endregion
	}
}