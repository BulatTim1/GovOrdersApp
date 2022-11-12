using GovOrdersApp.Data.DB;
using GovOrdersApp.Data.Orders;
using GovOrdersApp.Data.Users;

namespace GovOrdersApp.Services
{
	public class OrderService
    {
        static OrdersContext _context = new OrdersContext();

        public List<Order> GetOrders()
        {
            return _context.GetOrders();
        }

        public List<Order> GetOrdersByIndustry(string industry)
        {
            return _context.GetOrdersByIndustry(industry);
        }

        public List<Order> GetOrdersByBuilder(string id)
        {
            return _context.GetOrdersByBuilder(id);
        }
        
        public List<Order> GetOrdersByDesigner(string id)
        {
            return _context.GetOrdersByDesigner(id);
        }

        public Order GetOrder(string id)
        {
            return _context.GetOrder(id);
        }

        public bool AddOrder(string title, string description, string industry, string author)
        {
            Order order = new Order();
            order.Title = title;
            order.Description = description;
            order.Industry = industry;
            order.Author = author;
            _context.AddOrder(order);
            return true;
        }
    }
}
