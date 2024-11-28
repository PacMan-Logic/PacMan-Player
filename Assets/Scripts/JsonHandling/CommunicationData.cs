using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Unity.Mathematics;
using UnityEngine;


[Serializable]
public class FrontendData
{
    public enum MsgType
    {
        init_player_player,
        init_replay_player,
        load_frame,
        load_next_frame,
        load_players,
        play_speed,
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public MsgType message {get; set; }
    public string token {get; set; }


    public int payload { get; set; }
    public int speed {get; set; }

    [CanBeNull]
    public string replay_data {get; set; }

    public string play_speed { get; set; }
    public int index {get; set; }
    public List<string> players {get; set; }
}

[Serializable]
public class FrontendReplyData
{
    public enum MsgType
    {
        init_successfully,
        initialize_result,
        game_record,
        error_marker,
        loaded,
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public MsgType message {get; set; }
    public int number_of_frames {get; set; }
    public int height {get; set; }
    public bool init_result {get; set; }
    public string game_record {get; set; }
    public string err_msg {get; set; }
}

public class JsonFile{
    public List<GameData> Data {get; set; }

    public JsonFile()
    {
        Data = new List<GameData>();
    }

    public void Add(GameData data){
        Data.Add(data);
    }
}

    public class Info
    {
        public string request { get; set; }
        public string token { get; set; }
        public string content { get; set; }
    }

    public class HistoryInfo
    {
        public string request { get; set; }
        public List<string> content { get; set; }
    }

    public class WatchInfo
    {
        public string request { get; set; }
    }

    public class JudgerData
    {
        public string request { get; set; }
        public string content { get; set; }
    }
