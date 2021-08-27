using System;
using de.pta.Component.Common;

namespace de.pta.Testing
{
	/// <summary>
	/// Helper class for testing of components.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> U.Fretz, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class TestConsole
	{
		#region Members

		public enum Mode : int { ONLINE = 1, BATCH } ;
		private Mode currentMode;
		
		private static TestConsole instance;
		private System.IO.TextWriter output;
		private System.IO.TextWriter oldConsoleOut;
		private System.IO.TextWriter oldConsoleError;
		private System.Reflection.AssemblyName currentAssembly;
		
		#endregion //End of Members

		#region Constructors

		private TestConsole()
		{
			// Since we keep it single no outdoor instanciation
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Returns the one and only instance
		/// </summary>
		/// <returns> the test console </returns>
		public static TestConsole GetInstance()
		{
			if (null == instance) 
			{
				instance = new TestConsole();
				instance.currentMode = Mode.ONLINE;
				instance.output = Console.Out;
				instance.currentAssembly = System.Reflection.Assembly.GetExecutingAssembly().GetName();
			}
			return instance;
		}

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Propery Output
		/// </summary>
		/// <remarks>Default is the setting to standard output in case the parameter TestOutput is
		/// defined in the command line, an appropriate file is created and used for output.
		/// In case the output is defined via this method the same operation takes place
		/// and all outputs are redirected.</remarks>
		public System.IO.TextWriter Output 
		{
			get 
			{
				return output;
			}
			set 
			{
				output = value;
				redirectOutput();
			}
		}

		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Redirecting the output and store the existing console sttings.
		/// </summary>
		private void redirectOutput() 
		{
			oldConsoleOut = Console.Out;
			Console.SetOut(output);
			oldConsoleError = Console.Error;
			Console.SetError(output);
		}

		/// <summary>
		/// Should be called at as the very first statement in a testing module.
		/// </summary>
		/// <remarks>The command line is scanned and in case of Testmode=BATCH a 
		/// enter at the end of the testing module is not applied.
		/// In case the TestOutput is defined with a valid file reference, a newly created
		/// file will be used for all outputs made in the application. This is also true
		/// for all output to the console error and standard out stream. </remarks>
		/// <param name="args"> the command line parameters should be passed </param></param>
		public void BeginTestModule(string[] args) 
		{

			currentAssembly = System.Reflection.Assembly.GetCallingAssembly().GetName();

			for (int i=0; i < args.Length; i++) 
			{

				if (args[i].Equals("Testmode=BATCH")) 
				{
					currentMode = Mode.BATCH;
				}

				if (args[i].StartsWith("TestOutput=")) 
				{
					string filename = args[i];
					int index = filename.IndexOf("=")+1;
					filename = filename.Substring(index, filename.Length-index);
					output = new System.IO.StreamWriter(filename, false, System.Text.ASCIIEncoding.ASCII, 100);
					
					redirectOutput();

				}
				if (args[i].StartsWith("CONFIGPATH=")) 
				{
					string configname = args[i];
					int index = configname.IndexOf("=")+1;
					configname = configname.Substring(index, configname.Length-index);
					ConfigReader.GetInstance().ApplicationRootPath = configname;
										

				}

			}

			output.WriteLine("Testing module: " + currentAssembly.Name +
				" in version: " + currentAssembly.Version);

		}

		/// <summary>
		/// Any testing output, e.g. "Finished testing step ..." should be done via this method.
		/// </summary>
		/// <param name="text">text to write out</param>
		public void WriteLine(string text) 
		{
			output.Write("Testing "+currentAssembly.Name+" : ");
			output.WriteLine(text);
		}

		/// <summary>
		/// Should be called as the very last statement in the testing module.
		/// </summary>
		public void EndTestModule() 
		{

			output.WriteLine();
			output.WriteLine();
			output.Write("All tests successfully done!");
			output.Flush();
			output.Close();

			if (null != oldConsoleOut) 
			{
				Console.SetOut(oldConsoleOut);
			}
			if (null != oldConsoleError) 
			{
				Console.SetError(oldConsoleError);
			}

			if (currentMode.Equals(Mode.ONLINE))
			{
				Console.WriteLine("Press <ENTER> to exit ...");
				Console.In.ReadLine();
			}
		}

		#endregion // Methods
	}
}