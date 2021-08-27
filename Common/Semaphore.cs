using System;
using System.Threading;

namespace de.pta.Component.Common
{
	/// <summary>
	/// Encapsulates the semaphore funktionality.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/17/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class Semaphore
	{
		#region Members

		private int count;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Constructs the object.
		/// </summary>
		/// <param name="initialCount">The initial count value.</param>
		public Semaphore(int initialCount)
		{
			count = initialCount;
		}

		#endregion //End of Constructors

		#region Methods 

		/// <summary>
		/// Gets the resource if any exists, otherwise waits until
		/// one is freed.
		/// </summary>
		public void ObtainResource()
		{
			lock(this)
			{
				while ( count == 0 )
				{
					Monitor.Wait(this, Timeout.Infinite);
				}
				count--;
			}
		}

		/// <summary>
		/// Releases a resource.
		/// </summary>
		public void ReleaseResource()
		{
			lock(this)
			{
				count++;
				Monitor.Pulse(this);
			}
		}

		#endregion // End of Methods
	}
}
