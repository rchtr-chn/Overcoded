using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSpriteScript : MonoBehaviour
{
    public List<Sprite> spritesList = new List<Sprite>();
    public Image banner;

    private void Start()
    {
        if(banner == null) banner = GetComponent<Image>();
        int choice = Random.Range(0, spritesList.Count);
        banner.sprite = spritesList[choice];
    }
}
