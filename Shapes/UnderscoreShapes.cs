using System.Linq;
using Orchard;
using Orchard.DisplayManagement.Descriptors;
using Orchard.Environment.Extensions;
using Orchard.UI.Resources;


namespace js.Modernizr.Shapes
{
    public class UnderscoreShapes : IShapeTableProvider
    {
        private readonly IWorkContextAccessor _wca;
        public UnderscoreShapes(IWorkContextAccessor wca)
        {
            _wca = wca;
        }

        public void Discover(ShapeTableBuilder builder)
        {
            builder.Describe("HeadScripts")
                .OnDisplaying(shapeDisplayingContext =>
                {
                    var resourceManager = _wca.GetContext().Resolve<IResourceManager>();
                    var scripts = resourceManager.GetRequiredResources("script");


                    var currentModernizr = scripts
                            .Where(l => l.Name == "Underscore")
                            .FirstOrDefault();

                    if (currentModernizr == null)
                    {
                        resourceManager.Require("script", "Underscore").AtHead();
                    }                    
                });
        }
    }
}