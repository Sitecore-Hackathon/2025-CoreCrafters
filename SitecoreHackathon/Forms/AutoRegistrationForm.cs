using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using System;
using System.Threading.Tasks;
using SitecoreHackathon.Models;
using SitecoreHackathon.Controllers;

namespace SitecoreHackathon.Forms
{
	public class AutoRegistrationForm : DialogForm
	{
		protected TreePicker TemplatepPath;
		protected Edit Title;

		protected override void OnCancel(object sender, EventArgs args)
		{
			Assert.ArgumentNotNull(sender, "sender");
			Assert.ArgumentNotNull(args, "args");
			base.OnCancel(sender, args);
		}

		protected override void OnOK(object sender, EventArgs args)
		{
			Sitecore.Data.Database master = Sitecore.Configuration.Factory.GetDatabase("master");
			Assert.ArgumentNotNull(sender, "sender");
			Assert.ArgumentNotNull(args, "args");
			SheerResponse.Alert("Your " + Title.Value + " template has been successfully created!", new string[0]);
			Sitecore.Web.UI.Sheer.SheerResponse.CloseWindow();

			NewItemRequest request = new NewItemRequest();
			//fetch template name from the model
			request.Message = Title.Value;
			//fetch template path from the model
			Item templatePath = master.GetItem(TemplatepPath.Value);
			Task.Run(async () =>
			{
				try
				{
					ItemCreationController itemCreateController = new ItemCreationController();
					//generate script and create item
					var input = await itemCreateController.GenerateScript(request, templatePath.ID.ToString());

				}
				catch (Exception ex)
				{
					Log.Error("Error calling OpenAI", ex, this);
				}
			});

		}
	}
}
