using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusButtonScript : MonoBehaviour
{
    public GameObject ads;
    public void ClickButton()
    {
        Destroy(ads);
    }
}
