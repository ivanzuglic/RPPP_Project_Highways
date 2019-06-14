using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autoceste.Extensions;
using Microsoft.AspNetCore.Mvc;
using RPPP12.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using PdfRpt.FluentInterface;
using PdfRpt.Core.Contracts;

namespace RPPP12.Controllers
{
    public class ReportController : Controller
    {
        private readonly RPPP12Context ctx;
        private readonly AppSettings appData;
        private const string ExcelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public ReportController(RPPP12Context ctx, IOptionsSnapshot<AppSettings> options)
        {
            this.ctx = ctx;
            appData = options.Value;
        }

        #region Upravitelji
        /// <summary>
        /// Metoda za generaciju jednostavnog tabličnog prikaza za pdf datoteke.
        /// </summary>
        /// <returns>Vraća pdf datoteku.</returns>
        public async Task<IActionResult> UpravePDF()
        {
            string naslov = "Popis upravitelja";
            var upravitelji = await ctx.Upravitelj.Select(m => new Upravitelj
            {
                Oib = m.Oib,
                Ime = m.Ime,
                SifraSjedista = m.SifraSjedista,
                Telefon = m.Telefon,
                Email = m.Email
            })
                                  .AsNoTracking()
                                  .OrderBy(d => d.Oib)
                                  .ToListAsync();
            PdfReport report = CreateReport(naslov);
            #region Podnožje i zaglavlje
            report.PagesFooter(footer =>
            {
                footer.DefaultFooter(DateTime.Now.ToString("dd.MM.yyyy."));
            })
            .PagesHeader(header =>
            {
                header.CacheHeader(cache: true); // It's a default setting to improve the performance.
                header.DefaultHeader(defaultHeader =>
                {
                    defaultHeader.RunDirection(PdfRunDirection.LeftToRight);
                    defaultHeader.Message(naslov);
                });
            });
            #endregion
            #region Postavljanje izvora podataka i stupaca
            report.MainTableDataSource(dataSource => dataSource.StronglyTypedList(upravitelji));
            report.MainTableColumns(columns =>
            {
                columns.AddColumn(column =>
                {
                    column.IsRowNumber(true);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Right);
                    column.IsVisible(true);
                    column.Order(0);
                    column.Width(1);
                    column.HeaderCell("#", horizontalAlignment: HorizontalAlignment.Right);
                });
                columns.AddColumn(column =>
                {
                    column.PropertyName(nameof(Upravitelj.Oib));
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(1);
                    column.Width(2);
                    column.HeaderCell("OIB upravitelja");
                });
                columns.AddColumn(column =>
                {
                    column.PropertyName<Upravitelj>(x => x.Ime);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(2);
                    column.Width(3);
                    column.HeaderCell("Naziv upravitelja", horizontalAlignment: HorizontalAlignment.Center);
                });
                columns.AddColumn(column =>
                {
                    column.PropertyName<Upravitelj>(x => x.SifraSjedista);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(3);
                    column.Width(1);
                    column.HeaderCell("Sjedište upravitelja", horizontalAlignment: HorizontalAlignment.Center);
                });
                columns.AddColumn(column =>
                {
                    column.PropertyName<Upravitelj>(x => x.Telefon);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(4);
                    column.Width(1);
                    column.HeaderCell("Telefon upravitelja", horizontalAlignment: HorizontalAlignment.Center);
                });
                columns.AddColumn(column =>
                {
                    column.PropertyName<Upravitelj>(x => x.Email);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(4);
                    column.Width(1);
                    column.HeaderCell("Mail upravitelja", horizontalAlignment: HorizontalAlignment.Center);
                });
            });
            #endregion
            byte[] pdf = report.GenerateAsByteArray();
            if (pdf != null)
            {
                Response.Headers.Add("content-disposition", "inline; filename=upravitelji.pdf");
                return File(pdf, "application/pdf");
            }
            else
                return NotFound();
        }

        /// <summary>
        /// Metoda za generiranje tabličnog prikaza svih uređaja u excelu.
        /// </summary>
        /// <returns>Vraća excel datoteku.</returns>
        public async Task<IActionResult> UpraviteljiExcel()
        {
            var data = await ctx.Upravitelj.Select(m => new Upravitelj
            {
                Oib = m.Oib,
                Ime = m.Ime,
                SifraSjedista = m.SifraSjedista,
                Telefon = m.Telefon,
                Email = m.Email,
                Autocesta = m.Autocesta
            })
                                  .AsNoTracking()
                                  .OrderBy(d => d.Ime)
                                  .ToListAsync();
            var excel = ExcelCreator.CreateExcel<Upravitelj>(data, "upravitelji.xslsx");
            var content = excel.GetAsByteArray();
            return File(content, ExcelContentType, "upravitelji.xlsx");
        }
        /// <summary>
        /// Metoda za generaciju master-detail excel datoteke.
        /// </summary>
        /// <returns>Vraća excel datoteku.</returns>
        public async Task<IActionResult> UpraviteljiExcelMaster()
        {
            var masterData = await ctx.Upravitelj.Select(m => new Upravitelj
            {
                Oib = m.Oib,
            })
                                  .AsNoTracking()
                                  .OrderBy(d => d.Oib)
                                  .ToListAsync();
            byte[] content;
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Properties.Title = "Master-detail Upravitelji";
                excel.Workbook.Properties.Author = "Autoceste";
                var worksheet = excel.Workbook.Worksheets.Add("MD-Upravitelji");
                //First add the headers
                int i = 1;
                foreach (var data in masterData)
                {
                    worksheet.Cells[i, 1].Value = "Master ID:";
                    worksheet.Cells[i, 2].Value = data.Oib;
                    i++;
                    worksheet.Cells[i, 1].Value = "Details:";
                    i++;
                    var detailData = ctx.Autocesta
                     .AsNoTracking()
                     .Select(m => new Autocesta
                     {
                         SifraAutoceste = m.SifraAutoceste,
                         SifraPocetka = m.SifraPocetka,
                         SifraZavrsetka = m.SifraZavrsetka,
                         ImeAutoceste = m.ImeAutoceste,
                         Kilometraza = m.Kilometraza,
                         Nadimak = m.Nadimak,
                         SifraNacinaPlacanja = m.SifraNacinaPlacanja,
                         SifraUpravitelja = m.SifraUpravitelja
                     })
                     .Where(n => n.SifraUpravitelja == data.SifraUpravitelja)
                     .ToList();
                    if (detailData.Count == 0)
                    {
                        worksheet.Cells[i, 1].Value = "Nema detalja za prikazati";
                        i++;
                    }
                    else
                    {
                        worksheet.Cells[i, 1].Value = "sifra autoceste";
                        worksheet.Cells[i, 2].Value = "sifra pocetka";
                        worksheet.Cells[i, 3].Value = "sifra kraja";
                        worksheet.Cells[i, 4].Value = "sifra nacina placanja";
                        worksheet.Cells[i, 5].Value = "ime autoceste";
                        worksheet.Cells[i, 6].Value = "nadimak autoceste";
                        worksheet.Cells[i, 7].Value = "kilometraža";
                        i++;
                    }
                    foreach (var detail in detailData)
                    {
                        worksheet.Cells[i, 1].Value = detail.SifraAutoceste;
                        worksheet.Cells[i, 2].Value = detail.SifraPocetka;
                        worksheet.Cells[i, 3].Value = detail.SifraZavrsetka;
                        worksheet.Cells[i, 4].Value = detail.SifraNacinaPlacanja;
                        worksheet.Cells[i, 5].Value = detail.ImeAutoceste;
                        worksheet.Cells[i, 6].Value = detail.Nadimak;
                        worksheet.Cells[i, 7].Value = detail.Kilometraza;

                        i++;
                    }
                    i++;
                }
                worksheet.Cells[1, 1, i + 1, 7].AutoFitColumns();
                content = excel.GetAsByteArray();
            }
            return File(content, ExcelContentType, "MD-Upravitelji.xlsx");
        }
        #endregion

        private PdfReport CreateReport(string naslov)
        {
            var pdf = new PdfReport();
            pdf.DocumentPreferences(doc =>
            {
                doc.Orientation(PageOrientation.Portrait);
                doc.PageSize(PdfPageSize.A4);
                doc.DocumentMetadata(new DocumentMetadata
                {
                    Author = "FER-ZPR",
                    Application = "Firma.MVC Core",
                    Title = naslov
                });
                doc.Compression(new CompressionSettings
                {
                    EnableCompression = true,
                    EnableFullCompression = true
                });
            })
            .MainTableTemplate(template =>
            {
                template.BasicTemplate(BasicTemplate.ProfessionalTemplate);
            })
            .MainTablePreferences(table =>
            {
                table.ColumnsWidthsType(TableColumnWidthType.Relative);
               //table.NumberOfDataRowsPerPage(20);
               table.GroupsPreferences(new GroupsPreferences
                {
                    GroupType = GroupType.HideGroupingColumns,
                    RepeatHeaderRowPerGroup = true,
                    ShowOneGroupPerPage = true,
                    SpacingBeforeAllGroupsSummary = 5f,
                    NewGroupAvailableSpacingThreshold = 150,
                    SpacingAfterAllGroupsSummary = 5f
                });
                table.SpacingAfter(4f);
            });
            return pdf;
        }
    }
}