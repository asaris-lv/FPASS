using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;


namespace de.pta.Component.Errorhandling
{
	/// <summary>
	/// Summary description for UIExceptionDelegate.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> D.Hassloecher, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// <b>Author:</b> A. Seibt, PTA GmbH
	/// <b>Date:</b> Oct/11/2003
	///	<b>Remarks:</b> Added functionality to translate an error code in
	///	the Message property of an exception to a message text via
	///	a resource file.
	/// </pre>
	/// </remarks>
	public abstract class UIExceptionDelegate
	{
		#region Members

		private IPublisher      mPublisher;

		/// <summary>
		/// The name of the resource used for translating error codes
		/// </summary>
		private string          mResourceName = null;

		/// <summary>
		/// A ResourceManager to read the error texts.
		/// </summary>
		private ResourceManager mResMgr = null;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Default construtor.
		/// </summary>
		public UIExceptionDelegate()
		{
		}

		#endregion //End of Constructors

		#region Initialization
		#endregion //End of Initialization

		#region Accessors 

		public IPublisher Publisher
		{
			set 
			{
				mPublisher = value;
			}
			get
			{
				return mPublisher;
			}
		}


		/// <summary>
		/// The name of the resource used to translate error codes
		/// </summary>
		/// <value>The name of a recource</value>
		/// <remarks>
		/// The property also instantiate a ResourceManager which 
		/// controls the access to the resource.
		/// </remarks>
		public string ResourceName
		{
			get { return mResourceName; }
			set 
			{ 
				mResourceName = value; 
				mResMgr = new ResourceManager(mResourceName, Assembly.GetEntryAssembly());
			}
		}

		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Publishes the exception to the UI.
		/// </summary>
		public abstract void Publish(BaseUIException exception);

		/// <summary>
		/// Translate a message code in the exception message to a real
		/// message text via a resource file.
		/// </summary>
		/// <param name="pException">The exception which contains a message code.</param>
		/// <returns>The translated message from the resource file</returns>
		/// <remarks>
		/// If the passed exception is of type <see cref="de.pta.Component.Errorhandling.BaseApplicationException"><code>BaseApplicationException</code>>/see>,
		/// also parameters of the message text where resolved via the <code>MessageWithParam()</code> method.
		/// </remarks>
		public string GetMessage(Exception pException)
		{
			if(null == mResMgr)
			{
				if(pException is BaseApplicationException)
				{
					return ((BaseApplicationException)pException).MessageWithParam(pException.Message);
				}
				else
				{
					return pException.Message;
				}
			}

			// ensure that the current culture from the current thread is used (an application
			// can set the culture to a value which differs from the culture of the operating system)
			CultureInfo ci  = Thread.CurrentThread.CurrentCulture;
			string      msg = mResMgr.GetString(pException.Message.ToUpper(), ci);
			
			if(msg != null)
			{
				if(pException is BaseApplicationException)
				{
					return ((BaseApplicationException)pException).MessageWithParam(msg);
				}
				else
				{
					return msg;
				}
			}
			else
			{
				if(pException is BaseApplicationException)
				{
					return ((BaseApplicationException)pException).MessageWithParam(pException.Message);
				}
				else
				{
					return pException.Message;
				}
			}
		}

		#endregion // Methods

	}
}