using System;

namespace Degussa.FPASS.Bo.Administration
{
	/// <summary>
	/// Summary description for BOPrecMedical.
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">PTA GmbH</th>
	///			<th width="20%">12/01/2003</th>
	///			<th width="60%">Remarks</th>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class BOPrecMedical : AbstractAdminBO
	{
		#region Members

		private string	pmtyNotation;
		private string	pmtyHelpfile;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public BOPrecMedical()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void initialize()
		{

		}	

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string ProppmtyNotation
		{
			get 
			{
				return pmtyNotation;
			}
			set 
			{
				pmtyNotation = value;
			}
		} 
		
		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string ProppmtyHelpfile
		{
			get 
			{
				return pmtyHelpfile;
			}
			set 
			{
				pmtyHelpfile = value;
			}
		} 
		
		#endregion //End of Accessors

		#region Methods 

		#endregion // End of Methods

	}
}
