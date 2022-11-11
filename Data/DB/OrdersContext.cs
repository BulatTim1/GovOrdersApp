using GovOrdersApp.Data.Orders;
using MongoDB.Driver;

namespace GovOrdersApp.Data.DB
{
    public class OrdersContext
    {
        private IMongoCollection<Order> orders = DBConnection.orders;
        
    }
}
