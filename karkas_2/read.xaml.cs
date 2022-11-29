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
    /// Логика взаимодействия для read.xaml
    /// </summary>
    public partial class read : Window
    {
        private agent ag;
        public read()
        {
            InitializeComponent();
        }

        internal void init(agent ag)
        {
            this.ag = ag;
            name.Text = ag.Name_agent;
            Type.Text = ag.Type_agent;
            pochta.Text = ag.Pochta;
            phone.Text = ag.Phone.ToString();
            logo.Text = ag.Logo;
            adress.Text = ag.Adress;
            prior.Text = ag.Prioritet.ToString();
            direc.Text = ag.Director;
            I_a.Text = ag.INN.ToString();
            K_a.Text = ag.KPP.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(name.Text))
            {
                MessageBox.Show("Заполните поле Наименование");
                return;
            }
            if (String.IsNullOrEmpty(Type.Text))
            {
                MessageBox.Show("Заполните поле Тип");
                return;
            }
            if (String.IsNullOrEmpty(pochta.Text))
            {
                MessageBox.Show("Заполните поле Почта");
                return;
            }
            if (String.IsNullOrEmpty(logo.Text))
            {
                MessageBox.Show("Заполните поле Логотип");
                return;
            }
            if (String.IsNullOrEmpty(adress.Text))
            {
                MessageBox.Show("Заполните поле Адрес");
                return;
            }
            if (String.IsNullOrEmpty(direc.Text))
            {
                MessageBox.Show("Заполните поле Директор");
                return;
            }

            if (ag == null) ag = new agent();
            ag.Name_agent = name.Text;
            ag.Type_agent= Type.Text;
            ag.Pochta = pochta.Text;
            ag.Phone = double.Parse(phone.Text);
            ag.Logo = logo.Text;
            ag.Adress = adress.Text;
            ag.Prioritet = double.Parse(prior.Text);
            ag.Director = direc.Text;
            ag.INN = double.Parse(I_a.Text);
            ag.KPP = double.Parse(K_a.Text);
           

            if (ag.ID != 0)
            {
                MessageBox.Show("Изменения сохранены");
            }

            else
            {
                Mihailova_demo2Entities.getContext().agent.Add(ag);
                Mihailova_demo2Entities.getContext().SaveChanges();
                MessageBox.Show("Создан новый агент");
            }

        }
    }
}
