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

namespace ServiceProcess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Drone[] droneInstanceA = new Drone[5];
        Drone droneInstance = new Drone();
        Queue<Drone> regularServiceQueue = new Queue<Drone>();
        Queue<Drone> expressServiceQueue = new Queue<Drone>();
        List<Drone> finishedList = new List<Drone>();


        public MainWindow()
        {
            InitializeComponent();
        }

        public void DisplayRegularService()
        {
            lvRegularService.Items.Clear();
            foreach(Drone dr in regularServiceQueue)
            {
                lvRegularService.Items.Add(dr);
            }
        }

        public void ClearTextboxes()
        {
            tbClientName.Clear();
            tbDroneModel.Clear();
            tbServiceCost.Clear();
            tbServiceProblem.Clear();

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(tbClientName.Text) &&
                !string.IsNullOrEmpty(tbDroneModel.Text) &&
                !string.IsNullOrEmpty(tbServiceProblem.Text) &&
                !string.IsNullOrEmpty(tbServiceCost.Text) &&
                    !string.IsNullOrEmpty(cbTag.Text))
            {
                droneInstance.SetClientName(tbClientName.Text);
                droneInstance.SetDroneModel(tbDroneModel.Text);
                droneInstance.SetServiceProblem(tbServiceProblem.Text);
                droneInstance.SetServiceCost(tbServiceCost.Text);
                droneInstance.SetServiceTag(cbTag.Text);

                if (rbExpress.IsChecked == true)
                {
                    expressServiceQueue.Enqueue(droneInstance);
                }
                regularServiceQueue.Enqueue(droneInstance);
            }
            else
            {
                MessageBox.Show("PLease enter all data");
            }
        }
    }

    public class MainDiagram
    {
    }
   
}
