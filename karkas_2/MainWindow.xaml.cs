using karkas_2.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Security.Cryptography;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

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
            Thread thread = new Thread(oneupdateDate) {IsBackground = true };
            thread.Start();

            

            filter.SelectedIndex = 0;
            sortirov.SelectedIndex = 0;
            createnav();


        }

        private void oneupdateDate()
        {
            //if (sortirov.SelectedItem == null || filter == null || bazaList == null) return;

           
            var data = Mihailova_demo2Entities.getContext().agent.Select(a => a);

            var fil = Mihailova_demo2Entities.getContext().agent.Select(a => a.Type_agent).Distinct().Prepend("Все типы").ToList();
            var db = data.Take(10).ToList();
 
            foreach (var abs in db)
            {
                int date = DateTime.Now.Year;
                int PG = date - 2022;

                var P_A = abs.product_agent.Where(p => p.Data_real.Year > PG && p.Data_real.Year < date).Select(a => a.Count_prod).Sum();

                abs.Sale = (int)P_A;

                double skidka = (double)abs.product_agent.Select(a => a.Count_prod * a.product.min_cost).Sum();

                if (skidka < 10000 && skidka > 0)
                {
                    abs.Skid = 0;
                }
                else if (skidka > 10000 && skidka < 50000)
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
                    abs.Foreground = "#008000";
                }



                if (abs.Logo == null)

                {
                    abs.Logo = "agents/picture.png";
                }

            }


            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                filter.ItemsSource = fil;
                bazaList.ItemsSource = db;
                createnav();

            }));
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
            string ps = poisk.Text;


            if (tip != "Все типы")
            {
                data = data.Where(p => p.Type_agent == tip && p.Name_agent.Contains(ps));

            }
            else
            {
                data = data.Where(p => p.Name_agent.Contains(ps));

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
                data = data.OrderBy(a => a.Skid);
            }
            else if (text.Equals("Скидка по убыванию"))
            {
                data = data.OrderByDescending(a => a.Skid);
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

                var P_A = abs.product_agent.Where(p => p.Data_real.Year > PG && p.Data_real.Year < date).Select(a => a.Count_prod).Sum();

                abs.Sale = (int)P_A;

                double skidka = (double)abs.product_agent.Select(a => a.Count_prod * a.product.min_cost).Sum();

                if (skidka < 10000 && skidka > 0)
                {
                    abs.Skid = 0;
                }
                else if (skidka > 10000 && skidka < 50000)
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
                    abs.Foreground = "#008000";
                }



                if (abs.Logo == null)

                {
                    abs.Logo = "agents/picture.png";
                }

            }

            bazaList.ItemsSource = db;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            agent ag = ((Button)sender).DataContext as agent;
            var w = new read();
            w.init(ag);
            w.ShowDialog();
            updateDate();
        }

        private void bazaList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement)e.OriginalSource).DataContext as agent;
            if (item != null)
            {
                var w = new read();
                w.init(item);
                w.ShowDialog();
                updateDate();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            agent ag = ((Button)sender).DataContext as agent;
            var w = new read();
            w.ShowDialog();
            updateDate();
        }

        public void bazaList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int ch = bazaList.SelectedItems.Count;
            if (ch > 1)
            {
                read_pr.Visibility = Visibility.Visible;
            }
            else
            {
                read_pr.Visibility = Visibility.Hidden;

            }

        }



        public void Button_Click_4(object sender, RoutedEventArgs e)
        {

            List<agent> b_sel = bazaList.SelectedItems.Cast<agent>().ToList();

            var w = new Pri_Read();
            w.init(b_sel);
            w.ShowDialog();
            updateDate();
        }

        private void poisk_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            updateDate();
            createnav();

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
