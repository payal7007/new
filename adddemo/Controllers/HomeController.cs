using adddemo.Models;
using System;
//using adddemo.Models.demo.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace adddemo.Controllers
{
    public class HomeController : Controller
    {
        RepoModel repo = new RepoModel();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Addlist()
        {
            RepoModel repo = new RepoModel();
            List<MyAdvertiseModel> advertiseList = repo.Advertises();
            return View(advertiseList);
        }

        [HttpPost]
        public ActionResult ApproveAdvertisement( int advertiseId)
        {
            try
            {
                // RepoModel repo = new RepoModel();
                bool isApproved = repo.ApproveAdvertisement(advertiseId);

                if (isApproved != true)
                {
                    return View("Success");
                }

                else
                {
                    return View("Error");
                    //return Json(new { success = false });
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during the database update
                // Log the exception for debugging and monitoring purposes
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public ActionResult RejectAdvertisement(MyAdvertiseModel obj)
        {
            try
            {
                var repo = new RepoModel();
                bool isRejected = repo.RejectAdvertisement(obj);

                if (isRejected != true)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during the database update
                // Log the exception for debugging and monitoring purposes
                return Json(new { success = false });
            }
        }
        //public ActionResult Details(int? advertiseId)
        //{
        //    if (advertiseId == null)
        //    {
        //        // Handle the case where advertiseId is missing or null in the request.
        //        // You can display an error message or redirect to another view.
        //        return View("Error");
        //    }

        //    try
        //    {
        //        var repo = new RepoModel();
        //        MyAdvertiseModel advertisement = repo.GetAdvertisementDetails(advertiseId.Value);

        //        if (advertisement != null)
        //        {
        //            return View(advertisement);
        //        }
        //        else
        //        {
        //            // Handle the case where the advertisement with the given ID is not found
        //            // You can display an error message or redirect to another view.
        //            return View("NotFound");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception
        //        // Handle the error and display an error page or message
        //        return View("Error");
        //    }
        //}
        public ActionResult Details(int id)
        {
            var repo = new RepoModel();
            MyAdvertiseModel advertisement = repo.GetAdvertiseById(id);

            if (advertisement == null)
            {
                return HttpNotFound();
            }

            return View(advertisement);
        }
        // GET: /Home/Edit/123
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var repo = new RepoModel();
            // Retrieve the advertisement you want to edit by its ID
            MyAdvertiseModel advertisement = repo.GetAdvertiseById(id);

            if (advertisement == null)
            {
                return HttpNotFound();
            }

            return View(advertisement);
        }
        [HttpPost]

        public ActionResult Edit(MyAdvertiseModel advertisement)
        {
            if (ModelState.IsValid)
            {
                var repo = new RepoModel();
                bool updated = repo.UpdateAdvertisement(advertisement);

                if (updated)
                {
                    // Redirect to a success page or return a success message
                    return RedirectToAction("Success");
                }
                else
                {
                    // Handle update failure
                    ModelState.AddModelError("", "Failed to update the advertisement.");
                }
            }

            // If ModelState is not valid or update fails, return to the edit page with errors
            return View(advertisement);
        }


        public ActionResult Delete(int? advertiseId)
        {
            if (advertiseId == null)
            {
                return View("Error");
            }
            var repo = new RepoModel();
            // Delete the record from the database based on the advertiseId
            bool isDeleted = repo.DeleteAdvertise(advertiseId);

            if (isDeleted)
            {
                TempData["AlertMessage"] = "Record deleted successfully.";
                return RedirectToAction("Details");
            }
            else
            {
                return View("Error");
            }
        }

    }
}
