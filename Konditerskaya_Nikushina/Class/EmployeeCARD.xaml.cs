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
    /// Логика взаимодействия для EmployeeCARD.xaml
    /// </summary>
    public partial class EmployeeCARD : UserControl
    {
        public KonditerskayaEntities context { get; set; }
        public Employees employee { get; set; }
        public bool IsAdminAccess { get; set; }
        public EmployeeCARD()
        {
            InitializeComponent();
        }

        public void Update()
        {
            firstnameContainer.Text = employee.First_name;
            lastnameContainer.Text = employee.Last_name;
            patronimycContainer.Text = employee.Patronymic;
            jobpositionContainer.Text = employee.Job_position;
            if (IsAdminAccess)
            {
                var user = context.User.FirstOrDefault(u => u.user_code == employee.ID);

                if (user != null)
                { 
                    loginContainer.Text = "Логин: " + user.Login;
                    passwordContainer.Text = "Пароль: " + user.Password;
                }
            }
            else
            {
                loginContainer.Visibility = Visibility.Hidden;
                passwordContainer.Visibility = Visibility.Hidden;
            }
            DeleteButton.Visibility = IsAdminAccess
           ? Visibility.Visible
           : Visibility.Collapsed;

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Запрос подтверждения
            if (MessageBox.Show(
                "Вы уверены, что хотите удалить сотрудника?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning) != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                // Удаление связанной записи пользователя
                var user = context.User.FirstOrDefault(u => u.user_code == employee.ID);
                if (user != null)
                {
                    context.User.Remove(user);
                }

                // Сохранение в БД
                context.SaveChanges();

                // Удаление из интерфейса
                ((WrapPanel)this.Parent).Children.Remove(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления: {ex.Message}");
            }
        }
    }
}
