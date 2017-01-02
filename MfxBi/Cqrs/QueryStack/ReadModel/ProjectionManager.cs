using System;
using System.Linq;
using MfxBi.Cqrs.QueryStack.ReadModel.Model;

namespace MfxBi.Cqrs.QueryStack.ReadModel
{
    public class ProjectionManager : IDisposable
    {
        private readonly MfxbiDatabase _context = null;

        public ProjectionManager()
        {
            _context = new MfxbiDatabase();
            _context.Configuration.AutoDetectChangesEnabled = false;
        }

        public IQueryable<BookingSummary> BookingSummaries
        {
            get { return _context.BookingSummaries; }
        }

        public IQueryable<Room> Rooms
        {
            get { return _context.Rooms; }
        }
        public IQueryable<RoomConfiguration> RoomConfigurations
        {
            get { return _context.RoomConfigurations; }
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }
}