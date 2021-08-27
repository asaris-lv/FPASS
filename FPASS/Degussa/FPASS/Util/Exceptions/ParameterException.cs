using System;
using System.Collections.Generic;
using System.Text;
using Degussa.FPASS.Util.Exceptions;
using de.pta.Component.Errorhandling;

namespace FPASS.Degussa.FPASS.Util.Exceptions
{
	class ParameterException : BaseFPASSException
	{
        public ParameterException(string message) : base(message)
        {
            
        }

        public override BaseUIException GetUIException()
        {
            return new UIFatalException(this.Message);
        }		
	}
}
