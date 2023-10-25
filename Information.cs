using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProcess
{
    internal class Information
    {
        public Information()
        {

        }
        private string _clientName;
        private string _droneModel;
        private string _serviceProblem;
        private string _serviceCost;
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
        public string GetServiceCost()
        {
            return _serviceCost;
        }
        public string GetServiceTag()
        {
            return _serviceTag;
        }
        public void SetCientName(string clientName)
        {
            _clientName = clientName;
        }
        public void SetServiceProblem(string serviceProblem)
        {
            _serviceProblem = serviceProblem;
        }
        public void SetDroneModel(string droneModel)
        {
            _droneModel = droneModel;
        }
        public void SetServiceCost(string serviceCost)
        {
            _serviceCost = serviceCost;
        }
        public void SetServiceTag(string serviceTag)
        {
            _serviceTag = serviceTag;
        }
    }
}
