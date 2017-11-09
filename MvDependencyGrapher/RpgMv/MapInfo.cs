// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using MvDependencyGrapher.RpgMv;
//
//    var data = MapInfo.FromJson(jsonString);
//
namespace MvDependencyGrapher.RpgMv
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public partial class MapInfo
    {
        [JsonProperty("expanded")]
        public bool Expanded { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("order")]
        public long Order { get; set; }

        [JsonProperty("parentId")]
        public long ParentId { get; set; }

        [JsonProperty("scrollX")]
        public double ScrollX { get; set; }

        [JsonProperty("scrollY")]
        public double ScrollY { get; set; }
    }

    public partial class MapInfo
    {
        public static List<MapInfo> FromJson(string json) => JsonConvert.DeserializeObject<List<MapInfo>>(json, Converter.Settings);
    }

    public static partial class Serialize
    {
        public static string ToJson(this List<MapInfo> self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
