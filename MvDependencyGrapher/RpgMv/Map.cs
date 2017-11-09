// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using MvDependencyGrapher.RpgMv;
//
//    var data = Map.FromJson(jsonString);
//
namespace MvDependencyGrapher.RpgMv
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public partial class Map
    {
        [JsonProperty("autoplayBgm")]
        public bool AutoplayBgm { get; set; }

        [JsonProperty("autoplayBgs")]
        public bool AutoplayBgs { get; set; }

        [JsonProperty("battleback1Name")]
        public string Battleback1Name { get; set; }

        [JsonProperty("battleback2Name")]
        public string Battleback2Name { get; set; }

        [JsonProperty("bgm")]
        public AudioDefinition Bgm { get; set; }

        [JsonProperty("bgs")]
        public AudioDefinition Bgs { get; set; }

        [JsonProperty("data")]
        public List<long> Data { get; set; }

        [JsonProperty("disableDashing")]
        public bool DisableDashing { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("encounterList")]
        public List<object> EncounterList { get; set; }

        [JsonProperty("encounterStep")]
        public long EncounterStep { get; set; }

        [JsonProperty("events")]
        public List<Event> Events { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("parallaxLoopX")]
        public bool ParallaxLoopX { get; set; }

        [JsonProperty("parallaxLoopY")]
        public bool ParallaxLoopY { get; set; }

        [JsonProperty("parallaxName")]
        public string ParallaxName { get; set; }

        [JsonProperty("parallaxShow")]
        public bool ParallaxShow { get; set; }

        [JsonProperty("parallaxSx")]
        public long ParallaxSx { get; set; }

        [JsonProperty("parallaxSy")]
        public long ParallaxSy { get; set; }

        [JsonProperty("scrollType")]
        public long ScrollType { get; set; }

        [JsonProperty("specifyBattleback")]
        public bool SpecifyBattleback { get; set; }

        [JsonProperty("tilesetId")]
        public long TilesetId { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }

    public partial class Event
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("pages")]
        public List<Page> Pages { get; set; }

        [JsonProperty("x")]
        public long X { get; set; }

        [JsonProperty("y")]
        public long Y { get; set; }
    }

    public partial class Page
    {
        [JsonProperty("conditions")]
        public Conditions Conditions { get; set; }

        [JsonProperty("directionFix")]
        public bool DirectionFix { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("list")]
        public List<Command> List { get; set; }

        [JsonProperty("moveFrequency")]
        public long MoveFrequency { get; set; }

        [JsonProperty("moveRoute")]
        public MoveRoute MoveRoute { get; set; }

        [JsonProperty("moveSpeed")]
        public long MoveSpeed { get; set; }

        [JsonProperty("moveType")]
        public long MoveType { get; set; }

        [JsonProperty("priorityType")]
        public long PriorityType { get; set; }

        [JsonProperty("stepAnime")]
        public bool StepAnime { get; set; }

        [JsonProperty("through")]
        public bool Through { get; set; }

        [JsonProperty("trigger")]
        public long Trigger { get; set; }

        [JsonProperty("walkAnime")]
        public bool WalkAnime { get; set; }
    }

    public partial class MoveRoute
    {
        [JsonProperty("list")]
        public List<MoveCommand> List { get; set; }

        [JsonProperty("repeat")]
        public bool Repeat { get; set; }

        [JsonProperty("skippable")]
        public bool Skippable { get; set; }

        [JsonProperty("wait")]
        public bool Wait { get; set; }
    }

    public partial class MoveCommand
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("parameters")]
        public List<object> Parameters { get; set; }
    }

    public partial class Command
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("indent")]
        public long Indent { get; set; }

        [JsonProperty("parameters")]
        public List<object> Parameters { get; set; }
    }

    public partial class Image
    {
        [JsonProperty("characterIndex")]
        public long CharacterIndex { get; set; }

        [JsonProperty("characterName")]
        public string CharacterName { get; set; }

        [JsonProperty("direction")]
        public long Direction { get; set; }

        [JsonProperty("pattern")]
        public long Pattern { get; set; }

        [JsonProperty("tileId")]
        public long TileId { get; set; }
    }

    public partial class Conditions
    {
        [JsonProperty("actorId")]
        public long ActorId { get; set; }

        [JsonProperty("actorValid")]
        public bool ActorValid { get; set; }

        [JsonProperty("itemId")]
        public long ItemId { get; set; }

        [JsonProperty("itemValid")]
        public bool ItemValid { get; set; }

        [JsonProperty("selfSwitchCh")]
        public string SelfSwitchCh { get; set; }

        [JsonProperty("selfSwitchValid")]
        public bool SelfSwitchValid { get; set; }

        [JsonProperty("switch1Id")]
        public long Switch1Id { get; set; }

        [JsonProperty("switch1Valid")]
        public bool Switch1Valid { get; set; }

        [JsonProperty("switch2Id")]
        public long Switch2Id { get; set; }

        [JsonProperty("switch2Valid")]
        public bool Switch2Valid { get; set; }

        [JsonProperty("variableId")]
        public long VariableId { get; set; }

        [JsonProperty("variableValid")]
        public bool VariableValid { get; set; }

        [JsonProperty("variableValue")]
        public long VariableValue { get; set; }
    }

    public partial class AudioDefinition
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("pan")]
        public long Pan { get; set; }

        [JsonProperty("pitch")]
        public long Pitch { get; set; }

        [JsonProperty("volume")]
        public long Volume { get; set; }
    }

    public partial class Map
    {
        public static Map FromJson(string json) => JsonConvert.DeserializeObject<Map>(json, Converter.Settings);
    }

    public static partial class Serialize
    {
        public static string ToJson(this Map self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}
