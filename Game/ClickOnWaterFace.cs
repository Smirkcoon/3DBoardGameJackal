using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnWaterFace : MonoBehaviour
{
    public int x;
    public int z;
    public GameObject Pirat1, Pirat2, Pirat3;
    public GameObject MainCell;
    public GameObject WitchPiratMove;
    public bool CanClickOnFace = false;
    public int PlayerHere = 0;
    GameObject[] ShipCells;
    GameObject[] AllChildPirat;
    GameObject[] Cells;
    GameObject[] WaterCells;

    public int Beforex;
    public int Beforez;
    public GameObject BeforeCell;
    // Start is called before the first frame update
    void Start()
    {
        ShipCells = GameObject.FindGameObjectsWithTag("Ships");
        AllChildPirat = GameObject.FindGameObjectsWithTag("ChildPirat");
        Cells = GameObject.FindGameObjectsWithTag("MainCell");
        WaterCells = GameObject.FindGameObjectsWithTag("Water");
    }

    public void OnMouseUp()
    {

        if (CanClickOnFace == true)
        {
            foreach (GameObject ChildPirat in AllChildPirat)
            {
                if (ChildPirat.GetComponentInParent<ClickPirat>() != null)
                {
                    ChildPirat.GetComponentInParent<ClickPirat>().CSAP = true;
                }
            }
            WitchPiratMove.GetComponent<ClickPirat>().CameraLookAtMe();
            Beforex = WitchPiratMove.GetComponent<ClickPirat>().x;
            Beforez = WitchPiratMove.GetComponent<ClickPirat>().z;
            BeforeCell = WitchPiratMove.GetComponent<ClickPirat>().NowCell;
            if (WitchPiratMove.GetComponent<ClickPirat>().WithGold == true)
            {
                WitchPiratMove.GetComponent<ClickPirat>().WithGold = false;
                Destroy(WitchPiratMove.GetComponent<ClickPirat>().Monet);
                WitchPiratMove.GetComponent<ClickPirat>().CheckCellMonet();
            }
 
            KillOrNot();
            NegativCountOfPlayer(Beforex, Beforez);
            MoveInsideCell();
            if (WitchPiratMove.GetComponent<ClickPirat>().InShip == false)
            {
                WitchPiratMove.GetComponent<ClickPirat>().CellToMove = transform.position;
                WitchPiratMove.transform.position = transform.position;
                WitchPiratMove.GetComponent<ClickPirat>().x = x;
                WitchPiratMove.GetComponent<ClickPirat>().z = z;
                WitchPiratMove.GetComponent<ClickPirat>().InWater = true;
                WitchPiratMove.GetComponent<ClickPirat>().NowCell = this.gameObject;
                WitchPiratMove.GetComponent<ClickPirat>().DeselectCells();

                WitchPiratMove.GetComponent<ClickPirat>().EndTurn();

                Debug.Log("клик на воду1 без корабля");
            }
            else
            {
                Debug.Log("клик на воду2 с кораблем");
                Vector3 vec = transform.position;
                GameObject Ship = WitchPiratMove.GetComponent<ClickPirat>().MyShip;
                int sx = Ship.GetComponent<Ships>().x;
                int sz = Ship.GetComponent<Ships>().z;
                int px = WitchPiratMove.GetComponent<ClickPirat>().x;
                int pz = WitchPiratMove.GetComponent<ClickPirat>().z;
                if (px == sx && pz == sz)
                {
                    this.transform.parent.transform.position = Ship.transform.position;
                    Ship.transform.position = vec;
 
                    foreach (GameObject ChildPirat in AllChildPirat)
                    {
                        if (ChildPirat.GetComponentInParent<ClickPirat>() != null)
                        {
                            if (ChildPirat.GetComponentInParent<ClickPirat>().x == x)
                            {
                                if (ChildPirat.GetComponentInParent<ClickPirat>().z == z)
                                {
                                    if (ChildPirat.GetComponentInParent<ClickPirat>().MyShip == WitchPiratMove.GetComponentInParent<ClickPirat>().MyShip)
                                    {
                                        Debug.Log("свой в воде подобран");
                                        WitchPiratMove = ChildPirat.transform.parent.gameObject;
                                        NegativCountOfPlayer(x,z);
                                        ChildPirat.GetComponentInParent<ClickPirat>().MyShip.GetComponent<Ships>().WitchPiratMove = ChildPirat.transform.parent.gameObject;
                                        ChildPirat.GetComponentInParent<ClickPirat>().MyShip.GetComponent<Ships>().OnMouseUpBezEndTurn();                                      
                                    }
                                }
                            }
                            if (ChildPirat.GetComponentInParent<ClickPirat>().x == sx)
                            {
                                if (ChildPirat.GetComponentInParent<ClickPirat>().z == sz)
                                {                                   
                                    WitchPiratMove = ChildPirat.transform.parent.gameObject;
                                    NegativCountOfPlayer(x, z);
                                    ChildPirat.GetComponentInParent<ClickPirat>().x = x;
                                    ChildPirat.GetComponentInParent<ClickPirat>().z = z;
                                    ChildPirat.transform.parent.transform.position = Ship.transform.position;
                                    ChildPirat.GetComponentInParent<ClickPirat>().MyShip.GetComponent<Ships>().WitchPiratMove = ChildPirat.transform.parent.gameObject;                                    
                                    ChildPirat.GetComponentInParent<ClickPirat>().MyShip.GetComponent<Ships>().OnMouseUpBezEndTurn();
                                    Debug.Log("" + ChildPirat);                                        
                                }
                            }
                        }
                    }
                    WitchPiratMove.GetComponent<ClickPirat>().DeselectCells();
                    WitchPiratMove.GetComponent<ClickPirat>().EndTurn();
                    Ship.GetComponent<Ships>().x = x;
                    Ship.GetComponent<Ships>().z = z;
                    x = sx;
                    z = sz;                 
                }
            }
        }
    }
    public void KillOrNot()
    {
        if (Pirat1 != null)
        {
            if (Pirat1.tag != WitchPiratMove.tag)
            {
                Pirat1.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().WitchPiratMove = Pirat1;
                Pirat1.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().OnMouseUpForKill();
                Pirat1 = null;
            }
        }
        if (Pirat2 != null)
        {
            if (Pirat2.tag != WitchPiratMove.tag)
            {
                Pirat2.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().WitchPiratMove = Pirat2;
                Pirat2.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().OnMouseUpForKill();
                Pirat2 = null;
            }
        }
        if (Pirat3 != null)
        {
            if (Pirat3.tag != WitchPiratMove.tag)
            {
                Pirat3.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().WitchPiratMove = Pirat3;
                Pirat3.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().OnMouseUpForKill();
                Pirat3 = null;
            }
        }
    }
    public void MoveInsideCell()
    {
        PlayerHere += 1;
        if (Pirat1 == null)
        {
            Pirat1 = WitchPiratMove;
            Pirat1.GetComponent<ClickPirat>().CellToMove = new Vector3(Pirat1.GetComponent<ClickPirat>().CellToMove.x, 0.03f, Pirat1.GetComponent<ClickPirat>().CellToMove.z);
        }
        else
        {
            if (Pirat2 == null)
            {
                Pirat2 = WitchPiratMove;
                Pirat2.GetComponent<ClickPirat>().CellToMove = new Vector3(Pirat2.GetComponent<ClickPirat>().CellToMove.x - 0.4f, 0.03f, Pirat2.GetComponent<ClickPirat>().CellToMove.z);
            }
            else
            {
                if (Pirat3 == null)
                {
                    Pirat3 = WitchPiratMove;
                    Pirat3.GetComponent<ClickPirat>().CellToMove = new Vector3(Pirat3.GetComponent<ClickPirat>().CellToMove.x + 0.4f, 0.03f, Pirat3.GetComponent<ClickPirat>().CellToMove.z);
                }
            }
        }
    }
    public void NegativCountOfPlayer(int xx, int zz)
    {
        if (BeforeCell.GetComponent<ClickOnFaceOfCell>() != null)
        {
            if (BeforeCell.GetComponent<ClickOnFaceOfCell>().PlayerHere > 0)
            {               
                BeforeCell.GetComponent<ClickOnFaceOfCell>().PlayerHere -= 1;
            }
            if (BeforeCell.GetComponent<ClickOnFaceOfCell>().Pirat1 == WitchPiratMove)
            {
                BeforeCell.GetComponent<ClickOnFaceOfCell>().Pirat1 = null;
            }
            if (BeforeCell.GetComponent<ClickOnFaceOfCell>().Pirat2 == WitchPiratMove)
            {
                BeforeCell.GetComponent<ClickOnFaceOfCell>().Pirat2 = null;
            }
            if (BeforeCell.GetComponent<ClickOnFaceOfCell>().Pirat3 == WitchPiratMove)
            {
                BeforeCell.GetComponent<ClickOnFaceOfCell>().Pirat3 = null;
            }
            if (BeforeCell.GetComponent<ClickOnFaceOfCell>().Pirat4 == WitchPiratMove)
            {
                BeforeCell.GetComponent<ClickOnFaceOfCell>().Pirat4 = null;
            }
            if (BeforeCell.GetComponent<ClickOnFaceOfCell>().Pirat5 == WitchPiratMove)
            {
                BeforeCell.GetComponent<ClickOnFaceOfCell>().Pirat5 = null;
            }
        }       
        foreach (GameObject Cell in WaterCells)
        {
            if (Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().x == xx)
            {
                if (Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().z == zz)
                {
                    if (PlayerHere > 0)
                    {
                        PlayerHere -= 1;
                    }
                    if (Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().Pirat1 == WitchPiratMove)
                    {
                        Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().Pirat1 = null;
                    }
                    if (Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().Pirat2 == WitchPiratMove)
                    {
                        Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().Pirat2 = null;
                    }
                    if (Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().Pirat3 == WitchPiratMove)
                    {
                        Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().Pirat3 = null;
                    }
                }
            }
        }
    }
}
