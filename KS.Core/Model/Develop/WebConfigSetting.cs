namespace KS.Core.Model.Develop
{
    public class WebConfigSetting
    {
        //public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public int MasterDataKeyValueId { get; set; }
        public int MasterDataKeyValueTypeId { get; set; }
        public string MasterDataKeyValuePropertyName { get; set; }
        public bool InjectToJavaScript { get; set; }
        public string Description { get; set; }
        public string JavaScriptType { get; set; }

    }
}
