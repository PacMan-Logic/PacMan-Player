using Json;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    //int current_player;//用于记录当前玩家的ID是0还是1
    public static bool setRole=false;//用于记录还未设置角色
    public static int role;//记录角色,0为Pacman，1为Ghost
    public static bool other_finish=true;//用于记录另一位玩家是否已经输入完成,false = 未完成
    //bool I_can_input=false;//用于记录自己是否可以输入，false = 不能输入
    //public static bool get_finish_message=true;//用来判断后端发来的消息是gamedata还是另一位玩家完成的通知

    static bool initmap = true;
    public static void Interact(GameData data)
    {
        //Debug.Log($"Round: {data.Round}, Player: {data.Player}, Operation: {data.Operation}");
        if (!ModeController.IsInteractMode())
        {
            return;
        }
        Models.Ghost.Update(data);
        Models.Pacman.Update(data);
        if(initmap) {
            Models.TileMap.Update(data);
            Models.Point.Init(data);
            initmap = false;
        }
        foreach (var e in data.events){
            if(e == 2 || e == 3){
                initmap = true;
                other_finish = true;
            }
        }
    }
    public static void SetRole(int _role) {
        role = _role;
        setRole = true;
    }

    public static void OtherPlayerFinish(){
        other_finish = true;
    }
}