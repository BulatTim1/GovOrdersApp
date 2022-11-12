using GovOrdersApp.Data.Orders;
using MongoDB.Driver;

namespace GovOrdersApp.Data.DB
{
    public class OrdersContext
    {
        private IMongoCollection<Order> orders = DBConnection.orders;
        
        public List<Order> GetOrders()
        {
            return orders.Find(order => true).ToList();
        }

        public void AddOrder(Order order)
        {
            orders.InsertOne(order);
        }
        
        public void UpdateOrder(Order order)
        {
            orders.ReplaceOne(o => o.Id == order.Id, order);
        }
        
        public void DeleteOrder(string id)
        {
            orders.DeleteOne(order => order.Id == id);
        }
        
        public void DeleteOrder(Order order)
        {
            orders.DeleteOne(o => o.Id == order.Id);
        }

        public Order? GetOrder(string id)
        {
            try
            {
                return orders.Find(order => order.Id == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public List<Order> GetOrdersByIndustry(string industry)
        {
            return orders.Find(order => order.Industry == industry).ToList();
        }
        
        public List<Order> GetOrdersByUser(string id)
        {
            return orders.Find(order => order.Author == id).ToList();
        }

        public List<Order> GetOrdersByBuilder(string id)
        {
            return orders.Find(order => order.Builder == id).ToList();
        }

        public List<Order> GetOrdersByDesigner(string id)
        {
            return orders.Find(order => order.Designer == id).ToList();
        }
    }
}
