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
        
        Queue<Drone> regularServiceQueue = new Queue<Drone>();
        Queue<Drone> expressServiceQueue = new Queue<Drone>();
        List<Drone> finishedList = new List<Drone>();


        public MainWindow()
        {
            InitializeComponent();
        }

       

        

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(tbClientName.Text) &&
                !string.IsNullOrEmpty(tbDroneModel.Text) &&
                !string.IsNullOrEmpty(tbServiceProblem.Text) &&
                !string.IsNullOrEmpty(tbServiceCost.Text))
            {
                Drone droneInstance = new Drone();
                droneInstance.SetClientName(tbClientName.Text);
                droneInstance.SetDroneModel(tbDroneModel.Text);
                droneInstance.SetServiceProblem(tbServiceProblem.Text);
                droneInstance.SetServiceCost(double.Parse(tbServiceCost.Text));
                droneInstance.SetServiceTag(cbTag.Text);

                if (rbExpress.IsChecked == true)
                {
                    droneInstance.SetServiceCost(Convert.ToDouble(tbServiceCost) * 1.15);
                    expressServiceQueue.Enqueue(droneInstance);
                    DisplayExpressService();
                    ClearTextboxes();
                }
                else
                {
                    regularServiceQueue.Enqueue(droneInstance);
                    DisplayRegularService();
                    ClearTextboxes();
                }
                
            }
            else
            {
                MessageBox.Show("PLease enter all data");
            }
        }

        /* private void GetPriority()
         {
             int priority = 0;
             if (rbRegular.IsChecked == true)
             {
                 priority = 0;
             }
             else if (rbExpress.IsChecked == true)
             {
                 priority = 1;
             }
         }*/

        public void DisplayRegularService()
        {
            lvRegularService.Items.Clear();
            foreach (Drone dr in regularServiceQueue)
            {
                lvRegularService.Items.Add(new
                {
                    ClientName = dr.GetClientName(),
                    DroneModel = dr.GetDroneModel(),
                    ServiceProblem = dr.GetServiceProblem(),
                    ServiceCost = dr.GetServiceCost(),
                    RegularTag = dr.GetServiceTag()
                });
            }
        }

        public void DisplayExpressService()
        {
            lvRegularService.Items.Clear();
            foreach (Drone dr in regularServiceQueue)
            {
                lvRegularService.Items.Add(new
                {
                    Name = dr.GetClientName(),
                    Model = dr.GetDroneModel(),
                    Problem = dr.GetServiceProblem(),
                    Cost = dr.GetServiceCost(),
                    Tag = dr.GetServiceTag()
                });
            }
        }

        private void ListViewDisplayRegular_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lvRegularService.SelectedIndex != -1)
            {
                int index = lvRegularService.SelectedIndex;
                tbClientName.Text = regularServiceQueue.ElementAt(index).GetClientName();
                tbDroneModel.Text = regularServiceQueue.ElementAt(index).GetDroneModel();
                tbServiceProblem.Text = regularServiceQueue.ElementAt(index).GetServiceProblem();
                tbServiceCost.Text = regularServiceQueue.ElementAt(index).GetServiceCost().ToString();
                cbTag.Text = regularServiceQueue.ElementAt(index).GetServiceTag();
            }
            else
            {
                lvRegularService.UnselectAll();
            }
        }
        private void ListViewDisplayExpress_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lvExpressService.SelectedIndex != -1)
            {
                int index = lvExpressService.SelectedIndex;
                tbClientName.Text = expressServiceQueue.ElementAt(index).GetClientName();
                tbDroneModel.Text = expressServiceQueue.ElementAt(index).GetDroneModel();
                tbServiceProblem.Text = expressServiceQueue.ElementAt(index).GetServiceProblem();
                tbServiceCost.Text = expressServiceQueue.ElementAt(index).GetServiceCost().ToString();
                cbTag.Text = expressServiceQueue.ElementAt(index).GetServiceTag();
            }
            else
            {
                lvExpressService.UnselectAll();
            }
        }

        public void ClearTextboxes()
        {
            tbClientName.Clear(); tbClientName.Foreground = Brushes.CadetBlue; tbClientName.Text = "Enter Full Name";
            tbDroneModel.Clear(); tbDroneModel.Foreground = Brushes.CadetBlue; tbDroneModel.Text = "Enter Drone Model";
            tbServiceCost.Clear(); tbServiceCost.Foreground = Brushes.CadetBlue; tbServiceCost.Text = "Enter Service Cost";
            tbServiceProblem.Clear(); tbServiceProblem.Foreground = Brushes.CadetBlue; tbServiceProblem.Text = "Enter Service Problem";

        }

        /*private void IncrementTag()
        {
            // int currentTag = (int)Tag.value;
            //currentTag += 10;

        }*/

        private void tbClientName_GotFocus(object sender, RoutedEventArgs e)
        {
            tbClientName.Clear(); tbClientName.Foreground = Brushes.Black;
        }
        private void tbDroneModel_GotFocus(object sender, RoutedEventArgs e)
        {
            tbDroneModel.Clear(); tbClientName.Foreground = Brushes.Black;
        }
        private void tbServiceProblem_GotFocus(object sender, RoutedEventArgs e)
        {
            tbServiceProblem.Clear(); tbClientName.Foreground = Brushes.Black;
        }
        private void tbServiceCost_GotFocus(object sender, RoutedEventArgs e)
        {
            tbServiceCost.Clear(); tbClientName.Foreground = Brushes.Black;
        }
    }

    public class MainDiagram
    {
    }
   
}
