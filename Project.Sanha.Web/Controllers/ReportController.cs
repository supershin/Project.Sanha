using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Project.Sanha.Web.Models;
using Project.Sanha.Web.Repositories;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System;
using System.Reflection.PortableExecutable;
using System.Transactions;

namespace Project.Sanha.Web.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IApprove _approve;
        private readonly IHostEnvironment _hosting;

        public ReportController(IApprove approve,
            IHostEnvironment hostEnvironment)
        {
            _approve = approve;
            _hosting = hostEnvironment;
        }


        public IActionResult RptShopService(int transId, int juristicId)
        {
            string path = GetDataTransShopService(transId);

            ReportReturnModel returnData = new ReportReturnModel()
            {
                Path = path,
                JuristicID = juristicId
            };

            if (!string.IsNullOrEmpty(path))
            {
                return Json(
                          new
                          {
                              success = true,
                              data = returnData,
                          }
                );
            }
            else
            {
                return Json(
                          new
                          {
                              success = false,
                              data = "",
                          }
                );
            }
            
        }
        public string GetDataTransShopService(int transId)
        {
            var guid = Guid.NewGuid();
            TransactionOptions option = new TransactionOptions();
            option.Timeout = new TimeSpan(1, 0, 0);
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, option))
            {
                try
                {
                    ReportDetailForApprove reportDetail = _approve.DetailGenReport(transId);

                    Report1Model report1 = new Report1Model()
                    {
                        ShopName = reportDetail.ShopName,
                        OrderNO = reportDetail.OrderNO,
                        ProjectName = reportDetail.ProjectName,
                        AddrNO = reportDetail.AddrNo,
                        TransferDate = reportDetail.TransferDate,
                        UsedQuota = (int)reportDetail.UsedQuota,
                        Status = (int)reportDetail.Status,
                        StatusDesc = reportDetail.StatusDesc,
                        CustomerName = reportDetail.CustomerDetail.CustomerName,
                        CustomerMobile = reportDetail.CustomerDetail.CustomerMobile,
                        CustomerEmail = reportDetail.CustomerDetail.CustomerEmail,
                        RelationShip = reportDetail.CustomerDetail.RelationShip,
                        ImageSignCustomer = reportDetail.CustomerDetail.ImageSignCustomer,
                        DateSignCustomer = reportDetail.CustomerDetail.DateSignCustomer,
                        StaffName = reportDetail.StaffDetail.StaffName,
                        StaffMobile = reportDetail.StaffDetail.StaffMobile,
                        WorkDate = reportDetail.StaffDetail.WorkDate,
                        WorkTime = reportDetail.StaffDetail.WorkTime,
                        Remark = reportDetail.StaffDetail.Remark,
                        ImageSignStaff = reportDetail.StaffDetail.ImageSignStaff,
                        DateSignStaff = reportDetail.StaffDetail.DateSignStaff

                    };

                    Report2Model report2 = new Report2Model()
                    {
                        ShopName = reportDetail.ShopName,
                        OrderNO = reportDetail.OrderNO,
                        Images = reportDetail.Images.Select(image => image.ImagePath).ToList(),
                        ImageCheckIn = reportDetail.ImagesCheckIn.Select(image => image.ImageCIPath).ToList(),
                    };

                    getReport1(guid, report1);
                    getReport2(guid, report2);

                    string path = MegreMyPdfs(guid, reportDetail.OrderNO);

                    _approve.SaveFilePDF(guid, transId, report1.OrderNO, path);

                    scope.Complete();
                    //service add resource  
                    return path;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    scope.Dispose();
                }
            }
        }
        public void getReport1(Guid uuid, Report1Model report1)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var fontPath = _hosting.ContentRootPath + "/wwwroot/lib/fonts/BrowalliaUPC.ttf";
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
                                col.Item().AlignCenter().PaddingBottom(16).Text("บันทึกการบริการ " + report1.ShopName).FontSize(22).FontColor("#0000FF").Bold();

                            });

                        });
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Column(row1 =>
                            {
                                row.RelativeItem(12).AlignRight().Text("เลขที่เอกสาร : "+report1.OrderNO).FontSize(18).SemiBold();

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
                            table.Cell().Row(1).Column(2).Padding(2).PaddingLeft(4).Text(report1.ProjectName);
                            table.Cell().Row(2).Column(1).Padding(2).PaddingLeft(4).Text("เลขที่บ้าน");
                            table.Cell().Row(2).Column(2).Padding(2).PaddingLeft(4).Text(report1.AddrNO);
                            table.Cell().Row(3).Column(1).Padding(2).PaddingLeft(4).Text("วันที่โอน");
                            table.Cell().Row(3).Column(2).Padding(2).PaddingLeft(4).Text(report1.TransferDate);
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
                            table.Cell().Row(1).Column(2).Padding(2).PaddingLeft(4).Text(report1.CustomerName);
                            table.Cell().Row(2).Column(1).Padding(2).PaddingLeft(4).Text("เบอร์โทรศัพท์");
                            table.Cell().Row(2).Column(2).Padding(2).PaddingLeft(4).Text(report1.CustomerMobile);
                            table.Cell().Row(3).Column(1).Padding(2).PaddingLeft(4).Text("อีเมล");
                            table.Cell().Row(3).Column(2).Padding(2).PaddingLeft(4).Text(report1.CustomerEmail);
                            table.Cell().Row(4).Column(1).Padding(2).PaddingLeft(4).Text("ความสัมพันธ์") ;
                            table.Cell().Row(4).Column(2).Padding(2).PaddingLeft(4).Text(report1.RelationShip);
                            table.Cell().Row(5).Column(1).Padding(2).PaddingLeft(4).Text("จำนวนสิทธิ์ที่ใช้");
                            table.Cell().Row(5).Column(2).Padding(2).PaddingLeft(4).Text(report1.UsedQuota.ToString());

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
                            table.Cell().Row(1).Column(2).Padding(2).PaddingLeft(4).Text(report1.StaffName);
                            table.Cell().Row(2).Column(1).Padding(2).PaddingLeft(4).Text("เบอร์โทรศัพท์");
                            table.Cell().Row(2).Column(2).Padding(2).PaddingLeft(4).Text(report1.StaffMobile);
                            table.Cell().Row(3).Column(1).Padding(2).PaddingLeft(4).Text("วันที่เข้าทำงาน");
                            table.Cell().Row(3).Column(2).Padding(2).PaddingLeft(4).Text(report1.WorkDate);
                            table.Cell().Row(4).Column(1).Padding(2).PaddingLeft(4).Text("เวลาเข้าทำงาน");
                            table.Cell().Row(4).Column(2).Padding(2).PaddingLeft(4).Text(report1.WorkTime+" น.");
                            table.Cell().Row(5).Column(1).Padding(2).PaddingLeft(4).Text("หมายเหตุ");
                            table.Cell().Row(5).Column(2).Padding(2).PaddingLeft(4).Text(report1.Remark);
                        });
                        col1.Item().PaddingTop(20).LineHorizontal(1).LineColor(Colors.Grey.Medium);

                    });

                    page.Footer().Border(0.5f).Table(table2 =>
                    {

                        var sign_open = new FileStream(_hosting.ContentRootPath + "/" + report1.ImageSignCustomer, FileMode.Open);
                        var sign_open2 = new FileStream(_hosting.ContentRootPath + "/" + report1.ImageSignStaff, FileMode.Open);

                        table2.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(6);
                            columns.RelativeColumn(6);

                        });
                        table2.Cell().Row(1).Column(1).AlignCenter().PaddingTop(4).PaddingBottom(4).Text("เจ้าหน้าที่ ").SemiBold();
                        table2.Cell().Row(2).Column(1).AlignCenter().Width(60).Image(sign_open);
                        table2.Cell().Row(3).Column(1).AlignCenter().Text(report1.StaffName);
                        table2.Cell().Row(4).Column(1).AlignCenter().Text("วันที่ " + report1.DateSignStaff).Style(TextStyle.Default.FontSize(16));

                        table2.Cell().Row(1).Column(2).AlignCenter().PaddingTop(4).PaddingBottom(4).Text("ลูกค้า").SemiBold();
                        table2.Cell().Row(2).Column(2).AlignCenter().Width(60).Image(sign_open2);
                        table2.Cell().Row(3).Column(2).AlignCenter().Text(report1.CustomerName);
                        table2.Cell().Row(4).Column(2).AlignCenter().Text("วันที่ " + report1.DateSignCustomer).Style(TextStyle.Default.FontSize(16));
                    });


                });
            });
            document.GeneratePdf("Upload/temp/" + report1.OrderNO + "-" + uuid + "sector-1.pdf");
            //document.ShowInPreviewer();

        }

        public void getReport2(Guid uuid, Report2Model report2)
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
                                col.Item().AlignCenter().PaddingBottom(16).Text("บันทึกการบริการ " + report2.ShopName ).FontSize(22).FontColor("#0000FF").Bold();

                            });

                        });
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Column(row1 =>
                            {
                                row.RelativeItem(12).AlignRight().Text("เลขที่เอกสาร : "+report2.OrderNO).FontSize(18).SemiBold();

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

                           grid.Item(12).Text("รูปภาพการเช็คอิน").FontSize(18).Bold().Underline();

                           for (int i = 0; i < report2.ImageCheckIn.Count; i++)
                           {
                               //var imgPath = Directory.GetCurrentDirectory() + "/images/works" + i + ".jpg";
                               var imgPath = _hosting.ContentRootPath + "/" + report2.ImageCheckIn[i];
                               if (System.IO.File.Exists(imgPath))
                               {
                                   using var img = new FileStream(imgPath, FileMode.Open);

                                   grid.Item(6).Border(0.5f).Width(250).Image(img);
                               }
                           }

                           grid.Item(12).Text("รูปภาพประกอบผลงาน").FontSize(18).Bold().Underline();

                           for (int i = 0; i < report2.Images.Count ; i++)
                           {
                               //var imgPath = Directory.GetCurrentDirectory() + "/wwwroot/upload_data/defect/" + "";
                               var imgPath = _hosting.ContentRootPath + "/" + report2.Images[i];
                               if (System.IO.File.Exists(imgPath))
                               {
                                   using var img = new FileStream(imgPath, FileMode.Open);

                                   grid.Item(6).Border(0.5f).Width(250).Image(img);
                               }
                           }

                           
                       });
                });
            });

            document.GeneratePdf("Upload/temp/" + report2.OrderNO + "-" + uuid + "sector-2.pdf");
            // document.ShowInPreviewer();

        }

        public string MegreMyPdfs(Guid uuid, string orderNo)
        {
            string pdfFileLocation = _hosting.ContentRootPath + "/Upload/temp";
            int numberOfPdfs = 2;
            //string outFile = Directory.GetCurrentDirectory() + "\\wwwroot\\upload_data\\document\\file-" + complain_no + "-" + uuid + ".pdf";
            string outFile = _hosting.ContentRootPath + "/Upload/document/file-" + orderNo + "-" + uuid + ".pdf";
            Merge(pdfFileLocation, numberOfPdfs, outFile, orderNo, uuid.ToString(), false);

            
            return "Upload/document/file-" + orderNo + "-" + uuid + ".pdf";
        }

        public static void Merge(string pdfFileLocation, int numberOfPdfs, string outFile, string complain_no, string uuid, bool zeroIndex = true)
        {
            List<string> pdfs = new List<string>();
            pdfs.Add(pdfFileLocation + "/" + complain_no + "-" + uuid + "sector-1.pdf");
            if (System.IO.File.Exists(pdfFileLocation + "/" + complain_no + "-" + uuid + "sector-2.pdf"))
            {
                pdfs.Add(pdfFileLocation + "/" + complain_no + "-" + uuid + "sector-2.pdf");
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

        public JsonResult GetPathPDF(int transID)
        {
            try
            {
                if (transID == null) throw new Exception();

                string path = _approve.GetPathPDF(transID);

                return Json(
                          new
                          {
                              success = true,
                              data = path,
                          }
                );
            }
            catch (Exception ex)
            {
                return Json(
                            new
                            {
                                success = false,
                                message = ex.Message,
                                data = new[] { ex.Message },
                            }
               );
            }
        }
    }
}
