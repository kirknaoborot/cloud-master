using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using KancelarCloud.Models;
using KancelarCloud.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;
using MimeKit;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;


using iTextSharp.LGPLv2;
using iTextSharp.text;
using iTextSharp.text.pdf;



namespace KancelarCloud.Controllers
{
    public class HomeController : Controller
    {
        InternetAddressList internetAddressList = new InternetAddressList();
        ContextDBcs _context;
        IHostingEnvironment _appEnvironment;
        public HomeController(ContextDBcs context, IHostingEnvironment appEnv)
        {
            _context = context;
            _appEnvironment = appEnv;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel()
            {
                FileVlojs = _context.FileVlojs.Where(f => f.DeleteFile == false),
                Orgs = _context.Orgs,
                PageViewModel = new PageViewModel { NamePage = "Облако" }

            };

            return View(model);

        }
        public IActionResult Trash()
        {
            var model = new IndexViewModel()
            {
                FileVlojs = _context.FileVlojs.Where(f => f.DeleteFile == true),
                Orgs = _context.Orgs,
                PageViewModel = new PageViewModel { NamePage = "Корзина" }

            };

            return View("Index", model);
        }

        public IActionResult Recently()
        {

            var model = new IndexViewModel()
            {
                FileVlojs = _context.FileVlojs.Where(f => f.EnterDate >= DateTime.Now.Date.AddDays(-7) && f.DeleteFile == false),
                Orgs = _context.Orgs,
                PageViewModel = new PageViewModel { NamePage="Недавние"}
            };
            return View("Index", model);
        }
        public IActionResult SelectedList()
        {
            return View(_context.Orgs.ToList());
        }

        [HttpPost]

        public async Task<IActionResult> AddFile(IFormFile uploadFile)
        {
            if (uploadFile != null)
            {
                string path = "/Files/" + uploadFile.FileName.Replace(" ", string.Empty);//путь к папке
                using (var filestream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create,FileAccess.Write))
                {
                    await uploadFile.CopyToAsync(filestream);
                }
                FileVloj file = new FileVloj { FileName = uploadFile.FileName, PathFileName = path, EnterDate = DateTime.Now, SizeFile = Convert.ToInt32(uploadFile.Length), Type = uploadFile.ContentType, DeleteFile = false };
                _context.FileVlojs.Add(file);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]

        public IActionResult DeleteFile(int Id)
        {
            if (Id != 0)
            {
                FileVloj file = new FileVloj { Id = Id };
                var fl = _context.FileVlojs.First(f => f.Id.Equals(Id));
                if (fl.DeleteFile == true)

                {
                    string Delpath = fl.FileName.Replace(" ", string.Empty);
                    string trash = "/Files/" + Delpath;
                    var fullpath = _appEnvironment.WebRootPath + trash;
                    _context.FileVlojs.Remove(fl);
                    _context.SaveChanges();
                    System.IO.File.Delete(fullpath);
                    return RedirectToAction("Trash");
                }
                else
                {
                    fl.DeleteFile = true;
                    string flpath = fl.FileName.Replace(" ", string.Empty);
                    _context.FileVlojs.Update(fl);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

            else
            {
                return RedirectToAction("Index");
            }
        }
        public IActionResult Error()
        {
            return View();
        }

        /*работаtет*/
        public string Imagetransfer(string namefile)
        {
            if (namefile != null)
            {
                var image = _context.FileVlojs.First(f => f.FileName.Contains(namefile));

                return JsonConvert.SerializeObject(image);
            }
            else
            {
                return "error";
            }

        }
        /*Работает скачивание файла*/
        public IActionResult DownloadFile(int Id)
        {
            if (Id != 0)
            {
                var file = _context.FileVlojs.First(f => f.Id.Equals(Id));

                string file_path = file.PathFileName;
                string type = "application/octet-stream";
                string filename = file.FileName;
                return File(file_path, type, filename);
            }

            else
            {
                return Error();
            }

        }
        /*работает поиск*/
        public string Search(string namefile)
        {
            if (namefile != null)
            {
                var name = _context.FileVlojs.Where(f => f.FileName.Contains(namefile));
                return JsonConvert.SerializeObject(name);
            }

            else
            {
                return "Ничего не найдено";
            }
        }
        /*работает отправка мыла*/
        public async Task<IActionResult> SendMessage(int id, string[] Email)
        {

            foreach (var em in Email)
            {
                InternetAddress internetAddress = InternetAddress.Parse(ParserOptions.Default, em);
                internetAddressList.Add(internetAddress);
            }
            string ip = HttpContext.Request.Host.Value;
            var fileAttach = _context.FileVlojs.First(f => f.Id.Equals(id));
            string flpath = fileAttach.PathFileName;
            string namefile = fileAttach.FileName;
            using (var filestream = new FileStream(_appEnvironment.WebRootPath + flpath, FileMode.Open))
            {
                EmailService emailService = new EmailService();


                if (fileAttach.SizeFile > 20971520)
                {
                    await emailService.SendEmailAsync(internetAddressList, "Вложение",ip + "/Home/DownloadFile/" + id, null, null, null);
                }
                else
                {
                    await emailService.SendEmailAsync(internetAddressList, "Вложение", namefile, flpath, namefile, filestream);
                }

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        /*Справочник организаций*/
        public async Task<IActionResult> Org()
        {
            var items = await _context.Orgs.ToListAsync();

            var modal = new IndexViewModel
            {

                Orgs = items,

            };
            return View("_Orgs", modal);

        }
        /*Не реализовано добавление с прваочник организаций*/
        public IActionResult AddOrg(string Email, string nameorg)
        {
            KancelarCloud.Models.Org org = new Models.Org { NameOrg = nameorg, Email = Email };
            _context.Orgs.Add(org);
            _context.SaveChangesAsync();
            return RedirectToAction("_Orgs");
        }
        /*работает Удаление из справочника орг*/
        public string DeleteSpravochnik(int Id)
        {
            if (Id != 0)
            {
                var org = _context.Orgs.First(i => i.OrgId == Id);
                _context.Orgs.Remove(org);
                _context.SaveChangesAsync();
                return "success";
            }

            else
            {
                return "Error";
            }

        }
        /*не работает*/
        public IActionResult UdpateOrg(string nameorg)
        {
            KancelarCloud.Models.Org org = new Models.Org { NameOrg = nameorg };
            var updateorg = _context.Orgs.Where(o => o.NameOrg == nameorg).FirstOrDefault();
            updateorg.NameOrg = nameorg;
            _context.SaveChangesAsync();
            return RedirectToAction("_Orgs");
        }
        public void LoadHtgi(string num)
        {
            using (Models.HTGI.APPCONTEXT db = new Models.HTGI.APPCONTEXT())
            {
                var uid = db.DOCUMENTS.FromSql("SELECT UID,REMARK,NAME FROM DS_DOCUMENTS WHERE REG_NUM ={0}", num).ToList();
                string filename = num.Replace("-", string.Empty);
                string s = filename.Replace("/", string.Empty);
                foreach (var reguid in uid)
                {
                
                    var vloj = db.IMAGES.FromSql("SELECT DOCIMAGE FROM DS_IMAGES WHERE DOC_UID = {0}", reguid.UID).ToList();
                    if (vloj.Count != 0)
                    {
                        if (((reguid.REMARK.Contains("Документ потокового сканирования") == false)))
                        {
                            if ((reguid.NAME.Contains(".tif")) != true)
                            {
                                using (var stream = new FileStream(_appEnvironment.WebRootPath + "\\Files\\" + String.Format("{0}{1}", s, reguid.UID.ToString().Replace("-", String.Empty)), FileMode.Create, FileAccess.Write))
                                {
                                    System.IO.FileInfo filesize = new FileInfo(_appEnvironment.WebRootPath + "\\Files\\" + String.Format("{0}{1}", s, reguid.UID.ToString().Replace("-", String.Empty)));
                                    int size = Convert.ToInt32(filesize.Length);
                                    FileVloj file = new FileVloj { FileName = String.Format("{0}{1}", s, reguid.UID.ToString().Replace("-", String.Empty)), PathFileName = "/Files/" + String.Format("{0}{1}", s, reguid.UID.ToString().Replace("-", String.Empty)), EnterDate = DateTime.Now, DeleteFile = false, SizeFile = size, Type = "octet-stream" };
                                    _context.FileVlojs.Add(file);
                                    _context.SaveChanges();

                                    stream.Close();
                                    stream.Dispose();
                                }
                            }


                            else
                            {
                                var tempimage = iTextSharp.text.Image.GetInstance(vloj.First().DOCIMAGE);
                                Document document = new Document(new iTextSharp.text.Rectangle(tempimage.Width, tempimage.Height));

                                using (var stream = new FileStream(_appEnvironment.WebRootPath + "\\Files\\" + String.Format("{0}{1}", s, reguid.UID.ToString().Replace("-", String.Empty)) + ".pdf", FileMode.Create, FileAccess.Write))
                                {
                                    PdfWriter.GetInstance(document, stream);

                                    document.Open();
                                    foreach (var a in vloj)
                                    {
                                        iTextSharp.text.Image tiff = iTextSharp.text.Image.GetInstance(a.DOCIMAGE);
                                        tiff.ScalePercent(70, 85);
                                        document.Add(tiff);
                                    }
                                    System.IO.FileInfo filesize = new FileInfo(_appEnvironment.WebRootPath + "\\Files\\" + String.Format("{0}{1}", s, reguid.UID.ToString().Replace("-", String.Empty)) + ".pdf");
                                    int size = Convert.ToInt32(filesize.Length);
                                    FileVloj file = new FileVloj { FileName = String.Format("{0}{1}", s, reguid.UID.ToString().Replace("-", String.Empty) + ".pdf"), PathFileName = "/Files/" + String.Format("{0}{1}", s, reguid.UID.ToString().Replace("-", String.Empty)) + ".pdf", EnterDate = DateTime.Now, DeleteFile = false, SizeFile = size, Type = "application/pdf" };

                                    _context.FileVlojs.Add(file);
                                    _context.SaveChanges();
                                    document.Close();
                                    stream.Close();
                                    stream.Dispose();
                                }
                            }

                        }



                        else
                        {
                            var tempimage = iTextSharp.text.Image.GetInstance(vloj.First().DOCIMAGE);
                            Document document = new Document(new iTextSharp.text.Rectangle(tempimage.Width, tempimage.Height));

                            using (var stream = new FileStream(_appEnvironment.WebRootPath + "\\Files\\" + String.Format("{0}{1}", s, reguid.UID.ToString().Replace("-", String.Empty)) + ".pdf", FileMode.Append, FileAccess.Write))
                            {
                                PdfWriter.GetInstance(document, stream);

                                document.Open();
                                foreach (var a in vloj)
                                {
                                    iTextSharp.text.Image tiff = iTextSharp.text.Image.GetInstance(a.DOCIMAGE);
                                    tiff.ScalePercent(70, 85);
                                    document.Add(tiff);
                                }
                                System.IO.FileInfo filesize = new FileInfo(_appEnvironment.WebRootPath + "\\Files\\" + String.Format("{0}{1}", s, reguid.UID.ToString().Replace("-", String.Empty)) + ".pdf");
                                int size = Convert.ToInt32(filesize.Length);
                                FileVloj file = new FileVloj { FileName = String.Format("{0}{1}", s, reguid.UID.ToString().Replace("-", String.Empty) + ".pdf"), PathFileName = "/Files/" + String.Format("{0}{1}", s, reguid.UID.ToString().Replace("-", String.Empty)) + ".pdf", EnterDate = DateTime.Now, DeleteFile = false, SizeFile = size, Type = "application/pdf" };

                                _context.FileVlojs.Add(file);
                                _context.SaveChanges();
                                document.Close();
                                stream.Close();
                                stream.Dispose();
                            }
                        }
                    }

                    else
                    {
                        continue;
                    }


                }

           

            }

        }
    }

       
            }





        

    

    

