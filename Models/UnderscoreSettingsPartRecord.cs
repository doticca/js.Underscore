using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace js.Underscore.Models
{
    public class UnderscoreSettingsPartRecord: ContentPartRecord
    {
        public virtual bool AutoEnable { get; set; }
        public virtual bool AutoEnableAdmin { get; set; }

        public UnderscoreSettingsPartRecord()
        {
            AutoEnable = true;
            AutoEnableAdmin = true;
        }
    }
}