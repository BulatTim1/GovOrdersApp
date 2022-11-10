﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace GovOrdersApp.Data.DB
{
    public static class DBConnection
    {
        private static MongoClient client = new MongoClient("mongodb://localhost");
        internal static readonly IMongoDatabase db = client.GetDatabase("GovOrderApp");
        internal static readonly IMongoCollection<AppUser> users = db.GetCollection<AppUser>("Users");
        internal static readonly IMongoCollection<Order> orders = db.GetCollection<Order>("Orders");
    }
}