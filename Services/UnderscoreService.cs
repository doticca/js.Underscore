using System;
using System.Collections.Generic;
using System.Linq;
using Orchard;
using Orchard.Caching;
using Orchard.Environment.Extensions;
using js.Underscore.Models;

namespace js.Underscore.Services
{
    public interface IUnderscoreService : IDependency
    {
        bool GetAutoEnable();
        bool GetAutoEnableAdmin();
    }

    public class UnderscoreService : IUnderscoreService
    {
        private readonly IWorkContextAccessor _wca;
        private readonly ICacheManager _cacheManager;
        private readonly ISignals _signals;

        public UnderscoreService(IWorkContextAccessor wca, ICacheManager cacheManager, ISignals signals)
        {
            _wca = wca;
            _cacheManager = cacheManager;
            _signals = signals;
        }
        public bool GetAutoEnable()
        {
            return _cacheManager.Get(
                "js.Underscore.AutoEnable",
                ctx =>
                {
                    ctx.Monitor(_signals.When("js.Underscore.Changed"));
                    WorkContext workContext = _wca.GetContext();
                    var underscoreSettings =
                        (UnderscoreSettingsPart)workContext
                                                  .CurrentSite
                                                  .ContentItem
                                                  .Get(typeof(UnderscoreSettingsPart));
                    return underscoreSettings.AutoEnable;
                });
        }
        public bool GetAutoEnableAdmin()
        {
            return _cacheManager.Get(
                "js.Underscore.AutoEnableAdmin",
                ctx =>
                {
                    ctx.Monitor(_signals.When("js.Underscore.Changed"));
                    WorkContext workContext = _wca.GetContext();
                    var underscoreSettings =
                        (UnderscoreSettingsPart)workContext
                                                  .CurrentSite
                                                  .ContentItem
                                                  .Get(typeof(UnderscoreSettingsPart));
                    return underscoreSettings.AutoEnableAdmin;
                });
        }
    }
}