using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    public Slider slide;

    // Start is called before the first frame update

    public void SetMaxHP(int Mhp)
    {
        slide.maxValue = Mhp;
        slide.value = Mhp;
    }
    public void SetHp(int hp)
    {
        slide.value = hp;
    }
}
