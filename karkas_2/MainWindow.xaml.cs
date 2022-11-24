using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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


namespace karkas_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            bazaList.ItemsSource = Mihailova_demo2Entities.getContext().agent.ToList();
            
            filter.ItemsSource = Mihailova_demo2Entities.getContext().agent.Select(a => a.Type_agent).Distinct().Prepend("Все типы").ToList();
            filter.SelectedIndex = 0;
            sortirov.SelectedIndex = 0;
            if (bazaList.AlternationCount > 10)
            {
                
            }

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sortirov.SelectedItem == null || filter == null || bazaList == null) return;

            string tip = filter.SelectedItem.ToString();
            string text = ((ComboBoxItem)sortirov.SelectedItem).Content.ToString();
            var data = Mihailova_demo2Entities.getContext().agent.Select(a=>a);
            


            if (tip != "Все типы" )
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
            bazaList.ItemsSource = data.ToList();

        }

       
       
    }
}
