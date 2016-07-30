using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SensorRealTime
{
    public class HubConfig
    {
        public static void RegisterHubRoutes()
        {
            RouteTable.Routes.MapHubs();
        }
    }
}