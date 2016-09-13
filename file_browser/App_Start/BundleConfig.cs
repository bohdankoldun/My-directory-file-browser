using System.Web;
using System.Web.Optimization;

namespace file_browser
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/angular.js"
                      )); 
        }
    }
}
