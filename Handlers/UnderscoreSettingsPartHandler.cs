﻿using js.Underscore.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace js.Underscore.Handlers
{
    public class UnderscoreSettingsPartHandler: ContentHandler {
        public UnderscoreSettingsPartHandler(IRepository<UnderscoreSettingsPartRecord> repository)
        {
            T = NullLocalizer.Instance;
            Filters.Add(StorageFilter.For(repository));
            Filters.Add(new ActivatingFilter<UnderscoreSettingsPart>("Site"));

            OnInitializing<UnderscoreSettingsPart>((context, part) =>
            {
                part.AutoEnable = true;
                part.AutoEnableAdmin = true;
            });
        }

        public Localizer T { get; set; }

        protected override void GetItemMetadata(GetContentItemMetadataContext context) {
            if (context.ContentItem.ContentType != "Site")
                return;
            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("Underscore")));
        }
    }
}