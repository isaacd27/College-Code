using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SodaTracker : MonoBehaviour
{

    public int maxsoda;
    int soda;

    public HPbar SodaBar;

    public Text keytext;
    public Text sodaText;

    int keys;

    //public int SodaChange

    public void IncreaseKeys(int echange)
    {
        keys += echange; //Insert Negative number to decrease
    }

    public int GetKeys()
    {
        return keys; 
    }

    public int GetSoda()
    {
        return soda;
    }

    //public void MaxSodaChange(int echange)

    // Start is called before the first frame update
    void Start()
    {
        SodaBar.SetMaxHP(maxsoda); //note: maxsoda can be increased, this way.
    }

    // Update is called once per frame
    void Update()
    {
        sodaText.text = (soda.ToString() + "/" + maxsoda.ToString());
        SodaBar.SetHp(soda);

        keytext.text = keys.ToString();


       // if(SodaChange != 0){
       // maxsoda = SodaChange;
       // SodaBar.SetMaxHP(maxsoda);
       // SodaChance = 0;
       // }


    }
}
