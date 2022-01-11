using System.Web;
using System.Web.Optimization;

namespace ProvaCandidato
{
  public class BundleConfig
  {
    // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
    public static void RegisterBundles(BundleCollection bundles)
    {
      bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                  "~/Assets/Scripts/jquery-{version}.js",
                  "~/Assets/Scripts/jquery-ui-{version}.js"));

      bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                  "~/Assets/Scripts/jquery.validate*"));

      // Use the development version of Modernizr to develop with and learn from. Then, when you're
      // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
      bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                  "~/Assets/Scripts/modernizr-*"));

      bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Assets/Scripts/bootstrap.js",
                "~/Assets/Scripts/geral.js",
                "~/Assets/Scripts/respond.js"));

      bundles.Add(new StyleBundle("~/Assets/css").Include(
                "~/Assets/StyleSheets/bootstrap.css",
                "~/Assets/StyleSheets/geral.css",
                "~/Assets/themes/base/jquery*"));
    }
  }
}
