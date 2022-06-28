using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Optimization;
using IO = System.IO;

namespace KTB.DNet.Interface.WebUI.Libraries
{
    public class StyleImagePathBundle : Bundle
    {
        public StyleImagePathBundle(string virtualPath)
            : base(virtualPath, new IBundleTransform[1]
      {
        (IBundleTransform) new CssMinify()
      })
        {
        }

        public StyleImagePathBundle(string virtualPath, string cdnPath)
            : base(virtualPath, cdnPath, new IBundleTransform[1]
      {
        (IBundleTransform) new CssMinify()
      })
        {
        }

        public new Bundle Include(params string[] virtualPaths)
        {
            if (HttpContext.Current.IsDebuggingEnabled)
            {
                // Debugging. Bundling will not occur so act normal and no one gets hurt.
                base.Include(virtualPaths.ToArray());
                return this;
            }

            // In production mode so CSS will be bundled. Correct image paths.
            var bundlePaths = new List<string>();
            var svr = HttpContext.Current.Server;
            foreach (var path in virtualPaths)
            {
                var pattern = new Regex(@"url\s*\(\s*([""']?)([^:)]+)\1\s*\)", RegexOptions.IgnoreCase);
                var contents = IO.File.ReadAllText(svr.MapPath(path));
                if (!pattern.IsMatch(contents))
                {
                    bundlePaths.Add(path);
                    continue;
                }


                var bundlePath = (IO.Path.GetDirectoryName(path) ?? string.Empty).Replace(@"\", "/") + "/";
                var bundleUrlPath = VirtualPathUtility.ToAbsolute(bundlePath);
                var bundleFilePath = String.Format("{0}{1}.bundle{2}",
                                                   bundlePath,
                                                   IO.Path.GetFileNameWithoutExtension(path),
                                                   IO.Path.GetExtension(path));
                contents = pattern.Replace(contents, "url($1" + bundleUrlPath + "$2$1)");
                IO.File.WriteAllText(svr.MapPath(bundleFilePath), contents);
                bundlePaths.Add(bundleFilePath);
            }
            base.Include(bundlePaths.ToArray());
            return this;
        }

    }
}