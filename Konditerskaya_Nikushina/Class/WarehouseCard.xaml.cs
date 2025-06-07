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
    /// Логика взаимодействия для WarehouseCard.xaml
    /// </summary>
    public partial class WarehouseCard : UserControl
    {
        public KonditerskayaEntities context;
       public WarehouseAssortment assortment;
        public WarehouseCard()
        {
            InitializeComponent();
        }

        public void Update()
        {
            warehouseContainer.Text = assortment.Warehouse_ID.ToString();

            List<WarehouseAssortment> list = new List<WarehouseAssortment>();
            foreach (var item in context.WarehouseAssortment.ToList())
            {
                if (item.Warehouse_ID == assortment.Warehouse_ID)
                {
                    list.Add(item);
                }
            }
            dgItems.ItemsSource = list;
        }
    }
}
