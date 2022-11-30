using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
using System.Xml.Linq;

namespace karkas_2
{
    /// <summary>
    /// Логика взаимодействия для Pri_Read.xaml
    /// </summary>
    public partial class Pri_Read : Window
    {
        private List<agent> b_set;

        public Pri_Read()
        {
            InitializeComponent();

        }



        internal void init(List<agent> b_sel)
        {
            

            int M_P = 0;
            this.b_set = b_sel;
            foreach (var a in b_sel)
            {
                if (M_P < a.Prioritet)
                {
                    M_P = (int)a.Prioritet;

                }
            }

            pr.Text = M_P.ToString();


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int pr_s;
            try
            {
                pr_s = int.Parse(pr.Text);
            }
            catch
            {
                MessageBox.Show("Введите число");
                return;
            }

            foreach (var a in b_set)
            {
                a.Prioritet = pr_s;

            }

            Mihailova_demo2Entities.getContext().SaveChanges();
            MessageBox.Show("Изменения сохранены");
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
