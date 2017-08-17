using SeaportWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SeaportWebApplication.Controllers.ActionResults
{
    public class BookingOverlappingDateResult : IHttpActionResult
    {
        private string infoMessage;

        public BookingOverlappingDateResult(IQueryable<Pier> otherPiers)
        {
            infoMessage += "In diesem Zeitraum ist der Liegeplatz schon gebucht.";            
            if(otherPiers.Count() > 0)
            {
                // List the open piers for the user
                string otherPiersString = string.Empty;
                foreach(Pier pier in otherPiers)
                {
                    otherPiersString += string.Format(" ( Name: {0}, Id: {1} )", pier.Name, pier.Id);                    
                }
                infoMessage = string.Join(" ", infoMessage, "Folgende Liegeplätze sind in diesem Zeitraum noch frei:", otherPiersString);
            }
            else
            {
                // return a message that no pier is open at that time
                infoMessage = "Es wurden leider keine anderen offenen Liegeplätze in diesem Zeitraum gefunden.";
            }
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(infoMessage)
            };
            return Task.FromResult(response);
        }
    }
}