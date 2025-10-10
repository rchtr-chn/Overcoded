using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSpriteScript : MonoBehaviour
{
    public List<Sprite> SpritesList = new List<Sprite>();
    public Image Banner;

    private void Start()
    {
        if(Banner == null) Banner = GetComponent<Image>();
        int choice = Random.Range(0, SpritesList.Count);
        Banner.sprite = SpritesList[choice];
    }
}
