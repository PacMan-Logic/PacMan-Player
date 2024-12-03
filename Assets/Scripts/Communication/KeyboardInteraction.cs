using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using System.Numerics;
using Models;
using System.Linq;
using System.Data;

public class KeyboardInteraction : MonoBehaviour
{
    private UnityEngine.Vector3 direction = UnityEngine.Vector3.zero;

    private List<GameObject> obj = new List<GameObject>();

    private List<GameObject> clone = new List<GameObject>();

    private int targetnum = 0;  //目标操作个数
    private int index = 0;  //当前操作对象,对象为幽灵
    private List<MovementType> action = new List<MovementType>();

    private bool isactive = false;

    void Start(){
        if(!InteractController.setRole) return;
        if(InteractController.speedupstop){
            Models.Pacman.Speed = 1;  //如果加速停止，则将速度设为1(提前设置，在后端发信息之前)
            InteractController.speedupstop = false;
        }
        if(InteractController.role == 0){
            obj.Add(GameObject.FindWithTag("Pacmen"));
            targetnum = 1;
        }else{
            for(int i = 0; i < 3; i++){
                string name = "Ghost" + i;
                obj.Add(GameObject.FindWithTag(name));
            }
            targetnum = 3;
        }
        for(int i = 0; i < targetnum; i++){
            action.Add(MovementType.Zero);
            GameObject cloneobj = ChangeColorToRed(Instantiate(GameObject.FindWithTag("Ghost0"), obj[i].transform.position, obj[i].transform.rotation));
            cloneobj.GetComponent<Renderer>().enabled = false;
            cloneobj.GetComponent<GhostMove>().enabled = false;
            clone.Add(cloneobj);
            // 创建LineRenderer并添加到clone对象上
            LineRenderer lineRenderer = cloneobj.AddComponent<LineRenderer>();
            lineRenderer.positionCount = 2; // 设置线段的顶点数量
            lineRenderer.startWidth = 0.1f; // 设置线段的起始宽度
            lineRenderer.endWidth = 0.1f;   // 设置线段的结束宽度
            lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // 设置材质，这里使用默认的精灵材质
            lineRenderer.enabled = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(!isactive) return;
        if(InteractController.role == 0){
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)){
                action[0] = MovementType.Up;
                direction = new UnityEngine.Vector3(0, 1, 0);
                clone[0].transform.position = obj[0].transform.position+direction*Models.Pacman.Speed;
                clone[0].GetComponent<Renderer>().enabled = true;
                clone[0].GetComponent<LineRenderer>().enabled = true;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)){
                action[0] = MovementType.Down;
                direction = new UnityEngine.Vector3(0, -1, 0);
                clone[0].transform.position = obj[0].transform.position+direction*Models.Pacman.Speed;
                clone[0].GetComponent<Renderer>().enabled = true;
                clone[0].GetComponent<LineRenderer>().enabled = true;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)){
                action[0] = MovementType.Left;
                direction = new UnityEngine.Vector3(-1, 0, 0);
                clone[0].transform.position = obj[0].transform.position+direction*Models.Pacman.Speed;
                clone[0].GetComponent<Renderer>().enabled = true;
                clone[0].GetComponent<LineRenderer>().enabled = true;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)){
                action[0] = MovementType.Right;
                direction = new UnityEngine.Vector3(1, 0, 0);
                clone[0].transform.position = obj[0].transform.position+direction*Models.Pacman.Speed;
                clone[0].GetComponent<Renderer>().enabled = true;
                clone[0].GetComponent<LineRenderer>().enabled = true;
            }
        }else{
            ChangeColorToGreen(obj[index]);
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)){
                Debug.Log("Up");
                action[index] = MovementType.Up;
                direction = new UnityEngine.Vector3(0, 1, 0);
                clone[index].transform.position = obj[index].transform.position+direction*Models.Ghost.AllGhosts[index].Speed;
                clone[index].GetComponent<Renderer>().enabled = true;
                clone[index].GetComponent<LineRenderer>().enabled = true;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)){
                Debug.Log("Down");
                action[index] = MovementType.Down;
                direction = new UnityEngine.Vector3(0, -1, 0);
                clone[index].transform.position = obj[index].transform.position+direction*Models.Ghost.AllGhosts[index].Speed;
                clone[index].GetComponent<Renderer>().enabled = true;
                clone[index].GetComponent<LineRenderer>().enabled = true;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)){
                Debug.Log("Left");
                action[index] = MovementType.Left;
                direction = new UnityEngine.Vector3(-1, 0, 0);
                clone[index].transform.position = obj[index].transform.position+direction*Models.Ghost.AllGhosts[index].Speed;
                clone[index].GetComponent<Renderer>().enabled = true;
                clone[index].GetComponent<LineRenderer>().enabled = true;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)){
                Debug.Log("Right");
                action[index] = MovementType.Right;
                direction = new UnityEngine.Vector3(1, 0, 0);
                clone[index].transform.position = obj[index].transform.position+direction*Models.Ghost.AllGhosts[index].Speed;
                clone[index].GetComponent<Renderer>().enabled = true;
                clone[index].GetComponent<LineRenderer>().enabled = true;
            }else if(Input.GetKeyDown(KeyCode.Alpha1)){
                Debug.Log("alpha1");
                ChangeColorToOrange(obj[index]);
                index = 0;
                ChangeColorToGreen(obj[index]);
            }else if(Input.GetKeyDown(KeyCode.Alpha2)){
                Debug.Log("alpha2");
                ChangeColorToOrange(obj[index]);
                index = 1;
                ChangeColorToGreen(obj[index]);
            }else if(Input.GetKeyDown(KeyCode.Alpha3)){
                Debug.Log("alpha3");
                ChangeColorToOrange(obj[index]);
                index = 2;
                ChangeColorToGreen(obj[index]);
            }
        }
        for(int i = 0; i < targetnum; i++){
            LineRenderer lineRenderer = clone[i].GetComponent<LineRenderer>();
            if (lineRenderer != null){
                lineRenderer.SetPosition(0, obj[i].transform.position);
                lineRenderer.SetPosition(1, clone[i].transform.position);
            }
        }
        // 检查Enter键是否被按下（在PC上通常是回车键）
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Debug.Log("Enter key pressed");
            for(int i = 0; i < targetnum; i++){
                if(action[i] == MovementType.Zero){
                    return;
                }
            }
            if(InteractController.role == 0 || InteractController.other_finish){
                Debug.Log("发送");
                GameObject.Find("Main Controller").GetComponent<WebInteractionController>().SendAction(new Operation(ConvertEnumListToIntList(action)));
                pausekeyboard(1.5f);
            }else{
                Debug.Log("等待对方完成");
            }
        }
    }

    public void awakekeyboard(){
        isactive = true;
        Start();
    }

    public void pausekeyboard(float time){
        isactive = false;
        for(int i = 0; i < targetnum; i++){
            Destroy(clone[i]);
            if(InteractController.role == 1) ChangeColorToOrange(obj[i]);
        }
        clone = new List<GameObject>();
        obj = new List<GameObject>();
        action = new List<MovementType>();
        Invoke("awakekeyboard", time);
    }

    private GameObject ChangeColorToRed(GameObject clone)
    {
        // 获取克隆GameObject的SpriteRenderer组件
        SpriteRenderer spriteRenderer = clone.GetComponent<SpriteRenderer>();

        // 如果存在SpriteRenderer组件，则改变颜色
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
        }
        return clone;
    }
    private GameObject ChangeColorToGreen(GameObject clone)
    {
        // 获取克隆GameObject的SpriteRenderer组件
        SpriteRenderer spriteRenderer = clone.GetComponent<SpriteRenderer>();

        // 如果存在SpriteRenderer组件，则改变颜色
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.green;
        }
        return clone;
    }

    private GameObject ChangeColorToOrange(GameObject clone)
    {
        Color newColor = new Color32(0xF9, 0x89, 0x36, 0xFF);
        // 获取克隆GameObject的SpriteRenderer组件
        SpriteRenderer spriteRenderer = clone.GetComponent<SpriteRenderer>();

        // 如果存在SpriteRenderer组件，则改变颜色
        if (spriteRenderer != null)
        {
            spriteRenderer.color = newColor;
        }
        return clone;
    }

    public static List<int> ConvertEnumListToIntList(List<MovementType> enumList)
    {
        List<int> intList = new List<int>();
        foreach (MovementType movementType in enumList)
        {
            intList.Add((int)movementType);
        }
        return intList;
    }
}
