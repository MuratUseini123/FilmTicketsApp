using ClosedXML.Excel;
using ExcelDataReader;
using FilmTheater.Domain.DomainModels;
using FilmTheater.Domain.identity;
using GemBox.Document;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Film_Theater_191546.Controllers
{
    public class OrderController : Controller
    {
        public OrderController()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        public IActionResult Index()
        {
            HttpClient client = new HttpClient();

            string URL = "https://localhost:44376/api/admin/GetAllActiveOrders";

            HttpResponseMessage responseMessage = client.GetAsync(URL).Result;


            var data = responseMessage.Content.ReadAsAsync<List<TicketOrder>>().Result;

            return View(data);
        }





        public IActionResult GetTicketDetails(int id)
        {

            HttpClient client = new HttpClient();

            string URL = "https://localhost:44376/api/admin/GetOrderDetails";


            var model = new
            {
                Id = id,

            };


            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = client.PostAsync(URL, content).Result;

            var data = responseMessage.Content.ReadAsAsync<TicketOrder>().Result;

            return View(data);
        }

        public ActionResult SaveTicketOrderPDF(int id)
        {
            HttpClient client = new HttpClient();

            string URL = "https://localhost:44376/api/admin/GetOrderDetails";


            var model = new
            {
                Id = id,

            };


            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");

            HttpResponseMessage responseMessage = client.PostAsync(URL, content).Result;

            var data = responseMessage.Content.ReadAsAsync<TicketOrder>().Result;


            var document = DocumentModel.Load(templatePath);

            document.Content.Replace("{{TicketOrderNumber}}", data.id.ToString());
            document.Content.Replace("{{FilmTheaterUser}}", (data.OrderedBy.Name + " " + data.OrderedBy.Surname));
            document.Content.Replace("{{FilmTheaterUserEmail}}", data.OrderedBy.Email);

            StringBuilder sb = new StringBuilder();

            var total = 0.0;

            foreach (var item in data.FilmTicketsInOrder)
            {
                total += item.Quantity * item.FilmTicket.FilmTicketPrice;
                sb.AppendLine(item.FilmTicket.FilmName + " with quantity of: " + item.Quantity + " and price of: $" + item.FilmTicket.FilmTicketPrice);
            }

            document.Content.Replace("{{AllTicketsInOrder}}", sb.ToString());
            document.Content.Replace("{{TotalPrice}}", "$" + total.ToString());

            var stream = new MemoryStream();

            document.Save(stream, new PdfSaveOptions());

            string outputFileName = "OrderId" + "[" + data.id.ToString() + "]" + data.OrderedBy.UserName + "ExportInvoice.pdf";

            return File(stream.ToArray(), new PdfSaveOptions().ContentType, outputFileName);

        }


        public ActionResult ExportSpecificTicket()
        {
            return View();
        }

        [HttpPost]
        public FileContentResult ExportSpecificTicketConfirm()
        {
            var request = Request.Form["genreSelected"];
           
            string fileName = "All["+request+"]FilmTickets.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("All Tickets");

                worksheet.Cell(1, 1).Value = "Film Ticket id";
                worksheet.Cell(1, 2).Value = "Film Name";
                worksheet.Cell(1, 3).Value = "Film Poster Url";
                worksheet.Cell(1, 4).Value = "Film Description";
                worksheet.Cell(1, 5).Value = "Film Genre";
                worksheet.Cell(1, 6).Value = "Film Ticket Price";
                worksheet.Cell(1, 7).Value = "Film Rating";
                worksheet.Cell(1, 8).Value = "Valid Until";



                HttpClient client = new HttpClient();


                string URI = "https://localhost:44376/api/admin/GetAllFilmTickets";

                HttpResponseMessage responseMessage = client.GetAsync(URI).Result;

                var result = responseMessage.Content.ReadAsAsync<List<FilmTicket>>().Result;

                for (int i = 1; i <= result.Count(); i++)
                {
                    var item = result[i - 1];
                    if (item.FilmGenre == request)
                    {
                        worksheet.Cell(i + 1, 1).Value = item.id.ToString();
                        worksheet.Cell(i + 1, 2).Value = item.FilmName;
                        worksheet.Cell(i + 1, 3).Value = item.FilmPosterImage;
                        worksheet.Cell(i + 1, 4).Value = item.FilmDescription;
                        worksheet.Cell(i + 1, 5).Value = item.FilmGenre;
                        worksheet.Cell(i + 1, 6).Value = item.FilmTicketPrice;
                        worksheet.Cell(i + 1, 7).Value = item.FilmRating;
                        worksheet.Cell(i + 1, 8).Value = item.ValidUntilDate.ToString();
                    }
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, contentType, fileName);
                }

            }
        }

        public FileContentResult ExportAllTickets()
        {
            string fileName = "AllFilmTickets.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("All Tickets");

                worksheet.Cell(1, 1).Value = "Film Ticket Id";
                worksheet.Cell(1, 2).Value = "Film Name";
                worksheet.Cell(1, 3).Value = "Film Poster Url";
                worksheet.Cell(1, 4).Value = "Film Description";
                worksheet.Cell(1, 5).Value = "Film Genre";
                worksheet.Cell(1, 6).Value = "Film Ticket Price";
                worksheet.Cell(1, 7).Value = "Film Rating";
                worksheet.Cell(1, 8).Value = "Valid Until";



                HttpClient client = new HttpClient();


                string URI = "https://localhost:44376/api/admin/GetAllFilmTickets";

                HttpResponseMessage responseMessage = client.GetAsync(URI).Result;

                var result = responseMessage.Content.ReadAsAsync<List<FilmTicket>>().Result;

                for (int i = 1; i <= result.Count(); i++)
                {
                    var item = result[i - 1];

                    worksheet.Cell(i + 1, 1).Value = item.id.ToString();
                    worksheet.Cell(i + 1, 2).Value = item.FilmName;
                    worksheet.Cell(i + 1, 3).Value = item.FilmPosterImage;
                    worksheet.Cell(i + 1, 4).Value = item.FilmDescription;
                    worksheet.Cell(i + 1, 5).Value = item.FilmGenre;
                    worksheet.Cell(i + 1, 6).Value = item.FilmTicketPrice;
                    worksheet.Cell(i + 1, 7).Value = item.FilmRating;
                    worksheet.Cell(i + 1, 8).Value = item.ValidUntilDate.ToString();


                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, contentType, fileName);
                }

            }
        }
        private List<FilmTheaterUser> getUsersFromExcelFile(string fileName)
        {

            string pathToFile = $"{Directory.GetCurrentDirectory()}\\files\\{fileName}";

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            List<FilmTheaterUser> userList = new List<FilmTheaterUser>();

            using (var stream = System.IO.File.Open(pathToFile, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        userList.Add(new FilmTheater.Domain.identity.FilmTheaterUser
                        {
                            Name = reader.GetValue(0).ToString(),
                            Surname = reader.GetValue(1).ToString(),
                            Email = reader.GetValue(2).ToString(),
                            PasswordHash = reader.GetValue(3).ToString(),
                            PhoneNumber = reader.GetValue(5).ToString()
                        });
                    }
                }
            }

            return userList;

        }

        [HttpPost("[action]")]
        public IActionResult ImportUsers(IFormFile file)
        {

            //make a copy
            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\files\\{file.FileName}";


            using (FileStream fileStream = System.IO.File.Create(pathToUpload))
            {
                file.CopyTo(fileStream);

                fileStream.Flush();
            }

            //read data from uploaded file

            List<FilmTheaterUser> users = getUsersFromExcelFile(file.FileName);

            HttpClient client = new HttpClient();

            string URL = "https://localhost:44376/api/Admin/ImportAllUsers";



            HttpContent content = new StringContent(JsonConvert.SerializeObject(users), Encoding.UTF8, "application/json");


            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var result = response.Content.ReadAsAsync<bool>().Result;

            return RedirectToAction("Index");
        }


    }
}
