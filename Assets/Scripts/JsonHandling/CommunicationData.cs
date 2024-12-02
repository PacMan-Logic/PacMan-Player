using System;
using System.Collections;
using System.Collections.Generic;
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
    public MsgType message { get; set; }
    public string payload { get; set; }
    public string token { get; set; }
    public int speed { get; set; }
    public List<GameData> replay_data { get; set; }
    public int index { get; set; }
    public List<string> players { get; set; }
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
    public MsgType message { get; set; }
    public int number_of_frames { get; set; }
    public int height { get; set; }
    public bool init_result { get; set; }
    public string game_record { get; set; }
    public string err_msg { get; set; }
}

public class JsonFile
{
    public List<GameData> Data { get; set; }

    public JsonFile()
    {
        Data = new List<GameData>();
    }

    public void Add(GameData data)
    {
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


public class Operation
{
    public enum action {
        Static = 0,
        Up = 1,
        Left = 2,
        Down = 3,
        Right = 4
    }

    public int chara;
    public List<int> operation;

    public Operation( int _chara,List<int> _operation) {
        chara = _chara;
        operation = _operation;
    }

    public override string ToString()
    {
        string _string;
        _string = String.Join(" ", operation);
        var jsonAction = new{
            role = chara,
            action = _string
        };
        string jsonString = JsonConvert.SerializeObject(jsonAction);
        return jsonString;
    }
}

