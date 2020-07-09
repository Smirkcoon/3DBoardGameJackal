using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickPirat : MonoBehaviour
{
    public int x;
    public int z;   
    GameObject[] foundClickPirat;

    //можно выбрать другого пирата? CanSelectAnotherPirat
    public bool CSAP = true;
    public GameObject KillUnit;
    //чей сейчас ход
    public bool CanSelected = false;
    public bool BlackInGame;
    public bool RedInGame;
    public bool WhiteInGame;
    public bool YellowInGame;

    //разные состояния пирата
    public bool IsSelected = false;

    public bool InTrap = false;
    public bool InWater = false;
    public bool InHole = false;
    public bool InShip = true;
    public bool InVertolet = false;

    public bool IsDead = false;

    public bool WithGold = false;
    public bool UsedRom = false;
    public int StageInTrap = 0;

    public GameObject NowCell;

    public GameObject ButtonForSellectPirat;
    public GameObject ButtonForMonet;
    //разные ячейки
    GameObject[] Cells;
    GameObject[] WaterCells;
    GameObject[] ShipCells;
    GameObject[] AllChildPirat;

    public GameObject MyShip;

    public Camera cam;
    public GameObject Monet;
    public string Tag;
    public Vector3 CellToMove;

    public GameObject TCantplay;

    void Start()
    {
        Tag = this.gameObject.tag;
        Cells = GameObject.FindGameObjectsWithTag("MainCell");
        WaterCells = GameObject.FindGameObjectsWithTag("Water");
        ShipCells = GameObject.FindGameObjectsWithTag("Ships");
        AllChildPirat = GameObject.FindGameObjectsWithTag("ChildPirat");
    }
    public void FixedUpdate()
    {
        if (CellToMove != null)
        {           
            transform.position =  Vector3.Lerp(transform.position, CellToMove, 0.3f);
        }
    }
    public void FindNearPole()
    {
        DeselectCells();
        if (InVertolet == true) 
        {
            PInVertolet();
            return;
        }
        if (InWater == true)
        {
            PInWater();
            return;
        }
        if (InShip == true) 
        {
            PInShip();
            return;
        }
        if (InTrap == true) 
        {
            InTraptrue();
            return;
        }
        else
        {
            PInCell();
            return;
        }
    }
    public void DeselectCells()
    {
        foreach (GameObject Cell in Cells)
        {
            Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
            Cell.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
            Cell.transform.GetChild(1).GetComponent<ClickOnBackOfCell>().CanClickOnBack = false;
            Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().CanClickOnFace = false;
        }
        foreach (GameObject WCell in WaterCells)
        {                       
                WCell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                WCell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().CanClickOnFace = false;              
        }
        foreach (GameObject Ship in ShipCells)
        {
            Ship.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
            Ship.GetComponent<Ships>().CanClickOnFace = false;
        }

    }
    public void OnMouseUp() //detects if a mouse button is pressed
    {
        if (InHole == false)
        {
            if (UsedRom == false)
            {
                //можно выбрать другого пирата? CanSelectAnotherPirat для стрелок
                if (CSAP == true)
                {
                    if (CanSelected == true)
                    {
                        foundClickPirat = GameObject.FindGameObjectsWithTag("" + Tag);
                        Select();
                        //для кнопки убийства
                        KillUnit.GetComponent<KillUnit>().WitchPiratMove = gameObject;
                    }
                }
            }
        }
        else
        {           
            
            Debug.Log("Pirat in Hole");
        }
    }
    public void Select()
    {
        foreach (GameObject Pirat in foundClickPirat)
        {
            Pirat.GetComponent<ClickPirat>().ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().canSelect = true;
            cam.transform.position = new Vector3(transform.position.x, cam.transform.position.y, transform.position.z);
            if (Pirat.GetComponent<ClickPirat>().IsSelected == true)
            {                
                Pirat.transform.position = new Vector3(Pirat.transform.position.x, 0.03f, Pirat.transform.position.z);
                Pirat.GetComponent<ClickPirat>().IsSelected = false;
                Pirat.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("Selected", false);
                
                if (Pirat.GetComponent<ClickPirat>().WithGold == false)
                {
                    Pirat.GetComponent<ClickPirat>().ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().Nopick();
                }
                if (Pirat.GetComponent<ClickPirat>().WithGold == true)
                {
                    Pirat.GetComponent<ClickPirat>().ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().NopickMonet();
                }
            }
            if (Pirat == this.gameObject)
            {
                Pirat.GetComponent<ClickPirat>().IsSelected = true;
                this.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("Selected", true);
                transform.position = new Vector3(transform.position.x, 1, transform.position.z);
                FindNearPole();              
                if (WithGold == false)
                {
                    Pirat.GetComponent<ClickPirat>().ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().pick();
                }
                if (WithGold == true)
                {
                    Pirat.GetComponent<ClickPirat>().ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().pickMonet();
                }
            }           
        }
    }
    public void SelectNextPirat() 
    {
        if (foundClickPirat.Length == 0)
        {
            TCantplay.SetActive(true);
        }
        else
        {
            if (foundClickPirat[0].GetComponent<ClickPirat>() != null && foundClickPirat[0].GetComponent<ClickPirat>().InHole == false)
            {
                foundClickPirat[0].GetComponent<ClickPirat>().OnMouseUp();
            }
            else
            {
                if (foundClickPirat[1].GetComponent<ClickPirat>() != null && foundClickPirat[1].GetComponent<ClickPirat>().InHole == false)
                {
                    foundClickPirat[1].GetComponent<ClickPirat>().OnMouseUp();
                }
                else
                {
                    if (foundClickPirat[2].GetComponent<ClickPirat>() != null && foundClickPirat[2].GetComponent<ClickPirat>().InHole == false)
                    {
                        foundClickPirat[2].GetComponent<ClickPirat>().OnMouseUp();
                    }
                }
            }
        }
    }
    public void EndTurn()
    {
        AllChildPirat = GameObject.FindGameObjectsWithTag("ChildPirat");
        DeselectCells();
        IsSelected = false;
        this.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("Selected", false);
        StaticGameSettings.WhichTurn += 1;
        if (StaticGameSettings.WhichTurn == 4)
        {
            StaticGameSettings.WhichTurn = 0;
        }
        foreach (GameObject ChildPirat in AllChildPirat)
        {
            ChildPirat.GetComponentInParent<ClickPirat>().UsedRom = false;
            ChildPirat.GetComponentInParent<ClickPirat>().CanSelected = false;
            ChildPirat.GetComponentInParent<ClickPirat>().ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().canSelect = false;
            ChildPirat.GetComponentInParent<ClickPirat>().ButtonForMonet.GetComponent<buttonForMonet>().canclick = false;
        }
        switch (StaticGameSettings.WhichTurn)
        {
            case 0:
                if (RedInGame == false)
                {
                    EndTurn();
                    Debug.Log("RedTurnEND");
                }
                else
                {
                    foundClickPirat = GameObject.FindGameObjectsWithTag("PiratRed");
                    foreach (GameObject Pirat in foundClickPirat)
                    {
                        Pirat.GetComponent<ClickPirat>().CanSelected = true;
                        Pirat.GetComponent<ClickPirat>().ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().canSelect = true;
                        Pirat.GetComponent<ClickPirat>().ButtonForMonet.GetComponent<buttonForMonet>().canclick = true;
                    }
                    Debug.Log("RedTurn");
                    if (WithGold == false)
                    {
                        ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().Nopick();
                    }
                    if (WithGold == true)
                    {
                        ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().NopickMonet();
                    }
                    //выделяет рандомного                   
                    Invoke("SelectNextPirat", 0.6f);
                }
                break;
            case 1:
                if (YellowInGame == false)
                {
                    EndTurn();
                    Debug.Log("YellowTurnEND");
                }
                else
                {
                    foundClickPirat = GameObject.FindGameObjectsWithTag("PiratYellow");
                    foreach (GameObject Pirat in foundClickPirat)
                    {
                        Pirat.GetComponent<ClickPirat>().CanSelected = true;
                        Pirat.GetComponent<ClickPirat>().ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().canSelect = true;
                        Pirat.GetComponent<ClickPirat>().ButtonForMonet.GetComponent<buttonForMonet>().canclick = true;
                    }
                    Debug.Log("YellowTurn");
                    if (WithGold == false)
                    {
                        ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().Nopick();
                    }
                    if (WithGold == true)
                    {
                        ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().NopickMonet();
                    }
                    //выделяет рандомного
                    Invoke("SelectNextPirat", 0.6f);
                }
                break;
            case 2:
                if (BlackInGame == false)
                {
                    EndTurn();
                    Debug.Log("BlackTurnEND");
                }
                else
                {
                    foundClickPirat = GameObject.FindGameObjectsWithTag("PiratBlack");
                    foreach (GameObject Pirat in foundClickPirat)
                    {
                        Pirat.GetComponent<ClickPirat>().CanSelected = true;
                        Pirat.GetComponent<ClickPirat>().ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().canSelect = true;
                        Pirat.GetComponent<ClickPirat>().ButtonForMonet.GetComponent<buttonForMonet>().canclick = true;
                    }
                    Debug.Log("BlackTurn");
                    if (WithGold == false)
                    {
                        ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().Nopick();
                    }
                    if (WithGold == true)
                    {
                        ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().NopickMonet();
                    }
                    //выделяет рандомного
                    Invoke("SelectNextPirat", 0.6f);
                }
                break;
            case 3:
                if (WhiteInGame == false)
                {
                    EndTurn();
                    Debug.Log("WhiteTurnEND");
                }
                else
                {
                    foundClickPirat = GameObject.FindGameObjectsWithTag("PiratWhite");
                    foreach (GameObject Pirat in foundClickPirat)
                    {
                        Pirat.GetComponent<ClickPirat>().CanSelected = true;
                        Pirat.GetComponent<ClickPirat>().ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().canSelect = true;
                        Pirat.GetComponent<ClickPirat>().ButtonForMonet.GetComponent<buttonForMonet>().canclick = true;
                    }
                    Debug.Log("WhiteTurn");
                    if (WithGold == false)
                    {
                        ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().Nopick();
                    }
                    if (WithGold == true)
                    {
                        ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().NopickMonet();
                    }
                    //выделяет рандомного
                    Invoke("SelectNextPirat", 0.6f);
                }
                break;
        }
    }       
    public void Rom()
    {       
        UsedRom = true;       
        foundClickPirat = GameObject.FindGameObjectsWithTag(""+Tag);
        if (foundClickPirat[0].GetComponent<ClickPirat>().UsedRom == true)
        {
            if (foundClickPirat[1].GetComponent<ClickPirat>().UsedRom == true)
            {
                if (foundClickPirat[2].GetComponent<ClickPirat>().UsedRom == true)
                {
                    EndTurn();
                }
                else
                    if (foundClickPirat[2].GetComponent<ClickPirat>().InHole == true)
                {
                    foundClickPirat[2].GetComponent<ClickPirat>().UsedRom = true;
                    DeselectCells();
                    foundClickPirat[2].GetComponent<ClickPirat>().Rom();
                }
                else
                    foundClickPirat[2].GetComponent<ClickPirat>().OnMouseUp();
            }
            else
                if (foundClickPirat[1].GetComponent<ClickPirat>().InHole == true)
            {
                foundClickPirat[1].GetComponent<ClickPirat>().UsedRom = true;
                DeselectCells();
                foundClickPirat[1].GetComponent<ClickPirat>().Rom();
            }
            else
                foundClickPirat[1].GetComponent<ClickPirat>().OnMouseUp();
        }
        else
        {
            if (foundClickPirat[0].GetComponent<ClickPirat>().InHole == true)
            {
                foundClickPirat[0].GetComponent<ClickPirat>().UsedRom = true;
                DeselectCells();
                foundClickPirat[0].GetComponent<ClickPirat>().Rom();
            }
            else
                foundClickPirat[0].GetComponent<ClickPirat>().OnMouseUp();
        }     
    }
    public void PInVertolet() 
    {      
        foreach (GameObject Cell in Cells)
        {
            int px = Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().x;
            int pz = Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().z;
            Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().WitchPiratMove = this.gameObject;
            if (Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat1 == null || Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat1.tag == this.gameObject.tag)
            {
                if (Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat2 == null || Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat2.tag == this.gameObject.tag)
                {
                    if (Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat3 == null || Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat3.tag == this.gameObject.tag)
                    {
                        string spname = Cell.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
                        if (spname != "19" && spname != "20" && spname != "21" && spname != "22" && spname != "23" && spname != "24")
                        {
                            if (WithGold == false)
                            {
                                Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                                Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().CanClickOnFace = true;
                            }
                            if (WithGold == true)
                            {
                                if (spname != "92" && spname != "93" && spname != "94")
                                {
                                    Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                                    Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().CanClickOnFace = true;
                                }
                            }
                        }
                        if (px == x + 1 && pz == z + 1 || px == x + 1 && pz == z - 1 || px == x - 1 && pz == z + 1 || px == x - 1 && pz == z - 1 || px == x + 1 && pz == z || px == x - 1 && pz == z || px == x && pz == z + 1 || px == x && pz == z - 1)
                        {

                            Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                            Cell.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
                            Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().CanClickOnFace = true;
                            Cell.transform.GetChild(1).GetComponent<ClickOnBackOfCell>().CanClickOnBack = true;
                            Cell.transform.GetChild(1).GetComponent<ClickOnBackOfCell>().WitchPiratMove = this.gameObject;
                            Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().WitchPiratMove = this.gameObject;
                        }
                    }
                }

            }
        }
    }
   
    public void PInShip() 
    {
        foreach (GameObject Cell in Cells)
        {
            int px = Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().x;
            int pz = Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().z;
            
                if (px == x + 1 && pz == z || px == x - 1 && pz == z || px == x && pz == z + 1 || px == x && pz == z - 1)
                {
                    Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                    Cell.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
                    Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().CanClickOnFace = true;
                    Cell.transform.GetChild(1).GetComponent<ClickOnBackOfCell>().CanClickOnBack = true;
                    Cell.transform.GetChild(1).GetComponent<ClickOnBackOfCell>().WitchPiratMove = this.gameObject;
                    Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().WitchPiratMove = this.gameObject;
                }
                else
                {
                    Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                    Cell.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
                    Cell.transform.GetChild(1).GetComponent<ClickOnBackOfCell>().CanClickOnBack = false;
                    Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().CanClickOnFace = false;
                }
            
        }
        foreach (GameObject Cell in WaterCells)
        {
            int px = Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().x;
            int pz = Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().z;
            if (px == x + 1 && pz == z + 1 || px == x + 1 && pz == z - 1 || px == x - 1 && pz == z + 1 || px == x - 1 && pz == z - 1 || px == x + 1 && pz == z || px == x - 1 && pz == z || px == x && pz == z + 1 || px == x && pz == z - 1)
            {
                Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().CanClickOnFace = true;
                Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().WitchPiratMove = this.gameObject;
            }
            else
            {
                Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().CanClickOnFace = false;
            }
        }
    }
    public void PInWater() 
    {
        foreach (GameObject Cell in Cells)
        {           
                Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                Cell.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
                Cell.transform.GetChild(1).GetComponent<ClickOnBackOfCell>().CanClickOnBack = false;
                Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().CanClickOnFace = false;           
        }
        foreach (GameObject Cell in WaterCells)
        {
            int px = Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().x;
            int pz = Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().z;
            if (px == x + 1 && pz == z + 1 || px == x + 1 && pz == z - 1 || px == x - 1 && pz == z + 1 || px == x - 1 && pz == z - 1 || px == x + 1 && pz == z || px == x - 1 && pz == z || px == x && pz == z + 1 || px == x && pz == z - 1)
            {
                Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().CanClickOnFace = true;
                Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().WitchPiratMove = this.gameObject;
            }
            else
            {
                Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().CanClickOnFace = false;
            }
        }
        foreach (GameObject Ship in ShipCells)
        {
            int px = Ship.GetComponent<Ships>().x;
            int pz = Ship.GetComponent<Ships>().z;
            if (px == x + 1 && pz == z + 1 || px == x + 1 && pz == z - 1 || px == x - 1 && pz == z + 1 || px == x - 1 && pz == z - 1 || px == x + 1 && pz == z || px == x - 1 && pz == z || px == x && pz == z + 1 || px == x && pz == z - 1)
            {
                Ship.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                Ship.GetComponent<Ships>().CanClickOnFace = true;
                Ship.GetComponent<Ships>().WitchPiratMove = this.gameObject;
            }
            else
            {
                Ship.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                Ship.GetComponent<Ships>().CanClickOnFace = false;
            }
        }
    }
    public void PInCell()
    {
        foreach (GameObject Cell in Cells)
        {
            string spname = Cell.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
            if (WithGold == true)
            {

                if (spname != "92" && spname != "93" && spname != "94")
                {
                    int px = Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().x;
                    int pz = Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().z;

                    if (px == x + 1 && pz == z + 1 || px == x + 1 && pz == z - 1 || px == x - 1 && pz == z + 1 || px == x - 1 && pz == z - 1 || px == x + 1 && pz == z || px == x - 1 && pz == z || px == x && pz == z + 1 || px == x && pz == z - 1)
                    {
                        if (Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat1 == null || gameObject.tag == Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat1.tag)
                        {
                            if (Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat2 == null || gameObject.tag == Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat2.tag)
                            {
                                if (Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat3 == null || gameObject.tag == Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat3.tag)
                                {
                                    if (Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat4 == null || gameObject.tag == Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat4.tag)
                                    {
                                        if (Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat5 == null || gameObject.tag == Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat5.tag)
                                        {
                                            Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                                            Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().CanClickOnFace = true;
                                            Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().WitchPiratMove = this.gameObject;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                        Cell.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
                        Cell.transform.GetChild(1).GetComponent<ClickOnBackOfCell>().CanClickOnBack = false;
                        Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().CanClickOnFace = false;
                    }
                }
            }
        //без монеты
            else
            {                
                int px = Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().x;
                int pz = Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().z;
                if (spname != "92" && spname != "93" && spname != "94")
                {
                    if (px == x + 1 && pz == z + 1 || px == x + 1 && pz == z - 1 || px == x - 1 && pz == z + 1 || px == x - 1 && pz == z - 1 || px == x + 1 && pz == z || px == x - 1 && pz == z || px == x && pz == z + 1 || px == x && pz == z - 1)
                    {

                        Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                        Cell.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
                        Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().CanClickOnFace = true;
                        Cell.transform.GetChild(1).GetComponent<ClickOnBackOfCell>().CanClickOnBack = true;
                        Cell.transform.GetChild(1).GetComponent<ClickOnBackOfCell>().WitchPiratMove = this.gameObject;
                        Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().WitchPiratMove = this.gameObject;

                    }
                    else
                    {

                        Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                        Cell.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
                        Cell.transform.GetChild(1).GetComponent<ClickOnBackOfCell>().CanClickOnBack = false;
                        Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().CanClickOnFace = false;
                    }
                }
                else 
                {
                    if (px == x + 1 && pz == z + 1 || px == x + 1 && pz == z - 1 || px == x - 1 && pz == z + 1 || px == x - 1 && pz == z - 1 || px == x + 1 && pz == z || px == x - 1 && pz == z || px == x && pz == z + 1 || px == x && pz == z - 1)
                    {
                        if (Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat1 == null || gameObject.tag == Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat1.tag)
                        {
                            if (Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat2 == null || gameObject.tag == Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat2.tag)
                            {
                                if (Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat3 == null || gameObject.tag == Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat3.tag)
                                {
                                    if (Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat4 == null || gameObject.tag == Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat4.tag)
                                    {
                                        if (Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat5 == null || gameObject.tag == Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat5.tag)
                                        {
                                            Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                                            Cell.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
                                            Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().CanClickOnFace = true;
                                            Cell.transform.GetChild(1).GetComponent<ClickOnBackOfCell>().CanClickOnBack = true;
                                            Cell.transform.GetChild(1).GetComponent<ClickOnBackOfCell>().WitchPiratMove = this.gameObject;
                                            Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().WitchPiratMove = this.gameObject;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //форт воскрешения
                        if (spname == "94" && MyShip.GetComponent<Ships>().alldied.Count > 0 && px == x && pz == z)
                        {
                            Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                            Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().CanClickOnFace = true;
                            Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().WitchPiratMove = this.gameObject;
                        }
                        else
                        {
                            Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                            Cell.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
                            Cell.transform.GetChild(1).GetComponent<ClickOnBackOfCell>().CanClickOnBack = false;
                            Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().CanClickOnFace = false;
                        }
                    }
                }
            }
        }
        foreach (GameObject Cell in WaterCells)
        {
            int px = Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().x;
            int pz = Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().z;
            if (px == x + 1 && pz == z + 1 || px == x + 1 && pz == z - 1 || px == x - 1 && pz == z + 1 || px == x - 1 && pz == z - 1 || px == x + 1 && pz == z || px == x - 1 && pz == z || px == x && pz == z + 1 || px == x && pz == z - 1)
            {
                Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().CanClickOnFace = false;
            }
            else
            {
                Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().CanClickOnFace = false;
            }            
        }
        foreach (GameObject Ship in ShipCells)
        {
            int px = Ship.GetComponent<Ships>().x;
            int pz = Ship.GetComponent<Ships>().z;
            if (px == x + 1 && pz == z + 1 || px == x + 1 && pz == z - 1 || px == x - 1 && pz == z + 1 || px == x - 1 && pz == z - 1 || px == x + 1 && pz == z || px == x - 1 && pz == z || px == x && pz == z + 1 || px == x && pz == z - 1)
            {
                Ship.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                Ship.GetComponent<Ships>().CanClickOnFace = true;
                Ship.GetComponent<Ships>().WitchPiratMove = this.gameObject;
            }
            else
            {
                Ship.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                Ship.GetComponent<Ships>().CanClickOnFace = false;
            }
        }
    }
//срабатывает когда в ловушке из тру
    public void InTraptrue() 
    {
        foreach (GameObject Cell in Cells)
        {          
            int px = Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().x;
            int pz = Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().z;           
                if (px == x && pz == z)
                {
                    Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                    Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().CanClickOnFace = true;
                    Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().WitchPiratMove = this.gameObject;
                }
                else
                {
                    Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                    Cell.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
                    Cell.transform.GetChild(1).GetComponent<ClickOnBackOfCell>().CanClickOnBack = false;
                    Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().CanClickOnFace = false;
                }            
        }
        foreach (GameObject Cell in WaterCells)
        {
            Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
            Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().CanClickOnFace = false;
        }
        foreach (GameObject Cell in ShipCells)
        {
            Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
            Cell.GetComponent<Ships>().CanClickOnFace = false;
        }
    }
        public void CheckCellMonet() 
    {
        if (IsDead == false)
        {
            if (NowCell.GetComponent<ClickOnFaceOfCell>() != null)
            {
                if (NowCell.GetComponent<ClickOnFaceOfCell>().MonetInCell.Count > 0 || WithGold == true)
                {
                    ButtonForMonet.SetActive(true);
                    ButtonForMonet.GetComponent<buttonForMonet>().goldbutton();
                }
                else
                {
                    ButtonForMonet.GetComponent<buttonForMonet>().goldbutton();
                    ButtonForMonet.SetActive(false);
                }
            }
            else
            {
                ButtonForMonet.GetComponent<buttonForMonet>().goldbutton();
                ButtonForMonet.SetActive(false);
            }
        }
        else 
        {
            ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().dead();
            ButtonForMonet.SetActive(false);
            WithGold = false;
            Destroy(Monet);
        }
    }
    public void CameraLookAtMe()
    {
        cam.transform.position = new Vector3(transform.position.x, cam.transform.position.y, transform.position.z);       
    }
}