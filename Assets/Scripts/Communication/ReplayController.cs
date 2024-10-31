using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Json;
using Newtonsoft.Json;
using UI.Debug_Overlay;
using Unity.VisualScripting;
using UnityEngine;

public class ReplayController : MonoBehaviour
{
    public int nowRound;
    public JsonFile _replay = new JsonFile();
    public bool debugAutoUpdate = false;
    public static event Action onNewFrameLoaded; 

    public List<string> PlayerName;

    public bool FrontendDataEnd = false;
    public bool DataToMainControllerEnd = false;

    void Start(){
        //This is used to test locally.
        onNewFrameLoaded += LoadOrderly;
        OfflineFileInit();
        Debug.Log($"Replay Controller init success. Replay consists {_replay.Data.Count} frames.");
        Models.Point.Init(_replay.Data[0]);
        if (onNewFrameLoaded != null)
            onNewFrameLoaded.Invoke();
    }

    void FixedUpdate(){
        if(nowRound >= _replay.Data.Count - 1){
            Debug.Log("End");
            Time.timeScale = 0;
        }//暂停

        // LoadFrame(++nowRound + 1);  //Test LoadFrame.
        if(nowRound > 15){
            SetReplaySpeed(4);
        }
        
        if (debugAutoUpdate && onNewFrameLoaded != null)
            onNewFrameLoaded.Invoke();
    }

    public static void stepFrame()
    {
        onNewFrameLoaded.Invoke();
    }

    #region offline test functions
    private void OfflineFileInit(){
        using (StreamReader reader = new StreamReader(Constants.Constants.Record_Path))  //逐行读取
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var gameData = JsonConvert.DeserializeObject<GameData>(line);
                gameData.Map = Tilemap_Manage.convert(gameData.board);
                AddDataToReplay(gameData);
            }
        }
        GetComponent<MainController>().tileMap.Init(_replay.Data[0]);
        
        if (_replay == null || _replay.Data.Count == 0)
        {
            Debug.Log("Replay Data Is Null.");
            return;
        }
        nowRound = 0;
        ModelsInit(nowRound);
        SetReplayMode();
    }
    
    private void LoadOrderly(){
        Load_next_frame();
    }
    
    #endregion
    
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
    public void UpdateRoute(GameData gameData){
        if (gameData.Initalmap)
        {
            Models.TileMap.Update(gameData);
        }
        else
        {
            Models.Pacman.Update(gameData);
            Models.Ghost.Update(gameData);
        }
    }
    public void ClearRoute(){
        Models.Ghost.ClearRoute();
        Models.Pacman.ClearRoute();
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

        nowRound = 0;
        
        ModelUpdate(nowRound);
        SetReplayMode();
        //Init Ended.
    }

    public void LoadFrame(int frameIndex) {
        if (frameIndex >= _replay.Data.Count || frameIndex < 0)
        {
            Debug.Log("Frame Number Out of Range.");
            return;
        }

        var tarRoundData = _replay.Data[frameIndex];
        nowRound = frameIndex;
        ClearRoute();
        UpdateRoute(tarRoundData);
    }

    public void Load_next_frame() {
        if (nowRound == _replay.Data.Count - 1) {
            Debug.Log("Replay Reach the End.");
            return;
        }

        nowRound++;

        var gameData = _replay.Data[nowRound];
        UpdateRoute(gameData);
    }

    public void SetPlayerName(List<string> name) {
        PlayerName = name;
    }

    public void SetReplaySpeed(int speed){
        GhostMove.speed = speed;
        PacmanMove.speed = speed;
    }
    #endregion
    public void ModelUpdate(int frame){
        Models.Ghost.Update(_replay.Data[frame]);
        Models.Pacman.Update(_replay.Data[frame]);
        if (_replay.Data[frame].Initalmap)
        {
            Models.TileMap.Update(_replay.Data[frame]);
        }
    }

    public void ModelsInit(int frame)
    {
        Models.Pacman.Init(_replay.Data[frame]);
        Models.Ghost.Init(_replay.Data[frame]);
        // Models.TileMap.Init(_replay.Data[frame]);
        Models.Point.Init(_replay.Data[frame]);
    }
}
