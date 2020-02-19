using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IO;
using Microsoft.Azure;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using AutoShop_Web.Helpers;
using AutoShop_Shared.Controllers;

namespace AutoShop_Web.Controllers
{

        [Route("api/[controller]")]
        public class ImagesController : Controller
        {
            // make sure that appsettings.json is filled with the necessary details of the azure storage
            private readonly AzureStorageConfig storageConfig = null;

            public ImagesController(IOptions<AzureStorageConfig> config)
            {
                storageConfig = config.Value;
            }

            // POST /api/images/upload
            [HttpPost("[action]")]
            public async Task<IActionResult> Upload(ICollection<IFormFile> files)
            {
                bool isUploaded = false;

                try
                {

                    if (files.Count == 0)

                        return BadRequest("No files received from the upload");

                    if (storageConfig.AccountKey == string.Empty || storageConfig.AccountName == string.Empty)

                        return BadRequest("sorry, can't retrieve your azure storage details from appsettings.js, make sure that you add azure storage details there");

                    if (storageConfig.ImageContainer == string.Empty)

                        return BadRequest("Please provide a name for your image container in the azure blob storage");

                    foreach (var formFile in files)
                    {
                        if (StorageHelper.IsImage(formFile))
                        {
                            if (formFile.Length > 0)
                            {
                                using (Stream stream = formFile.OpenReadStream())
                                {
                                    isUploaded = await StorageHelper.UploadFileToStorage(stream, formFile.FileName, storageConfig);
                                }
                            }
                        }
                        else
                        {
                            return new UnsupportedMediaTypeResult();
                        }
                    }

                    if (isUploaded)
                    {
                        if (storageConfig.ThumbnailContainer != string.Empty)

                            return new AcceptedAtActionResult("GetThumbNails", "Images", null, null);

                        else

                            return new AcceptedResult();
                    }
                    else

                        return BadRequest("Look like the image couldnt upload to the storage");


                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            // GET /api/images/thumbnails
            [HttpGet("thumbnails")]
            public async Task<IActionResult> GetThumbNails()
            {

                try
                {
                    if (storageConfig.AccountKey == string.Empty || storageConfig.AccountName == string.Empty)

                        return BadRequest("sorry, can't retrieve your azure storage details from appsettings.js, make sure that you add azure storage details there");

                    if (storageConfig.ImageContainer == string.Empty)

                        return BadRequest("Please provide a name for your image container in the azure blob storage");

                    List<string> thumbnailUrls = await StorageHelper.GetThumbNailUrls(storageConfig);

                    return new ObjectResult(thumbnailUrls);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }

        }
    }/*
    // GET: Images
    public ActionResult Index()
        {
            return View();
        }

        // GET: Images/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Images/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Images/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Images/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Images/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Images/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Images/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
*/