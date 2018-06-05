using System;
using System.Collections.Generic;
using System.Web.Optimization;
using BundleTransformer.Core.Builders;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Transformers;
using KS.Core.Model;
using KS.Core.Model.Develop;

namespace KS.Core.CodeManager.BrowsersCode.Base
{
    public abstract class BaseBundleManager
    {
        public virtual void AddBundle(List<BundleOption> bundleOptions)
        {
            //Bundle bndl = BundleTable.Bundles.GetBundleFor("~/Content/css");
            //if (bndl != null)
            //{
            //    BundleTable.Bundles.Remove(bndl);
            //}

            //Bundle bndl2 = new Bundle("~/Content/css");
            //bndl2.Include("~/Content/site.css", "~/Content/secondStyles.css", ... );
            //BundleTable.Bundles.Add(bndl2);


            foreach (var bundleRequest in bundleOptions)
            {

                var nullBuilder = new NullBuilder();
                var styleTransformer = new StyleTransformer();
                var scriptTransformer = new ScriptTransformer();
                var nullOrderer = new NullOrderer();
                var extention = bundleRequest.Url.Substring(bundleRequest.Url.LastIndexOf(".", StringComparison.Ordinal)+1);
                if (extention.ToLower() == "js")
                {
                    var commonScriptsBundle = new 
                        Bundle(bundleRequest.Url.Replace(".", "-"));
                    commonScriptsBundle.Include(
                       bundleRequest.Sources.ToArray()
                         );
                    commonScriptsBundle.Builder = nullBuilder;
                    commonScriptsBundle.Transforms.Add(scriptTransformer);
                    commonScriptsBundle.Orderer = nullOrderer;

                    BundleTable.Bundles.Add(commonScriptsBundle);
                }
                else
                {
                    var commonStylesBundle = new 
                        Bundle(bundleRequest.Url.Replace(".", "-"));
                    commonStylesBundle.Include(
                       bundleRequest.Sources.ToArray()
                         );
                    commonStylesBundle.Builder = nullBuilder;
                    commonStylesBundle.Transforms.Add(styleTransformer);
                    commonStylesBundle.Orderer = nullOrderer;

                    BundleTable.Bundles.Add(commonStylesBundle);
                }

            }

            //return JObject.Parse(JsonConvert.SerializeObject(new object(), Formatting.None));

        }

        public virtual void RemoveBundle(string name)
        {
            var bndl = BundleTable.Bundles.GetBundleFor(name);
            if (bndl != null)
            {
                BundleTable.Bundles.Remove(bndl);
            }

        }
    }
}
