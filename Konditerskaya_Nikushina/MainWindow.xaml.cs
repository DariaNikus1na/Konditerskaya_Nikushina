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

namespace Konditerskaya_Nikushina
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private KonditerskayaEntities _context;
        public MainWindow()
        {
            InitializeComponent();
            _context = new KonditerskayaEntities();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = txtBoxLogin.Text;
            string password = txtBoxPassword.Password;

            // Проверка на пустые поля
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите логин и пароль");
                return;
            }

            // Поиск пользователя в базе данных
            var user = _context.User.FirstOrDefault(u => u.Login == login && u.Password == password);

            if (user != null)
            {
                MessageBox.Show($"Добро пожаловать, {user.First_name} {user.Last_name}!");
                switch (user.Position_code)
                {
                    case 1: { Window window = new AdminAccess(); window.Show(); this.Close(); }; break;
                    case 2: case 3: { Window window = new AccountingAccess(); window.Show(); this.Close(); } break;
                    case 5: case 6: { Window window = new WarehouseAcces(); window.Show(); this.Close(); } break;
                    case 7: case 8: { Window window = new ManagementAccess(); window.Show(); this.Close(); } break;
                    case 4: { Window window = new AnalyticsAccess(); window.Show(); this.Close(); } break;
                        
                }
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Result = MessageBox.Show("Вы действительно хотите выйти?", "Выход...", MessageBoxButton.YesNo, MessageBoxImage.Question); if (Result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
