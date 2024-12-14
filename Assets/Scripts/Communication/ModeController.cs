using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeController : MonoBehaviour
{
    
    public enum Mode{
        Error = 0,
        Replay = 1,
        Interact = 2,
    }

    private static Mode _mode = Mode.Error;



    public void SwitchReplayMode(){
        _mode = Mode.Replay;
        GameObject.Find("Main Controller").GetComponent<ReplayController>().enabled=true;
        GameObject.Find("Main Controller").GetComponent<KeyboardInteraction>().enabled=false;
    }

    public void SwitchInteractMode(){
        _mode = Mode.Interact;
        GameObject.Find("Main Controller").GetComponent<ReplayController>().enabled=false;
        GameObject.Find("Main Controller").GetComponent<KeyboardInteraction>().enabled=true;
    }

    public static bool IsReplayMode(){
        return _mode == Mode.Replay;
    }

    public static bool IsInteractMode(){
        return _mode == Mode.Interact;
    }
}
