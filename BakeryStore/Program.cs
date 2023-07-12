using BL;
using Persistence;
using DAL;
using CS;
using MySqlConnector;
class Program
{
    static void Main()
    {
        Ultilities CS = new Ultilities();
        OrderBL oBL = new OrderBL();
        OrderDetailsBL ordDtlsBL = new OrderDetailsBL();
        ProductBL pBL = new ProductBL();
        string[] cashierMenu = { "Create Order", "Confirm Order", "Log Out" };
        string[] sellerMenu = { "Unprocessed Orders", "Processing Orders", "Log Out" };
        string[] cashierSubMenu = { "Add Product To Order", "Show Order", "Back To Main Menu" };
        string[] loginMenu = { "Login", "Exit" };
        string username, pwd;
        UserBL uBL = new UserBL();
        do
        {
            int loginChoice = CS.MenuHandle("Bakery Store", loginMenu);
            switch (loginChoice)
            {
                case 1:
                    CS.Title("Login");
                    Console.Write(" UserName: ");
                    username = Console.ReadLine() ?? "";
                    Console.Write(" PassWord: ");
                    pwd = Console.ReadLine() ?? "";
                    int roleID = uBL.Authorize(username, pwd);
                    if (roleID == 1)
                    {
                        string[] unprocessedAction = { "Change Status To Processing", "Back To Previous Menu" };
                        string[] processingAction = { "Change Status To Completed", "Back To Previous Menu" };
                        int ordID;
                        bool active = true;
                        bool activeSub = true;
                        while (active)
                        {
                            int bartenderChoice = CS.MenuHandle("Seller", sellerMenu);
                            switch (bartenderChoice)
                            {
                                case 1:
                                    List<Order> unprocessedOrders = oBL.GetUnprocessedOrders();
                                    if (unprocessedOrders.Count() > 0)
                                    {
                                        CS.Title("Unprocessed Orders");
                                        Console.WriteLine("| {0,36} |", "Order ID");
                                        CS.Line();
                                        foreach (Order odr in unprocessedOrders)
                                            Console.WriteLine("| {0,36} |", odr.ID);
                                        CS.Line();
                                        Console.Write("Enter Order ID To View Details: ");
                                        int.TryParse(Console.ReadLine(), out ordID);
                                        Console.Clear();
                                        List<OrderDetails> orderDetails = ordDtlsBL.GetOrderDetailsByID(ordID);
                                        CS.Title("Order ID " + ordID);
                                        Console.WriteLine("| {0,23} | {1, 10} |", "Product", "Quantity");
                                        CS.Line();
                                        foreach (OrderDetails odr in orderDetails)
                                        {
                                            Console.WriteLine("| {0,23} | {1, 10} |", odr.ProductName, odr.ProductQuantity);
                                        }
                                        CS.Line();
                                        switch (CS.MenuHandle(null, unprocessedAction))
                                        {
                                            case 1:
                                                if (oBL.UpdateOrderStatus(ordID, "Processing"))
                                                {
                                                    Console.WriteLine("Change Order Status To Processing Successfully");
                                                }
                                                else Console.WriteLine("Something Went Wrong");
                                                break;
                                            case 2:
                                                break;
                                            default:
                                                break;
                                        }
                                        CS.PressAnyKeyToContinue();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Don't Have Any Orders Have Status Unprocessed");
                                        CS.PressAnyKeyToContinue();
                                    }
                                    break;
                                case 2:
                                    List<Order> processingOrders = oBL.GetProcessingOrders();
                                    if (processingOrders.Count() > 0)
                                    {
                                        CS.Title("Processing Orders");
                                        Console.WriteLine("| {0,36} |", "Order ID");
                                        CS.Line();
                                        foreach (Order odr in processingOrders)
                                            Console.WriteLine("| {0,36} |", odr.ID);
                                        CS.Line();
                                        Console.Write("Enter Order ID To View Details: ");
                                        int.TryParse(Console.ReadLine(), out ordID);
                                        Console.Clear();
                                        List<OrderDetails> orderDetails = ordDtlsBL.GetOrderDetailsByID(ordID);
                                        CS.Title("Order ID " + ordID);
                                        Console.WriteLine("| {0,23} | {1, 10} |", "Product", "Quantity");
                                        CS.Line();
                                        foreach (OrderDetails odr in orderDetails)
                                        {
                                            Console.WriteLine("| {0,23} | {1, 10} |", odr.ProductName, odr.ProductQuantity);
                                        }

                                        CS.Line();
                                        do
                                        {
                                            switch (CS.MenuHandle(null, processingAction))
                                            {
                                                case 1:
                                                    if (oBL.UpdateOrderStatus(ordID, "Completed"))
                                                    {
                                                        Console.WriteLine("Completed Order!");
                                                        activeSub = false;
                                                    }
                                                    else Console.WriteLine("Something Went Wrong");
                                                    break;
                                                case 2:
                                                    activeSub = false;
                                                    break;
                                                default:
                                                    break;
                                            }
                                        } while (activeSub);
                                        CS.PressAnyKeyToContinue();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Don't Have Any Orders Have Status Unprocessed");
                                        CS.PressAnyKeyToContinue();
                                    }
                                    break;
                                case 3:
                                    active = false;
                                    break;
                                default:
                                    break;
                            }
                        }

                         
                    }
                    else if (roleID == 2)
                    {
                        bool active = true, activeSub = true;
                        int productID, productQuantity, orderID = oBL.GetTheLastOrderID() + 1, ordID;
                        string[] paymentMethodSelect = { "Cash", "Banking" };
                        string answer;
                        List<OrderDetails> listOrderDetails = new List<OrderDetails>();
                        Product productGetted = new Product();
                        while (active)
                        {
                            int cashierChoice = CS.MenuHandle("Cashier", cashierMenu);
                            switch (cashierChoice)
                            {
                                case 1:
                                    bool isAddProduct = true;
                                    while (isAddProduct)
                                    {
                                        OrderDetails ordDetails = new OrderDetails();
                                        CS.Title("List Product");
                                        List<Product> products = pBL.GetAllProduct();
                                        if (products.Count() != 0)
                                        {
                                            Console.WriteLine("| {0,3} | {1,30} | {2,9} | {3, 12} |", "ID", "Product", "Quantity", "Price");
                                            CS.Line();
                                            foreach (Product product in products)
                                                Console.WriteLine("| {0,3} | {1,30} | {2,9} | {3, 12} |", product.ID, product.Name, product.Quantity, product.Price);
                                            Console.Write("Select Product ID To Add To Order: ");
                                            int.TryParse(Console.ReadLine(), out productID);
                                            productGetted = pBL.GetProductByID(productID);
                                            Console.Write("Input Quantity For " + productGetted.Name + ": ");
                                            int.TryParse(Console.ReadLine(), out productQuantity);
                                            ordDetails.OrderID = orderID;
                                            ordDetails.ProductName = productGetted.Name;
                                            ordDetails.ProductQuantity = productQuantity;
                                            listOrderDetails.Add(ordDetails);
                                            Console.Write("Continue To Add Product To Order (Y/N): ");
                                            answer = Console.ReadLine() ?? "";
                                            if (String.Equals(answer, "N") || String.Equals(answer, "n"))
                                            {
                                                isAddProduct = false;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Don't Have Any Product");
                                            CS.PressAnyKeyToContinue();
                                        }
                                    }
                                    oBL.GetOrderCreator(uBL.GetUserIDByUserName(username), orderID);
                                    ordDtlsBL.AddListProductToOrder(listOrderDetails);
                                    Console.WriteLine("Create Order Completed");
                                    break;
                                case 2:
                                    CS.Title("List Order Completed");
                                    List<Order> completedOrders = oBL.GetCompletedOrders();
                                    if (completedOrders.Count() != 0)
                                    {
                                        CS.Title("Completed Orders");
                                        Console.WriteLine("| {0,36} |", "Order ID");
                                        CS.Line();
                                        foreach (Order odr in completedOrders)
                                            Console.WriteLine("| {0,36} |", odr.ID);
                                        CS.Line();
                                        Console.Write("Input Order ID To View Order Details: ");
                                        int.TryParse(Console.ReadLine(), out ordID);
                                        List<OrderDetails> orderDtls = ordDtlsBL.GetOrderDetailsByID(ordID);
                                        CS.Title("Order ID " + ordID);
                                        Console.WriteLine("| {0,19} | {1, 10} | {2, 10} |", "Product", "Quantity", "Price");
                                        CS.Line();
                                        foreach (OrderDetails odr in orderDtls)
                                        {
                                            Console.WriteLine("| {0,19} | {1, 10} | {2, 10} |", odr.ProductName, odr.ProductQuantity, pBL.GetPriceByProductName(odr.ProductName));
                                        }
                                        CS.Title("Total Price: " + oBL.CalculateTotalPriceInOrder(orderDtls));
                                        Console.Write("(Y) To Confirm Order, (N) To Cancel Confirm Order: ");
                                        string confirmAnswer = Console.ReadLine() ?? "";
                                        if (confirmAnswer == "Y" || confirmAnswer == "y")
                                        {
                                            int oID = 0;
                                            foreach (OrderDetails item in orderDtls)
                                            {
                                                oID = item.OrderID;
                                            }
                                            int paymentChoice = CS.MenuHandle("Payment Method", paymentMethodSelect);
                                            switch (paymentChoice)
                                            {
                                                case 1:
                                                    oBL.UpdateOrderPaymentMethod(oID, "Cash");
                                                    break;
                                                case 2:
                                                    oBL.UpdateOrderPaymentMethod(oID, "Banking");
                                                    break;
                                                default:
                                                    break;
                                            }
                                            oBL.UpdateOrderStatus(oID, "Export");
                                        }
                                        else if (confirmAnswer == "N" || confirmAnswer == "n") break;
                                        CS.Line();
                                        CS.PressAnyKeyToContinue();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Don't Have Any Order Have Completed Status");
                                        CS.PressAnyKeyToContinue();
                                    }
                                    break;
                                case 3:
                                    active = false;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid User Name Or Password!");
                        Main();
                    }
                    break;
                case 2:
                    return;
                default:
                    break;
            }
        } while (true);

    }
}