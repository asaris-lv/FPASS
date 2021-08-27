using System;
using System.Threading;
using System.Collections;

namespace de.pta.Component.Common
{
	/// <summary>
	/// Provides an administration for the synchronised queues.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/17/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class QueueFactory
	{
		#region Members

		private static QueueFactory instance = null;
		private Hashtable queues;

		#endregion //End of Members

		#region Constructors

		private QueueFactory()
		{
			// Default Constructor, private for singleton reasons.
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
			// Initializes the members.
			queues = new Hashtable();
		}	

		/// <summary>
		/// Returns the one and only instance of the Singleton.
		/// </summary>
		/// <returns>An instance of the QueueFactory</returns>
		public static QueueFactory GetInstance()
		{
			if ( null == instance )
			{
				instance = new QueueFactory();
			}

			return instance;
		}

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Creates a new SynchronizedQueue object for the given id. If already a
		/// queue exists for the id, no new object is created.
		/// </summary>
		/// <param name="id">The id of the new queue.</param>
		/// <param name="initSender">The initial semaphore count for the sender.</param>
		/// <param name="initReceiver">The initial semaphore count for the receiver.</param>
		/// <exception cref="CommonException">
		/// Throws an exception, if the queue to create already exists.
		/// </exception>
		public void CreateQueueForId(String id, int initSender, int initReceiver)
		{
			lock(this)
			{
				SynchronizedQueue queue = null;

				// Ensure, that the queue does not already exist.
				if ( !queues.ContainsKey(id) )
				{
					// Create new object.
					queue = new SynchronizedQueue(id, initSender, initReceiver);

					// Add to collection.
					queues.Add(id, queue);
				}
				else
				{
					throw new CommonException("ERROR_COMMON_QUEUE_FACTORY_QUEUE_EXISTS");
				}
			}
		}

		/// <summary>
		/// Gets a reference to the SynchronizedQueue object.
		/// </summary>
		/// <param name="id">The id of the requested queue.</param>
		/// <returns>
		/// A reference to the SynchronizedQueue object, if the queue exists for
		/// the given id, otherwise null.
		/// </returns>
		public SynchronizedQueue GetQueue(String id)
		{
			lock(this)
			{
				SynchronizedQueue queue = null;

				// If the queue for the given id exists,
				// get the reference.
				if ( queues.ContainsKey(id) )
				{
					queue = (SynchronizedQueue)queues[id];
				}

				return queue;
			}
		}

		/// <summary>
		/// Removes an SynchronizedQueue from the administation of the singleton.
		/// </summary>
		/// <param name="id">The id of the queue to remove.</param>
		public void RemoveQueueForId(String id)
		{
			// If the queue for the given id exists,
			// remove the object.
			if ( queues.ContainsKey(id) )
			{
				queues.Remove(id);
			}
		}

		#endregion // End of Methods

	}
}