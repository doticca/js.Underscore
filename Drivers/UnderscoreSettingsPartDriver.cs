using js.Underscore.Models;
using js.Underscore.Services;
using Orchard.Caching;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using js.Underscore.ViewModels;

namespace js.Underscore.Drivers
{
    public class UnderscoreSettingsPartDriver : ContentPartDriver<UnderscoreSettingsPart> 
    {
        private readonly ISignals _signals;
        private readonly IUnderscoreService _underscoreService;

        public UnderscoreSettingsPartDriver(ISignals signals, IUnderscoreService underscoreService)
        {
            _signals = signals;
            _underscoreService = underscoreService;
        }

        protected override string Prefix { get { return "UnderscoreSettings"; } }

        protected override DriverResult Editor(UnderscoreSettingsPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Underscore_UnderscoreSettings",
                               () => shapeHelper.EditorTemplate(
                                   TemplateName: "Parts/Underscore.UnderscoreSettings",
                                   Model: new UnderscoreSettingsViewModel
                                   {
                                       AutoEnable = part.AutoEnable,
                                       AutoEnableAdmin = part.AutoEnableAdmin,
                                   },
                                   Prefix: Prefix)).OnGroup("Underscore");
        }

        protected override DriverResult Editor(UnderscoreSettingsPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part.Record, Prefix, null, null);
            _signals.Trigger("js.Underscore.Changed");
            return Editor(part, shapeHelper);
        }

        protected override void Exporting(UnderscoreSettingsPart part, ExportContentContext context)
        {
            var element = context.Element(part.PartDefinition.Name);

            element.SetAttributeValue("AutoEnable", part.AutoEnable);
            element.SetAttributeValue("AutoEnableAdmin", part.AutoEnableAdmin);
        }

        protected override void Importing(UnderscoreSettingsPart part, ImportContentContext context)
        {
            var partName = part.PartDefinition.Name;

            part.Record.AutoEnable = GetAttribute<bool>(context, partName, "AutoEnable");
            part.Record.AutoEnableAdmin = GetAttribute<bool>(context, partName, "AutoEnableAdmin");
        }

        private TV GetAttribute<TV>(ImportContentContext context, string partName, string elementName)
        {
            string value = context.Attribute(partName, elementName);
            if (value != null)
            {
                return (TV)Convert.ChangeType(value, typeof(TV));
            }
            return default(TV);
        }
    }
}