using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Cms.Backend.Api.Utils
{
    public class JsonHelper
    {
        public static string Serialize(dynamic obj)
        {
            var setting = new JsonSerializerSettings();
            setting.ContractResolver = new CamelCasePropertyNamesContractResolver();
            setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            var result = JsonConvert.SerializeObject(obj, Formatting.Indented, setting);
            return result;
        }
    }
}
