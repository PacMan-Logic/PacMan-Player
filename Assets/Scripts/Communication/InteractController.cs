using Json;
using UnityEngine;

int current_player;//用于记录当前玩家的ID是0还是1
int role;//记录角色
bool finish_0;//用于记录几号玩家是否已经输入完成
bool finish_1;
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
}