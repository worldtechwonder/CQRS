using System.Data.Entity;
using MfxBi.Cqrs.QueryStack.ReadModel.Model;

namespace MfxBi.Cqrs.QueryStack.ReadModel
{
    public class MfxbiDatabase : DbContext
    {
        public MfxbiDatabase()
            : base("mfx")
        {
            
        }
        public DbSet<BookingSummary> BookingSummaries { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomConfiguration> RoomConfigurations { get; set; }

    }
}