using System.Web.Http.OData.Query;

namespace KS.Core.UI.OdataQuery
{
    class DynamicQueryValidator : System.Web.Http.OData.Query.Validators.ODataQueryValidator
    {
        public override void Validate(ODataQueryOptions options, ODataValidationSettings validationSettings)
        {
            var op = options;
            var t = op.Context;
            var e = op.Filter;
            var b = op.RawValues;
         
            base.Validate(options, validationSettings);

        }
    }
}
