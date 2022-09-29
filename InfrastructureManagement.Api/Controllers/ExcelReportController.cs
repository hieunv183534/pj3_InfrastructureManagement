using InfrastructureManagement.Core.Entities;
using InfrastructureManagement.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace InfrastructureManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelReportController : ControllerBase
    {
        private List<Category> categories = new List<Category>();
        private List<Item> items = new List<Item>();
        private List<Report> reports = new List<Report>();
        private List<Account> accounts = new List<Account>();

        public ExcelReportController(ICategoryService categoryService, IItemService itemService, IReportService reportService, IAccountService accountService)
        {
            this.accounts = (List<Account>)accountService.GetAll().Response.Data;
            this.categories = (List<Category>)categoryService.GetAll().Response.Data;
            this.items = (List<Item>)itemService.GetAll().Response.Data;
            this.reports = (List<Report>)reportService.GetAll().Response.Data;
        }

        [Authorize(Roles ="admin")]
        [HttpGet("overview")]
        public async Task<IActionResult> GetOverviewExcel()
        {

            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                //Đặt tên người tạo file
                package.Workbook.Properties.Author = "HieuNV";
                //Đặt tiêu đề cho file
                package.Workbook.Properties.Title = "Thống kê tổng quan cơ sở vật chất";
                // tạo sheet 
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                // tạo fontsize và fontfamily cho sheet
                workSheet.Cells.Style.Font.Size = 11;
                workSheet.Cells.Style.Font.Name = "Calibri";
                workSheet.Cells[1, 1, 50, 50].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                workSheet.Column(1).Width = 16;
                workSheet.Column(2).Width = 16;
                workSheet.Column(3).Width = 16;

                workSheet.Cells[1, 1].Value = "THỐNG KÊ TỔNG QUAN";
                workSheet.Cells[1, 1].Style.Font.Size = 16;
                workSheet.Cells[1, 1, 1, 3].Merge = true;
                workSheet.Cells[1, 1, 1, 3].Style.Font.Bold = true;
                workSheet.Cells[1, 1, 1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                workSheet.Cells[3, 1].Value = "Tiêu đề";
                workSheet.Cells[3, 1, 3, 2].Merge = true;
                workSheet.Cells[3, 3].Value = "Số lượng";

                workSheet.Cells[3, 1].Style.Font.Bold = true;
                workSheet.Cells[3, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[3, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D8D8D8"));
                workSheet.Cells[3, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[3, 1].Style.Border.Left.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#D4D4D4"));
                workSheet.Cells[3, 3].Style.Font.Bold = true;
                workSheet.Cells[3, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[3, 3].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D8D8D8"));


                workSheet.Cells[4, 1].Value = "Số danh mục";
                workSheet.Cells[4, 1, 4, 2].Merge = true;

                workSheet.Cells[5, 1].Value = "Số đối tượng";
                workSheet.Cells[5, 1, 5, 2].Merge = true;

                workSheet.Cells[6, 1, 10, 1].Merge = true;
                workSheet.Cells[6, 2].Value = "Đang sử dụng";
                workSheet.Cells[7, 2].Value = "Đang hỏng";
                workSheet.Cells[8, 2].Value = "Đang sửa chữa";
                workSheet.Cells[9, 2].Value = "Đang cất kho";
                workSheet.Cells[10, 2].Value = "Đã thanh lí";

                workSheet.Cells[11, 1].Value = "Số vấn đề xử lí";
                workSheet.Cells[11, 1, 11, 2].Merge = true;

                workSheet.Cells[12, 1, 16, 1].Merge = true;
                workSheet.Cells[12, 1].Value = "Báo hỏng";
                workSheet.Cells[12, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheet.Cells[17, 1, 21, 1].Merge = true;
                workSheet.Cells[17, 1].Value = "Báo thiếu";
                workSheet.Cells[17, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;



                workSheet.Cells[12, 2].Value = "Mới";
                workSheet.Cells[13, 2].Value = "Đã duyệt";
                workSheet.Cells[14, 2].Value = "Đã từ chối";
                workSheet.Cells[15, 2].Value = "Đang xử lí";
                workSheet.Cells[16, 2].Value = "Đã hoàn thành";
                workSheet.Cells[17, 2].Value = "Mới";
                workSheet.Cells[18, 2].Value = "Đã duyệt";
                workSheet.Cells[19, 2].Value = "Đã từ chối";
                workSheet.Cells[20, 2].Value = "Đang xử lí";
                workSheet.Cells[21, 2].Value = "Đã hoàn thành";

                workSheet.Cells[4, 3].Value = this.categories.Count;
                workSheet.Cells[5, 3].Value = this.items.Count;
                workSheet.Cells[6, 3].Value = this.items.Where(i=> i.Status == Core.Enums.ItemStatus.Using).ToList().Count;
                workSheet.Cells[7, 3].Value = this.items.Where(i => i.Status == Core.Enums.ItemStatus.Broken).ToList().Count;
                workSheet.Cells[8, 3].Value = this.items.Where(i => i.Status == Core.Enums.ItemStatus.UnderMaintenance).ToList().Count;
                workSheet.Cells[9, 3].Value = this.items.Where(i => i.Status == Core.Enums.ItemStatus.Storage).ToList().Count;
                workSheet.Cells[10, 3].Value = this.items.Where(i => i.Status == Core.Enums.ItemStatus.Liquidation).ToList().Count;
                workSheet.Cells[11, 3].Value = this.reports.Count;
                workSheet.Cells[12, 3].Value = this.reports.Where(r => r.Type == Core.Enums.ReportType.Broken && r.Status == Core.Enums.ReportStatus.New).ToList().Count;
                workSheet.Cells[13, 3].Value = this.reports.Where(r => r.Type == Core.Enums.ReportType.Broken && r.Status == Core.Enums.ReportStatus.Confirm).ToList().Count;
                workSheet.Cells[14, 3].Value = this.reports.Where(r => r.Type == Core.Enums.ReportType.Broken && r.Status == Core.Enums.ReportStatus.Reject).ToList().Count;
                workSheet.Cells[15, 3].Value = this.reports.Where(r => r.Type == Core.Enums.ReportType.Broken && r.Status == Core.Enums.ReportStatus.Doing).ToList().Count;
                workSheet.Cells[16, 3].Value = this.reports.Where(r => r.Type == Core.Enums.ReportType.Broken && r.Status == Core.Enums.ReportStatus.Complete).ToList().Count;
                workSheet.Cells[17, 3].Value = this.reports.Where(r => r.Type == Core.Enums.ReportType.Missing && r.Status == Core.Enums.ReportStatus.New).ToList().Count;
                workSheet.Cells[18, 3].Value = this.reports.Where(r => r.Type == Core.Enums.ReportType.Missing && r.Status == Core.Enums.ReportStatus.Confirm).ToList().Count;
                workSheet.Cells[19, 3].Value = this.reports.Where(r => r.Type == Core.Enums.ReportType.Missing && r.Status == Core.Enums.ReportStatus.Reject).ToList().Count;
                workSheet.Cells[20, 3].Value = this.reports.Where(r => r.Type == Core.Enums.ReportType.Missing && r.Status == Core.Enums.ReportStatus.Doing).ToList().Count;
                workSheet.Cells[21, 3].Value = this.reports.Where(r => r.Type == Core.Enums.ReportType.Missing && r.Status == Core.Enums.ReportStatus.Complete).ToList().Count;

                package.Save();
            }
            stream.Position = 0;
            string excelName = $"Overview-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            //return File(stream, "application/octet-stream", excelName);  
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("category")]
        public async Task<IActionResult> GetCategoryExcel()
        {

            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                //Đặt tên người tạo file
                package.Workbook.Properties.Author = "HieuNV";
                //Đặt tiêu đề cho file
                package.Workbook.Properties.Title = "Danh mục các loại đối tượng quản lí";
                // tạo sheet 
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                // tạo fontsize và fontfamily cho sheet
                workSheet.Cells.Style.Font.Size = 11;
                workSheet.Cells.Style.Font.Name = "Calibri";

                // danh sách các tên cột
                string[] arrColumnHeader = { "STT" , "Mã danh mục","Tên danh mục", "Số lượng đối tượng"};



                // merge các column lại từ column 1 đến số column header
                // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                workSheet.Cells[1, 1].Value = "DANH MỤC";
                workSheet.Cells[1, 1].Style.Font.Size = 16;
                workSheet.Cells[1, 1, 1, arrColumnHeader.Count()].Merge = true;
                // in đậm
                workSheet.Cells[1, 1, 1, arrColumnHeader.Count()].Style.Font.Bold = true;
                // căn giữa
                workSheet.Cells[1, 1, 1, arrColumnHeader.Count()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                workSheet.Cells[2, 1].Value = "";
                workSheet.Cells[2, 1].Style.Font.Size = 15;
                workSheet.Cells[2, 1, 2, arrColumnHeader.Count()].Merge = true;

                // Gán row header
                for (var i = 0; i < arrColumnHeader.Length; i++)
                {
                    workSheet.Cells[3, i + 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[3, i + 1].Style.Border.Left.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#D4D4D4"));
                    workSheet.Cells[3, i + 1].Value = arrColumnHeader[i];
                }
                // chỉnh style cho bảng
                workSheet.Row(3).Style.Font.Bold = true;
                workSheet.Row(3).Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Row(3).Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D8D8D8"));
                workSheet.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //Chỉnh độ rộng các cột 
                workSheet.Column(1).Width = 5;
                workSheet.Column(2).Width = 15;
                workSheet.Column(3).Width = 25;
                workSheet.Column(4).Width = 25;

                // Gán data list vào sheet
                var rowIndex = 4;
                foreach (var category in categories)
                {
                    workSheet.Cells[rowIndex, 1].Value = rowIndex - 3;
                    workSheet.Cells[rowIndex, 2].Value = category.Code;
                    workSheet.Cells[rowIndex, 3].Value = category.Name;
                    workSheet.Cells[rowIndex, 4].Value = this.items.Where(i => i.CategoryId == category.Id).ToList().Count;
                    rowIndex++;
                }
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"CategoryList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            //return File(stream, "application/octet-stream", excelName);  
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("item")]
        public async Task<IActionResult> GetItemExcel()
        {

            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                //Đặt tên người tạo file
                package.Workbook.Properties.Author = "HieuNV";
                //Đặt tiêu đề cho file
                package.Workbook.Properties.Title = "Danh sách các đối tượng cơ sở vật chất";
                // tạo sheet 
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                // tạo fontsize và fontfamily cho sheet
                workSheet.Cells.Style.Font.Size = 11;
                workSheet.Cells.Style.Font.Name = "Calibri";

                // danh sách các tên cột
                string[] arrColumnHeader = { "STT", "Mã đối tượng", "Tên đối tượng", "Danh mục", "Chỉ số chất lượng","Trạng thái"};



                // merge các column lại từ column 1 đến số column header
                // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                workSheet.Cells[1, 1].Value = "DANH SÁCH CƠ SỞ VẬT CHẤT";
                workSheet.Cells[1, 1].Style.Font.Size = 16;
                workSheet.Cells[1, 1, 1, arrColumnHeader.Count()].Merge = true;
                // in đậm
                workSheet.Cells[1, 1, 1, arrColumnHeader.Count()].Style.Font.Bold = true;
                // căn giữa
                workSheet.Cells[1, 1, 1, arrColumnHeader.Count()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                workSheet.Cells[2, 1].Value = "";
                workSheet.Cells[2, 1].Style.Font.Size = 15;
                workSheet.Cells[2, 1, 2, arrColumnHeader.Count()].Merge = true;

                // Gán row header
                for (var i = 0; i < arrColumnHeader.Length; i++)
                {
                    workSheet.Cells[3, i + 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[3, i + 1].Style.Border.Left.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#D4D4D4"));
                    workSheet.Cells[3, i + 1].Value = arrColumnHeader[i];
                }
                // chỉnh style cho bảng
                workSheet.Row(3).Style.Font.Bold = true;
                workSheet.Row(3).Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Row(3).Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D8D8D8"));
                workSheet.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //Chỉnh độ rộng các cột 
                workSheet.Column(1).Width = 5;
                workSheet.Column(2).Width = 20;
                workSheet.Column(3).Width = 25;
                workSheet.Column(4).Width = 25;
                workSheet.Column(5).Width = 15;
                workSheet.Column(6).Width = 25;

                // Gán data list vào sheet
                var rowIndex = 4;
                foreach (var item in items)
                {
                    workSheet.Cells[rowIndex, 1].Value = rowIndex - 3;
                    workSheet.Cells[rowIndex, 2].Value = item.Code;
                    workSheet.Cells[rowIndex, 3].Value = item.Name;
                    workSheet.Cells[rowIndex, 4].Value = item.Category.Name;
                    workSheet.Cells[rowIndex, 5].Value = item.QualityScore;
                    workSheet.Cells[rowIndex, 6].Value = item.Status.ToString();
                    rowIndex++;
                }
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"ItemList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            //return File(stream, "application/octet-stream", excelName);  
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("report")]
        public async Task<IActionResult> GetProblemExcel()
        {

            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                //Đặt tên người tạo file
                package.Workbook.Properties.Author = "HieuNV";
                //Đặt tiêu đề cho file
                package.Workbook.Properties.Title = "Danh sách các vấn đề báo cáo";

                #region sheetBaohong

                // tạo sheet 
                var workSheet = package.Workbook.Worksheets.Add("Báo hỏng");
                // tạo fontsize và fontfamily cho sheet
                workSheet.Cells.Style.Font.Size = 11;
                workSheet.Cells.Style.Font.Name = "Calibri";

                // danh sách các tên cột
                string[] arrColumnHeader = { "STT", "Mã đối tượng", "Tên đối tượng", "Người báo cáo","Thời gian báo cáo", "Nội dung đính kèm", "Phản hồi", "Trạng thái" };



                // merge các column lại từ column 1 đến số column header
                // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                workSheet.Cells[1, 1].Value = "DANH SÁCH BÁO HỎNG";
                workSheet.Cells[1, 1].Style.Font.Size = 16;
                workSheet.Cells[1, 1, 1, arrColumnHeader.Count()].Merge = true;
                // in đậm
                workSheet.Cells[1, 1, 1, arrColumnHeader.Count()].Style.Font.Bold = true;
                // căn giữa
                workSheet.Cells[1, 1, 1, arrColumnHeader.Count()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                workSheet.Cells[2, 1].Value = "";
                workSheet.Cells[2, 1].Style.Font.Size = 15;
                workSheet.Cells[2, 1, 2, arrColumnHeader.Count()].Merge = true;

                // Gán row header
                for (var i = 0; i < arrColumnHeader.Length; i++)
                {
                    workSheet.Cells[3, i + 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[3, i + 1].Style.Border.Left.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#D4D4D4"));
                    workSheet.Cells[3, i + 1].Value = arrColumnHeader[i];
                }
                // chỉnh style cho bảng
                workSheet.Row(3).Style.Font.Bold = true;
                workSheet.Row(3).Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Row(3).Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D8D8D8"));
                workSheet.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //Chỉnh độ rộng các cột 
                workSheet.Column(1).Width = 5;
                workSheet.Column(2).Width = 15;
                workSheet.Column(3).Width = 25;
                workSheet.Column(4).Width = 20;
                workSheet.Column(5).Width = 25;
                workSheet.Column(6).Width = 50;
                workSheet.Column(7).Width = 40;
                workSheet.Column(8).Width = 25;

                // Gán data list vào sheet
                var rowIndex = 4;
                var baoHongs = this.reports.Where(r => r.Type == Core.Enums.ReportType.Broken);
                foreach (var report in baoHongs)
                {
                    workSheet.Cells[rowIndex, 1].Value = rowIndex - 3;
                    workSheet.Cells[rowIndex, 2].Value = report.PositionItem.Code;
                    workSheet.Cells[rowIndex, 3].Value = report.PositionItem.Name;
                    workSheet.Cells[rowIndex, 4].Value = report.Reporter.Username;
                    workSheet.Cells[rowIndex, 5].Value = report.CreatedAt.ToString();
                    workSheet.Cells[rowIndex, 6].Value = report.Note;
                    workSheet.Cells[rowIndex, 7].Value = report.Reply;
                    workSheet.Cells[rowIndex, 8].Value = report.Status.ToString();
                    rowIndex++;
                }

                #endregion

                #region sheetBaoThieu

                // tạo sheet 
                var workSheet1 = package.Workbook.Worksheets.Add("Báo thiếu");
                // tạo fontsize và fontfamily cho sheet
                workSheet1.Cells.Style.Font.Size = 11;
                workSheet1.Cells.Style.Font.Name = "Calibri";

                // danh sách các tên cột
                string[] arrColumnHeader1 = { "STT", "Mã đối tượng", "Tên đối tượng","Cần đối tượng loại","Số lượng cần", "Người báo cáo", "Thời gian báo cáo", "Nội dung đính kèm", "Phản hồi", "Trạng thái" };



                // merge các column lại từ column 1 đến số column header
                // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                workSheet1.Cells[1, 1].Value = "DANH SÁCH BÁO THIẾU, CẦN CSVC";
                workSheet1.Cells[1, 1].Style.Font.Size = 16;
                workSheet1.Cells[1, 1, 1, arrColumnHeader1.Count()].Merge = true;
                // in đậm
                workSheet1.Cells[1, 1, 1, arrColumnHeader1.Count()].Style.Font.Bold = true;
                // căn giữa
                workSheet1.Cells[1, 1, 1, arrColumnHeader1.Count()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                workSheet1.Cells[2, 1].Value = "";
                workSheet1.Cells[2, 1].Style.Font.Size = 15;
                workSheet1.Cells[2, 1, 2, arrColumnHeader1.Count()].Merge = true;

                // Gán row header
                for (var i = 0; i < arrColumnHeader1.Length; i++)
                {
                    workSheet1.Cells[3, i + 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet1.Cells[3, i + 1].Style.Border.Left.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#D4D4D4"));
                    workSheet1.Cells[3, i + 1].Value = arrColumnHeader1[i];
                }
                // chỉnh style cho bảng
                workSheet1.Row(3).Style.Font.Bold = true;
                workSheet1.Row(3).Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet1.Row(3).Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D8D8D8"));
                workSheet1.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //Chỉnh độ rộng các cột 
                workSheet1.Column(1).Width = 5;
                workSheet1.Column(2).Width = 15;
                workSheet1.Column(3).Width = 25;
                workSheet1.Column(4).Width = 25;
                workSheet1.Column(5).Width = 20;
                workSheet1.Column(6).Width = 20;
                workSheet1.Column(7).Width = 25;
                workSheet1.Column(8).Width = 50;
                workSheet1.Column(9).Width = 40;
                workSheet1.Column(10).Width = 20;

                // Gán data list vào sheet
                var rowIndex1 = 4;
                var baoThieus = this.reports.Where(r => r.Type == Core.Enums.ReportType.Missing);
                foreach (var report in baoThieus)
                {
                    workSheet1.Cells[rowIndex1, 1].Value = rowIndex1 - 3;
                    workSheet1.Cells[rowIndex1, 2].Value = report.PositionItem.Code;
                    workSheet1.Cells[rowIndex1, 3].Value = report.PositionItem.Name;
                    workSheet1.Cells[rowIndex1, 4].Value = report.Category.Name;
                    workSheet1.Cells[rowIndex1, 5].Value = report.Quantity;
                    workSheet1.Cells[rowIndex1, 6].Value = report.Reporter.Username;
                    workSheet1.Cells[rowIndex1, 7].Value = report.CreatedAt.ToString();
                    workSheet1.Cells[rowIndex1, 8].Value = report.Note;
                    workSheet1.Cells[rowIndex1, 9].Value = report.Reply;
                    workSheet1.Cells[rowIndex1, 10].Value = report.Status.ToString();
                    rowIndex1++;
                }

                #endregion

                package.Save();
            }
            stream.Position = 0;
            string excelName = $"ReportList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            //return File(stream, "application/octet-stream", excelName);  
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
