using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using Json;
using Newtonsoft.Json;
using UnityEngine;

public class WebInteractionController : MonoBehaviour
{
    #region members
    public string token64;
    private static bool _loaded = false;
    #endregion

    public void Update()
    {
        if(_loaded)return;
        _loaded = true;
        SendInitCompleteToFronted();
    }

    #region sendDataToFrontend
    private void SendToFrontend(FrontendReplyData reply)
    {
        string information = JsonConvert.SerializeObject(reply);
        Send_frontend(information);
    }
    private void SendErrorToFrontend(string message)
    {
        SendToFrontend(
            new FrontendReplyData()
            {
                message = FrontendReplyData.MsgType.error_marker,
                err_msg = message
            }
        );
    }
    private void SendFrameCountToFrontend(int count){
        SendToFrontend(
            new FrontendReplyData()
            {
                message = FrontendReplyData.MsgType.init_successfully,
                number_of_frames = count,
                init_result = true
            }
        );
    }
    private void SendInitCompleteToFronted(){
        SendToFrontend(
            new FrontendReplyData()
            {
                message = FrontendReplyData.MsgType.loaded
            }
        );
    }
    #endregion
    public void HandleMessage(string buffer){
        FrontendData msg;
        try
        {
            msg = JsonConvert.DeserializeObject<FrontendData>(buffer);
        }
        catch (Exception e)
        {
            SendErrorToFrontend(e.Message);
            return;
        }
        try
        {
            switch (msg.message)
            {
                case FrontendData.MsgType.init_player_player:
                    /*TODO*/
                    break;
                case FrontendData.MsgType.init_replay_player:      //This message is to initialize replay mode instead of start replay.
                    int frameCount = Convert.ToInt32(msg.payload);
                    for(int i = 0;i < frameCount;i++){
                        Getoperation(i);
                    }
                    GetComponent<ReplayController>().ReplayFileInitialized();
                    SendFrameCountToFrontend(frameCount - 1);
                    break;
                case FrontendData.MsgType.load_frame:
                    Debug.Log("Load frame " + msg.index);
                    GetComponent<ReplayController>().LoadFrame(msg.index);
                    break;
                case FrontendData.MsgType.load_next_frame:
                    Debug.Log("Load the next frame.");
                    GetComponent<ReplayController>().Load_next_frame();
                    break;
                case FrontendData.MsgType.load_players:
                    Debug.Log("Load Player name.");
                    GetComponent<ReplayController>().SetPlayerName(msg.players);
                    break;
                case FrontendData.MsgType.play_speed:
                    GetComponent<ReplayController>().SetReplaySpeed(msg.speed);
                    break;
                default:
                    break;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void HandleOperation(string Operation){
        var gameData = JsonConvert.DeserializeObject<GameData>(Operation);
        GetComponent<ReplayController>().AddDataToReplay(gameData);
    }





    #region jsFunc 
    // 下面的函数是定义在output.jslib中的库函数
    [DllImport("__Internal")]
    private static extern void Connect_ws(string address);

    [DllImport("__Internal")]
    private static extern void Send_ws(string strPayload);

    [DllImport("__Internal")]
    private static extern void Send_frontend(string json);

    [DllImport("__Internal")]
    private static extern void Getoperation(int index);
    #endregion
}
