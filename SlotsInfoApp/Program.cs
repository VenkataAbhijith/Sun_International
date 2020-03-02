using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;
using System.ServiceProcess;
using System.ServiceModel;
using System.Configuration;

using System.Data.SqlClient;

using log4net;
using log4net.Config;

using siml.service;

using com.siml.gaming.slots.webinfo.impl;

namespace SlotsInfoApp
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static Dictionary<string, ServiceManagerCommand> _commands = new Dictionary<string, ServiceManagerCommand>
        {
            {"-console", ServiceManagerCommand.Application}, 
            {"-install", ServiceManagerCommand.Install}, 
            {"-uninstall", ServiceManagerCommand.UnInstall},
            {"-start", ServiceManagerCommand.Start}, 
            {"-stop", ServiceManagerCommand.Stop}
        };

        static void Main(string[] args)
        {
            XmlConfigurator.Configure(new System.IO.FileInfo(ConfigurationManager.AppSettings["Log4NetConfigPath"]));

            try
            {
                if (args.Length == 0)
                {
                    ServiceMain();
                    return;
                }

                ServiceManagerCommand command;

                if (!TryParseCommandLine(args, out command))
                {
                    PrintUsage();
                    return;
                }

                ServiceManager serviceManager = new ServiceManager(ConfigurationManager.AppSettings["ServiceName"]);
                if (command == ServiceManagerCommand.Application)
                {
                    RunAsConsole();
                }
                else
                {
                    serviceManager.RunCommand(command);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString(), ex);
            }
        }
        static void ServiceMain()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] { new SlotsInfoWindowsService() };
            ServiceBase.Run(ServicesToRun);
        }
        private static bool TryParseCommandLine(string[] args, out ServiceManagerCommand command)
        {
            command = ServiceManagerCommand.Unknown;

            if (args.Length > 1) return false;

            string commandLineArg = args[0];
            if (_commands.ContainsKey(commandLineArg))
            {
                command = _commands[commandLineArg];
                return true;
            }
            return false;

        }
        private static void PrintUsage()
        {
            string exeName = Assembly.GetExecutingAssembly().ManifestModule.Name;
            log.Info("Usage:");

            foreach (var item in _commands)
            {
                log.Info("  " + exeName + " " + item.Key);
            }
        }
        static void RunAsConsole()
        {
            using (ServiceHost serviceHost = new ServiceHost(typeof(SlotsInfoService)))
            {
                serviceHost.Open();

                // The service can now be accessed.
                log.Info("The service is ready.");
                log.Info("Press <ENTER> to terminate service.");
                log.Info("");
                System.Console.ReadLine();
            }
        }
    }
}
