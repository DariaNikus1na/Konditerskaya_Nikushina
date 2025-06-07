using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Konditerskaya_Nikushina.Class
{
    /// <summary>
    /// Логика взаимодействия для ProductCard.xaml
    /// </summary>
    public partial class ProductCard : UserControl
    {
        public KonditerskayaEntities context { get; set; }
        public Product product { get; set; }
        public bool IsAdminAccess { get; set; }
        private string Image { get; set; }
        public ProductCard()
        {
            InitializeComponent();
        }

        public void Update()
        {
            nameContainer.Text = product.Name;
            descContainer.Text = product.Description;
            ingridientsContainer.Text = "Состав: " + product.Composition;
            priceContainer.Text = product.Price.ToString() + " руб.";
            expiryContainer.Text = "Срок хранения: " + product.Expiry_date.ToString() + " сут.";

            DeleteButton.Visibility = IsAdminAccess
           ? Visibility.Visible
           : Visibility.Collapsed;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Запрос подтверждения
            if (MessageBox.Show(
                "Вы уверены, что хотите удалить продукцию?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning) != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                // 1. Перезагружаем продукт из текущего контекста
                var productToDelete = context.Product.Find(this.product.product_code);

                if (productToDelete == null)
                {
                    MessageBox.Show("Продукт не найден в базе данных");
                    return;
                }

                // 2. Удаляем продукт
                context.Product.Remove(productToDelete);

                // 3. Сохраняем изменения
                context.SaveChanges();

                // 4. Удаляем из интерфейса
                if (this.Parent is Panel parentPanel)
                {
                    parentPanel.Children.Remove(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления: {ex.Message}\n\n{ex.InnerException?.Message}");
            }
        }
    }
    
}
