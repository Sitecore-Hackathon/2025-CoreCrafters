using System;
using System.Web;

using Sitecore;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;
using Sitecore.Web.UI.Sheer;

namespace SitecoreHackathon.Commands
{
    public class FridayAI : Command
    {
		public override void Execute(CommandContext context)
		{
			Sitecore.Context.ClientPage.Start(this, "Run", context.Parameters);
		}
		protected static void Run(ClientPipelineArgs args)
		{
			if (!args.IsPostBack) // First-time execution
			{
				UrlString urlString = new UrlString(UIUtil.GetUri("control:CustomForm"));
				SheerResponse.ShowModalDialog(urlString.ToString(), "800", "300", "", true);
				//UrlString urlString = new UrlString(UIUtil.GetUri("control:ChatForm")); // Ensure correct path
				//SheerResponse.ShowModalDialog(urlString.ToString(), "400", "500", "", true);
				args.WaitForPostBack(); // Wait for user interaction
			}
			else // Postback event
			{
				if (args.HasResult)
				{
					SheerResponse.Alert("Dialog closed with result: " + args.Result);
				}
			}
			//if (!args.IsPostBack)
			//{
			//	UrlString urlString = new UrlString("/sitecore/shell/Applications/ChatForm.aspx");
			//	SheerResponse.ShowModalDialog(urlString.ToString(), "800", "300", "", true);
			//}

		}
	}
}