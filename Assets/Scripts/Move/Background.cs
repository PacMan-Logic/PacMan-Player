using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    public Sprite[] sprites;
    Image image;
    public int last_idx;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = sprites[0];
        last_idx = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Models.Data.level - 1 != last_idx)
        {
            image.sprite = sprites[Models.Data.level - 1];
            last_idx = Models.Data.level - 1;
        }
    }
}
