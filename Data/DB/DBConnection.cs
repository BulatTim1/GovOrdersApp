using GovOrdersApp.Data.Orders;
using GovOrdersApp.Data.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace GovOrdersApp.Data.DB
{
    public static class DBConnection
    {
        private static MongoClient client = new MongoClient("mongodb://localhost");
        internal static readonly IMongoDatabase db = client.GetDatabase("GovOrderApp");
        internal static readonly IMongoCollection<AppUser> users = db.GetCollection<AppUser>("Users");
        internal static readonly IMongoCollection<Order> orders = db.GetCollection<Order>("Orders");
        internal static readonly GridFSBucket gridFS = new GridFSBucket(db);
    }
}