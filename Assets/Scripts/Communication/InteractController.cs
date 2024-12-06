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
    public static bool speedupstop = false;  //如果是true则从下一回合开始停止加速

    static bool initmap = true;
    public static void Interact(GameData data)
    {
        //Debug.Log($"Round: {data.Round}, Player: {data.Player}, Operation: {data.Operation}");
        if (!ModeController.IsInteractMode())
        {
            return;
        }
        if(initmap) {
            Models.TileMap.Update(data);
            Models.Point.Init(data);
            initmap = false;
        }
        if(data.pacman_skills.Count > 1 && data.pacman_skills[1] == 1){
            speedupstop = true; //持续时间只剩一回合，下一回合要停止加速（除非这一回合吃了加速豆）
        }
        foreach (var e in data.events){
            if(e == 2 || e == 3){
                initmap = true;
                other_finish = true;
            }
            else if(e == 0){
                Models.Pacman.eaten = true;
            }
        }
        Models.Ghost.Update(data);
        Models.Pacman.Update(data);
    }
    public static void SetRole(int _role) {
        role = _role;
        setRole = true;
    }

    public static void OtherPlayerFinish(){
        other_finish = true;
    }
}