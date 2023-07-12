using Persistence;
using DAL;

namespace BL
{
    public class OrderBL
    {
        OrderDAL oDAL = new OrderDAL();
        public List<Order> GetUnprocessedOrders()
        {
            List<Order> orders = new List<Order>();
            List<Order> unprocessedOrders = new List<Order>();
            orders = oDAL.GetOrders();
            foreach (Order odr in orders)
            {
                if (String.Equals(odr.Status, "Unprocessed"))
                    unprocessedOrders.Add(odr);
            }
            return unprocessedOrders;
        }
        public List<Order> GetProcessingOrders()
        {
            List<Order> orders = new List<Order>();
            List<Order> processingOrders = new List<Order>();
            orders = oDAL.GetOrders();
            foreach (Order odr in orders)
            {
                if (String.Equals(odr.Status, "Processing"))
                    processingOrders.Add(odr);
            }
            return processingOrders;
        }
        public List<Order> GetCompletedOrders()
        {
            List<Order> orders = new List<Order>();
            List<Order> completedOrders = new List<Order>();
            orders = oDAL.GetOrders();
            foreach (Order odr in orders)
            {
                if (String.Equals(odr.Status, "Completed"))
                    completedOrders.Add(odr);
            }
            return completedOrders;
        }
        public bool InsertOrder(Order order)
        {
            return oDAL.InsertOrder(order);
        }
        public bool UpdateOrderStatus(int orderID, string newOrderStatus)
        {
            return oDAL.UpdateOrderStatus(orderID, newOrderStatus);
        }
        public bool GetOrderCreator(int userID, int orderID)
        {
            Order order = new Order();
            order.ID = orderID;
            order.UserID = userID;
            return oDAL.InsertOrder(order);
        }
        public int GetTheLastOrderID()
        {
            List<Order> orders = new List<Order>();
            orders = oDAL.GetOrders();
            return orders[orders.Count() - 1].ID;
        }
        public decimal CalculateTotalPriceInOrder(List<OrderDetails> orderDetails)
        {
            ProductBL pBL = new ProductBL();
            decimal sum = 0;
            decimal rowPrice;
            foreach (OrderDetails item in orderDetails)
            {
                rowPrice = item.ProductQuantity * pBL.GetPriceByProductName(item.ProductName);
                sum += rowPrice;
            }
            return sum;
        }
        public bool UpdateOrderPaymentMethod(int orderID, string paymentMethod) {
            return oDAL.UpdateOrderPaymentMethod(orderID, paymentMethod);
        }
    }
}
