using Sitecore.Pipelines;
using System.Web.Mvc;
using System.Web.Routing;

namespace SitecoreHackathon.Infrastructure
{
    public class LoadRoutes
    {
        public void Process(PipelineArgs args)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}