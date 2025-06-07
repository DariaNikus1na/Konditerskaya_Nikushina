using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Konditerskaya_Nikushina.Class
{
    /// <summary>
    /// Логика взаимодействия для OrderCard.xaml
    /// </summary>
    public partial class OrderCard : UserControl
    {
        public OrderFullInfo Order { get; set; }

        public KonditerskayaEntities context;

        public OrderCard()
        {
            InitializeComponent();
        }

        public void Update()
        {
            ordernumberComtainer.Text = Order.Order_number.ToString();
            dateContainer.Text = Order.Date.ToString();
            clientContainer.Text = Order.Client;
            statusContainer.Text = Order.Status;
            paymentmethodContainer.Text = Order.Payment_method;
            employeeContainer.Text = Order.Employee;
            addressContainer.Text = Order.Address;

            List<OrderCompositionInfo> list = new List<OrderCompositionInfo>();
            foreach (var item in context.OrderCompositionInfo.ToList())
            {
                if (item.Order == Order.Order_number)
                {
                    list.Add(item);
                }
            }
            dgItems.ItemsSource = list;

        }

    }


    
}
