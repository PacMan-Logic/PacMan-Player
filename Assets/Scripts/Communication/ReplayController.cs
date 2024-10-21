using System.Collections;
using System.Collections.Generic;
using System.IO;
using Json;
using Newtonsoft.Json;
using UnityEngine;

public class ReplayController : MonoBehaviour
{
    public int nowRound;
    JsonFile _replay;

    public List<string> PlayerName;

    public void AddDataToReplay(GameData gameData) {
        _replay.Add(gameData);
    }

    //回放文件解析完成，并向Pacman,Ghost,Tilemap发送第一帧GameData.
    public void ReplayFileInitialized()
    {
        if (_replay?.Datas == null || _replay.Datas.Count == 0)
        {
            Debug.Log("Replay Data Is Null.");
            return;
        }

        var initRoundData = _replay.Datas[0];

        nowRound = 0;
        UpdateObjectInfo(initRoundData);
    }

    public void LoadFrame(int frameIndex) {
        if (frameIndex >= _replay.Datas.Count || frameIndex < 0)
        {
            Debug.Log("Frame Number Out of Range.");
            return;
        }

        var tarRoundData = _replay.Datas[frameIndex];
        nowRound = frameIndex;
        UpdateObjectInfo(tarRoundData);
    }

    public void Load_next_frame() {
        if (nowRound == _replay.Datas.Count - 1) {
            Debug.Log("Replay Reach the End.");
            return;
        }

        nowRound++;
        UpdateObjectInfo(_replay.Datas[nowRound]);
    }

    private static void UpdateObjectInfo(GameData initRoundData)
    {
        Models.Ghost.Update(initRoundData);
        Models.Pacman.Update(initRoundData);
        Models.TileMap.Update(initRoundData);
    }

    public void SetPlayerName(List<string> name) {
        PlayerName = name;
    }
}
