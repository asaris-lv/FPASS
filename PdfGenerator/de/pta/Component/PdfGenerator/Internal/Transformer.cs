using System;
using System.IO;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml;
using System.Windows.Forms;
using de.pta.Component.PdfGenerator.Exceptions.Internal;
using de.pta.Component.PdfGenerator.Internal.Configuration;

namespace de.pta.Component.PdfGenerator.Internal
{
	/// <summary>
	/// Transformer offers functionality to transform XML data files and Streams using XSLT transformation 
	/// mechanisms.
	/// Transformation means to add formatting information to the XML nodes of the data XML 
	/// and to replace the used XML nodes from the concrete XML document's technical context by 
	/// XML nodes matching the XML syntax of XMLPDF that is the PDF writing class library 
	/// used by this PdfGenerator component.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M. Mehlem, PTA GmbH
	/// <b>Date:</b> Aug/25/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class Transformer
	{
		#region Members

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Default Constructor
		/// </summary>
		public Transformer()
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

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Transforms an XML data file to a formatted XML file taking the specified
		/// XSLT transformation file into account. 
		/// The transformed XML file contains XML nodes matching the syntax of the 
		/// used XMLPDF library and contains additional formatting information as
		/// specified by the XSLT file.
		/// The path of the transformed XML file is returned. 
		/// </summary>
		/// <param name="pTemplateFileName">The path of the XML data file to transform</param>
		/// <param name="pLayoutFileName">The path of the XSLT transformation file</param>
		/// <returns>The path of the transformed XML file</returns>
		/// <exception cref="TransformerException">Thrown when any error occurs while 
		/// transforming the XML file.</exception>
		/// <remarks>
		/// </remarks>
		internal string TransformXML(string pTemplateFileName, string pLayoutFileName) 
		{
			string transformedXML = "";
			try
			{
				
				XPathDocument doc  = new XPathDocument(pTemplateFileName);
				XslTransform trans = new XslTransform();
			
				trans.Load(pLayoutFileName);
				transformedXML = VirtualFileName(pTemplateFileName);
				FileStream fs = new FileStream(transformedXML, FileMode.Create);

				XPathNavigator nav = doc.CreateNavigator();
				trans.Transform(nav, null, fs, null);
				fs.Close();
			}
			catch(Exception e)
			{
				throw new TransformerException("Transformation of template file failed", e);
			}

			return transformedXML;
		}

		/// <summary>
		/// Transforms an XML data Stream to a formatted XML Stream taking the specified
		/// XSLT transformation file into account. 
		/// The transformed XML contains XML tags matching the syntax of the 
		/// used XMLPDF library and contains additional formatting information as
		/// specified by the XSLT file.
		/// A new MemoryStream containing the transformed XML is returned. 
		/// </summary>
		/// <param name="pTemplate">A Stream of the XML data document to transform</param>
		/// <param name="pLayoutFileName">The path of the XSLT transformation file</param>
		/// <returns>A MemoryStream of the transformed XML Stream</returns>
		/// <exception cref="TransformerException">Thrown when any error occurs while 
		/// transforming the XML file.</exception>
		/// <remarks>
		/// </remarks>
		internal Stream TransformXML(Stream pTemplate, string pLayoutFileName) 
		{
			MemoryStream transformedXMLStream = null;
			try
			{	
				XPathDocument doc  = new XPathDocument(pTemplate);
				XslTransform trans = new XslTransform();
			
				trans.Load(pLayoutFileName);
				transformedXMLStream = new MemoryStream();

				XPathNavigator nav = doc.CreateNavigator();
				trans.Transform(nav, null, transformedXMLStream, null);
			}
			catch(Exception e)
			{
				throw new TransformerException("Transformation of template file failed", e);
			}

			return transformedXMLStream;
		}


		/// <summary>
		/// Generates a temporarily to use virtual file name, containing the current time stamp
		/// in milliseconds to identify the transformed XML file by a unique file name.
		/// </summary>
		/// <param name="pBaseFileName">The path of the original XML file to transform that is used
		/// as a base for the virtual file name.</param>
		/// <returns>The path of the transformed XML file</returns>
		/// <remarks>
		/// </remarks>
		private string VirtualFileName(string pBaseFileName)
		{
			string virtualFileName = pBaseFileName;
			if(null != pBaseFileName)
			{
				int index = pBaseFileName.IndexOf(".xml");
				if(-1 < index)
				{
					virtualFileName = pBaseFileName.Substring(0, index);
				}
				virtualFileName += "_" + Environment.TickCount;
				virtualFileName += ".xml";
			}
			return virtualFileName;
		}
		#endregion // End of Methods
	}
}