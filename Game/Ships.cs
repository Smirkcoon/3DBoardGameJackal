using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ships : MonoBehaviour
{
    public int x;
    public int z;
    GameObject[] foundClickPirat;
    public int PlayerHere = 3;
    Sprite ColorSpriteShips;
    public Sprite RedShip;
    public Sprite WhiteShip;
    public Sprite YellowShip;
    public Sprite BlackShip;
    public GameObject WitchPiratMove;
    public bool CanClickOnFace = false;
    GameObject[] Cells;
    GameObject[] AllChildPirat;
    GameObject[] WaterCells;
    public GameObject Pirat1, Pirat2, Pirat3;
    public int Beforex;
    public int Beforez;
    public GameObject BeforeCell;
    public List<GameObject> VsegoMonetSobrano = new List<GameObject>();
    public GameObject VsegoMonetSobranoT;
    public List<GameObject> alldied = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {       
        Cells = GameObject.FindGameObjectsWithTag("MainCell");
        ColorSpriteShips = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        AllChildPirat = GameObject.FindGameObjectsWithTag("ChildPirat");
        WaterCells = GameObject.FindGameObjectsWithTag("Water");
        if (ColorSpriteShips == BlackShip)
        {
            VsegoMonetSobranoT = GameObject.FindGameObjectWithTag("AllGoldBlack");
            foundClickPirat = GameObject.FindGameObjectsWithTag("PiratBlack");
            foreach (GameObject Pirat in foundClickPirat)
            {
                FirstPosition(Pirat);
            }
        }
        if (ColorSpriteShips == RedShip)
        {
            VsegoMonetSobranoT = GameObject.FindGameObjectWithTag("AllGoldRed");
            foundClickPirat = GameObject.FindGameObjectsWithTag("PiratRed");
            foreach (GameObject Pirat in foundClickPirat)
            {
                FirstPosition(Pirat);
            }
        }
        if (ColorSpriteShips == YellowShip)
        {
            VsegoMonetSobranoT = GameObject.FindGameObjectWithTag("AllGoldYellow");
            foundClickPirat = GameObject.FindGameObjectsWithTag("PiratYellow");
            foreach (GameObject Pirat in foundClickPirat)
            {
                FirstPosition(Pirat);
            }
        }
        if (ColorSpriteShips == WhiteShip)
        {
            VsegoMonetSobranoT = GameObject.FindGameObjectWithTag("AllGoldWhite");
            foundClickPirat = GameObject.FindGameObjectsWithTag("PiratWhite");
            foreach (GameObject Pirat in foundClickPirat)
            {
                FirstPosition(Pirat);
            }
        }
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
            NegativCountOfPlayer(Beforex, Beforez, WitchPiratMove);
            
            if (WitchPiratMove.GetComponent<ClickPirat>().MyShip == this.gameObject)
            {
                WitchPiratMove.GetComponent<ClickPirat>().CellToMove = transform.position;
                WitchPiratMove.transform.position = transform.position;
                WitchPiratMove.GetComponent<ClickPirat>().x = x;
                WitchPiratMove.GetComponent<ClickPirat>().z = z;
                WitchPiratMove.GetComponent<ClickPirat>().InWater = false;
                WitchPiratMove.GetComponent<ClickPirat>().InShip = true;
                WitchPiratMove.GetComponent<ClickPirat>().NowCell = this.gameObject;
                if (WitchPiratMove.GetComponent<ClickPirat>().WithGold == true) 
                {
                    VsegoMonetSobrano.Add(WitchPiratMove.GetComponent<ClickPirat>().Monet);
                    Destroy(WitchPiratMove.GetComponent<ClickPirat>().Monet);
                    WitchPiratMove.GetComponent<ClickPirat>().Monet = null;
                    WitchPiratMove.GetComponent<ClickPirat>().WithGold = false;
                    Debug.Log("всего монет собрано" + VsegoMonetSobrano.Count);
                    VsegoMonetSobranoT.GetComponent<Text>().text = "(" + VsegoMonetSobrano.Count + ")";
                }
                WitchPiratMove.GetComponent<ClickPirat>().DeselectCells();

                WitchPiratMove.GetComponent<ClickPirat>().EndTurn();
            }
            else
            {
                if (WitchPiratMove.GetComponent<ClickPirat>().IsSelected == true)
                {                    
                    WitchPiratMove.GetComponent<ClickPirat>().EndTurn();
                    WitchPiratMove.GetComponent<ClickPirat>().IsDead = true;
                    WitchPiratMove.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().alldied.Add(WitchPiratMove);
                    WitchPiratMove.SetActive(false);
                }
                else
                {                
                    WitchPiratMove.GetComponent<ClickPirat>().IsDead = true;
                    WitchPiratMove.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().alldied.Add(WitchPiratMove);
                    WitchPiratMove.SetActive(false);
                }
            }
            WitchPiratMove.GetComponent<ClickPirat>().CheckCellMonet();
            Invoke("MoveInsideCell", 0.2f);
        }
    }
    //при убистве пиратов с возращением на корабль
    public void OnMouseUpForKill()
    {

        if (WitchPiratMove.GetComponent<ClickPirat>().MyShip == this.gameObject)
        {
            BeforeCell = WitchPiratMove.GetComponent<ClickPirat>().NowCell;
            NegativCountOfPlayer(Beforex, Beforez, WitchPiratMove);
            WitchPiratMove.GetComponent<ClickPirat>().Monet = null;
            WitchPiratMove.GetComponent<ClickPirat>().WithGold = false;            
            WitchPiratMove.GetComponent<ClickPirat>().NowCell = this.gameObject;
            WitchPiratMove.transform.position = transform.position;
            WitchPiratMove.GetComponent<ClickPirat>().CellToMove = transform.position;            
            WitchPiratMove.GetComponent<ClickPirat>().x = x;
            WitchPiratMove.GetComponent<ClickPirat>().z = z;
            WitchPiratMove.GetComponent<ClickPirat>().InWater = false;
            WitchPiratMove.GetComponent<ClickPirat>().InShip = true;
            WitchPiratMove.GetComponent<ClickPirat>().CheckCellMonet();
            MoveInsideCell();
        }

    }
    //чтобы подбирать с воды пиратов и перемещатся по воде
    public void OnMouseUpBezEndTurn()
    {
        foreach (GameObject ChildPirat in AllChildPirat)
        {
            if (ChildPirat.GetComponentInParent<ClickPirat>() != null)
            {
                ChildPirat.GetComponentInParent<ClickPirat>().CSAP = true;
            }
        }
        if (WitchPiratMove.GetComponent<ClickPirat>().MyShip == this.gameObject)
        {
            WitchPiratMove.GetComponent<ClickPirat>().NowCell = gameObject;
            WitchPiratMove.GetComponent<ClickPirat>().CellToMove = transform.position;
            WitchPiratMove.transform.position = transform.position;
            WitchPiratMove.GetComponent<ClickPirat>().InWater = false;
            WitchPiratMove.GetComponent<ClickPirat>().InShip = true;
            BeforeCell = gameObject;
            NegativCountOfPlayer(x, z, WitchPiratMove);
            MoveInsideCell();
        }
        else
        {
            WitchPiratMove.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().alldied.Add(WitchPiratMove);
            WitchPiratMove.SetActive(false);
        }     
    }
    public void MoveInsideCell()
    {
        PlayerHere += 1;
        if (Pirat1 == null)
        {
            Pirat1 = WitchPiratMove;
            Debug.Log("Pirat1");
            Pirat1.GetComponent<ClickPirat>().CellToMove = new Vector3(Pirat1.GetComponent<ClickPirat>().CellToMove.x, 0.03f, Pirat1.GetComponent<ClickPirat>().CellToMove.z);
        }
        else
        {
            if (Pirat2 == null)
            {
                Debug.Log("Pirat2");
                Pirat2 = WitchPiratMove;
                Pirat2.GetComponent<ClickPirat>().CellToMove = new Vector3(Pirat2.GetComponent<ClickPirat>().CellToMove.x - 0.4f, 0.03f, Pirat2.GetComponent<ClickPirat>().CellToMove.z);
            }
            else
            {
                if (Pirat3 == null)
                {
                    Debug.Log("Pirat3");
                    Pirat3 = WitchPiratMove;
                    Pirat3.GetComponent<ClickPirat>().CellToMove = new Vector3(Pirat3.GetComponent<ClickPirat>().CellToMove.x + 0.4f, 0.03f, Pirat3.GetComponent<ClickPirat>().CellToMove.z);
                }
            }
        }
    }
public void NegativCountOfPlayer(int xx, int zz, GameObject Pirat)
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
                    if (Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().Pirat1 == Pirat)
                    {
                        Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().Pirat1 = null;
                    }
                    if (Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().Pirat2 == Pirat)
                    {
                        Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().Pirat2 = null;
                    }
                    if (Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().Pirat3 == Pirat)
                    {
                        Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().Pirat3 = null;
                    }
                }
            }
        }
    }
    public void FirstPosition(GameObject pirat) 
    {
        pirat.GetComponent<ClickPirat>().CellToMove = transform.position;
        pirat.transform.position = transform.position;       
        pirat.GetComponent<ClickPirat>().NowCell = this.gameObject;
        pirat.GetComponent<ClickPirat>().MyShip = this.gameObject;
        pirat.GetComponent<ClickPirat>().x = x;
        pirat.GetComponent<ClickPirat>().z = z;
        if (Pirat1 == null)
        {
            Pirat1 = pirat;
            Pirat1.GetComponent<ClickPirat>().CellToMove = new Vector3(transform.position.x, 0.03f, transform.position.z);
        }
        else
        {
            if (Pirat2 == null)
            {
                Pirat2 = pirat;
                Pirat2.GetComponent<ClickPirat>().CellToMove = new Vector3(transform.position.x - 0.4f, 0.03f, transform.position.z);
            }
            else
            {
                if (Pirat3 == null)
                {
                    Pirat3 = pirat;
                    Pirat3.GetComponent<ClickPirat>().CellToMove = new Vector3(transform.position.x + 0.4f, 0.03f, transform.position.z);
                }
            }
        }
    }
}
