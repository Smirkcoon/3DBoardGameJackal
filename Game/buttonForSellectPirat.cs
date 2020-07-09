using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonForSellectPirat : MonoBehaviour
{
    public GameObject MainPirat;
    public Sprite Pick;
    public Sprite NoPick;
    public Sprite PickMonet;
    public Sprite NoPickMonet;
    public Sprite Dead;

    public bool canSelect = false;


    // Start is called before the first frame update
    void Awake()
    {
        MainPirat.GetComponent<ClickPirat>().ButtonForSellectPirat = this.gameObject;
    }
    public void OnMouseUp()//detects if a mouse button is pressed
    {
        if (canSelect)
        {
            MainPirat.GetComponent<ClickPirat>().OnMouseUp();
            
        }
    }
    public void pick()
    {
        GetComponent<SpriteRenderer>().sprite = Pick;
    }
    public void Nopick()
    {
        GetComponent<SpriteRenderer>().sprite = NoPick;
    }
    public void pickMonet()
    {
        GetComponent<SpriteRenderer>().sprite = PickMonet;
    }
    public void NopickMonet()
    {
        GetComponent<SpriteRenderer>().sprite = NoPickMonet;
    }
    public void dead()
    {
        GetComponent<SpriteRenderer>().sprite = Dead;
    }
}
