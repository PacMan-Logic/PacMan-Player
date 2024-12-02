using Json;
using UnityEngine;

//int current_player;//用于记录当前玩家的ID是0还是1
bool not_setRole=false;//用于记录还未设置角色
int role;//记录角色
bool other_finish=false;//用于记录另一位玩家是否已经输入完成,false = 未完成
//bool I_can_input=false;//用于记录自己是否可以输入，false = 不能输入
bool get_finish_message=false;//用来判断后端发来的消息是gamedata还是另一位玩家完成的通知
public class InteractController : MonoBehaviour
{
    public void Interact(GameData data)
    {
        //Debug.Log($"Round: {data.Round}, Player: {data.Player}, Operation: {data.Operation}");
        if (!ModeController.IsInteractMode())
        {
            return;
        }
        Models.Ghost.Update(data);
        Models.Pacman.Update(data);
        Models.TileMap.Update(data);
    }
    public void SetRole(int _role) {
        role = _role;
    }

    public void OtherPlayerFinish(){
        other_finish = true;
    }
}