//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.IO;
//using System.Net;
//using System.Net.Mail;
//using System.Runtime.InteropServices;
//using Outlook = Microsoft.Office.Interop.Outlook;
//using Office = Microsoft.Office.Core;
// MATT ALL YOU HAVE TO DO IS FIX THE ONE MAP ERROR AND IT WILL BE DONE.................................
//namespace DarkDarkGalaxy
//{
//    public class KralovecEmail
//    {
//        private void ThisAddIn_Startup(object sender, System.EventArgs e)
//        {
//            SendEmailtoContacts();
//        }

//        private void SendEmailtoContacts()
//        {
//            string subjectEmail = "Meeting has been rescheduled.";
//            string bodyEmail = "Meeting is one hour later.";

//            // Create Instance of MAP Folder to fix  Error _______
//            Outlook.MAPIFolder sentContacts = (Outlook.MAPIFolder)
//                this.Application.ActiveExplorer().Session.GetDefaultFolder
//                (Outlook.OlDefaultFolders.olFolderContacts);
//            // for each contact in outlook 
//            foreach (Outlook.ContactItem contact in sentContacts.Items)
//            {
//                if (contact.Email1Address.Contains("example.com"))
//                {
//                    this.CreateEmailItem(subjectEmail, contact
//                        .Email1Address, bodyEmail);
//                }
//            }
//        }

//        private void CreateEmailItem(string subjectEmail,
//               string toEmail, string bodyEmail)
//        {
//            // Instaince of outlook application 
//            Microsoft.Office.Interop.Outlook.Application app = new Microsoft.Office.Interop.Outlook.Application();
//            Microsoft.Office.Interop.Outlook.MailItem mailItem = app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

//            mailItem.Subject = subjectEmail;
//            mailItem.To = toEmail;
//            mailItem.Body = bodyEmail;
//            mailItem.Importance = Outlook.OlImportance.olImportanceLow;
//            ((Outlook._MailItem)mailItem).Send();
//        }

//    }
//}
