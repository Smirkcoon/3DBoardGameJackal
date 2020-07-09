using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonForMonet : MonoBehaviour
{
    public GameObject MainPirat;
    public Sprite Plus;
    public Sprite Minus;
    public bool canclick = false;
    public void goldbutton()
    { 
        if (MainPirat.GetComponent<ClickPirat>().WithGold == true) 
        {
            GetComponent<SpriteRenderer>().sprite = Minus;
            return;
        }
        if (MainPirat.GetComponent<ClickPirat>().NowCell.GetComponent<ClickOnFaceOfCell>() != null)
        {
            if (MainPirat.GetComponent<ClickPirat>().WithGold == false && MainPirat.GetComponent<ClickPirat>().NowCell.GetComponent<ClickOnFaceOfCell>().MonetInCell.Count > 0)
            {
                GetComponent<SpriteRenderer>().sprite = Plus;
                return;
            }
            if (MainPirat.GetComponent<ClickPirat>().WithGold == false && MainPirat.GetComponent<ClickPirat>().NowCell.GetComponent<ClickOnFaceOfCell>().MonetInCell.Count == 0)
            {
                if (MainPirat.GetComponent<ClickPirat>().IsSelected == true)
                {
                    MainPirat.GetComponent<ClickPirat>().ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().pick();
                }
                else
                {
                    MainPirat.GetComponent<ClickPirat>().ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().Nopick();
                }
            }
        }
        else
        {
            if (MainPirat.GetComponent<ClickPirat>().IsSelected == true)
            {
                MainPirat.GetComponent<ClickPirat>().ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().pick();
            }
            else
            {
                MainPirat.GetComponent<ClickPirat>().ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().Nopick();
            }
        }
    }
    public void OnMouseUp() //detects if a mouse button is pressed
    {
        string spname = MainPirat.GetComponent<ClickPirat>().NowCell.GetComponent<SpriteRenderer>().sprite.name;
        Debug.Log("" + spname);

        if (spname != "25" && spname != "26" && spname != "27" && spname != "28" && spname != "29" && spname != "30" && spname != "31" && spname != "32" && spname != "33" && spname != "34" && spname != "35" && spname != "36" && spname != "37" && spname != "38" && spname != "39" && spname != "40" && spname != "41" && spname != "42" && spname != "43" && spname != "44" && spname != "45")
        {
            if (canclick == true)
            {
                if (MainPirat.GetComponent<ClickPirat>().WithGold == false && GetComponent<SpriteRenderer>().sprite == Plus)
                {
                    GetComponent<SpriteRenderer>().sprite = Minus;
                    MainPirat.GetComponent<ClickPirat>().WithGold = true;
                    MainPirat.GetComponent<ClickPirat>().NowCell.GetComponent<ClickOnFaceOfCell>().MonetHereMinus(this.gameObject);
                    if (MainPirat.GetComponent<ClickPirat>().IsSelected == true)
                    {
                        MainPirat.GetComponent<ClickPirat>().DeselectCells();
                        MainPirat.GetComponent<ClickPirat>().FindNearPole();
                        MainPirat.GetComponent<ClickPirat>().ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().pickMonet();
                    }
                    else
                    {
                        MainPirat.GetComponent<ClickPirat>().ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().NopickMonet();
                    }
                    return;
                }
                if (MainPirat.GetComponent<ClickPirat>().WithGold == true && GetComponent<SpriteRenderer>().sprite == Minus)
                {
                    GetComponent<SpriteRenderer>().sprite = Plus;
                    MainPirat.GetComponent<ClickPirat>().WithGold = false;
                    MainPirat.GetComponent<ClickPirat>().NowCell.GetComponent<ClickOnFaceOfCell>().MonetHerePlus(MainPirat.GetComponent<ClickPirat>().Monet);
                    MainPirat.GetComponent<ClickPirat>().Monet = null;
                    if (MainPirat.GetComponent<ClickPirat>().IsSelected == true)
                    {
                        MainPirat.GetComponent<ClickPirat>().DeselectCells();
                        MainPirat.GetComponent<ClickPirat>().FindNearPole();
                        MainPirat.GetComponent<ClickPirat>().ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().pick();
                    }
                    else
                    {
                        MainPirat.GetComponent<ClickPirat>().ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().Nopick();
                    }
                    return;
                }
            }
        }
    }
}
