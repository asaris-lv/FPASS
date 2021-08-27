using System;
using System.IO;
using System.Xml;
using XMLPDF;
using de.pta.Component.PdfGenerator.Exceptions.Internal;


namespace de.pta.Component.PdfGenerator.Internal
{
	/// <summary>
	/// Generator provides all public methods of the used XMLPDF library to convert an XML 
	/// source into a PDF result.
	/// Each method encapsulates the call of the library by a try/catch and declares a
	/// PdfGenerator exception to be thrown to enhance stability of this PdfGenerator 
	/// component.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M. Mehlem, PTA GmbH
	/// <b>Date:</b> Aug/25/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class Generator
	{
		#region Members

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Default Constructor
		/// </summary>
		public Generator()
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
		/// Generates a PDF document from an XML template.
		/// </summary>
		/// <param name="pTemplateFileName">The path of the file on disk containing the template XML.</param>
		/// <param name="pPDFFileName">The path of the PDF file that will be created. 
		/// If this file already exists it is overwritten.</param>
		/// <exception cref="GeneratorException">Thrown when any error occurs while generating.</exception>
		/// <remarks>
		/// </remarks>
		internal void Generate(string pTemplateFileName, string pPDFFileName)
		{
			PDFDocument doc = new PDFDocument();
			try
			{
				doc.Generate(pTemplateFileName, pPDFFileName);
			}
			catch(Exception e)
			{
				throw new GeneratorException("Generating PDF failed", e);
			}
		}

		/// <summary>
		/// Generates a PDF document from an XML template, merging additional data.
		/// </summary>
		/// <param name="pTemplateFileName">The path of the file on disk containing the template XML.</param>
		/// <param name="pPDFFileName">The path of the PDF file that will be created. 
		/// If this file already exists it is overwritten.</param>
		/// <param name="pDataFileName">The path of the file where XML data to be merged into the template 
		/// is stored.</param>
		/// <exception cref="GeneratorException">Thrown when any error occurs while generating.</exception>
		/// <remarks>
		/// </remarks>
		internal void Generate(string pTemplateFileName, string pPDFFileName, string pDataFileName)
		{
			PDFDocument doc = new PDFDocument();
			try{
				doc.Generate(pTemplateFileName, pPDFFileName, pDataFileName);
			}
			catch(Exception e)
			{
				throw new GeneratorException("Generating PDF failed", e);
			}
		}

		/// <summary>
		/// Generates a PDF document from an XML template.
		/// </summary>
		/// <param name="pXMLDoc">An XmlDocument containing the template XML.</param>
		/// <param name="pPDFStream">A Stream containing the PDF file that will be created.</param>
		/// <exception cref="GeneratorException">Thrown when any error occurs while generating.</exception>
		/// <remarks>
		/// </remarks>
		internal void Generate(XmlDocument pXMLDoc, Stream pPDFStream)
		{
			PDFDocument doc = new PDFDocument();
			try
			{
				doc.Generate(pXMLDoc, pPDFStream);
			}
			catch(Exception e)
			{
				throw new GeneratorException("Generating PDF failed", e);
			}
		}

		/// <summary>
		/// Generates a PDF document from an XML template.
		/// </summary>
		/// <param name="pTemplateFileName">The path of the file on disk containing the template XML.</param>
		/// <param name="pPDFStream">A Stream containing the PDF file that will be created.</param>
		/// <exception cref="GeneratorException">Thrown when any error occurs while generating.</exception>
		/// <remarks>
		/// </remarks>
		internal void Generate(string pTemplateFileName, Stream pPDFStream)
		{
			PDFDocument doc = new PDFDocument();
			try
			{
				doc.Generate(pTemplateFileName, pPDFStream);
			}
			catch(Exception e)
			{
				throw new GeneratorException("Generating PDF failed", e);
			}
		}

		/// <summary>
		/// Generates a PDF document from an XML template, merging additional data.
		/// </summary>
		/// <param name="pTemplateFileName">The path of the file on disk containing the template XML.</param>
		/// <param name="pPDFStream">A Stream containing the PDF file that will be created.</param>
		/// <param name="pDataFileName">A String containing the XML data elements to be merged into the template XML.</param>
		/// <exception cref="GeneratorException">Thrown when any error occurs while generating.</exception>
		/// <remarks>
		/// </remarks>
		internal void Generate(string pTemplateFileName, Stream pPDFStream, string pDataFileName)
		{
			PDFDocument doc = new PDFDocument();
			try
			{
				doc.Generate(pTemplateFileName, pPDFStream, pDataFileName);
			}
			catch(Exception e)
			{
				throw new GeneratorException("Generating PDF failed", e);
			}
		}
		
		/// <summary>
		/// Generates a PDF document from an XML template.
		/// </summary>
		/// <param name="pTemplateStream">A Stream containing the template XML.</param>
		/// <param name="pPDFStream">A Stream containing the PDF file that will be created.</param>
		/// <exception cref="GeneratorException">Thrown when any error occurs while generating.</exception>
		/// <remarks>
		/// </remarks>
		internal void Generate(Stream pTemplateStream, Stream pPDFStream)
		{
			PDFDocument doc = new PDFDocument();
			try
			{
				doc.Generate(pTemplateStream, pPDFStream);
			}
			catch(Exception e)
			{
				throw new GeneratorException("Generating PDF failed", e);
			}
		}

		/// <summary>
		/// Generates a PDF document from an XML template, merging additional data.
		/// </summary>
		/// <param name="pTemplateStream">A Stream containing the template XML.</param>
		/// <param name="pPDFStream">A Stream containing the PDF file that will be created.</param>
		/// <param name="pDataFileName">A String containing the XML data elements to be merged into the template XML.</param>
		/// <exception cref="GeneratorException">Thrown when any error occurs while generating.</exception>
		/// <remarks>
		/// </remarks>
		internal void Generate(Stream pTemplateStream, Stream pPDFStream, string pDataFileName)
		{
			PDFDocument doc = new PDFDocument();
			try
			{
				doc.Generate(pTemplateStream, pPDFStream, pDataFileName);
			}
			catch(Exception e)
			{
				throw new GeneratorException("Generating PDF failed", e);
			}
		}

		#endregion // End of Methods
	}
}
