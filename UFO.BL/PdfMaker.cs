﻿using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.BL.execptions;
using UFO.DomainClasses;

namespace UFO.BL {

	public class PdfMaker : IPdfMaker {
		private IEnumerable<SpectacledayTimeSlot> spectacleDayTimeSlots;
		private string pdfName;
		private string pdfPath;
		private IEnumerable<Performance> performances;
		private IEnumerable<Area> areas;
		private IEnumerable<Venue> venues;
		private IEnumerable<TimeSlot> timeSlots;
		private IEnumerable<Artist> artists;

		public PdfMaker(string pdfName, string pdfPath) {
			this.pdfName = pdfName;
			this.pdfPath = pdfPath;
		}

		public void MakeSpectacleSchedule(IEnumerable<SpectacledayTimeSlot> spectacleDayTimeSlots,
										  IEnumerable<Performance> performances,
										  IEnumerable<Area> areas,
										  IEnumerable<Venue> venues,
										  IEnumerable<TimeSlot> timeSlots,
										  IEnumerable<Artist> artists) {
			try {
				this.spectacleDayTimeSlots = spectacleDayTimeSlots;
				this.performances = performances;
				this.areas = areas;
				this.venues = venues;
				this.timeSlots = timeSlots;
				this.artists = artists;
				Document document = CreateDocument();
				PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(false, PdfFontEmbedding.Always);
				pdfRenderer.Document = document;
				pdfRenderer.RenderDocument();
				pdfRenderer.PdfDocument.Save(pdfPath + pdfName);
			} catch {
				throw new BusinessLogicException("Could not create PDF-File.");
			}
		}

		public Document CreateDocument() {
			Document document = new Document();
			document.DefaultPageSetup.Orientation = Orientation.Landscape;
			document.Info.Title = "UFO-Schedule";
			document.Info.Subject = "Schedule of Spectacle Performances";
			document.Info.Author = "Christoph Wurst, Stefan Rösch";

			DefineStyles(document);
			CreatePage(document);

			return document;
		}

		private void DefineStyles(Document document) {
			Style style = document.Styles["Normal"];
			style.Font.Name = "Segoe UI";

			style = document.Styles[StyleNames.Header];
			style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

			style = document.Styles[StyleNames.Footer];
			style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

			style = document.Styles.AddStyle("Table", "Normal");
			style.Font.Name = "Segoe UI";
			style.Font.Size = 8;
		}

		private void CreatePage(Document document) {
			Section section = document.AddSection();

			Paragraph paragraph = section.Footers.Primary.AddParagraph();
			paragraph.AddText("Ultimate Festival Organizer Inc.");
			paragraph.Format.Font.Size = 9;
			paragraph.Format.Alignment = ParagraphAlignment.Center;
			CreateTableForAreas(section);
		}

		private void CreateTableForAreas(Section section) {
			var table = section.AddTable();
			table.Style = "Table";
			table.Borders.Width = 0.25;

			CreateColumns(table);

			foreach (var area in areas) {
				CreateTableForArea(table, area);
				section.AddParagraph("");
			}
		}

		private void CreateTableForArea(Table table, Area area) {
			CreateHeaderRow(table, area);
			CreateDataRow(table, area);
		}

		private void CreateDataRow(Table table, Area area) {
			foreach (var venue in venues.Where(v => v.AreaId == area.Id)) {
				Row row = table.AddRow();
				row.Cells[0].AddParagraph(venue.ShortDescription + " " + venue.Description);
				row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
				InsertArtistsIntoCells(venue, row);
			}
		}

		private void InsertArtistsIntoCells(Venue venue, Row row) {
			int i = 0;
			foreach (var timeSlot in timeSlots) {
				i++;
				SpectacledayTimeSlot daySlot = spectacleDayTimeSlots.Where(s => s.TimeSlotId == timeSlot.Id).FirstOrDefault();
				Performance performance = performances.Where(p => p.SpectacledayTimeSlot == daySlot.Id &&
																  p.VenueId == venue.Id).FirstOrDefault();
				Artist currArtist;
				if (performance != null) {
					currArtist = artists.Where(a => a.Id == performance.ArtistId).FirstOrDefault();
				} else {
					currArtist = new Artist() { Name = "" };
				}
				row.Cells[i].AddParagraph(currArtist.Name);
			}
		}

		private void CreateHeaderRow(Table table, Area area) {
			Row row = table.AddRow();
			row.Format.Alignment = ParagraphAlignment.Center;
			row.Format.Font.Bold = true;
			int i = 0;
			row.Cells[i].AddParagraph(area.Name);
			foreach (var timeSlot in timeSlots) {
				i++;
				row.Cells[i].AddParagraph(timeSlot.Start + "-" + timeSlot.End);
				row.Cells[i].VerticalAlignment = VerticalAlignment.Center;
			}
		}

		private void CreateColumns(Table table) {
			Column column = table.AddColumn("4cm");
			column.Format.Alignment = ParagraphAlignment.Center;

			foreach (var timeSlot in this.timeSlots) {
				column = table.AddColumn("3cm");
				column.Format.Alignment = ParagraphAlignment.Center;
			}
		}
	}
}