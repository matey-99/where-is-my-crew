using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsController : MonoBehaviour
{
    private GameObject sword = default;

    public void Init(GameObject sword)
    {
        this.sword = sword;
    }

    public void ShowSword()
    {
        sword.SetActive(true);
    }

    public void HideSword()
    {
        sword.SetActive(false);
    }
}
