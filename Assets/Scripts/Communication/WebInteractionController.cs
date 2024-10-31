using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using JetBrains.Annotations;
using Json;
using Newtonsoft.Json;
using UnityEngine;

public class WebInteractionController : MonoBehaviour
{
    #region members
    public string tokenB64;
    private static bool _loaded = false;
    #endregion

    public void Update()
    {
        if(_loaded)return;
        _loaded = true;
        SendInitCompleteToFronted();
    }

    #region online mode function
    public void ConnectToJudger(string token){
        if(Connect(token)){
            Debug.Log("Connect to Judger");
            ReplyConnectionSucceed(token);
        }
    }

    private bool Connect(string token){
        try{
            var bytes = Convert.FromBase64String(token);
            var uri = Encoding.UTF8.GetString(bytes);
            Debug.Log(uri);
            

            /*
            TODO:
                setPlayerid()
            */

            Connect_ws("wss://" + uri);
            return true;
        }
        catch( Exception e){
            Debug.Log(e);
            SendErrorToFrontend(e.Message);
            return false;
        }
    }

    private void ReplyConnectionSucceed(string token){
            var info = new Info{
                request = "connect",
                token = token,
                content = null
        };

        tokenB64 = token;
        JsonSerializerSettings settings = new() {NullValueHandling = NullValueHandling.Ignore };
        var jsonString = JsonConvert.SerializeObject(info, settings);
        Debug.Log(jsonString);
        Write(jsonString);
    }

    private void Write(string information){
        try{
            Send_ws(information);
        }
        catch (Exception e){
            Debug.Log("$Failed to send message: {e.Message}");
            SendErrorToFrontend(e.Message);
        }
    }
    #endregion
    public void ReceiveWebSocketMessage(string information)
    {
        try
        {
            var judgerData = JsonConvert.DeserializeObject<JudgerData>(information);
            if (judgerData.request == "action")
            {
                var jsonData = JsonConvert.DeserializeObject<GameData>(judgerData.content);
                // GetComponent<InteractController>().Interact(jsonData);
            }
            else
            {
                Debug.Log(judgerData.request);
                SendErrorToFrontend(judgerData.request);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            SendErrorToFrontend(e.Message);
        }
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

                    ConnectToJudger(msg.token);
                    GetComponent<ModeController>().SwitchInteractMode();

                    break;
                case FrontendData.MsgType.init_replay_player:      //This message is to initialize replay mode instead of start replay.
                    GetComponent<ModeController>().SwitchReplayMode();
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
