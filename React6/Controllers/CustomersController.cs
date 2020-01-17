﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Onboard.Models;

namespace Onboard.Controllers
{
    public class CustomersController : Controller
    {
        private OnboardEntities1 db = new OnboardEntities1();
        public ActionResult Index()
        {
            return View();
        }

        // GET: Customers
        public JsonResult GetCustomer()
        {
            try
            {
                var customerList = db.Customers.Select(x => new { x.id, x.name, x.address }).ToList();
                return new JsonResult { Data = customerList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
            catch (Exception e)
            {
                Console.Write("Exception Occured /n {0}", e.Data);
                return new JsonResult { Data = "Data Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
        }

        //Create Customer
        public JsonResult CreateCustomer(Customer customer)
        {
            try
            {
                db.Customers.Add(customer);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write("Exception Occured /n {0}", e.Data);
                return new JsonResult { Data = "Create Customer Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Customer created", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        //Delete Customer
        public JsonResult DeleteCustomer(int id)
        {
            try
            {
                var customer = db.Customers.Where(x => x.id == id).SingleOrDefault();
                if (customer != null)
                {
                    db.Customers.Remove(customer);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.Write("Exception Occured /n {0}", e.Data);
                return new JsonResult { Data = "Deletion Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
            return new JsonResult { Data = "Success Customer Deleted", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        //Update Customer
        public JsonResult UpdateCustomer(Customer customer)
        {
            try
            {
                Customer dbCustomer = db.Customers.Where(x => x.id == customer.id).SingleOrDefault();
                dbCustomer.name = customer.name;
                dbCustomer.address = customer.address;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write("Exception Occured /n {0}", e.Data);
                return new JsonResult { Data = "Update Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Customer details updated", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }


        //GetUpdate Customer
        public JsonResult GetUpdateCustomer(int id)
        {
            try
            {
                Customer customer = db.Customers.Where(x => x.id == id).SingleOrDefault();
                return new JsonResult { Data = customer, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception e)
            {
                Console.Write("Exception Occured /n {0}", e.Data);
                return new JsonResult { Data = "Customer Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

    }
}