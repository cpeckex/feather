﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Frontend.Security;
using Telerik.Sitefinity.Frontend.Resources.Resolvers;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Frontend.Mvc.Controllers
{
    /// <summary>
    /// This class contains logic for resolving the views of the MVC designer of a widget.
    /// </summary>
    public class DesignerController : Controller
    {
        /// <summary>
        /// Returns the designer view which handles the property editing for the particular widget. 
        /// If there is custom designer for the particular widget it will be retrieved, otherwise it will fallback to the default designer.
        /// The default designer is located under <see cref="Telerik.Sitefinity.Frontend.Mvc.Views.Designer.Master.Designer.cshtml"/>.
        /// </summary>
        /// <param name="widgetName">The name of the widget.</param>
        public virtual ActionResult GetDesigner(string widgetName)
        {
            FrontendManager.AuthenticationEvaluator.RequestBackendUserAuthentication();

            string desinerName = DesignerController.designerNameTemplate.Arrange(DesignerController.defaultDesignerViewType);
            this.ViewBag.ControlName = widgetName;
            return this.View(desinerName);
        }

        /// <summary>
        /// Returns specific view associated with the widget designer. If such view is available for the particular widget it will be displayed,
        /// otherwise it will try to fallback to the default view with the specified type (located in Telerik.Sitefinity.Frontend.Mvc.Views.Designer),
        /// If default view for the specified view type is not available exception will be thrown.
        /// </summary>
        /// <param name="widgetName">The name of the widget.</param>
        /// <param name="viewType">Type of the view which is requested. For example Simple, Advanced</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">View cannot be found on the searched locations.</exception>
        public ActionResult GetDesignerPartialView(string widgetName, string viewType)
        {
            FrontendManager.AuthenticationEvaluator.RequestBackendUserAuthentication();
       
            string viewName = DesignerController.designerNameTemplate.Arrange(viewType); 

            return this.PartialView(viewName);
        }

        private const string defaultDesignerViewType = "Master";
        private const string designerNameTemplate = "{0}.Designer";
    }
}
