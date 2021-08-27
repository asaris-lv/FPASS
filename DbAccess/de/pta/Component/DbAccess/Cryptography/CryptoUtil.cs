using System;
using System.IO;
using System.Security.Cryptography;

namespace de.pta.Component.DbAccess.Cryptography
{
	/// <summary>
	/// The purpose of this class is to encrypt / decrypt the connection string
	/// stored in the configuration file with the Rijndal algorithm.
	/// <p>
	/// <b>Because they key for the algorithm is stored in the class, the
	/// dll has to be encrypted with the Dotfuscator tool provided with
	/// Visual Studio .NET</b>
	/// </p>
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <list type="table">
	/// <item>
	/// <term><b>Author:</b></term>
	/// <description>A. Seibt, PTA GmbH</description>
	/// </item>
	/// <item>
	/// <term><b>Date:</b></term>
	/// <description>Aug/28/2003</description>
	/// </item>
	/// <item>
	/// <term><b>Remarks:</b></term>
	/// <description>initial version</description>
	/// </item>
	/// </list>
	/// </remarks>
	public class CryptoUtil
	{
		#region Members

		/// <summary>The key used to en- / decrypt the connection string.</summary>
		private static byte[] mKey = {0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16};

		/// <summary>The initialization vector for the key.</summary>
		private static byte[] mIV  = {0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16};

		#endregion //End of Members

		#region Constructors

		// No constructor because the class provides only static methods

		#endregion //End of Constructors

		#region Initialization
		#endregion //End of Initialization

		#region Accessors 
		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Takes a encrypted connectionstring as parameter and returns
		/// the decrypted connectionstring.
		/// </summary>
		/// <param name="pInputString">encrypted connectionstring</param>
		/// <returns>decrypted connectionstring</returns>
		public static string Decrypt(string pInputString)
		{

			MemoryStream inStream = new MemoryStream();
			byte[] inBytes = Convert.FromBase64String(pInputString);
			inStream.Write(inBytes,0,inBytes.Length);
			inStream.Position = 0;			

			MemoryStream outStream = new MemoryStream();
			byte[] buffer = new byte[128];

			SymmetricAlgorithm algorithm = SymmetricAlgorithm.Create("Rijndael"); 
			algorithm.IV = mIV;
			algorithm.Key = mKey;
			ICryptoTransform transform = algorithm.CreateDecryptor();
			CryptoStream cryptedStream = new CryptoStream(inStream, transform, CryptoStreamMode.Read);
 
			int restLength = cryptedStream.Read(buffer,0,buffer.Length);
			while(restLength > 0)
			{
				outStream.Write(buffer, 0, restLength);
				restLength = cryptedStream.Read(buffer,0,buffer.Length);
			}

			string outputString =  System.Text.Encoding.Default.GetString(outStream.ToArray());

			cryptedStream.Close();
			cryptedStream = null;
			inStream.Close();
			inStream = null;
			outStream.Close();
			outStream = null;

			return outputString;
		}
	
		/// <summary>
		/// Takes a connectionstring as parameter and returns
		/// the encrypted connectionstring.
		/// </summary>
		/// <param name="pInputString">connectionstring</param>
		/// <returns>encrypted connectionstring</returns>
		public static string Encrypt(string pInputString)
		{
			MemoryStream inStream = new MemoryStream();
			byte[] inBytes = new byte[pInputString.Length];
			inBytes = System.Text.Encoding.Default.GetBytes(pInputString);
			inStream.Write(inBytes,0,inBytes.Length);
			inStream.Position = 0;

			MemoryStream outStream = new MemoryStream();
			byte[] buffer = new byte[128];

			SymmetricAlgorithm algorithm = SymmetricAlgorithm.Create("Rijndael"); 
			algorithm.IV = mIV;
			algorithm.Key = mKey;
			ICryptoTransform transform = algorithm.CreateEncryptor();
			CryptoStream cryptedStream = new CryptoStream(outStream, transform, CryptoStreamMode.Write);
 
			int restLength = inStream.Read(buffer,0,buffer.Length);
			while(restLength > 0)
			{
				cryptedStream.Write(buffer,0,restLength);
				restLength = inStream.Read(buffer,0,buffer.Length);
			}
			cryptedStream.FlushFinalBlock();
		
			string outputString = System.Convert.ToBase64String(outStream.ToArray());

			cryptedStream.Close();
			cryptedStream = null;
			inStream.Close();
			inStream = null;
			outStream.Close();
			outStream = null;

			return outputString;
		}

		#endregion // End of Methods


	}
}
