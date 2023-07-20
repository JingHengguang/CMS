namespace Cms.Backend.Api.Model
{
    public class Corporate_information : BaseModel
    {
        public string Name { get; set; }
        public string About { get; set; }
        public string Recruitment { get; set; }
        public string Official_account { get; set; }
        public string WeChat { get; set; }
        public string QQ { get; set; }
        public string ICP { get; set; }
        public string FTR { get; set; }
    }
}