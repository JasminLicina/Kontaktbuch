using KontaktbuchApi.Model;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Net.Cache;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace KontaktbuchApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KontaktBuchController : ControllerBase
    {
        private readonly ILogger<KontaktBuchController> _logger;

        public KontaktBuchController(ILogger<KontaktBuchController> logger)
        {
            _logger = logger;
        }

        SqlConnection databaseConnection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\licinaj\Documents\Kontaktbuch.mdf;Integrated Security = True; Connect Timeout = 30");


        [HttpGet("getallcontacts")]


        public List<Contacts> Get()
        {
            List<Contacts> contacts= new List<Contacts>();

            databaseConnection.Open();

            string query = "Select * from Kontaktbuch";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, databaseConnection);

            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            foreach (DataRow element in dataSet.Tables[0].Rows)
            
            {
                contacts.Add(new Contacts 
                { 
                    Id = Convert.ToInt32(element["Id"]), 
                    FirstName = element["FirstName"].ToString(), 
                    LastName = element["LastName"].ToString(), 
                    PhoneNumber = element["PhoneNumber"].ToString(), 
                    Age = Convert.ToInt32(element["Age"]) }
                );              
            }
            databaseConnection.Close();

            return contacts;
        }


        [HttpPost("addcontact2")]
        public List<Contacts> Add(ContactCollection contact)
        {
            var d = new List<Contacts>();
            d = contact.ContactList;
            d.Add(contact.Contact);
            return d;
        }


        [HttpPost("edit")]
        public List<Contacts> EditContacts(ContactCollection contact)
        {
            var newContactList = new List<Contacts>();

            foreach (var contactItem in contact.ContactList)
            {
                if (contactItem.Id == contact.Contact.Id)
                {
                    newContactList.Add(contact.Contact);
                }
                else
                {
                    newContactList.Add(contactItem);
                }
            }
            return newContactList;

        }


        [HttpPost("remove")]

        public void Delete(ContactCollection contact)
        {
            databaseConnection.Open();

            string query = "Select * from Kontaktbuch";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, databaseConnection);

            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);


            foreach (DataRow element in dataSet.Tables[0].Rows)

            {
                if (Convert.ToInt32(element["Id"]) == contact.Contact.Id)
                {

                }
            }

            databaseConnection.Close();


            Get();
        }



        //public List<Contacts> removeContacts(ContactCollection contact)
        //{

        //    var newContactList = new List<Contacts>();


        //    foreach (var contactItem in contact.ContactList)
        //    {
        //        if (contactItem.Id != contact.Contact.Id)
        //        {
        //            newContactList.Add(new Contacts
        //            {
        //                Age = contactItem.Age,
        //                FirstName = contactItem.FirstName,
        //                Id = contactItem.Id,
        //                LastName = contactItem.LastName,
        //                PhoneNumber = contactItem.PhoneNumber,
        //            });
        //        }
        //    }

        //    return newContactList;
        //}

    }
}
