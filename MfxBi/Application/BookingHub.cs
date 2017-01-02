using System;
using Microsoft.AspNet.SignalR;

namespace MfxBi.Application
{
    public class BookingHub : Hub
    {
        public void NotifyReceived(string connId, Guid requestId)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<BookingHub>();
            hubContext.Clients.Client(connId).requestReceived(requestId);
        }

        public void NotifyDenormalized(string connId)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<BookingHub>();
            hubContext.Clients.Client(connId).requestDenormalized();
        }

        public void NotifyApproved(string connId)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<BookingHub>();
            hubContext.Clients.Client(connId).requestApproved();
        }

        public void NotifyProgress(string connId, int percentage)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<BookingHub>();
            hubContext.Clients.Client(connId).updateProgressBar(percentage, connId);
        }

        public void NotifyEnd(string connId)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<BookingHub>();
            hubContext.Clients.Client(connId).clearProgressBar();
        }
    }
}