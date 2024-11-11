using Json;
using UnityEngine;

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