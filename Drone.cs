using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceProcess
{
    internal class Drone
    {
        public Drone()
        {

        }
        private string _clientName;
        private string _droneModel;
        private string _serviceProblem;
        private double _serviceCost;
        private string _serviceTag;

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
                _serviceCost = 49.9;
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
        public string DisplayFinishService()
        {
            return GetClientName() + " : $" + GetServiceCost();
        }
    }
}
