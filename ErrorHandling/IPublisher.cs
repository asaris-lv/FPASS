using System;

namespace de.pta.Component.Errorhandling
{
	/// <summary>
	/// The interface IPublisher requires the method Publish that is usered
	/// by the UIExceptionDelegate to publish a Errormessage to the using instance of a program.
	/// The publishing method is normally defined by the application using the component.
	/// </summary>
	public interface IPublisher
	{
		void Publish(BaseUIException e);
	}
}
