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

namespace Repository_Teme_Geometrie_Computationala
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitBaseWeeks();
            GenerateButtons(14);
        }
        List<BaseWeek> weeks = new List<BaseWeek>();

        private void GenerateButtons(int nrOfButtons)
        {
            for (int i = 1; i <= nrOfButtons; i++)
            {
                WeekButton button = new WeekButton(i, this);

                SetWeeksPanel.Children.Add(button);
            }
        }

        public void InitBaseWeeks()
        {
            weeks.Clear();
            weeks.Add(new Week1(this));
            weeks.Add(new Week1(this));
            weeks.Add(new Week1(this));
            weeks.Add(new Week1(this));
            weeks.Add(new Week1(this));
            weeks.Add(new Week1(this));
            weeks.Add(new Week1(this));
            weeks.Add(new Week1(this));
            weeks.Add(new Week1(this));
            weeks.Add(new Week1(this));
            weeks.Add(new Week1(this));
            weeks.Add(new Week1(this));
            weeks.Add(new Week1(this));
            weeks.Add(new Week1(this));
            weeks.Add(new Week1(this));
        }
        public BaseWeek GetWeek(int index)
        {
            return weeks[index];
        }
    }
}
