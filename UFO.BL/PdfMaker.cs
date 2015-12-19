using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.BL {

	public class PdfMaker : IPdfMaker {
		private string pdfName;
		private string pdfPath;

		public PdfMaker(string pdfName, string pdfPath) {
			this.pdfName = pdfName;
			this.pdfPath = pdfPath;
		}

		public void MakeSpectacleSchedule(IEnumerable<SpectacledayTimeSlot> spectacledayTimeslots, IEnumerable<Performance> performances) {
			Document document = new Document();
			Section section = document.AddSection();
			Paragraph paragraph = section.AddParagraph();
			paragraph.Format.Font.Color = Color.FromCmyk(100, 30, 20, 50);
			paragraph.AddFormattedText("Hello UFO!");
			PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(false, PdfFontEmbedding.Always);
			pdfRenderer.Document = document;
			pdfRenderer.RenderDocument();
			pdfRenderer.PdfDocument.Save(pdfPath + pdfName);
			Process.Start(pdfPath + pdfName);
		}
	}
}