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
using System.Windows.Shapes;

namespace karkas_2
{
    /// <summary>
    /// Логика взаимодействия для Hello.xaml
    /// </summary>
    public partial class Hello : Window
    {
        public Hello()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (admin.Text == "0000")
            {
                new MainWindow().Show();
                Close();
            }
            else
            {
                MessageBox.Show("Вы ввели неправильный пароль, введите 0000");
            }
        }



        private void Button_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Return)
            {
                Button_Click(null, null);
            }
        }
    }
}
