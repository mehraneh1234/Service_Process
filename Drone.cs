using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceProcess
{      // A separate class file to hold the data items of the Drone. Use separate getter and setter methods,
       // ensure the attributes are private and the accessor methods are public. Add a display method that returns a string
       // for Client Name and Service Cost. Add suitable code to the Client Name and Service Problem accessor methods so the
       // data is formatted as Title case or Sentence case. Save the class as “Drone.cs”.
    internal class Drone
    {
        // The default constructor 
        public Drone()
        {
        }// Decalare private variables
        private string _clientName;
        private string _droneModel;
        private string _serviceProblem;
        private double _serviceCost;
        private string _serviceTag;
        // Getter methods to get the values of the fields from the class
        public string GetClientName()
        {
            return _clientName;
        }
        public string GetDroneModel()
        {
            return _droneModel;
        }
        public string GetServiceProblem()
        {
            return _serviceProblem;
        }
        public double GetServiceCost()
        {
            return _serviceCost;
        }
        public string GetServiceTag()
        {
            return _serviceTag;
        }
        // Setter methods to set the values of the fields in the class
        public void SetClientName(string clientName)
        {
            if (clientName ==null)
            {
                _clientName = "Unknown";
            }
            else
            {
                _clientName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(clientName);
            }
        }
        public void SetServiceProblem(string serviceProblem)
        {
            _serviceProblem = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(serviceProblem);
        }
        public void SetDroneModel(string droneModel)
        {
            _droneModel = droneModel;
        }
        public void SetServiceCost(double serviceCost)
        {
            if (serviceCost < 0)
            {
                _serviceCost = 49.99;
            }
            else
            {
                _serviceCost = serviceCost;
            }
            
        }
        public void SetServiceTag(string serviceTag)
        {
            _serviceTag = serviceTag;
        }
        // This method is for how display the finished service in the finish list box.
        public string DisplayFinishService()
        {
            return GetClientName() + " : $" + GetServiceCost();
        }
    }
}
