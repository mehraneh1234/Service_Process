using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        
        
        Queue<Drone> RegularService = new Queue<Drone>();
        Queue<Drone> ExpressService = new Queue<Drone>();
        List<Drone> FinishedList = new List<Drone>();
        //private int currentTag = 100;


        public MainWindow()
        {
            InitializeComponent();
            FillComboBox(); 
            
        }

       private void FillComboBox()
        {
            for (int i = 100; i <= 900; i +=10)
            {
                cbTag.Items.Add(i);
            }
            // Set the default value to 100
            cbTag.SelectedIndex = 0; 
        }

        

        private void AddNewItem_Click(object sender, RoutedEventArgs e)
        {
            //tbServiceCost.Text = "125";
            if (!string.IsNullOrEmpty(tbClientName.Text) &&
                !string.IsNullOrEmpty(tbDroneModel.Text) &&
                !string.IsNullOrEmpty(tbServiceProblem.Text) &&
                !string.IsNullOrEmpty(tbServiceCost.Text))
            {
                Drone droneInstance = new Drone();
                droneInstance.SetClientName(tbClientName.Text);
                droneInstance.SetDroneModel(tbDroneModel.Text);
                droneInstance.SetServiceProblem(tbServiceProblem.Text);
                droneInstance.SetServiceCost(Double.Parse(tbServiceCost.Text));
                droneInstance.SetServiceTag(cbTag.Text);

                if (GetServicePriority() == 1)
                {
                    droneInstance.SetServiceCost(Double.Parse(tbServiceCost.Text) * 1.15);
                    ExpressService.Enqueue(droneInstance);
                    DisplayExpressService();
                    ClearTextboxes(); 
                }
                else
                {
                    RegularService.Enqueue(droneInstance);
                    DisplayRegularService();
                    ClearTextboxes();
                }
                IncrementTag();
            }
            else
            {
                MessageBox.Show("PLease enter all data");
            }
        }

        private int GetServicePriority()
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
            return priority;
        }

        public void DisplayRegularService()
        {
            lvRegularService.Items.Clear();
            foreach (Drone dr in RegularService)
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
            lvExpressService.Items.Clear();
            foreach (Drone dr in ExpressService)
            {
                lvExpressService.Items.Add(new
                {
                    Name = dr.GetClientName(),
                    Model = dr.GetDroneModel(),
                    Problem = dr.GetServiceProblem(),
                    Cost = dr.GetServiceCost(),
                    Tag = dr.GetServiceTag()
                });
            }
        }

        public void DisplayFinishService()
        {
            lbFinishService.Items.Clear();
            foreach (Drone d in FinishedList)
            {
                lbFinishService.Items.Add(d.DisplayFinishService());
            }
        }

        private void ListViewDisplayRegular_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lvRegularService.SelectedIndex != -1)
            {
                int index = lvRegularService.SelectedIndex;
                tbClientName.Text = RegularService.ElementAt(index).GetClientName();
                tbDroneModel.Text = RegularService.ElementAt(index).GetDroneModel();
                tbServiceProblem.Text = RegularService.ElementAt(index).GetServiceProblem();
                tbServiceCost.Text = RegularService.ElementAt(index).GetServiceCost().ToString();
                cbTag.Text = RegularService.ElementAt(index).GetServiceTag();
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
                tbClientName.Text = ExpressService.ElementAt(index).GetClientName();
                tbDroneModel.Text = ExpressService.ElementAt(index).GetDroneModel();
                tbServiceProblem.Text = ExpressService.ElementAt(index).GetServiceProblem();
                tbServiceCost.Text = ExpressService.ElementAt(index).GetServiceCost().ToString();
                cbTag.Text = ExpressService.ElementAt(index).GetServiceTag();
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
            tbServiceCost.Clear(); tbServiceCost.Foreground = Brushes.CadetBlue; tbServiceCost.Text = "125";
            tbServiceProblem.Clear(); tbServiceProblem.Foreground = Brushes.CadetBlue; tbServiceProblem.Text = "Enter Service Problem";
            rbRegular.IsChecked = true;
        }

        private void IncrementTag()
        {
            int currentTag = (int)cbTag.SelectedValue;
            currentTag += 10;
            if (currentTag <=900)
            {
                cbTag.Items.Add(currentTag);
                cbTag.SelectedItem = currentTag;
            }
        }

       
        private void tbServiceCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-1.]*[.,]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void btnFinishRegular_Click(object sender, RoutedEventArgs e)
        {
            if (RegularService.Count > 0)
            {
                FinishedList.Add(RegularService.Dequeue());
                DisplayRegularService();
                DisplayFinishService();
            }
        }

        private void btnFinishExpress_Click(object sender, RoutedEventArgs e)
        {
            if (ExpressService.Count > 0)
            {
                FinishedList.Add(ExpressService.Dequeue());
                DisplayExpressService();
                DisplayFinishService();
            }
        }

        private void lbFinishService_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbFinishService.SelectedItem != null)
            {
                int index = lbFinishService.SelectedIndex;
                FinishedList.RemoveAt(index);
                DisplayFinishService();
            }
        }

        private void tbClientName_GotFocus(object sender, RoutedEventArgs e)
        {
            tbClientName.Clear(); tbClientName.Foreground = Brushes.Black;
        }
        private void tbDroneModel_GotFocus(object sender, RoutedEventArgs e)
        {
            tbDroneModel.Clear(); tbDroneModel.Foreground = Brushes.Black;
        }
        private void tbServiceProblem_GotFocus(object sender, RoutedEventArgs e)
        {
            tbServiceProblem.Clear(); tbServiceProblem.Foreground = Brushes.Black;
        }
        private void tbServiceCost_GotFocus(object sender, RoutedEventArgs e)
        {
            tbServiceCost.Clear(); tbServiceCost.Foreground = Brushes.Black;
        }

        
    }

    public class MainDiagram
    {
    }
   
}
