using Konditerskaya_Nikushina.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Konditerskaya_Nikushina
{
    /// <summary>
    /// Логика взаимодействия для AdminAccess.xaml
    /// </summary>
    public partial class AdminAccess : Window
    {
        private KonditerskayaEntities _context;
        public AdminAccess()
        {
            InitializeComponent();
            _context = new KonditerskayaEntities();

            FillAllTables();

        }

        private void FillAllTables()
        {
            FillEmployees();
            FillClients();
            FillProducts();
            FillOrders();
            FillWarehouses();
        }

        private void FillEmployees()
        {
            List<Employees> list = _context.Employees.ToList();
            foreach (Employees employee in list)
            {
                EmployeeCARD card = new EmployeeCARD();
                card.employee = employee;
                card.IsAdminAccess = true;
                card.context = _context;
                card.Update();
                card.Margin = new Thickness(5);
                EmployeePanel.Children.Add(card);
            }
        }

        private void FillClients()
        {
            dgClients.ItemsSource = _context.Client.ToList();
        }
        private void FillProducts()
        {
            List<Product> list = _context.Product.ToList();
            foreach (Product product in list)
            {
                ProductCard card = new ProductCard();
                card.product = product;
                card.context = _context;
                card.IsAdminAccess = true;
                card.Update();
                card.Margin = new Thickness(5);
                ProductPanel.Children.Add(card);
            }
        }

        private void FillOrders()
        {
            List<OrderFullInfo> list = _context.OrderFullInfo.ToList();
            foreach (OrderFullInfo order in list)
            {
                OrderCard card = new OrderCard();
                card.Order = order;
                card.context = _context;
                
                card.Update();
                card.Margin = new Thickness(5);
                OrderPanel.Children.Add(card);
            }
        }

        private void FillWarehouses()
        {
            List<WarehouseAssortment> list = _context.WarehouseAssortment.ToList();
            foreach (WarehouseAssortment warehouse in list)
            {
                WarehouseCard card = new WarehouseCard();
                card.assortment = warehouse;
                card.context = _context;

                card.Update();
                card.Margin = new Thickness(5);
                WarehousePanel.Children.Add(card);
            }
        }
    }
}
