using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Json;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ReplayController : MonoBehaviour
{
    public int nowRound;
    public JsonFile _replay = new JsonFile();
    public bool debugAutoUpdate = false;
    public static event Action onNewFrameLoaded;
    public static event Action UpdateUI;

    public List<string> PlayerName;

    public bool FrontendDataEnd = false;
    public bool DataToMainControllerEnd = false;
    public bool isInited = false;
    public bool is_init = false;
    public bool eaten = false;
    public static int replayspeed = 1;
    public int map_width = 38;


    #region test function
    void Start(){
        //This is used to test locally.
        onNewFrameLoaded += LoadOrderly;
        //OfflineFileInit();
        //Debug.Log($"Replay Controller init success. Replay consists {_replay.Data.Count} frames.");
        //Models.Point.Init(_replay.Data[0]);
        if (onNewFrameLoaded != null)
            onNewFrameLoaded.Invoke();
        map_width = 38;
    }

    void FixedUpdate(){
        if(nowRound >= _replay.Data.Count - 1){
            Debug.Log("End");
        }//暂停
        
        if (debugAutoUpdate && onNewFrameLoaded != null)
            onNewFrameLoaded.Invoke();
    }

    #endregion

    public static void stepFrame()
    {
        onNewFrameLoaded.Invoke();
    }

    //#region offline test functions
    //private void OfflineFileInit(){
    //    using (StreamReader reader = new StreamReader(Constants.Constants.Record_Path))  //逐行读取
    //    {
    //        string line;
    //        while ((line = reader.ReadLine()) != null)
    //        {
    //            var gameData = JsonConvert.DeserializeObject<GameData>(line);
    //            gameData.Map = Tilemap_Manage.convert(gameData.board);
    //            AddDataToReplay(gameData);
    //        }
    //    }
    //    Models.TileMap.Init(_replay.Data[0]);
    //    ReplayFileInitialized();
    //}

    public void MsgToReplay(string payload)
    {
        using (StringReader reader = new StringReader(payload))  //逐行读取
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var gameData = JsonConvert.DeserializeObject<GameData>(line);
                gameData.Map = Tilemap_Manage.convert(gameData.board);
                AddDataToReplay(gameData);
            }
        }
    }
    
    private void LoadOrderly(){
        Load_next_frame();
    }
    
    #region Frontend Function
    public void AddDataToReplay(GameData gameData) {
        if(_replay == null)Debug.Log("replay not init.");
        _replay.Add(gameData);
    }
    #endregion
    
    #region function on MainController
    public void SetReplayMode(){
        GetComponent<MainController>().Mode = 0;
    }
    #endregion

    #region function on Models
    public void UpdateRoute(GameData gameData)
    {
        if (gameData.status == 1)
        {
            Models.TileMap.Update(gameData);
            Models.Point.Init(gameData);
            Debug.Log("Map Updated");
        }
        else
        {
            Models.Pacman.Update(gameData);
            Models.Ghost.Update(gameData);
        }
    }
    #endregion


    #region Frontend Instructions
    //回放文件解析完成，并向Pacman,Ghost,Tilemap发送第一帧GameData.
    public void ReplayFileInitialized()
    {
        if (_replay == null || _replay.Data.Count == 0)
        {
            Debug.Log("Replay Data Is Null.");
            return;
        }

        initialmapdata(); //初始化地图数据

        nowRound = 1;
        Debug.Log("Model Updated");
        Debug.Log(_replay.Data.Count);
        Debug.Log(_replay.Data[1].board[0][0]);
        Debug.Log(_replay.Data[1].Map.Length);
        ModelUpdate(nowRound);
        Models.TileMap.Update(_replay.Data[1]);
        Models.Point.Init(_replay.Data[1]);
        SetReplayMode();
        //Init Ended.
    }

    public void LoadFrame(int frameIndex) {
        if (frameIndex >= _replay.Data.Count || frameIndex < 0)
        {
            Debug.Log("Frame Number Out of Range.");
            return;
        }
        if(frameIndex == 0){frameIndex = 1;}

        var tarRoundData = _replay.Data[frameIndex];
        nowRound = frameIndex;
        // Debug.Log("Load Frame Successfully");
        // Models.TileMap.Update(tarRoundData);
        // map_width = _replay.Data[frameIndex].board.Count;
        // Debug.Log(_replay.Data[frameIndex].board.Count);
        // Models.Point.Init(tarRoundData);
        // Models.Pacman.Update(tarRoundData);
        // Models.Ghost.Update(tarRoundData);
        is_init = true;
        ModelUpdate(frameIndex);
        //GetComponent<ReplayDebuggingUI>().UpdateTexts();
        UpdateUI.Invoke();
        Debug.Log("Load Ghosts Successfully");
    }

    public void Load_next_frame() {
        if (nowRound == _replay.Data.Count - 1) {
            Debug.Log("Replay Reach the End.");
            return;
        }

        nowRound++;

        var gameData = _replay.Data[nowRound];
        map_width = gameData.board.Count;
        Debug.Log("next   " + map_width);
        ModelUpdate(nowRound);
    }

    public void SetPlayerName(List<string> name) {
        PlayerName = name;
    }

    public void SetReplaySpeed(int level){
        GhostMove.level = level;
        PacmanMove.level = level;
        replayspeed = level;
        Time.fixedDeltaTime = 1f / level;
    }
    #endregion
    public void ModelUpdate(int frame){
        Debug.Log("Update Frame: " + frame);
        Debug.Log("Round: "+ _replay.Data[frame].Round);
        if(StopreasonUI.nowtext != ""){
            StopreasonUI.UpdateText("");
        }
        if(_replay.Data[frame].StopReason != null){
            Debug.Log("Stop Reason: " + _replay.Data[frame].StopReason);
            StopreasonUI.UpdateText(_replay.Data[frame].StopReason);
            return;
        }
        if (is_init)
        {
            Models.TileMap.Update(_replay.Data[frame]);
            map_width = _replay.Data[frame].board.Count;
            Models.Point.Init(_replay.Data[frame]);
            is_init = false;
        }
        foreach (var e in _replay.Data[frame].events)
        {
            if(e == 2 || e == 3)
                is_init = true;
        }
        Models.Ghost.Update(_replay.Data[frame]);
        Models.Pacman.Update(_replay.Data[frame]);
    }


    public void initialmapdata(){
        for(int i = 2 ; i < _replay.Data.Count;i++){
            if(_replay.Data[i].board.Count == 0){
                _replay.Data[i].board = _replay.Data[i-1].board;
                for(int j = 0;j<_replay.Data[i-1].pacman_step_block.Count;j++){
                    int x = _replay.Data[i-1].pacman_step_block[j][0];
                    int y = _replay.Data[i-1].pacman_step_block[j][1];
                    if(x >= 0){
                        _replay.Data[i].board[x][y] = 1;
                        if(_replay.Data[i-1].pacman_skills[2] > 0){ //magnet
                            if(_replay.Data[i].board[x-1][y] > 1 && _replay.Data[i].board[x-1][y] != 8) _replay.Data[i].board[x-1][y] = 1;
                            if(_replay.Data[i].board[x+1][y] > 1 && _replay.Data[i].board[x+1][y] != 8) _replay.Data[i].board[x+1][y] = 1;
                            if(_replay.Data[i].board[x][y-1] > 1 && _replay.Data[i].board[x][y-1] != 8) _replay.Data[i].board[x][y-1] = 1;
                            if(_replay.Data[i].board[x][y+1] > 1 && _replay.Data[i].board[x][y+1] != 8) _replay.Data[i].board[x][y+1] = 1;
                            if(_replay.Data[i].board[x+1][y+1] > 1 && _replay.Data[i].board[x+1][y+1] != 8) _replay.Data[i].board[x+1][y+1] = 1;
                            if(_replay.Data[i].board[x+1][y-1] > 1 && _replay.Data[i].board[x+1][y-1] != 8) _replay.Data[i].board[x+1][y-1] = 1;
                            if(_replay.Data[i].board[x-1][y+1] > 1 && _replay.Data[i].board[x-1][y+1] != 8) _replay.Data[i].board[x-1][y+1] = 1;
                            if(_replay.Data[i].board[x-1][y-1] > 1 && _replay.Data[i].board[x-1][y-1] != 8) _replay.Data[i].board[x-1][y-1] = 1;
                        }
                    }
                }
                _replay.Data[i].Map = Tilemap_Manage.convert(_replay.Data[i].board);
            }
        }
    }

    public void HandleMessage(string message)   //Handle init message from Web
    {
        Debug.Log("Received message: " + message);
        var data = JsonConvert.DeserializeObject<FrontendData>(message);
        Debug.Log($"Message type: {data.message}, content: {data.replay_data}");
        if (data.replay_data != null)
        {
            var gamedata = JsonConvert.DeserializeObject<GameData>(data.replay_data);
            AddDataToReplay(gamedata);
        }
    }

}
