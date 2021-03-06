﻿using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Frontend.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;

namespace Telerik.Sitefinity.Frontend.Designers
{
    /// <summary>
    /// This class contains logic for resolving the designer responsible for the property editing of a widget.
    /// </summary>
    public class DesignerResolver : IDesignerResolver
    {
        #region Public members

        /// <summary>
        /// Gets the widget designer URL based on the widget type. 
        /// If there is a record in the <see cref="DesignerResolver.Registry"/> for this widget type it would be retrieved with biggest priority.
        /// Otherwise the URL specified by <see cref="Telerik.Sitefinity.Frontend.Designer.DesignerUrlAttribute"/> will be retrieved.
        /// If the URL is not specified explicitly for a MVC widget this method will retrieve the default designer <see cref="Telerik.Sitefinity.Frontend.Mvc.Controllers.DesignerController"/>.
        /// If null then the default property editor URL should be used.
        /// </summary>
        /// <param name="widgetType">Type of the widget.</param>
        /// <exception cref="ArgumentNullException">widgetType</exception>
        public string GetUrl(Type widgetType)
        {
            if (widgetType == null)
                throw new ArgumentNullException("widgetType");

            string designerUrl;

            if (this.TryResolveUrlFromAttribute(widgetType, out designerUrl))
                return designerUrl;
            else
               return this.GetDefaultUrl(widgetType);
        }

        #endregion 

        #region Private members
        
        /// <summary>
        /// Resolve a designer URL for the specified widget type if such is specified with <see cref="DesignerUrlAttribute"/>.
        /// </summary>
        /// <param name="widgetType">Type of the widget.</param>
        /// <param name="designerUrl">The designer URL.</param>
        /// <returns></returns>
        private bool TryResolveUrlFromAttribute(Type widgetType, out string designerUrl)
        {
            bool designerAttrExists = false;
            designerUrl = null;
            var attributes = widgetType.GetCustomAttributes(typeof(DesignerUrlAttribute), inherit: true);
            var designerAttr = attributes.FirstOrDefault() as DesignerUrlAttribute;

            if (designerAttr != null)
            {
                designerUrl = designerAttr.Url;
                designerAttrExists = true;
            }

            return designerAttrExists;
        }

        /// <summary>
        /// Gets the default designer URL.
        /// </summary>
        /// <param name="widgetName">Name of the widget.</param>
        /// <returns></returns>
        private string GetDefaultUrl(Type widgetType)
        {
            string designerUrl = null;
            var controllerRegistry = FrontendManager.ControllerFactory;
            bool isController = controllerRegistry.IsController(widgetType);

            if (isController)
            {
                string controllerName = controllerRegistry.GetControllerName(widgetType);
                designerUrl = string.Format(DesignerResolver.defaultActionUrlTemplate, controllerName);
            }
            
            return designerUrl;
        }

        #endregion

        #region Constants
        
        private const string defaultActionUrlTemplate = "~/Telerik.Sitefinity.Frontend/Designer/GetDesigner/{0}";

        #endregion 
    }
}
