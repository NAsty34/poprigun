using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace karkas_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int nowPage, maxCount;
        public MainWindow()
        {

            InitializeComponent();
            //bazaList.ItemsSource = Mihailova_demo2Entities.getContext().agent.ToList().Take(10);

            filter.ItemsSource = Mihailova_demo2Entities.getContext().agent.Select(a => a.Type_agent).Distinct().Prepend("Все типы").ToList();

            filter.SelectedIndex = 0;
            sortirov.SelectedIndex = 0;
            createnav();
            
        }



        public void createnav()
        {
            nav.Children.Clear();
            Button ba = new Button();
            ba.Content = "<";
            ba.Click += Button_Click;
            nav.Children.Add(ba);
            ba.Style = this.FindResource("navigations") as Style;
            int np = nowPage;
            if (np + 4 > maxCount) np = maxCount - 4;
            if (np < 0)
            {
                np = 0;
            }




            for (int i = np; i < Math.Min(np + 4, maxCount); i++)
            {

                Button b = new Button();
                b.Content = i + 1;
                b.Click += Button_Click;
                nav.Children.Add(b);
                b.Style = this.FindResource("navigations") as Style;

                if (i == nowPage)
                {
                    b.Style = this.FindResource("navPageNow") as Style;
                }
            }


            ba = new Button();
            ba.Content = ">";
            ba.Click += Button_Click;
            nav.Children.Add(ba);
            ba.Style = this.FindResource("navigations") as Style;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sortirov.SelectedItem == null || filter == null || bazaList == null) return;
            nowPage = 0;
            updateDate();
            createnav();
        }

        private void updateDate()
        {

            if (sortirov.SelectedItem == null || filter == null || bazaList == null) return;

            string tip = filter.SelectedItem.ToString();
            string text = ((ComboBoxItem)sortirov.SelectedItem).Content.ToString();
            var data = Mihailova_demo2Entities.getContext().agent.Select(a => a);



            if (tip != "Все типы")
            {
                data = data.Where(p => p.Type_agent == tip);

            }
            if (text.Equals("Наименование по возрастанию"))
            {

                data = data.OrderBy(a => a.Name_agent);

            }
            else if (text.Equals("Наименование по убыванию"))
            {
                data = data.OrderByDescending(a => a.Name_agent);
            }
            else if (text.Equals("Скидка по возрастанию"))
            {
                data = data.OrderBy(a => a.Name_agent);
            }
            else if (text.Equals("Скидка по убыванию"))
            {
                data = data.OrderByDescending(a => a.Name_agent);
            }
            else if (text.Equals("Приоритетнось по возрастанию"))
            {
                data = data.OrderBy(p => p.Prioritet);
            }
            else if (text.Equals("Приоритетнось по убыванию"))
            {
                data = data.OrderByDescending(p => p.Prioritet);
            }
            else
            {
                data = data.OrderBy(p => p.Name_agent);
            }

            var db = data.Skip(nowPage * 10).Take(10).ToList();
            maxCount = (int)Math.Ceiling(data.Count() / 10.0);

            

            foreach (var abs in db)
            {
                int date = DateTime.Now.Year;
                int PG = date - 2022;
                
                var P_A = abs.product_agent.Where(p=>p.Data_real.Year > PG && p.Data_real.Year < date).Select(a => a.Count_prod).Sum();
                
                abs.Sale = (int)P_A;

                double skidka = (double)abs.product_agent.Select(a => a.Count_prod * a.product.min_cost).Sum();
                
                if (skidka < 10000 && skidka>0)
                {
                    abs.Skid = 0;
                }
                else if (skidka>10000 && skidka < 50000)
                {
                    abs.Skid = 5;
                }
                else if (skidka > 50000 && skidka < 150000)
                {
                    abs.Skid = 10;
                }
                else if (skidka > 150000 && skidka < 500000)
                {
                    abs.Skid = 20;
                }
                else
                {
                    abs.Skid = 25;
                }

               

                if (abs.Logo == null)

                {
                    abs.Logo = "agents/picture.png";
                }
                
            }

            bazaList.ItemsSource = db;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Button sen = (Button)sender;
            string con = sen.Content.ToString();
            if (con == "<")
            {
                if (nowPage > 0)
                {
                    nowPage--;
                }
            }
            else if (con == ">")
            {
                if (nowPage + 1 < maxCount)
                {
                    nowPage++;
                }
            }
            else
            {
                nowPage = Convert.ToInt32(con) - 1;
            }
            updateDate();
            createnav();


        }
    }
}
