using System;
using System.Threading;
using System.Collections;

namespace de.pta.Component.Common
{
	/// <summary>
	/// Implements a synchronized queue.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/17/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class SynchronizedQueue
	{
		#region Members

		private String		id;
		private Semaphore	sender;
		private Semaphore	receiver;
		private Queue		inputQueue;
		private Queue		outputQueue;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Default constuction.
		/// </summary>
		/// <param name="id">The id of the synchronized queue object.</param>
		/// <param name="initCountSender">The initial semaphore count for the sender.</param>
		/// <param name="initCountReceiver">The initial semaphore count for the receiver.</param>
		public SynchronizedQueue(String id, int initCountSender, int initCountReceiver)
		{
			initialize(id, initCountSender, initCountReceiver);

			// Set id
			this.id = id;
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize(String id, int initSender, int initReceiver)
		{
			this.id				= id;
			this.sender			= new Semaphore(initSender);
			this.receiver		= new Semaphore(initReceiver);
			this.inputQueue		= new Queue();
			this.outputQueue	= new Queue();
		}	

		#endregion //End of Initialization

		#region Accessors

		/// <summary>
		/// Gets the id of the synchronized queue (read only).
		/// </summary>
		public String Id
		{
			get
			{
				return id;
			}
		}

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Sends a new object to to receiver and releases a sender resource.
		/// </summary>
		/// <param name="obj">A reference to an object.</param>
		public void Send(Object obj)
		{
			// Enqueue the object in the input queue.
			inputQueue.Enqueue(obj);

			// Release a sender resource.
			sender.ReleaseResource();
		}

		/// <summary>
		/// Sends the object back in the output queue and releases a receiver resource.
		/// </summary>
		public void SendBack(Object obj)
		{
			// Enqueue the processed object in the output queue.
			outputQueue.Enqueue(obj);

			// Release a receiver's resource.
			receiver.ReleaseResource();
		}

		/// <summary>
		/// Gets and removes the object at the beginning of the input queue, 
		/// after obtained a sender resource.
		/// </summary>
		/// <returns>A reference to an object.</returns>
		public Object GetInput()
		{
			// Try to get (wait) sender resource.
			sender.ObtainResource();

			// Return the first object from the input queue.
			return inputQueue.Dequeue();
		}

		/// <summary>
		/// Gets and removes the object at the beginning of the output queue,
		/// after obtained a receiver resource.
		/// </summary>
		/// <returns>A reference to an object.</returns>
		public Object GetOutput()
		{
			// Try to get (wait) the receiver resource.
			receiver.ObtainResource();

			// Return the first object from the output queue.
			return outputQueue.Dequeue();
		}

		#endregion // End of Methods

	}
}
