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

        #region Naplatne postaje
        /// <summary>
        /// Metoda za generaciju jednostavnog tabličnog prikaza za pdf datoteke.
        /// </summary>
        /// <returns>Vraća pdf datoteku.</returns>
        public async Task<IActionResult> PostajePDF()
        {
            string naslov = "Popis postaja";
            var postaje = await ctx.NaplatnaPostaja.Select(m => new NaplatnaPostaja
            {
                SifraPostaje = m.SifraPostaje,
                ImePostaje = m.ImePostaje,
                SifraDionice = m.SifraDionice,
                SifraLokacijePostaje = m.SifraLokacijePostaje,

            })
                                  .AsNoTracking()
                                  .OrderBy(d => d.ImePostaje)
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
            report.MainTableDataSource(dataSource => dataSource.StronglyTypedList(postaje));
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
                    column.PropertyName(nameof(NaplatnaPostaja.SifraPostaje));
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(1);
                    column.Width(2);
                    column.HeaderCell("Sifra postaje");
                });
                columns.AddColumn(column =>
                {
                    column.PropertyName<NaplatnaPostaja>(x => x.ImePostaje);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(2);
                    column.Width(3);
                    column.HeaderCell("Ime postaje", horizontalAlignment: HorizontalAlignment.Center);
                });
                columns.AddColumn(column =>
                {
                    column.PropertyName<NaplatnaPostaja>(x => x.SifraDionice);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(3);
                    column.Width(1);
                    column.HeaderCell("Sifra dionice", horizontalAlignment: HorizontalAlignment.Center);
                });
                columns.AddColumn(column =>
                {
                    column.PropertyName<NaplatnaPostaja>(x => x.SifraLokacijePostaje);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(4);
                    column.Width(1);
                    column.HeaderCell("Sifra lokacije postaje", horizontalAlignment: HorizontalAlignment.Center);
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
        public async Task<IActionResult> PostajeExcel()
        {
            var data = await ctx.NaplatnaPostaja.Select(m => new NaplatnaPostaja
            {
                SifraPostaje = m.SifraPostaje,
                ImePostaje = m.ImePostaje,
                SifraDionice = m.SifraDionice,
                SifraLokacijePostaje = m.SifraLokacijePostaje,

            })
                                  .AsNoTracking()
                                  .OrderBy(d => d.ImePostaje)
                                  .ToListAsync();
            var excel = ExcelCreator.CreateExcel<NaplatnaPostaja>(data, "postaje.xslsx");
            var content = excel.GetAsByteArray();
            return File(content, ExcelContentType, "postaje.xlsx");
        }
        /// <summary>
        /// Metoda za generaciju master-detail excel datoteke.
        /// </summary>
        /// <returns>Vraća excel datoteku.</returns>
        public async Task<IActionResult> PostajeExcelMaster()
        {
            var masterData = await ctx.NaplatnaPostaja.Select(m => new NaplatnaPostaja
            {
                SifraPostaje = m.SifraPostaje,
            })
                                  .AsNoTracking()
                                  .OrderBy(d => d.SifraPostaje)
                                  .ToListAsync();
            byte[] content;
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Properties.Title = "Master-detail naplatne postaje";
                excel.Workbook.Properties.Author = "Autoceste";
                var worksheet = excel.Workbook.Worksheets.Add("MD-Postaje");
                //First add the headers
                int i = 1;
                foreach (var data in masterData)
                {
                    worksheet.Cells[i, 1].Value = "Master ID:";
                    worksheet.Cells[i, 2].Value = data.SifraPostaje;
                    i++;
                    worksheet.Cells[i, 1].Value = "Details:";
                    i++;
                    var detailData = ctx.Zaposlenik
                     .AsNoTracking()
                     .Select(m => new Zaposlenik
                     {
                         SifraZaposlenika = m.SifraZaposlenika,
                         SifraVrsteZaposlenika = m.SifraVrsteZaposlenika,
                         Ime = m.Ime,
                         Prezime = m.Prezime,
                         Telefon = m.Telefon,
                         SifraPostaje = m.SifraPostaje
                     })
                     .Where(n => n.SifraPostaje == data.SifraPostaje)
                     .ToList();
                    if (detailData.Count == 0)
                    {
                        worksheet.Cells[i, 1].Value = "Nema detalja za prikazati";
                        i++;
                    }
                    else
                    {
                        worksheet.Cells[i, 1].Value = "sifra zaposlenika";
                        worksheet.Cells[i, 2].Value = "vrsta zaposlenika";
                        worksheet.Cells[i, 3].Value = "ime";
                        worksheet.Cells[i, 4].Value = "prezime";
                        worksheet.Cells[i, 5].Value = "telefon";
                        worksheet.Cells[i, 6].Value = "ime postaje";
                        i++;
                    }
                    foreach (var detail in detailData)
                    {
                        worksheet.Cells[i, 1].Value = detail.SifraZaposlenika;
                        worksheet.Cells[i, 2].Value = detail.SifraVrsteZaposlenika;
                        worksheet.Cells[i, 3].Value = detail.Ime;
                        worksheet.Cells[i, 4].Value = detail.Prezime;
                        worksheet.Cells[i, 5].Value = detail.Telefon;
                        worksheet.Cells[i, 6].Value = detail.SifraPostaje;

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

        #region Dionice
        /// <summary>
        /// Metoda za generaciju jednostavnog tabličnog prikaza za pdf datoteke.
        /// </summary>
        /// <returns>Vraća pdf datoteku.</returns>
        public async Task<IActionResult> DionicePDF()
        {
            string naslov = "Popis postaja";
            var postaje = await ctx.NaplatnaPostaja.Select(m => new NaplatnaPostaja
            {
                SifraPostaje = m.SifraPostaje,
                ImePostaje = m.ImePostaje,
                SifraDionice = m.SifraDionice,
                SifraLokacijePostaje = m.SifraLokacijePostaje,

            })
                                  .AsNoTracking()
                                  .OrderBy(d => d.ImePostaje)
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
            report.MainTableDataSource(dataSource => dataSource.StronglyTypedList(postaje));
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
                    column.PropertyName(nameof(NaplatnaPostaja.SifraPostaje));
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(1);
                    column.Width(2);
                    column.HeaderCell("Sifra postaje");
                });
                columns.AddColumn(column =>
                {
                    column.PropertyName<NaplatnaPostaja>(x => x.ImePostaje);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(2);
                    column.Width(3);
                    column.HeaderCell("Ime postaje", horizontalAlignment: HorizontalAlignment.Center);
                });
                columns.AddColumn(column =>
                {
                    column.PropertyName<NaplatnaPostaja>(x => x.SifraDionice);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(3);
                    column.Width(1);
                    column.HeaderCell("Sifra dionice", horizontalAlignment: HorizontalAlignment.Center);
                });
                columns.AddColumn(column =>
                {
                    column.PropertyName<NaplatnaPostaja>(x => x.SifraLokacijePostaje);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(4);
                    column.Width(1);
                    column.HeaderCell("Sifra lokacije postaje", horizontalAlignment: HorizontalAlignment.Center);
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
        public async Task<IActionResult> DioniceExcel()
        {
            var data = await ctx.Dionica.Select(m => new Dionica
            {
                Naziv = m.Naziv,
                SifraPocetka = m.SifraPocetka,
                SifraKraja = m.SifraKraja,
                SifraAutoceste = m.SifraAutoceste,
                Duljina = m.Duljina
            })
                                  .AsNoTracking()
                                  .OrderBy(d => d.Naziv)
                                  .ToListAsync();
            var excel = ExcelCreator.CreateExcel<Dionica>(data, "dionice.xslsx");
            var content = excel.GetAsByteArray();
            return File(content, ExcelContentType, "dionice.xlsx");
        }
        /// <summary>
        /// Metoda za generaciju master-detail excel datoteke.
        /// </summary>
        /// <returns>Vraća excel datoteku.</returns>
        public async Task<IActionResult> DioniceExcelMaster()
        {
            var masterData = await ctx.Dionica.Select(m => new Dionica
            {
                SifraDionice=m.SifraDionice,
            })
                                  .AsNoTracking()
                                  .OrderBy(d => d.SifraDionice)
                                  .ToListAsync();
            byte[] content;
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Properties.Title = "Master-detail dionice";
                excel.Workbook.Properties.Author = "Autoceste";
                var worksheet = excel.Workbook.Worksheets.Add("MD-Dionica");
                //First add the headers
                int i = 1;
                foreach (var data in masterData)
                {
                    worksheet.Cells[i, 1].Value = "Master ID:";
                    worksheet.Cells[i, 2].Value = data.SifraDionice;
                    i++;
                    worksheet.Cells[i, 1].Value = "Details:";
                    i++;
                    var detailData = ctx.Objekt
                     .AsNoTracking()
                     .Select(m => new Objekt
                     {
                         ImeObjekta = m.ImeObjekta,
                         SifraDionice = m.SifraDionice,
                         SifraVrstaObjekta = m.SifraVrstaObjekta,
                     })
                     .Where(n => n.SifraDionice == data.SifraDionice)
                     .ToList();
                    if (detailData.Count == 0)
                    {
                        worksheet.Cells[i, 1].Value = "Nema detalja za prikazati";
                        i++;
                    }
                    else
                    {
                        worksheet.Cells[i, 1].Value = "ime objekta";
                        worksheet.Cells[i, 2].Value = "sifra dionice";
                        worksheet.Cells[i, 3].Value = "sifra vrste objekta";
                        i++;
                    }
                    foreach (var detail in detailData)
                    {
                        worksheet.Cells[i, 1].Value = detail.ImeObjekta;
                        worksheet.Cells[i, 2].Value = detail.SifraDionice;
                        worksheet.Cells[i, 3].Value = detail.SifraVrstaObjekta;

                        i++;
                    }
                    i++;
                }
                worksheet.Cells[1, 1, i + 1, 7].AutoFitColumns();
                content = excel.GetAsByteArray();
            }
            return File(content, ExcelContentType, "MD-Dionice.xlsx");
        }
        #endregion

        #region Naplatne kućice
        /// <summary>
        /// Metoda za generaciju jednostavnog tabličnog prikaza za pdf datoteke.
        /// </summary>
        /// <returns>Vraća pdf datoteku.</returns>
        public async Task<IActionResult> KućicePDF()
        {
            string naslov = "Popis naplatnih kućica";
            var postaje = await ctx.NaplatnaKucica.Select(m => new NaplatnaKucica
            {
                SifraPostaja=m.SifraPostaja,
                SifraBlagajnika=m.SifraBlagajnika,
                VrstaNaplatneKucice=m.VrstaNaplatneKucice

            })
                                  .AsNoTracking()
                                  .OrderBy(d => d.SifraPostaja)
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
            report.MainTableDataSource(dataSource => dataSource.StronglyTypedList(postaje));
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
                    column.PropertyName(nameof(NaplatnaKucica.SifraKucica));
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(1);
                    column.Width(2);
                    column.HeaderCell("Sifra kucice");
                });
                columns.AddColumn(column =>
                {
                    column.PropertyName<NaplatnaKucica>(x => x.SifraBlagajnika);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(2);
                    column.Width(3);
                    column.HeaderCell("Sifra blagajnika", horizontalAlignment: HorizontalAlignment.Center);
                });
                columns.AddColumn(column =>
                {
                    column.PropertyName<NaplatnaKucica>(x => x.VrstaNaplatneKucice);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(3);
                    column.Width(1);
                    column.HeaderCell("Vrsta kucice", horizontalAlignment: HorizontalAlignment.Center);
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
        public async Task<IActionResult> KuciceExcel()
        {
            var data = await ctx.NaplatnaKucica.Select(m => new NaplatnaKucica
            {
                SifraKucica=m.SifraKucica,
                SifraBlagajnika=m.SifraBlagajnika,
                VrstaNaplatneKucice=m.VrstaNaplatneKucice
            })
                                  .AsNoTracking()
                                  .OrderBy(d => d.SifraKucica)
                                  .ToListAsync();
            var excel = ExcelCreator.CreateExcel<NaplatnaKucica>(data, "kucice.xslsx");
            var content = excel.GetAsByteArray();
            return File(content, ExcelContentType, "kucice.xlsx");
        }
        /// <summary>
        /// Metoda za generaciju master-detail excel datoteke.
        /// </summary>
        /// <returns>Vraća excel datoteku.</returns>
        public async Task<IActionResult> KuciceExcelMaster()
        {
            var masterData = await ctx.NaplatnaKucica.Select(m => new NaplatnaKucica
            {
                SifraKucica=m.SifraKucica,
            })
                                  .AsNoTracking()
                                  .OrderBy(d => d.SifraKucica)
                                  .ToListAsync();
            byte[] content;
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Properties.Title = "Master-detail kucice";
                excel.Workbook.Properties.Author = "Autoceste";
                var worksheet = excel.Workbook.Worksheets.Add("MD-Kucica");
                //First add the headers
                int i = 1;
                foreach (var data in masterData)
                {
                    worksheet.Cells[i, 1].Value = "Master ID:";
                    worksheet.Cells[i, 2].Value = data.SifraKucica;
                    i++;
                    worksheet.Cells[i, 1].Value = "Details:";
                    i++;
                    var detailData = ctx.Racun
                     .AsNoTracking()
                     .Select(m => new Racun
                     {
                         RegistarskaOznaka=m.RegistarskaOznaka,
                         DatumVrijeme=m.DatumVrijeme,
                         SifraKategorijaVozila=m.SifraKategorijaVozila,
                         SifraNacinPlacanja=m.SifraNacinPlacanja
                     })
                     .Where(n => n.SifraKucica == data.SifraKucica)
                     .ToList();
                    if (detailData.Count == 0)
                    {
                        worksheet.Cells[i, 1].Value = "Nema detalja za prikazati";
                        i++;
                    }
                    else
                    {
                        worksheet.Cells[i, 1].Value = "reg oznaka";
                        worksheet.Cells[i, 2].Value = "datum i vrijeme";
                        worksheet.Cells[i, 3].Value = "sifra kategorije vozila";
                        worksheet.Cells[i, 4].Value = "sifra nacina placanja";
                        i++;
                    }
                    foreach (var detail in detailData)
                    {
                        worksheet.Cells[i, 1].Value = detail.RegistarskaOznaka;
                        worksheet.Cells[i, 2].Value = detail.DatumVrijeme;
                        worksheet.Cells[i, 3].Value = detail.SifraKategorijaVozila;
                        worksheet.Cells[i, 4].Value = detail.SifraNacinPlacanja;

                        i++;
                    }
                    i++;
                }
                worksheet.Cells[1, 1, i + 1, 7].AutoFitColumns();
                content = excel.GetAsByteArray();
            }
            return File(content, ExcelContentType, "MD-Kucice.xlsx");
        }
        #endregion

        #region Uredaj
        /// <summary>
        /// Metoda za generaciju jednostavnog tabličnog prikaza za pdf datoteke.
        /// </summary>
        /// <returns>Vraća pdf datoteku.</returns>
        public async Task<IActionResult> UredajPDF()
        {
            string naslov = "Popis uređaja";
            var uredaji = await ctx.Uredaj.Select(m => new Uredaj
            {
                Status=m.Status,
                SifraObjekta =m.SifraObjekta,
                SifraVrsteUredaja=m.SifraVrsteUredaja

            })
                                  .AsNoTracking()
                                  .OrderBy(d => d.SifraUredaja)
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
            report.MainTableDataSource(dataSource => dataSource.StronglyTypedList(uredaji));
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
                    column.PropertyName(nameof(Uredaj.Status));
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(1);
                    column.Width(2);
                    column.HeaderCell("Status");
                });
                columns.AddColumn(column =>
                {
                    column.PropertyName<Uredaj>(x => x.SifraObjekta);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(2);
                    column.Width(3);
                    column.HeaderCell("Sifra objekta", horizontalAlignment: HorizontalAlignment.Center);
                });
                columns.AddColumn(column =>
                {
                    column.PropertyName<Uredaj>(x => x.SifraVrsteUredaja);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(3);
                    column.Width(1);
                    column.HeaderCell("Sifra vrste", horizontalAlignment: HorizontalAlignment.Center);
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
        public async Task<IActionResult> UredajiExcel()
        {
            var data = await ctx.Uredaj.Select(m => new Uredaj
            {
                Status=m.Status,
                SifraObjekta = m.SifraObjekta,
                SifraVrsteUredaja = m.SifraVrsteUredaja
            })
                                  .AsNoTracking()
                                  .OrderBy(d => d.SifraUredaja)
                                  .ToListAsync();
            var excel = ExcelCreator.CreateExcel<Uredaj>(data, "uredaji.xslsx");
            var content = excel.GetAsByteArray();
            return File(content, ExcelContentType, "uredaji.xlsx");
        }
        /// <summary>
        /// Metoda za generaciju master-detail excel datoteke.
        /// </summary>
        /// <returns>Vraća excel datoteku.</returns>
        public async Task<IActionResult> UredajiExcelMaster()
        {
            var masterData = await ctx.Uredaj.Select(m => new Uredaj
            {
                SifraUredaja = m.SifraUredaja,
            })
                                  .AsNoTracking()
                                  .OrderBy(d => d.SifraUredaja)
                                  .ToListAsync();
            byte[] content;
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Properties.Title = "Master-detail kucice";
                excel.Workbook.Properties.Author = "Autoceste";
                var worksheet = excel.Workbook.Worksheets.Add("MD-Uredaj");
                //First add the headers
                int i = 1;
                foreach (var data in masterData)
                {
                    worksheet.Cells[i, 1].Value = "Master ID:";
                    worksheet.Cells[i, 2].Value = data.SifraUredaja;
                    i++;
                    worksheet.Cells[i, 1].Value = "Details:";
                    i++;
                    var detailData = ctx.Scenarij
                     .AsNoTracking()
                     .Select(m => new Scenarij
                     {
                         NazivScenarija=m.NazivScenarija,
                         Procedura=m.Procedura,
                         SifraVrsteObjekta=m.SifraVrsteObjekta,
                         SifraVrsteScenarija=m.SifraVrsteScenarija
                     })
                     .Where(n => n.SifraScenarija == data.SifraUredaja)
                     .ToList();
                    if (detailData.Count == 0)
                    {
                        worksheet.Cells[i, 1].Value = "Nema detalja za prikazati";
                        i++;
                    }
                    else
                    {
                        worksheet.Cells[i, 1].Value = "scenarij";
                        worksheet.Cells[i, 2].Value = "procedura";
                        worksheet.Cells[i, 3].Value = "sifra vrste";
                        worksheet.Cells[i, 4].Value = "sifra vrste objekta";
                        i++;
                    }
                    foreach (var detail in detailData)
                    {
                        worksheet.Cells[i, 1].Value = detail.NazivScenarija;
                        worksheet.Cells[i, 2].Value = detail.Procedura;
                        worksheet.Cells[i, 3].Value = detail.SifraVrsteScenarija;
                        worksheet.Cells[i, 4].Value = detail.SifraVrsteObjekta;

                        i++;
                    }
                    i++;
                }
                worksheet.Cells[1, 1, i + 1, 7].AutoFitColumns();
                content = excel.GetAsByteArray();
            }
            return File(content, ExcelContentType, "MD-Uredaji.xlsx");
        }
        #endregion

        #region Dogadaji
        /// <summary>
        /// Metoda za generaciju jednostavnog tabličnog prikaza za pdf datoteke.
        /// </summary>
        /// <returns>Vraća pdf datoteku.</returns>
        public async Task<IActionResult> DogadajiPDF()
        {
            string naslov = "Popis dogadaja";
            var dogadaji = await ctx.Dogadaj.Select(m => new Dogadaj
            {
                SifraDogadaj=m.SifraDogadaj,
                DatumVrijeme=m.DatumVrijeme,
                Link=m.Link,
                SifraRazinaOpasnosti=m.SifraRazinaOpasnosti

            })
                                  .AsNoTracking()
                                  .OrderBy(d => d.Link)
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
            report.MainTableDataSource(dataSource => dataSource.StronglyTypedList(dogadaji));
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
                    column.PropertyName(nameof(Dogadaj.SifraDogadaj));
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(1);
                    column.Width(2);
                    column.HeaderCell("Sifra dogadaja");
                });
                columns.AddColumn(column =>
                {
                    column.PropertyName<Dogadaj>(x => x.DatumVrijeme);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(2);
                    column.Width(3);
                    column.HeaderCell("datum i vrijeme", horizontalAlignment: HorizontalAlignment.Center);
                });
                columns.AddColumn(column =>
                {
                    column.PropertyName<Dogadaj>(x => x.Link);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(3);
                    column.Width(1);
                    column.HeaderCell("Link", horizontalAlignment: HorizontalAlignment.Center);
                });
                columns.AddColumn(column =>
                {
                    column.PropertyName<Dogadaj>(x => x.SifraRazinaOpasnosti);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(3);
                    column.Width(1);
                    column.HeaderCell("razina opasnosti", horizontalAlignment: HorizontalAlignment.Center);
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
        public async Task<IActionResult> DogadajiExcel()
        {
            var data = await ctx.Dogadaj.Select(m => new Dogadaj
            {
                SifraDogadaj = m.SifraDogadaj,
                DatumVrijeme = m.DatumVrijeme,
                Link = m.Link,
                SifraRazinaOpasnosti = m.SifraRazinaOpasnosti,
                Opis = m.Opis
            })
                                  .AsNoTracking()
                                  .OrderBy(d => d.SifraDogadaj)
                                  .ToListAsync();
            var excel = ExcelCreator.CreateExcel<Dogadaj>(data, "dogadaji.xslsx");
            var content = excel.GetAsByteArray();
            return File(content, ExcelContentType, "dogadaji.xlsx");
        }
        /// <summary>
        /// Metoda za generaciju master-detail excel datoteke.
        /// </summary>
        /// <returns>Vraća excel datoteku.</returns>
        public async Task<IActionResult> DogadajiExcelMaster()
        {
            var masterData = await ctx.Dogadaj.Select(m => new Dogadaj
            {
                SifraDogadaj = m.SifraDogadaj,
            })
                                  .AsNoTracking()
                                  .OrderBy(d => d.SifraDogadaj)
                                  .ToListAsync();
            byte[] content;
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Properties.Title = "Master-detail";
                excel.Workbook.Properties.Author = "Autoceste";
                var worksheet = excel.Workbook.Worksheets.Add("MD");
                //First add the headers
                int i = 1;
                foreach (var data in masterData)
                {
                    worksheet.Cells[i, 1].Value = "Master ID:";
                    worksheet.Cells[i, 2].Value = data.SifraDogadaj;
                    i++;
                    worksheet.Cells[i, 1].Value = "Details:";
                    i++;
                    var detailData = ctx.Stanje
                     .AsNoTracking()
                     .Select(m => new Stanje
                     {
                         VrijemePocetka=m.VrijemePocetka,
                         VrijemeZavrsetka=m.VrijemeZavrsetka,
                         Opis=m.Opis
                     })
                     .Where(n => n.SifraDogadaj == data.SifraDogadaj)
                     .ToList();
                    if (detailData.Count == 0)
                    {
                        worksheet.Cells[i, 1].Value = "Nema detalja za prikazati";
                        i++;
                    }
                    else
                    {
                        worksheet.Cells[i, 1].Value = "vrijeme pocetka";
                        worksheet.Cells[i, 2].Value = "vrijeme zavrsetka";
                        worksheet.Cells[i, 3].Value = "opis";
                        i++;
                    }
                    foreach (var detail in detailData)
                    {
                        worksheet.Cells[i, 1].Value = detail.VrijemePocetka;
                        worksheet.Cells[i, 2].Value = detail.VrijemeZavrsetka;
                        worksheet.Cells[i, 3].Value = detail.Opis;

                        i++;
                    }
                    i++;
                }
                worksheet.Cells[1, 1, i + 1, 7].AutoFitColumns();
                content = excel.GetAsByteArray();
            }
            return File(content, ExcelContentType, "MD-Dogadaji.xlsx");
        }
        #endregion
    }
}

