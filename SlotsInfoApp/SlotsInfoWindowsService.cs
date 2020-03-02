using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceProcess;
using System.Configuration.Install;

using com.siml.gaming.slots.webinfo.impl;

namespace SlotsInfoApp
{
    public class SlotsInfoWindowsService : ServiceBase
    {

        public ServiceHost serviceHost = null;

        public SlotsInfoWindowsService()
        {
            ServiceName = "SlotsInfoService";
        }

        protected override void OnStart(string[] args)
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }

            serviceHost = new ServiceHost(typeof(SlotsInfoService));
            serviceHost.Open();
        }

        protected override void OnStop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        } 

    }
}
