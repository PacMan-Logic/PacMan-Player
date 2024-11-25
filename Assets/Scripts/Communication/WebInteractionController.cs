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
        // 这个函数被调用代表所有的 Awake() Start() 都启用了
        // 也即初始化已经完成
        // 本地调用直接打开会报错
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

    // 向后端发送 action
    // 游戏UI逻辑需要使用
    public void SendAction(Operation??? action)
    {
        string sendAction = action.ToString();
        sendAction += '\n';
        var sendMessage = new Info{
            request = "action",
            token = tokenB64,
            content = sendAction
        };
        var jsonMessage = JsonConvert.SerializeObject(sendMessage);
        //Debug.Log($"Send message {jsonMessage}");
        Write(jsonMessage);
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
    // 向网页发送回复信息
    private void SendToFrontend(FrontendReplyData reply)
    {
        string information = JsonConvert.SerializeObject(reply);
        Send_frontend(information);
    }
    //向网页报错
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
    // 告知网页总帧数
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
        // 告知前端网页unity已经初始化完成，接收队列中的信息
        SendToFrontend(
            new FrontendReplyData()
            {
                message = FrontendReplyData.MsgType.loaded
            }
        );
    }
    #endregion
    // 提供给前端网页使用
    // 接收网页信息
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
    /*
     * 这个函数是前端网页调用的，通过output.jslib与player.html协同实现通信
     * 逻辑为：unity调用Getoperation函数
     *       -> output.jslib中实现该函数，调用window.SendOperation
     *       -> player.html中实现window.SendOperation，调用Main Controller组件中的HandleOperation
     *       -> 到达该函数
     */
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
