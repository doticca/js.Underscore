using Orchard.UI.Resources;

namespace js.Underscore {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();

            manifest.DefineScript("Underscore")
                .SetUrl("underscore-min.js", "underscore.js")
                .SetVersion("1.5.2");
        }
    }
}
