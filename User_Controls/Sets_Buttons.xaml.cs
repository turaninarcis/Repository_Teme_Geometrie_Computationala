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

namespace Repository_Teme_Geometrie_Computationala.User_Controls
{
    /// <summary>
    /// Interaction logic for Sets_Buttons.xaml
    /// </summary>
    public partial class Sets_Buttons : UserControl
    {
        static int numberOfWeeks=14;
        MainWindow mainWindow;
        public Sets_Buttons()
        {
            InitializeComponent();
        }
        public void Init(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            GenerateButtons(numberOfWeeks);

        }

        private void GenerateButtons(int nrOfButtons)
        {
            for(int i = 1; i<= nrOfButtons;i++) 
            {
                WeekButton button= new WeekButton($"..\\..\\..\\Probleme\\Saptamana{i}.txt",mainWindow);
                
                button.Name = "buttonWeek" + i;
                button.Width =stackPanelButoane.Width;
                button.Content= "Week "+i;
                button.RenderSize= stackPanelButoane.RenderSize;
                button.Height = 50;
                stackPanelButoane.Children.Add(button);
            }
        }
    }
}
