using Sitecore.Mvc.Controllers;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using SitecoreHackathon.Models;
using Sitecore.Diagnostics;
using System;
using Sitecore.Data.Items;
using Sitecore.Data.Masters;
using Sitecore.Data;
using Sitecore.Mvc.Names;
using Sitecore.SecurityModel;
using Sitecore.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using System;
using System.Linq;
using Sitecore.Jobs;
using Sitecore.Abstractions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Web;
using System.Web.UI.WebControls;

using System.IO;
using static Sitecore.Configuration.State;
using System.Web.UI.WebControls;
using System.Web.Services.Description;
using System.Text;
using System.Collections;
using Sitecore.Resources.Media;
using System.Net;
using System.Threading.Tasks;
using Sitecore.Data.Masters;
using Sitecore.Data.Managers;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using SitecoreHackathon.Models;
using SitecoreHackathon.Controllers;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace SitecoreHackathon.Controllers
{
	public class ItemCreationController : SitecoreController
	{
		// protected FileUpload file; 
		protected TreePicker FileDropdownTreePicker;
		protected Edit FieldNames;
		[HttpPost]
		public async Task<ActionResult> GenerateScript(NewItemRequest request, string templatePath)
		{

			// Validate the request
			if (request == null || string.IsNullOrEmpty(request.Message))
			{
				return Json(new { success = false, message = "Invalid input parameters." });
			}
			Database master = Sitecore.Configuration.Factory.GetDatabase("master");
			try
			{
				// Log the received request for debugging purposes
				Log.Info($"Received request to generate template details: {request.Message}", this);

				// Call the ChatGPT API to generate the template details based on the natural language request
				var scriptService = new ChatGptService();
				var script = await scriptService.GetChatGptResponseAsync(request.Message);

				// Log the generated template details for debugging purposes
				Log.Info($"Generated Template Details: {script}", this);
				var templateName = Regex.Match(script, "templateName = \\\"(.*?)\\\"").Groups[1].Value;
				var sectionName = Regex.Match(script, "sectionName = \\\"(.*?)\\\"").Groups[1].Value;
				var fieldsCsv = Regex.Match(script, "fieldsCsv = \\\"(.*?)\\\"").Groups[1].Value;
				var fieldTypesPipe = Regex.Match(script, "fieldTypesPipe = \\\"(.*?)\\\"").Groups[1].Value;
				Database masterDb = Sitecore.Configuration.Factory.GetDatabase("master");
				using (new SecurityDisabler()) // Disable security to allow changes
				{
					// Define the parent folder where the template should be created
					Item templateFolder = masterDb.GetItem(templatePath);

					if (templateFolder != null)
					{
						// Create the Template Item
						Item newTemplate = templateFolder.Add(templateName, new TemplateItem(masterDb.GetItem(Sitecore.TemplateIDs.Template)));

						if (newTemplate != null)
						{
							newTemplate.Editing.BeginEdit();
							try
							{
								// Create Section
								Item section = newTemplate.Add(sectionName, new TemplateItem(masterDb.GetItem(Sitecore.TemplateIDs.TemplateSection)));

								if (section != null)
								{
									// Split the field names and field types
									string[] fieldNames = fieldsCsv.Split(',');
									string[] fieldTypes = fieldTypesPipe.Split('|');

									if (fieldNames.Length != fieldTypes.Length)
									{
										throw new Exception("Mismatch between field names and field types.");
									}

									for (int i = 0; i < fieldNames.Length; i++)
									{
										string fieldName = fieldNames[i].Trim();
										string fieldType = fieldTypes[i].Trim();

										// Add Field
										Item field = section.Add(fieldName, new TemplateItem(masterDb.GetItem(Sitecore.TemplateIDs.TemplateField)));

										if (field != null)
										{
											field.Editing.BeginEdit();
											field["Type"] = fieldType;
											field.Editing.EndEdit();
										}
									}
								}
							}
							finally
							{
								newTemplate.Editing.EndEdit();
							}

						}
					}
				}
				return Json(new { success = true, templateDetails = script });
			}
			catch (Exception ex)
			{
				// Handle exceptions and provide meaningful error response
				Log.Error("Error generating template details.", ex, this);
				return Json(new { success = false, message = ex.Message });
			}
		}
	}
}
