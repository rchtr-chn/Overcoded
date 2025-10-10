using UnityEngine;

public class VirusButtonScript : MonoBehaviour
{
    public GameObject Ads;
    public void ClickButton()
    {
        Destroy(Ads);
    }
}
