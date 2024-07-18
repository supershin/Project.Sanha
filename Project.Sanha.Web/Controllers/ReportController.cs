using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Reflection.PortableExecutable;

namespace Project.Sanha.Web.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult RptShopService()
        {
            GetDataTransShopService();
            return View();
        }
        private void GetDataTransShopService()
        {
            var guid = Guid.NewGuid();
            var order_no = "12345";
            //getReport1();
            getReport2(guid,order_no);
        }
        public void getReport1(Guid uuid, string order_no)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var fontPath = Directory.GetCurrentDirectory() + "/wwwroot/lib/fonts/BrowalliaUPC.ttf";
            //var fontPath = Directory.GetCurrentDirectory() + "/fonts/browaub.ttf";

            FontManager.RegisterFont(System.IO.File.OpenRead(fontPath));

            var document = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.MarginTop(1.5f, Unit.Centimetre);
                    page.MarginBottom(1.5f, Unit.Centimetre);
                    page.MarginLeft(1, Unit.Centimetre);
                    page.MarginRight(1, Unit.Centimetre);

                    page.PageColor(QuestPDF.Helpers.Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(16));
                    page.PageColor(QuestPDF.Helpers.Colors.White);
                    page.DefaultTextStyle(TextStyle
                               .Default
                               .FontFamily("BrowalliaUPC")
                               .FontSize(16));

                    //var titleStyle = TextStyle.Default.FontSize(22).SemiBold().FontColor(Colors.Blue.Darken4).FontFamily("BrowalliaUPC");

                    page.Header().Column(column =>
                    {
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().AlignCenter().PaddingBottom(16).Text("บันทึกการบริการ E-Voucher ล้างแอร์ และทำความสะอาด").FontSize(22).FontColor("#0000FF").Bold();

                            });

                        });
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Column(row1 =>
                            {
                                row.RelativeItem(12).AlignRight().Text("เลขที่เอกสาร : 50240712345").FontSize(18).SemiBold();

                            });
                        });
                    });


                    page.Content().PaddingVertical(4).Column(col1 =>
                    {
                        col1.Item().Text(" ข้อมูลการใช้บริการ  ").FontColor("#FFFFFF").FontSize(18).SemiBold().BackgroundColor("#3b5998");
                        col1.Item().LineHorizontal(1).LineColor(Colors.Grey.Medium);

                        col1.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(10);
                            });
                            table.Cell().Row(1).Column(1).Padding(2).PaddingLeft(4).Text("ชื่อโครงการ");
                            table.Cell().Row(1).Column(2).Padding(2).PaddingLeft(4).Text("โมดิช โรห์ม ฮิล");
                            table.Cell().Row(2).Column(1).Padding(2).PaddingLeft(4).Text("เลขที่บ้าน");
                            table.Cell().Row(2).Column(2).Padding(2).PaddingLeft(4).Text("239/95");
                            table.Cell().Row(3).Column(1).Padding(2).PaddingLeft(4).Text("วันที่โอน");
                            table.Cell().Row(3).Column(2).Padding(2).PaddingLeft(4).Text("01/01/2567");
                        });
                        //-----------------------------
                        col1.Item().PaddingTop(20).Text(" ข้อมูลลูกค้า              ").FontColor("#FFFFFF").FontSize(18).SemiBold().BackgroundColor("#3b5998");
                        col1.Item().LineHorizontal(1).LineColor(Colors.Grey.Medium);

                        col1.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(10);
                            });
                            table.Cell().Row(1).Column(1).Padding(2).PaddingLeft(4).Text("ชื่อ-นามสกุล");
                            table.Cell().Row(1).Column(2).Padding(2).PaddingLeft(4).Text("Siri Risi");
                            table.Cell().Row(2).Column(1).Padding(2).PaddingLeft(4).Text("เบอร์โทรศัพท์");
                            table.Cell().Row(2).Column(2).Padding(2).PaddingLeft(4).Text("0961159999");
                            table.Cell().Row(3).Column(1).Padding(2).PaddingLeft(4).Text("อีเมล");
                            table.Cell().Row(3).Column(2).Padding(2).PaddingLeft(4).Text("example@email.com");
                        });
                        //---------------------------------
                        col1.Item().PaddingTop(20).Text(" ข้อมูลการเจ้าหน้าที่ ").FontColor("#FFFFFF").FontSize(18).SemiBold().BackgroundColor("#3b5998");
                        col1.Item().LineHorizontal(1).LineColor(Colors.Grey.Medium);

                        col1.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(10);
                            });
                            table.Cell().Row(1).Column(1).Padding(2).PaddingLeft(4).Text("ชื่อ-นามสกุล");
                            table.Cell().Row(1).Column(2).Padding(2).PaddingLeft(4).Text("Tuanjit Tongkaew");
                            table.Cell().Row(2).Column(1).Padding(2).PaddingLeft(4).Text("วันที่เข้าทำงาน");
                            table.Cell().Row(2).Column(2).Padding(2).PaddingLeft(4).Text("01/01/2567");
                            table.Cell().Row(3).Column(1).Padding(2).PaddingLeft(4).Text("เวลาเข้าทำงาน");
                            table.Cell().Row(3).Column(2).Padding(2).PaddingLeft(4).Text("9.00 - 10.00 น.");
                            table.Cell().Row(4).Column(1).Padding(2).PaddingLeft(4).Text("หมายเหตุ");
                            table.Cell().Row(4).Column(2).Padding(2).PaddingLeft(4).Text("อุปกรณ์ขัดข้อง");
                        });
                        col1.Item().PaddingTop(20).LineHorizontal(1).LineColor(Colors.Grey.Medium);

                    });

                    page.Footer().Border(0.5f).Table(table2 =>
                    {

                        var sign_open = new FileStream(Directory.GetCurrentDirectory() + "/wwwroot/images/testImg/signatur1.jpg", FileMode.Open);
                        var sign_open2 = new FileStream(Directory.GetCurrentDirectory() + "/wwwroot/images/testImg/signatur2.jpg", FileMode.Open);

                        table2.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(6);
                            columns.RelativeColumn(6);

                        });
                        table2.Cell().Row(1).Column(1).AlignCenter().PaddingTop(4).PaddingBottom(4).Text("เจ้าหน้าที่ ").SemiBold();
                        table2.Cell().Row(2).Column(1).AlignCenter().Width(60).Image(sign_open);
                        table2.Cell().Row(3).Column(1).AlignCenter().Text("Tuanjit Tongkaew");
                        table2.Cell().Row(4).Column(1).AlignCenter().Text("วันที่ " + DateTime.Now.ToString("dd/MM/yyyy")).Style(TextStyle.Default.FontSize(16));

                        table2.Cell().Row(1).Column(2).AlignCenter().PaddingTop(4).PaddingBottom(4).Text("ลูกค้า").SemiBold();
                        table2.Cell().Row(2).Column(2).AlignCenter().Width(60).Image(sign_open2);
                        table2.Cell().Row(3).Column(2).AlignCenter().Text("Siri Risi");
                        table2.Cell().Row(4).Column(2).AlignCenter().Text("วันที่ " + DateTime.Now.ToString("dd/MM/yyyy")).Style(TextStyle.Default.FontSize(16));
                    });


                });
            });
            //document.GeneratePdf("temp/" + order_no + "-" + uuid + "sector-1.pdf");
             document.ShowInPreviewer();

        }

        public void getReport2(Guid uuid, string order_no)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var document = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.MarginTop(2, Unit.Centimetre);
                    page.MarginBottom(2, Unit.Centimetre);
                    page.MarginLeft(1, Unit.Centimetre);
                    page.MarginRight(1, Unit.Centimetre);
                    page.PageColor(QuestPDF.Helpers.Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(16));
                    page.PageColor(QuestPDF.Helpers.Colors.White);
                    page.DefaultTextStyle(TextStyle
                               .Default
                               .FontFamily("BrowalliaUPC")
                               .FontSize(16));

                    //var titleStyle = TextStyle.Default.FontSize(22).SemiBold().FontColor(Colors.Blue.Darken4).FontFamily("BrowalliaUPC");

                    page.Header().Column(column =>
                    {
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().AlignCenter().PaddingBottom(16).Text("บันทึกการบริการ E-Voucher ล้างแอร์ และทำความสะอาด").FontSize(22).FontColor("#0000FF").Bold();

                            });

                        });
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Column(row1 =>
                            {
                                row.RelativeItem(12).AlignRight().Text("เลขที่เอกสาร : 50240712345").FontSize(18).SemiBold();

                            });
                        });
                    });

                    page.Content()
                       .PaddingBottom(20)
                       .PaddingTop(20)
                       .AlignCenter()
                       .Grid(grid =>
                       {
                           grid.VerticalSpacing(15);
                           grid.HorizontalSpacing(15);
                           grid.AlignLeft();
                           grid.Columns(12);

                           grid.Item(12).Text("รูปภาพประกอบผลงาน").FontSize(18).Bold().Underline();

                           for (int i = 1; i < 5; i++)
                           {
                               //var imgPath = Directory.GetCurrentDirectory() + "/wwwroot/upload_data/defect/" + "";
                               var imgPath = Directory.GetCurrentDirectory() + "/wwwroot/images/testImg/work" + i + ".jpg";
                               if (System.IO.File.Exists(imgPath))
                               {
                                   using var img = new FileStream(imgPath, FileMode.Open);

                                   grid.Item(6).Border(0.5f).Image(img);
                               }
                           }
                       });
                });
            });

            document.GeneratePdf("temp/" + order_no + "-" + uuid + "sector-2.pdf");
            // document.ShowInPreviewer();

        }

        public void MegreMyPdfs(Guid uuid, string complain_no)
        {
            string pdfFileLocation = Directory.GetCurrentDirectory() + "\\temp\\";
            int numberOfPdfs = 2;
            //string outFile = Directory.GetCurrentDirectory() + "\\wwwroot\\upload_data\\document\\file-" + complain_no + "-" + uuid + ".pdf";
            string outFile = Directory.GetCurrentDirectory() + "\\Upload\\document\\file-" + complain_no + "-" + uuid + ".pdf";
            Merge(pdfFileLocation, numberOfPdfs, outFile, complain_no, uuid.ToString(), false);
        }

        public static void Merge(string pdfFileLocation, int numberOfPdfs, string outFile, string complain_no, string uuid, bool zeroIndex = true)
        {
            List<string> pdfs = new List<string>();
            pdfs.Add(pdfFileLocation + complain_no + "-" + uuid + "sector-1.pdf");
            if (System.IO.File.Exists(pdfFileLocation + complain_no + "-" + uuid + "sector-2.pdf"))
            {
                pdfs.Add(pdfFileLocation + complain_no + "-" + uuid + "sector-2.pdf");
            }
    
            CombineMultiplePdFs(pdfs, outFile);
        }
        public static void CombineMultiplePdFs(List<string> fileNames, string outFile)
        {
            // step 1: creation of a document-object
            iTextSharp.text.Document document = new iTextSharp.text.Document();

            FileStream fs = new FileStream(outFile, FileMode.Create);
            // step 2: we create a writer that listens to the document
            PdfCopy writer = new PdfCopy(document, fs);

            // step 3: we open the document
            document.Open();

            foreach (string fileName in fileNames)
            {
                // we create a reader for a certain document
                PdfReader reader = new PdfReader(fileName);
                reader.ConsolidateNamedDestinations();

                // step 4: we add content
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    PdfImportedPage page = writer.GetImportedPage(reader, i);
                    writer.AddPage(page);
                }

                PrAcroForm form = reader.AcroForm;
                if (form != null)
                {
                    //writer.CopyDocumentFields(reader);
                    writer.CopyAcroForm(reader);
                }

                reader.Close();
            }

            // step 5: we close the document and writer

            document.Close();
            writer.Close();
            fs.Close();
            fs.Dispose();

        }
    }
}
