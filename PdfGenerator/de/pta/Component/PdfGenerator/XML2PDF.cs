using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using de.pta.Component.PdfGenerator.Exceptions;
using de.pta.Component.PdfGenerator.Exceptions.Internal;
using de.pta.Component.PdfGenerator.Internal;
using de.pta.Component.PdfGenerator.Internal.Configuration;

namespace de.pta.Component.PdfGenerator
{
	/// <summary>
	/// XML2PDF provides several methods to generate PDF documents from XML templates based on 
	/// an XML to PDF conversion class library (XMLPDF).
	/// <para>Within these methods there are two different categories:
	/// - First all <c>Generate()</c> methods of the used class library are available. Be aware that 
	/// these methods require the usage of the original XML tags as specified for the used XMLPDF
	/// class library.
	/// - Second additional <c>GeneratePDF()</c> methods are provided offering additional features like
	/// XSLT transformation to add formatting information and to map application specific XML tags of 
	/// the source XML document to the original XML tags as specified for the used XMLPDF class library.
	/// In this case the XML source document contains only data of a specific application, using own XML
	/// tags that derive from the technical context of that application.
	/// The mapping of these tags to XMLPDF specific tags and the layout of the PDF document to generate 
	/// are defined by an additional XSLT document using XSLT transformation mechanisms.
	/// Another feature rich method <c>GeneratePDF()</c> offers the possibility to specify a report to be
	/// generated by report name and a report user profile as defined in the applications configuration.xml 
	/// file. The report configuration (for example the XML template, the layout and the PDF file name) is 
	/// read from the configuration file, taking default values if wanted and no user profile is specified.
	/// </para>
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M. Mehlem, PTA GmbH
	/// <b>Date:</b> Aug/25/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class XML2PDF
	{
		#region Members

		/// <summary>
		/// Generator that provides all methods to generate PDF of the XMLPDF library
		/// </summary>
		private Generator mGenerator;
		/// <summary>
		/// Transformer that provides all methods to transform an XML source file for example by XSLT
		/// </summary>
		private Transformer mTransformer;
		
		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Default Constructor
		/// </summary>
		private XML2PDF()
		{
			initialize();
		}

		/// <summary>
		/// Factory method to get a new XML2PDF instance.
		/// </summary>
		public static XML2PDF NewInstance()
		{
			return new XML2PDF();
		}
		
		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void initialize()
		{
			mGenerator = new Generator();
			mTransformer = new Transformer();
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 


		/// <summary>
		/// Generates a PDF document Stream or File from an XML template Stream or File taking all configuration items of the 
		/// application's configuration.xml file into account.
		/// If the XML template and the PDF files as specified in the configuration should be used, both Stream parameters must
		/// be null. If only one of both should be the configuration specified file, then that parameter should be null. This way
		/// combinations of input file and output Stream vice versa are possible. 
		/// A specified Stream takes always priority over the according specified file of the configuration.
		/// </summary>
		/// <param name="pTemplateStream">A Stream of the XML template. If this parameter is null, the XML template is tried
		/// to be read from an XML template file as specified in the configuration.xml file.</param>
		/// <param name="pPDFStream">A Stream of the generated PDF document. If this parameter is null, the PDF document is tried
		/// to be written to a file as specified in the configuration.xml file.</param>
		/// <param name="pReportId">The id of the report that should be generated as a PDF document.</param>
		/// <param name="pUserProfileId">The id of the user profile the report should be generated for. 
		/// If not specified, that is when given an empty string or null, the default profile is used if the 
		/// boolean pUseDefaultProfile is true, otherwise an error is thrown.</param>
		/// <param name="pUseDefaultProfile">A boolean controlling whether the default configuration for
		/// the specified report should be used if no user profile is specified or if parts of a user profile's
		/// attributes are not specified so that the default values should be used for these attributes.</param>
		/// <exception cref="XML2PDFException">Thrown when any error occurs while analysing the configuration
		/// or while generating.</exception>
		/// <remarks>
		/// </remarks>
		public void GeneratePDF(Stream pTemplateStream, Stream pPDFStream, string pReportId, string pUserProfileId, bool pUseDefaultProfile) 
		{
			bool profileSpecified = ( null != pUserProfileId && 0 < pUserProfileId.Length );
			string basePath;
			string templateFile;
			string layoutFile;
			string pdfFile;
			XML2PDFConfiguration masterConfig;
			ReportConfiguration reportConfig;
			ReportUserProfile userProfile;

			// pre check
			if( !profileSpecified )
			{
				if(!pUseDefaultProfile)
				{
					throw new XML2PDFException("User profile for the requested report was not specified");
				}
			}
			this.AnalyseConfiguration();// filling the configuration
			masterConfig = XML2PDFConfiguration.GetInstance();// referencing the filled configuration
			// handle the case, that a specified report configuration does not exist
			if(! masterConfig.ReportConfigurations.ContainsKey(pReportId))
			{
				throw new XML2PDFException("Specified report configuration does "
										 + "not exist in configuration.xml");
			}
			reportConfig = (ReportConfiguration) masterConfig.ReportConfigurations[pReportId];// referencing the filled report configuration
			
			// the base path is defined unique on the master XML2PDF configuration for all reports
			basePath = masterConfig.BasePath;
			
			// the defaults are defined on the report configuration of a specified report
			// if no file name was specified, then file names should be an empty string (without basePath)
			templateFile = (null != reportConfig.DefaultTemplateFile && 0 < reportConfig.DefaultTemplateFile.Length) ? basePath + reportConfig.DefaultTemplateFile : "";
			layoutFile = (null != reportConfig.DefaultLayoutFile && 0 < reportConfig.DefaultLayoutFile.Length) ? basePath + reportConfig.DefaultLayoutFile : "";
			pdfFile = (null != reportConfig.DefaultPDFFile && 0 < reportConfig.DefaultPDFFile.Length) ? basePath + reportConfig.DefaultPDFFile : "";
			
			if(profileSpecified)
			{
				// handle the case, that a specified user profile does not exist for the report
				if(! reportConfig.ReportUserProfiles.ContainsKey(pUserProfileId))
				{
					throw new XML2PDFException("Specified user profile for the requested report does "
											 + "not exist in configuration.xml");
				}
				// the user profile specified attributes are defined on the matching report user profile
				userProfile = (ReportUserProfile) reportConfig.ReportUserProfiles[pUserProfileId];
				// if the profile's attributes are not specified, then take the defaults if pUseDefaultProfile is true
				templateFile = (null != userProfile.TemplateFile && 0 < userProfile.TemplateFile.Length) ?  basePath + userProfile.TemplateFile : ((pUseDefaultProfile) ? templateFile : "");
				layoutFile = (null != userProfile.LayoutFile && 0 < userProfile.LayoutFile.Length) ? basePath + userProfile.LayoutFile : ((pUseDefaultProfile) ? layoutFile : "");
				pdfFile = (null != userProfile.PDFFile && 0 < userProfile.PDFFile.Length) ? basePath + userProfile.PDFFile : ((pUseDefaultProfile) ? pdfFile : "");
			}
			// check the layout file name if a layout file is required for this report
			if(reportConfig.LayoutRequired)
			{
				if(null == layoutFile || (null != layoutFile && 0 == layoutFile.Length))
				{
					throw new XML2PDFException("A layout file is required for the requested report but does "
						+ "not exist in configuration.xml");
				}
			}
			// invokation depending on the analysed configuration
			this.InvokeGeneration(pTemplateStream, pPDFStream, templateFile, layoutFile, pdfFile);
			
		}
		
		/// <summary>
		/// Generates a PDF document from an XML template containing the data and from an XSLT file
		/// containing layout and formatting information and XMLPDF specific tag mappings.
		/// </summary>
		/// <param name="pTemplateFileName">The path of the file on disk containing the template XML.</param>
		/// <param name="pLayoutFileName">The path of the XSLT file on disk containing the formatting information
		/// and the tag mappings.</param>
		/// <param name="pPDFFileName">The path of the PDF file that will be created. 
		/// If this file already exists it is overwritten.</param>
		/// <exception cref="XML2PDFException">Thrown when any error occurs while generating.</exception>
		/// <remarks>
		/// </remarks>
		public void GeneratePDF(string pTemplateFileName, string pLayoutFileName, string pPDFFileName) 
		{
			if(null == pTemplateFileName || pTemplateFileName.Equals(""))
			{
				throw new XML2PDFException("Template file is not specified");
			}
			if(null == pLayoutFileName || pLayoutFileName.Equals(""))
			{
				throw new XML2PDFException("Layout file is not specified");
			}
			if(null == pPDFFileName || pPDFFileName.Equals(""))
			{
				throw new XML2PDFException("PDF file is not specified");
			}
			FileStream templateFile;
			FileStream pdfFile;
			try
			{
				templateFile = new FileStream(pTemplateFileName, FileMode.Open);
				pdfFile = new FileStream(pPDFFileName, FileMode.Create);
			}
			catch(Exception e)
			{
				throw new XML2PDFException(e.Message, e);
			}
			this.GeneratePDF(templateFile, pLayoutFileName, pdfFile);
			try
			{
				//the pdf stream must be closed
				pdfFile.Close();
			}
			catch(Exception eClose)
			{
				throw new XML2PDFException(eClose.Message, eClose);
			}
		}

		/// <summary>
		/// Generates a PDF document Stream from an XML template file containing the data and from an 
		/// XSLT file containing layout and formatting information and XMLPDF specific tag mappings.
		/// </summary>
		/// <param name="pTemplateFileName">The path of the file on disk containing the template XML.</param>
		/// <param name="pLayoutFileName">The path of the XSLT file on disk containing the formatting information
		/// and the tag mappings.</param>
		/// <param name="pPDFStream">A Stream of the generated PDF document. The Stream is not closed.</param>
		/// <exception cref="XML2PDFException">Thrown when any error occurs while generating.</exception>
		/// <remarks>
		/// </remarks>
		public void GeneratePDF(string pTemplateFileName, string pLayoutFileName, Stream pPDFStream) 
		{
			FileStream template;
			try
			{
				template = new FileStream(pTemplateFileName, FileMode.Open);
			}
			catch(Exception e0pen)
			{
				throw new XML2PDFException(e0pen.Message, e0pen);
			}
			this.GeneratePDF(template, pLayoutFileName, pPDFStream);
			try
			{
				template.Close();
			}
			catch(Exception eClose)
			{
				throw new XML2PDFException(eClose.Message, eClose);
			}
		}

		/// <summary>
		/// Generates a PDF document file from an XML template Stream containing the data and from an 
		/// XSLT file containing layout and formatting information and XMLPDF specific tag mappings.
		/// </summary>
		/// <param name="pTemplateStream">A Stream of the XML template.</param>
		/// <param name="pLayoutFileName">The path of the XSLT file on disk containing the formatting information
		/// and the tag mappings.</param>
		/// <param name="pPDFFileName">The path of the PDF file that will be created. 
		/// If this file already exists it is overwritten.</param>
		/// <exception cref="XML2PDFException">Thrown when any error occurs while generating.</exception>
		/// <remarks>
		/// </remarks>
		public void GeneratePDF(Stream pTemplateStream, string pLayoutFileName, string pPDFFileName) 
		{
			FileStream pdf;
			try
			{
				pdf = new FileStream(pPDFFileName, FileMode.Create);
			}
			catch(Exception eCreate)
			{
				throw new XML2PDFException(eCreate.Message, eCreate);
			}
			this.GeneratePDF(pTemplateStream, pLayoutFileName, pdf);
			try
			{
				pdf.Close();
			}
			catch(Exception eClose)
			{
				throw new XML2PDFException(eClose.Message, eClose);
			}
		}

		/// <summary>
		/// Generates a PDF document Stream from an XML template Stream containing the data and from an 
		/// XSLT file containing layout and formatting information and XMLPDF specific tag mappings.
		/// </summary>
		/// <param name="pTemplateStream">A Stream of the XML template.</param>
		/// <param name="pLayoutFileName">The path of the XSLT file on disk containing the formatting information
		/// and the tag mappings.</param>
		/// <param name="pPDFStream">A Stream of the generated PDF document. The Stream is not closed.</param>
		/// <exception cref="XML2PDFException">Thrown when any error occurs while generating.</exception>
		/// <remarks>
		/// </remarks>
		public void GeneratePDF(Stream pTemplateStream, string pLayoutFileName, Stream pPDFStream) 
		{
			if(null == pTemplateStream)
			{
				throw new XML2PDFException("Template stream is not specified");
			}
			if(null == pLayoutFileName || pLayoutFileName.Equals(""))
			{
				throw new XML2PDFException("Layout file is not specified");
			}
			if(null == pPDFStream)
			{
				throw new XML2PDFException("PDF stream is not specified");
			}
			
			Stream transformedTemplate;
			try
			{
				transformedTemplate = mTransformer.TransformXML(pTemplateStream, pLayoutFileName);
			}
			catch(TransformerException te)
			{
				Exception innerError = te.InnerException;
				string message = te.Message;
				if(null != innerError)
				{
					message += "\nOriginal error: " + te.InnerException.Message;
				}
				throw new XML2PDFException(message, te);
			}

			try
			{
				mGenerator.Generate(transformedTemplate, pPDFStream);
			}
			catch(GeneratorException ge)
			{
				Exception innerError = ge.InnerException;
				string message = ge.Message;
				if(null != innerError)
				{
					message += "\nOriginal error: " + ge.InnerException.Message;
				}
				throw new XML2PDFException(message, ge);
			}
			finally
			{
				//the streams must be closed
				transformedTemplate.Close();
				pTemplateStream.Close();
			}
		}

		/// <summary>
		/// Generates a PDF document from an XML template containing the data and containing layout and 
		/// formatting information and XMLPDF specific tags.
		/// </summary>
		/// <param name="pTemplateFileName">The path of the file on disk containing the template XML with data and
		/// formatting information merged, using the standard tags of the XMLPDF class library.</param>
		/// <param name="pPDFFileName">The path of the PDF file that will be created. 
		/// If this file already exists it is overwritten.</param>
		/// <exception cref="XML2PDFException">Thrown when any error occurs while generating.</exception>
		/// <remarks>
		/// </remarks>
		public void Generate(string pTemplateFileName, string pPDFFileName) 
		{
			if(null == pTemplateFileName || pTemplateFileName.Equals(""))
			{
				throw new XML2PDFException("Template file is not specified");
			}
			if(null == pPDFFileName || pPDFFileName.Equals(""))
			{
				throw new XML2PDFException("PDF file is not specified");
			}
			try
			{
				mGenerator.Generate(pTemplateFileName, pPDFFileName);
			}
			catch(GeneratorException ge)
			{
				Exception innerError = ge.InnerException;
				string message = ge.Message;
				if(null != innerError)
				{
					message += "\nOriginal error: " + ge.InnerException.Message;
				}
				throw new XML2PDFException(message, ge);
			}
		}

		/// <summary>
		/// Generates a PDF document from an XML template, merging additional data.
		/// </summary>
		/// <param name="pTemplateFileName">The path of the file on disk containing the template XML with data and
		/// formatting information merged, using the standard tags of the XMLPDF class library.</param>
		/// <param name="pPDFFileName">The path of the PDF file that will be created. 
		/// If this file already exists it is overwritten.</param>
		/// <param name="pDataFileName">The path of the file where XML data to be merged into the template 
		/// is stored.</param>
		/// <exception cref="XML2PDFException">Thrown when any error occurs while generating.</exception>
		/// <remarks>
		/// </remarks>
		public void Generate(string pTemplateFileName, string pPDFFileName, string pDataFileName)
		{
			if(null == pTemplateFileName || pTemplateFileName.Equals(""))
			{
				throw new XML2PDFException("Template file is not specified");
			}
			if(null == pPDFFileName || pPDFFileName.Equals(""))
			{
				throw new XML2PDFException("PDF file is not specified");
			}
			if(null == pDataFileName || pDataFileName.Equals(""))
			{
				throw new XML2PDFException("Data file is not specified");
			}
			try
			{
				mGenerator.Generate(pTemplateFileName, pPDFFileName, pDataFileName);
			}
			catch(GeneratorException ge)
			{
				Exception innerError = ge.InnerException;
				string message = ge.Message;
				if(null != innerError)
				{
					message += "\nOriginal error: " + ge.InnerException.Message;
				}
				throw new XML2PDFException(message, ge);
			}
		}

		/// <summary>
		/// Generates a PDF document from an XML template.
		/// </summary>
		/// <param name="pXMLDoc">An XmlDocument containing the template XML with data and
		/// formatting information merged, using the standard tags of the XMLPDF class library.</param>
		/// <param name="pPDFStream">A Stream containing the PDF file that will be created.</param>
		/// <exception cref="XML2PDFException">Thrown when any error occurs while generating.</exception>
		/// <remarks>
		/// </remarks>
		public void Generate(XmlDocument pXMLDoc, Stream pPDFStream)
		{
			if(null == pXMLDoc )
			{
				throw new XML2PDFException("Template XML document is not specified");
			}
			if(null == pPDFStream)
			{
				throw new XML2PDFException("PDF stream is not specified");
			}
			try
			{
				mGenerator.Generate(pXMLDoc, pPDFStream);
			}
			catch(GeneratorException ge)
			{
				Exception innerError = ge.InnerException;
				string message = ge.Message;
				if(null != innerError)
				{
					message += "\nOriginal error: " + ge.InnerException.Message;
				}
				throw new XML2PDFException(message, ge);
			}
		}

		/// <summary>
		/// Generates a PDF document from an XML template.
		/// </summary>
		/// <param name="pTemplateFileName">The path of the file on disk containing the template XML with data and
		/// formatting information merged, using the standard tags of the XMLPDF class library.</param>
		/// <param name="pPDFStream">A Stream containing the PDF file that will be created.</param>
		/// <exception cref="XML2PDFException">Thrown when any error occurs while generating.</exception>
		/// <remarks>
		/// </remarks>
		public void Generate(string pTemplateFileName, Stream pPDFStream)
		{
			if(null == pTemplateFileName || pTemplateFileName.Equals(""))
			{
				throw new XML2PDFException("Template file is not specified");
			}
			if(null == pPDFStream)
			{
				throw new XML2PDFException("PDF stream is not specified");
			}
			try
			{
				mGenerator.Generate(pTemplateFileName, pPDFStream);
			}
			catch(GeneratorException ge)
			{
				Exception innerError = ge.InnerException;
				string message = ge.Message;
				if(null != innerError)
				{
					message += "\nOriginal error: " + ge.InnerException.Message;
				}
				throw new XML2PDFException(message, ge);
			}
		}

		/// <summary>
		/// Generates a PDF document from an XML template.
		/// </summary>
		/// <param name="pTemplateStream">A Stream containing the template XML with data and
		/// formatting information merged, using the standard tags of the XMLPDF class library.</param>
		/// <param name="pPDFFileName">The path of the PDF file that will be created. 
		/// If this file already exists it is overwritten.</param>
		/// <exception cref="XML2PDFException">Thrown when any error occurs while generating.</exception>
		/// <remarks>
		/// </remarks>
		public void Generate(Stream pTemplateStream, string pPDFFileName)
		{
			FileStream pdf;
			try
			{
				pdf = new FileStream(pPDFFileName, FileMode.Create);
			}
			catch(Exception eCreate)
			{
				throw new XML2PDFException(eCreate.Message, eCreate);
			}
			this.Generate(pTemplateStream, pdf);
			try
			{
				pdf.Close();
			}
			catch(Exception eClose)
			{
				throw new XML2PDFException(eClose.Message, eClose);
			}
		}

		/// <summary>
		/// Generates a PDF document from an XML template, merging additional data.
		/// </summary>
		/// <param name="pTemplateFileName">The path of the file on disk containing the template XML with data and
		/// formatting information merged, using the standard tags of the XMLPDF class library.</param>
		/// <param name="pPDFStream">A Stream containing the PDF file that will be created.</param>
		/// <param name="pDataFileName">A String containing the XML data elements to be merged into the template XML.</param>
		/// <exception cref="XML2PDFException">Thrown when any error occurs while generating.</exception>
		/// <remarks>
		/// </remarks>
		public void Generate(string pTemplateFileName, Stream pPDFStream, string pDataFileName)
		{
			if(null == pTemplateFileName || pTemplateFileName.Equals(""))
			{
				throw new XML2PDFException("Template file is not specified");
			}
			if(null == pPDFStream)
			{
				throw new XML2PDFException("PDF stream is not specified");
			}
			if(null == pDataFileName || pDataFileName.Equals(""))
			{
				throw new XML2PDFException("Data file is not specified");
			}
			try
			{
				mGenerator.Generate(pTemplateFileName, pPDFStream, pDataFileName);
			}
			catch(GeneratorException ge)
			{
				Exception innerError = ge.InnerException;
				string message = ge.Message;
				if(null != innerError)
				{
					message += "\nOriginal error: " + ge.InnerException.Message;
				}
				throw new XML2PDFException(message, ge);
			}
		}
		
		/// <summary>
		/// Generates a PDF document from an XML template.
		/// </summary>
		/// <param name="pTemplateStream">A Stream containing the template XML with data and
		/// formatting information merged, using the standard tags of the XMLPDF class library.</param>
		/// <param name="pPDFStream">A Stream containing the PDF file that will be created.</param>
		/// <exception cref="XML2PDFException">Thrown when any error occurs while generating.</exception>
		/// <remarks>
		/// </remarks>
		public void Generate(Stream pTemplateStream, Stream pPDFStream)
		{
			if(null == pTemplateStream )
			{
				throw new XML2PDFException("Template XML stream is not specified");
			}
			if(null == pPDFStream)
			{
				throw new XML2PDFException("PDF stream is not specified");
			}
			try
			{
				mGenerator.Generate(pTemplateStream, pPDFStream);
			}
			catch(GeneratorException ge)
			{
				Exception innerError = ge.InnerException;
				string message = ge.Message;
				if(null != innerError)
				{
					message += "\nOriginal error: " + ge.InnerException.Message;
				}
				throw new XML2PDFException(message, ge);
			}
		}

		/// <summary>
		/// Generates a PDF document from an XML template, merging additional data.
		/// </summary>
		/// <param name="pTemplateStream">A Stream containing the template XML with data and
		/// formatting information merged, using the standard tags of the XMLPDF class library.</param>
		/// <param name="pPDFStream">A Stream containing the PDF file that will be created.</param>
		/// <param name="pDataPath">A String containing the XML data elements to be merged into the template XML.</param>
		/// <exception cref="XML2PDFException">Thrown when any error occurs while generating.</exception>
		/// <remarks>
		/// </remarks>
		public void Generate(Stream pTemplateStream, Stream pPDFStream, string pDataFileName)
		{
			if(null == pTemplateStream )
			{
				throw new XML2PDFException("Template XML stream is not specified");
			}
			if(null == pPDFStream)
			{
				throw new XML2PDFException("PDF stream is not specified");
			}
			if(null == pDataFileName || pDataFileName.Equals(""))
			{
				throw new XML2PDFException("Data file is not specified");
			}
			try
			{
				mGenerator.Generate(pTemplateStream, pPDFStream, pDataFileName);
			}
			catch(GeneratorException ge)
			{
				throw new XML2PDFException(ge.Message + "\nOriginal error: " + ge.Message, ge);
			}
		}
		
		/// <summary>
		/// Reads the configuration.xml file and analyses the PdfGenerator node to set all relevant
		/// configuration items needed to generate a specified PDF document.
		/// If the PDF's configuration is set using this method, no further file names must be specified 
		/// when invoking the <c>GeneratePDF()</c>method but a report name and a report user profile should
		/// be specified.
		/// </summary>
		/// <exception cref="XML2PDFException">Thrown when any error occurs while analysing the configuration.</exception>
		/// <remarks>
		/// </remarks>
		private void AnalyseConfiguration()
		{	
			if(!XML2PDFConfiguration.GetInstance().AlreadyConfigured)
			{
				ConfigFactory cf = ConfigFactory.GetFactory(ConfigurationTypes.Xml);
				XML2PDFConfiguration config = cf.GetConfiguration();
			}
		}

		/// <summary>
		/// Invokes the adequate <c>GeneratePDF()</c> or <c>Generate()</c> method according to the Stream parameters
		/// and File configuration setting analysed and handled by method <c>GeneratePDF(Stream pTemplateStream, 
		/// Stream pPDFStream, string pReportId, string pUserProfileId, bool pUseDefaultProfile)</c>. The aim of this method
		/// is to implement some needed pre checks before invoking the generating process and to shorten the implementation 
		/// of the mentioned method.
		/// </summary>
		/// <param name="pTemplateStream">A Stream of the XML template. If this parameter is null, the XML template is tried
		/// to be read from the specified XML template file.</param>
		/// <param name="pPDFStream">A Stream of the generated PDF document. If this parameter is null, the PDF document is tried
		/// to be written to the specified PDF file.</param>
		/// <param name="pTemplateFileName">The path of the file on disk containing the template XML.</param>
		/// <param name="pLayoutFileName">The path of the XSLT file on disk containing the formatting information
		/// and the tag mappings.</param>
		/// <param name="pPDFFileName">The path of the PDF file that will be created. 
		/// If this file already exists it is overwritten.</param>
		/// <exception cref="XML2PDFException">Thrown when any error occurs while generating.</exception>
		private void InvokeGeneration(Stream pTemplateStream, Stream pPDFStream, string pTemplateFile, string pLayoutFile, string pPdfFile)
		{
			// pre checks
			if(null == pTemplateStream)// no Stream to read from specified
			{
				if(null == pTemplateFile || (null != pTemplateFile && 0 == pTemplateFile.Length)) // no File to read from specified
				{
					//nothing to read the template from: error
					throw new XML2PDFException("No template file or stream specified");
				}
			}
			if(null == pPDFStream)// no Stream to write to specified
			{
				if(null == pPdfFile || (null != pPdfFile && 0 == pPdfFile.Length)) // no File to write to specified
				{
					//nothing to write the PDF to: error
					throw new XML2PDFException("No PDF file or stream specified");
				}
			
			}
			// invokation depending on the analysed configuration
			if(0 < pLayoutFile.Length)
			{
				if(null != pTemplateStream)
				{
					if(null != pPDFStream)//read from and write to Streams
					{
						this.GeneratePDF(pTemplateStream, pLayoutFile, pPDFStream);
					}
					else//read from a Stream, write to a File
					{
						this.GeneratePDF(pTemplateStream, pLayoutFile, pPdfFile);
					}
				}
				else
				{
					if(null != pPDFStream)//read from a File and write to a Stream
					{
						this.GeneratePDF(pTemplateFile, pLayoutFile, pPDFStream);
					}
					else//read from and write to Files
					{
						this.GeneratePDF(pTemplateFile, pLayoutFile, pPdfFile);
					}
				}
			}
			else
			{
				if(null != pTemplateStream)
				{
					if(null != pPDFStream)//read from and write to Streams
					{
						this.Generate(pTemplateStream, pPDFStream);
					}
					else//read from a Stream, write to a File
					{
						this.Generate(pTemplateStream, pPdfFile);
					}
				}
				else
				{
					if(null != pPDFStream)//read from a File and write to a Stream
					{
						this.Generate(pTemplateFile, pPDFStream);
					}
					else//read from and write to Files
					{
						this.Generate(pTemplateFile, pPdfFile);
					}
				}
			}
		}

		#endregion // End of Methods
	}
}