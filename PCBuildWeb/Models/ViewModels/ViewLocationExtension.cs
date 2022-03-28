using Microsoft.AspNetCore.Mvc.Razor;

namespace PCBuildWeb.Models.ViewModels
{
    public class ViewLocationExtension : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context) { }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            return new[]
            {
                "/Views/Parts/{1}/{0}.cshtml",
                "/Views/Properties/{1}/{0}.cshtml"
            }.Union(viewLocations);
        }
    }
}
