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
    /// Mehraneh - 30062786 - AT3 - Drone Srivice Management System
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Declare queues and list of a class object 
        Queue<Drone> RegularService = new Queue<Drone>();
        Queue<Drone> ExpressService = new Queue<Drone>();
        List<Drone> FinishedList = new List<Drone>();
        // Constructor of the class which calls two methods 
        public MainWindow()
        {
            InitializeComponent();
            FillComboBox(); 
            
        }
        // This method makes a numeric control which minimum value is 100, maximum is 900 with increments of 10. 
        private void FillComboBox()
        {
            for (int i = 100; i <= 900; i +=10)
            {
                cbTag.Items.Add(i);
            }
            // Set the default value to 100
            cbTag.SelectedIndex = 0; 
        }
        // AddNewItem_Click method that will add a new service item to a Queue<> based on the priority
        private void AddNewItem_Click(object sender, RoutedEventArgs e)
        {
            // To verify that all input controllers are not null
            if (!string.IsNullOrEmpty(tbClientName.Text) &&
                !string.IsNullOrEmpty(tbDroneModel.Text) &&
                !string.IsNullOrEmpty(tbServiceProblem.Text) &&
                !string.IsNullOrEmpty(tbServiceCost.Text))
            {// Make an instance of the class to add the new values of the fields
                Drone droneInstance = new Drone();
                droneInstance.SetClientName(tbClientName.Text);
                droneInstance.SetDroneModel(tbDroneModel.Text);
                droneInstance.SetServiceProblem(tbServiceProblem.Text);
                droneInstance.SetServiceCost(Double.Parse(tbServiceCost.Text));
                droneInstance.SetServiceTag(cbTag.Text);
                // If the client chose express then the cost of the srvice will multiply to 1.15 then add the data to the express queue
                if (GetServicePriority() == 1)
                {
                    droneInstance.SetServiceCost(Double.Parse(tbServiceCost.Text) * 1.15);
                    ExpressService.Enqueue(droneInstance);
                    DisplayExpressService();
                    ClearTextboxes(); 
                }
                else // If the priority is regular then add the data to the regular queue
                {
                    RegularService.Enqueue(droneInstance);
                    DisplayRegularService();
                    ClearTextboxes();
                }// increment 1 to the tag
                IncrementTag();
            }
            else
            {
                MessageBox.Show("PLease enter all data");
            }
        }
        // Declare the priority in regards to which radio buttons is checked.
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
        // This method displays the regular service queue in the related list view.
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
        // This method displays the express service queue in the related list view.
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
        // This method displays the finish service list in the related list box.
        public void DisplayFinishService()
        {
            lbFinishService.Items.Clear();
            foreach (Drone d in FinishedList)
            {
                lbFinishService.Items.Add(d.DisplayFinishService());
            }
        }
        // By click on each item in the regular list view, all fields of that row display in the related text boxes.
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
                rbRegular.IsChecked = true;
            }
            else
            {
                lvRegularService.UnselectAll();
            }
        }
        //  // By click on each item in the express list view, all fields of that row display in the related text boxes.
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
                rbExpress.IsChecked = true;
            }
            else
            {
                lvExpressService.UnselectAll();
            }
        }
        // To clear all text boxes and dispaly after that the related textes
        public void ClearTextboxes()
        {
            tbClientName.Clear(); tbClientName.Foreground = Brushes.CadetBlue; tbClientName.Text = "Enter Full Name";
            tbDroneModel.Clear(); tbDroneModel.Foreground = Brushes.CadetBlue; tbDroneModel.Text = "Enter Drone Model";
            tbServiceCost.Clear(); tbServiceCost.Foreground = Brushes.CadetBlue; tbServiceCost.Text = "125";
            tbServiceProblem.Clear(); tbServiceProblem.Foreground = Brushes.CadetBlue; tbServiceProblem.Text = "Enter Service Problem";
            rbRegular.IsChecked = true;
        }
        // By default add 10 to the selected item in the tag combobox.
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
        // This method makes the Service Cost textbox can only accept a double value with two decimal point.
        private void tbServiceCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-1.]*[.,]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }
        // By clicking on the finish in front of regular list view, the first inserted item of that queue will be deleted 
        // from the queue and displays in the finish list box.
        private void btnFinishRegular_Click(object sender, RoutedEventArgs e)
        {
            if (RegularService.Count > 0)
            {
                FinishedList.Add(RegularService.Dequeue());
                DisplayRegularService();
                DisplayFinishService();
            }
        }
        // By clicking on the finish in front of express list view, the first inserted item of that queue will be deleted 
        // from the queue and displays in the finish list box.
        private void btnFinishExpress_Click(object sender, RoutedEventArgs e)
        {
            if (ExpressService.Count > 0)
            {
                FinishedList.Add(ExpressService.Dequeue());
                DisplayExpressService();
                DisplayFinishService();
            }
        }
        // By double click on each item in the finish list box that item will be deleted from the list.
        private void lbFinishService_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbFinishService.SelectedItem != null)
            {
                int index = lbFinishService.SelectedIndex;
                FinishedList.RemoveAt(index);
                DisplayFinishService();
            }
        }
        // By clicking on each text boxes controller the default text will be cleared and the color of the text will be black.
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
