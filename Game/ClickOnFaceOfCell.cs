using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnFaceOfCell : MonoBehaviour
{
    //кординаты клетки
    public int x;
    public int z;
    public int PlayerHere = 0;

    public GameObject Moneta;
    public GameObject Pirat1, Pirat2, Pirat3, Pirat4, Pirat5;
    //с какой клетки пират пришел
    public GameObject BeforeCell;
    string BeforeSpName;
    public int Beforex;
    public int Beforez;

    public GameObject MainCell;
    public GameObject WitchPiratMove;
    public bool CanClickOnFace = false;
    public bool FirstVisit = true;
    public bool VertoletIsUsed = false;

    //для стрелок в основном
    GameObject[] AllChildPirat;
    GameObject[] Cells;
    GameObject[] WaterCells;
    GameObject[] ShipCells;
    public List<GameObject> MonetInCell = new List<GameObject>();
    public bool WitchPiratMoveInMove;

    //для движения пирата
    public float smoothTime = 0.3F;

    public float CellRotation;

    string SpName;
    Vector3 vec;

    void Start()
    {
        AllChildPirat = GameObject.FindGameObjectsWithTag("ChildPirat");
        Cells = GameObject.FindGameObjectsWithTag("MainCell");
        //Debug.Log(CellRotation);
        WaterCells = GameObject.FindGameObjectsWithTag("Water");
        ShipCells = GameObject.FindGameObjectsWithTag("Ships");
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
            WitchPiratMove.GetComponent<ClickPirat>().InShip = false;
            BeforeCell = WitchPiratMove.GetComponent<ClickPirat>().NowCell;
            Beforex = WitchPiratMove.GetComponent<ClickPirat>().x;
            Beforez = WitchPiratMove.GetComponent<ClickPirat>().z;
            WitchPiratMove.GetComponent<ClickPirat>().NowCell = this.gameObject;
            WitchPiratMove.GetComponent<ClickPirat>().CheckCellMonet();
            CheckWhatCell();
            KillOrNot();
            Invoke("MoveInsideCell", 0.2f);
            NegativCountOfPlayer(Beforex, Beforez, WitchPiratMove);      
        }
    }
    public void CheckWhatCell()
    {      
        if (BeforeCell !=null) 
        {
            if (BeforeCell.GetComponent<SpriteRenderer>() != null)
            {
                BeforeSpName = BeforeCell.GetComponent<SpriteRenderer>().sprite.name;
                switch (BeforeSpName)
                {
                    case "95":
                        foreach (GameObject ChildPirat in AllChildPirat)
                        {
                            if (ChildPirat.GetComponentInParent<ClickPirat>() != null)
                            {
                                ChildPirat.GetComponentInParent<ClickPirat>().InVertolet = false;
                            }
                        }
                        break;                       
                }
            }            
        }
        SpName = GetComponent<SpriteRenderer>().sprite.name;        
        switch (SpName)
        {
            //Озеро
            case "19":case "20":case "21":case "22":case "23":case "24":
                int xx = x;
                int zz = z;
                if ((Beforex - x) != 0)
                {
                    xx = (x - Beforex) + x;
                }
                if ((Beforez - z) != 0)
                {
                    zz = (z - Beforez) + z;
                }
                MoveToFaceCell(xx, zz);               
                break;
            //ловушка 2
            case "49":case "50":case "51":case "52":case "53":
                int StageInTrap2 = WitchPiratMove.GetComponent<ClickPirat>().StageInTrap;
                switch (StageInTrap2)
                {
                    case 0:
                        WitchPiratMove.GetComponent<ClickPirat>().CellToMove = transform.GetChild(1).gameObject.transform.position;
                        WitchPiratMove.GetComponent<ClickPirat>().x = x;
                        WitchPiratMove.GetComponent<ClickPirat>().z = z;
                        WitchPiratMove.GetComponent<ClickPirat>().InTrap = true;
                        WitchPiratMove.GetComponent<ClickPirat>().StageInTrap = 1;
                        WitchPiratMove.GetComponent<ClickPirat>().DeselectCells();
                        WitchPiratMove.GetComponent<ClickPirat>().EndTurn();
                        break;
                    case 1:
                        WitchPiratMove.GetComponent<ClickPirat>().CellToMove = transform.GetChild(2).gameObject.transform.position;
                        WitchPiratMove.GetComponent<ClickPirat>().InTrap = false;
                        WitchPiratMove.GetComponent<ClickPirat>().StageInTrap = 0;
                        WitchPiratMove.GetComponent<ClickPirat>().DeselectCells();
                        WitchPiratMove.GetComponent<ClickPirat>().EndTurn();
                        break;
                }
                break;
            //ловушка 3
            case "54":
            case "55":
            case "56":
            case "57":
                int StageInTrap3 = WitchPiratMove.GetComponent<ClickPirat>().StageInTrap;
                switch (StageInTrap3)
                {
                    case 0:
                        WitchPiratMove.GetComponent<ClickPirat>().CellToMove = transform.GetChild(3).gameObject.transform.position;
                        WitchPiratMove.GetComponent<ClickPirat>().x = x;
                        WitchPiratMove.GetComponent<ClickPirat>().z = z;
                        WitchPiratMove.GetComponent<ClickPirat>().InTrap = true;
                        WitchPiratMove.GetComponent<ClickPirat>().StageInTrap = 1;
                        WitchPiratMove.GetComponent<ClickPirat>().DeselectCells();
                        WitchPiratMove.GetComponent<ClickPirat>().EndTurn();
                        break;
                    case 1:
                        WitchPiratMove.GetComponent<ClickPirat>().CellToMove = transform.GetChild(4).gameObject.transform.position;
                        WitchPiratMove.GetComponent<ClickPirat>().StageInTrap = 2;
                        WitchPiratMove.GetComponent<ClickPirat>().DeselectCells();
                        WitchPiratMove.GetComponent<ClickPirat>().EndTurn();
                        break;
                    case 2:
                        WitchPiratMove.GetComponent<ClickPirat>().CellToMove = transform.GetChild(5).gameObject.transform.position;
                        WitchPiratMove.GetComponent<ClickPirat>().InTrap = false;
                        WitchPiratMove.GetComponent<ClickPirat>().StageInTrap = 0;
                        WitchPiratMove.GetComponent<ClickPirat>().DeselectCells();
                        WitchPiratMove.GetComponent<ClickPirat>().EndTurn();
                        break;
                }
                break;
            //ловушка 4
            case "58":
            case "59":
                int StageInTrap4 = WitchPiratMove.GetComponent<ClickPirat>().StageInTrap;
                switch (StageInTrap4)
                {
                    case 0:
                        WitchPiratMove.GetComponent<ClickPirat>().CellToMove = transform.GetChild(6).gameObject.transform.position;
                        WitchPiratMove.GetComponent<ClickPirat>().x = x;
                        WitchPiratMove.GetComponent<ClickPirat>().z = z;
                        WitchPiratMove.GetComponent<ClickPirat>().InTrap = true;
                        WitchPiratMove.GetComponent<ClickPirat>().StageInTrap = 1;
                        WitchPiratMove.GetComponent<ClickPirat>().DeselectCells();
                        WitchPiratMove.GetComponent<ClickPirat>().EndTurn();
                        break;
                    case 1:
                        WitchPiratMove.GetComponent<ClickPirat>().CellToMove = transform.GetChild(7).gameObject.transform.position;                       
                        WitchPiratMove.GetComponent<ClickPirat>().StageInTrap = 2;
                        WitchPiratMove.GetComponent<ClickPirat>().DeselectCells();
                        WitchPiratMove.GetComponent<ClickPirat>().EndTurn();
                        break;
                    case 2:
                        WitchPiratMove.GetComponent<ClickPirat>().CellToMove = transform.GetChild(8).gameObject.transform.position;
                        WitchPiratMove.GetComponent<ClickPirat>().StageInTrap = 3;
                        WitchPiratMove.GetComponent<ClickPirat>().DeselectCells();
                        WitchPiratMove.GetComponent<ClickPirat>().EndTurn();
                        break;
                    case 3:
                        WitchPiratMove.GetComponent<ClickPirat>().CellToMove = transform.GetChild(9).gameObject.transform.position;
                        WitchPiratMove.GetComponent<ClickPirat>().InTrap = false;
                        WitchPiratMove.GetComponent<ClickPirat>().StageInTrap = 0;
                        WitchPiratMove.GetComponent<ClickPirat>().DeselectCells();
                        WitchPiratMove.GetComponent<ClickPirat>().EndTurn();
                        break;
                }
                break;
            //ловушка 5
            case "60":
                int StageInTrap5 = WitchPiratMove.GetComponent<ClickPirat>().StageInTrap;
                switch (StageInTrap5)
                {
                    case 0:                        
                        WitchPiratMove.GetComponent<ClickPirat>().CellToMove = transform.GetChild(10).gameObject.transform.position;
                        WitchPiratMove.GetComponent<ClickPirat>().x = x;
                        WitchPiratMove.GetComponent<ClickPirat>().z = z;
                        WitchPiratMove.GetComponent<ClickPirat>().InTrap = true;
                        WitchPiratMove.GetComponent<ClickPirat>().StageInTrap = 1;
                        WitchPiratMove.GetComponent<ClickPirat>().DeselectCells();
                        WitchPiratMove.GetComponent<ClickPirat>().EndTurn();
                        break;
                    case 1:
                        WitchPiratMove.GetComponent<ClickPirat>().CellToMove = transform.GetChild(11).gameObject.transform.position;
                        WitchPiratMove.GetComponent<ClickPirat>().StageInTrap = 2;
                        WitchPiratMove.GetComponent<ClickPirat>().DeselectCells();
                        WitchPiratMove.GetComponent<ClickPirat>().EndTurn();
                        break;
                    case 2:
                        WitchPiratMove.GetComponent<ClickPirat>().CellToMove = transform.GetChild(12).gameObject.transform.position;
                        WitchPiratMove.GetComponent<ClickPirat>().StageInTrap = 3;
                        WitchPiratMove.GetComponent<ClickPirat>().DeselectCells();
                        WitchPiratMove.GetComponent<ClickPirat>().EndTurn();
                        break;
                    case 3:
                        WitchPiratMove.GetComponent<ClickPirat>().CellToMove = transform.GetChild(13).gameObject.transform.position;
                        WitchPiratMove.GetComponent<ClickPirat>().StageInTrap = 4;
                        WitchPiratMove.GetComponent<ClickPirat>().DeselectCells();
                        WitchPiratMove.GetComponent<ClickPirat>().EndTurn();
                        break;
                    case 4:
                        WitchPiratMove.GetComponent<ClickPirat>().CellToMove = transform.GetChild(14).gameObject.transform.position;
                        WitchPiratMove.GetComponent<ClickPirat>().InTrap = false;
                        WitchPiratMove.GetComponent<ClickPirat>().StageInTrap = 0;
                        WitchPiratMove.GetComponent<ClickPirat>().DeselectCells();
                        WitchPiratMove.GetComponent<ClickPirat>().EndTurn();
                        break;
                }
                break;
            //стрелки вправо
            case "25":
            case "26":
            case "27":
                StrelkaNaPirata();
                switch (CellRotation)
                {
                    case 0:
                        Strelka(x, z - 1);
                        break;
                    case 90:
                        Strelka(x - 1, z);
                        break;
                    case 180:
                        Strelka(x, z + 1);
                        break;
                    case 270:
                        Strelka(x + 1, z);
                        break;
                }
                break;
            //стрелки вправо-вверх
            case "28":
            case "29":
            case "30":
                StrelkaNaPirata();
                switch (CellRotation)
                {
                    case 0:
                        Strelka(x - 1, z - 1);
                        break;
                    case 90:
                        Strelka(x - 1, z + 1);
                        break;
                    case 180:
                        Strelka(x + 1, z + 1);
                        break;
                    case 270:
                        Strelka(x + 1, z - 1);
                        break;
                }
                break;
            //стрелки лево вправо
            case "31":
            case "32":
            case "33":
                StrelkaNaPirata();
                switch (CellRotation)
                {
                    case 0:
                        Strelka(x, z - 1);
                        Strelka(x, z + 1);
                        break;
                    case 90:
                        Strelka(x - 1, z);
                        Strelka(x + 1, z);
                        break;
                    case 180:
                        Strelka(x, z + 1);
                        Strelka(x, z - 1);
                        break;
                    case 270:
                        Strelka(x + 1, z);
                        Strelka(x - 1, z);
                        break;
                }
                break;
            //стрелки вправо-вверх влево-вниз
            case "34":
            case "35":
            case "36":
                StrelkaNaPirata();
                switch (CellRotation)
                {
                    case 0:
                        Strelka(x - 1, z - 1);
                        Strelka(x + 1, z + 1);
                        break;
                    case 90:
                        Strelka(x - 1, z + 1);
                        Strelka(x + 1, z - 1);
                        break;
                    case 180:
                        Strelka(x + 1, z + 1);
                        Strelka(x - 1, z - 1);
                        break;
                    case 270:
                        Strelka(x + 1, z - 1);
                        Strelka(x - 1, z + 1);
                        break;
                }
                break;
            //стрелки вправо вниз верх-лево
            case "37":
            case "38":
            case "39":
                StrelkaNaPirata();
                switch (CellRotation)
                {
                    case 0:
                        Strelka(x, z - 1);//которая вправо
                        Strelka(x + 1, z);// которая вниз
                        Strelka(x - 1, z + 1);
                        break;
                    case 90:
                        Strelka(x - 1, z);
                        Strelka(x, z - 1);
                        Strelka(x + 1, z + 1);
                        break;
                    case 180:
                        Strelka(x, z + 1);
                        Strelka(x - 1, z);
                        Strelka(x + 1, z - 1);
                        break;
                    case 270:
                        Strelka(x + 1, z);
                        Strelka(x, z + 1);
                        Strelka(x - 1, z - 1);
                        break;
                }
                break;
            //стрелки лево право верх низ
            case "40":
            case "41":
            case "42":
                StrelkaNaPirata();
                Strelka(x, z - 1);
                Strelka(x - 1, z);
                Strelka(x, z + 1);
                Strelka(x + 1, z);
                break;
            //стрелки по диагонали
            case "43":
            case "44":
            case "45":
                StrelkaNaPirata();
                Strelka(x - 1, z - 1);
                Strelka(x - 1, z + 1);
                Strelka(x + 1, z + 1);
                Strelka(x + 1, z - 1);
                break;
            //ловушки с ямой
            case "46":
            case "47":
            case "48":
                MovePlayerHere();
                if (PlayerHere < 1) 
                {
                    WitchPiratMove.GetComponent<ClickPirat>().InHole = true;
                }
                if (PlayerHere >= 1)
                {
                    foreach (GameObject ChildPirat in AllChildPirat)
                    {
                        if (ChildPirat.GetComponentInParent<ClickPirat>().x == x)
                        {                           
                            if (ChildPirat.GetComponentInParent<ClickPirat>().z == z)
                            {
                                ChildPirat.GetComponentInParent<ClickPirat>().InHole = false;
                            }
                        }
                    }
                }
                
                break;
            //ром
            case "61":
            case "62":
            case "63":
            case "64":
                WitchPiratMove.GetComponent<ClickPirat>().CellToMove = transform.position;
                WitchPiratMove.GetComponent<ClickPirat>().x = x;
                WitchPiratMove.GetComponent<ClickPirat>().z = z;
                WitchPiratMove.GetComponent<ClickPirat>().Rom();
                break;
            //ящер
            case "65":
            case "66":
            case "67":
            case "68":
                MoveToFaceCell(Beforex, Beforez);
                break;
            //воздушный шар
            case "69":
            case "70":
                WitchPiratMove.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().WitchPiratMove = WitchPiratMove;
                WitchPiratMove.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().CanClickOnFace = true;
                WitchPiratMove.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().OnMouseUp();                
                break;
            //пушка
            case "71":
            case "72":
                StrelkaNaPirata();
                switch (CellRotation)
                {
                    case 0:
                        Strelka(x = 0, z);
                        break;
                    case 90:
                        Strelka(x, z = 12);
                        break;
                    case 180:
                        Strelka(x = 12, z);
                        break;
                    case 270:
                        Strelka(x, z= 0);
                        break;
                }
                break;
            //монеты 1
            case "73":case "74":case "75":case "76":case "77":
                if (FirstVisit)
                {
                    vec = transform.GetChild(3).gameObject.transform.position;
                    vec.y += 0.5f;
                    GameObject Monetaclone = Instantiate(Moneta, vec, Quaternion.identity);
                    //Monet += 1;
                    MonetInCell.Add(Monetaclone);
                    FirstVisit = false;
                }
                MovePlayerHere();
                WitchPiratMove.GetComponent<ClickPirat>().CheckCellMonet();
                break;
            //монеты 2
            case "78":case "79": case "80":case "81":case "82":case "83":
                if (FirstVisit)
                {
                    vec = transform.GetChild(3).gameObject.transform.position;
                    vec.y = 0.08f;
                    GameObject Monetaclone = Instantiate(Moneta, vec, Quaternion.identity);
                    //Monet += 1;
                    MonetInCell.Add(Monetaclone);
                    vec.y = 0.19f;
                    vec.z += 0.1f;
                    GameObject Monetaclone2 = Instantiate(Moneta, vec, Quaternion.identity);
                    //Monet += 1;
                    MonetInCell.Add(Monetaclone2);
                    FirstVisit = false;
                }
                MovePlayerHere();
                WitchPiratMove.GetComponent<ClickPirat>().CheckCellMonet();
                break;
            //монеты 3
            case "84":
            case "85":
            case "86":
                if (FirstVisit)
                {
                    vec = transform.GetChild(3).gameObject.transform.position;
                    vec.y = 0.08f;
                    GameObject Monetaclone = Instantiate(Moneta, vec, Quaternion.identity);
                    //Monet += 1;
                    MonetInCell.Add(Monetaclone);
                    vec.y = 0.19f;
                    vec.z += 0.1f;
                    GameObject Monetaclone2 = Instantiate(Moneta, vec, Quaternion.identity);
                    //Monet += 1;
                    MonetInCell.Add(Monetaclone2);
                    vec.y = 0.3f;
                    vec.z -= 0.1f;
                    GameObject Monetaclone3 = Instantiate(Moneta, vec, Quaternion.identity);
                    //Monet += 1;
                    MonetInCell.Add(Monetaclone3);
                    FirstVisit = false;
                }
                MovePlayerHere();
                WitchPiratMove.GetComponent<ClickPirat>().CheckCellMonet();
                break;
            //монеты 4
            case "87":
            case "88":
                if (FirstVisit)
                {
                    vec = transform.GetChild(3).gameObject.transform.position;
                    vec.y = 0.08f;
                    GameObject Monetaclone = Instantiate(Moneta, vec, Quaternion.identity);
                    //Monet += 1;
                    MonetInCell.Add(Monetaclone);
                    vec.y = 0.19f;
                    vec.z += 0.1f;
                    GameObject Monetaclone2 = Instantiate(Moneta, vec, Quaternion.identity);
                    //Monet += 1;
                    MonetInCell.Add(Monetaclone2);
                    vec.y = 0.3f;
                    vec.z -= 0.1f;
                    GameObject Monetaclone3 = Instantiate(Moneta, vec, Quaternion.identity);
                    //Monet += 1;
                    MonetInCell.Add(Monetaclone3);
                    vec.y = 0.41f;
                    vec.z += 0.1f;
                    GameObject Monetaclone4 = Instantiate(Moneta, vec, Quaternion.identity);
                    //Monet += 1;
                    MonetInCell.Add(Monetaclone4);
                    FirstVisit = false;
                }
                MovePlayerHere();
                WitchPiratMove.GetComponent<ClickPirat>().CheckCellMonet();
                break;
            //монеты 5
            case "89":
                if (FirstVisit) {
                    vec = transform.GetChild(3).gameObject.transform.position;
                    vec.y = 0.08f;
                    GameObject Monetaclone = Instantiate(Moneta, vec, Quaternion.identity);
                    //Monet += 1;
                    MonetInCell.Add(Monetaclone);
                    vec.y = 0.19f;
                    vec.z += 0.1f;
                    GameObject Monetaclone2 = Instantiate(Moneta, vec, Quaternion.identity);
                    //Monet += 1;
                    MonetInCell.Add(Monetaclone2);
                    vec.y = 0.3f;
                    vec.z -= 0.1f;
                    GameObject Monetaclone3 = Instantiate(Moneta, vec, Quaternion.identity);
                    //Monet += 1;
                    MonetInCell.Add(Monetaclone3);
                    vec.y = 0.41f;
                    vec.z += 0.1f;
                    GameObject Monetaclone4 = Instantiate(Moneta, vec, Quaternion.identity);
                    //Monet += 1;
                    MonetInCell.Add(Monetaclone4);
                    vec.y = 0.52f;
                    vec.z -= 0.1f;
                    GameObject Monetaclone5 = Instantiate(Moneta, vec, Quaternion.identity);
                    //Monet += 1;
                    MonetInCell.Add(Monetaclone5);
                    FirstVisit = false;
                }                
                MovePlayerHere();
                WitchPiratMove.GetComponent<ClickPirat>().CheckCellMonet();

                break;
            //конь
            case "90":
            case "91":
                StrelkaNaPirata();
                Strelka(x - 1, z + 2);
                Strelka(x - 2, z + 1);
                Strelka(x - 2, z - 1);
                Strelka(x - 1, z - 2);
                Strelka(x + 1, z + 2);
                Strelka(x + 2, z + 1);
                Strelka(x + 2, z - 1);
                Strelka(x + 1, z - 2);
                break;                         
            //форт
            case "92":
            case "93":
                MovePlayerHere();
                break;
            //форт с воскрешением
            case "94":               
                MovePlayerHere();
                if (WitchPiratMove.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().alldied.Count > 0)
                {
                    int i = WitchPiratMove.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().alldied.Count - 1;
                    if (WitchPiratMove.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().alldied[i] != null)
                    {
                        if (Beforex == x)
                        {
                            if (Beforez == z)
                            {
                                WitchPiratMove = WitchPiratMove.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().alldied[i];
                                WitchPiratMove.GetComponent<ClickPirat>().x = x;
                                WitchPiratMove.GetComponent<ClickPirat>().z = z;
                                WitchPiratMove.SetActive(true);
                                WitchPiratMove.transform.position = transform.position;
                                WitchPiratMove.GetComponent<ClickPirat>().ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().Nopick();
                                MoveInsideCell();
                                WitchPiratMove.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().alldied.Remove(WitchPiratMove.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().alldied[i]);
                            }
                        }
                    }
                }
                break;
            //вертолет
            case "95":
                if (VertoletIsUsed == false)
                {
                    WitchPiratMove.GetComponent<ClickPirat>().InVertolet = true;
                    VertoletIsUsed = true;
                }
                    MovePlayerHere();
                break;
            //людоед
            case "96":                            
                WitchPiratMove.GetComponent<ClickPirat>().EndTurn();
                WitchPiratMove.GetComponent<ClickPirat>().IsDead = true;
                WitchPiratMove.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().alldied.Add(WitchPiratMove);
                WitchPiratMove.GetComponent<ClickPirat>().CheckCellMonet();
                WitchPiratMove.SetActive(false);
                break;
            //пустые
            case "1":case "2":case "3":case "4":case "5":case "6":case "7":case "8":case "9":case "10":case "11":case "12":case "13":case "14":case "15":case "16":case "17":case "18":
            case "97":case "98":case "99":case "100":case "101": case "102":case "103":case "104":case "105":case "106":case "107":case "108":case "109":case "110":case "111":case "112":
            case "113":case "114":case "115":case "116":case "117":
                MovePlayerHere();
                break;
        }
    }
    public void StrelkaNaPirata()
    {
        WitchPiratMove.GetComponent<ClickPirat>().CellToMove = gameObject.transform.position;
        WitchPiratMove.GetComponent<ClickPirat>().x = x;
        WitchPiratMove.GetComponent<ClickPirat>().z = z;
        WitchPiratMove.GetComponent<ClickPirat>().NowCell = gameObject;
        WitchPiratMove.GetComponent<ClickPirat>().DeselectCells();
        foreach (GameObject ChildPirat in AllChildPirat)
        {
            if (ChildPirat.GetComponentInParent<ClickPirat>() != null)
            {
                ChildPirat.GetComponentInParent<ClickPirat>().CSAP = false;
            }           
        }
    }
    public void Strelka(int x, int z)
    {
        foreach (GameObject Cell in WaterCells)
        {
            int px = Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().x;
            int pz = Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().z;
            if (px == x && pz == z)
            {               
                    Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                    Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().CanClickOnFace = true;
                    Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().WitchPiratMove = WitchPiratMove;               
            }
        }
        foreach (GameObject Cell in Cells)
        {
            int px = Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().x;
            int pz = Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().z;
            string spname = Cell.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;           
           
            if (WitchPiratMove.GetComponent<ClickPirat>().WithGold == true)
            {
                //форты
                if (spname != "92" && spname != "93" && spname != "94")
                {
                    if (px == x && pz == z)
                    {
                        Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                        Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().CanClickOnFace = true;                        
                        Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().WitchPiratMove = WitchPiratMove;
                    }
                }
            }
            else
            {
                if (px == x && pz == z)
                {
                    if (SpName == "25" || SpName == "26" || SpName == "27" || SpName == "28" || SpName == "29" || SpName == "30")
                    {
                        if (spname == "65" || spname == "66" || spname == "67" || spname == "68")
                        {                           
                            WitchPiratMove.GetComponent<ClickPirat>().EndTurn();
                            WitchPiratMove.GetComponent<ClickPirat>().IsDead = true;
                            WitchPiratMove.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().alldied.Add(WitchPiratMove);
                            WitchPiratMove.GetComponent<ClickPirat>().CheckCellMonet();
                            WitchPiratMove.SetActive(false);
                        }
                        else
                        {
                            Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                            Cell.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
                            Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().CanClickOnFace = true;
                            Cell.transform.GetChild(1).GetComponent<ClickOnBackOfCell>().CanClickOnBack = true;
                            Cell.transform.GetChild(1).GetComponent<ClickOnBackOfCell>().WitchPiratMove = WitchPiratMove;
                            Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().WitchPiratMove = WitchPiratMove;
                        }
                    }
                    else
                    {

                        Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                        Cell.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
                        Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().CanClickOnFace = true;
                        Cell.transform.GetChild(1).GetComponent<ClickOnBackOfCell>().CanClickOnBack = true;
                        Cell.transform.GetChild(1).GetComponent<ClickOnBackOfCell>().WitchPiratMove = WitchPiratMove;
                        Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().WitchPiratMove = WitchPiratMove;
                    }
                }
            }            
        }
        foreach (GameObject Ship in ShipCells)
        {
            int px = Ship.GetComponent<Ships>().x;
            int pz = Ship.GetComponent<Ships>().z;
            if (px == x && pz == z)
            {              
                    Ship.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                    Ship.GetComponent<Ships>().CanClickOnFace = true;
                    Ship.GetComponent<Ships>().WitchPiratMove = WitchPiratMove;
            }
        }
    }
    public void MovePlayerHere()
    {
        WitchPiratMove.GetComponent<ClickPirat>().CellToMove = transform.position;
        WitchPiratMoveInMove = true;
        WitchPiratMove.GetComponent<ClickPirat>().x = x;
        WitchPiratMove.GetComponent<ClickPirat>().z = z;       
        WitchPiratMove.GetComponent<ClickPirat>().DeselectCells();
        WitchPiratMove.GetComponent<ClickPirat>().EndTurn();
    }
    //для озера
    public void MoveToFaceCell(int xx, int zz)
    {
        foreach (GameObject Cell in Cells)
        {
            if (Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().x == xx)
            {
                if (Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().z == zz)
                {                   
                    if (Cell.transform.eulerAngles.x != 0)
                    {                                 
                        WitchPiratMove.GetComponent<ClickPirat>().CellToMove = gameObject.transform.position;
                        WitchPiratMove.GetComponent<ClickPirat>().x = x;
                        WitchPiratMove.GetComponent<ClickPirat>().z = z;
                        WitchPiratMove.GetComponent<ClickPirat>().NowCell = gameObject;
                        Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().WitchPiratMove = WitchPiratMove;
                        Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().CanClickOnFace = true;
                        Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().OnMouseUp();                                            
                    }
                    else
                    {
                        WitchPiratMove.GetComponent<ClickPirat>().CellToMove = gameObject.transform.position;
                        WitchPiratMove.GetComponent<ClickPirat>().x = x;
                        WitchPiratMove.GetComponent<ClickPirat>().z = z;
                        Cell.transform.GetChild(1).GetComponent<ClickOnBackOfCell>().WitchPiratMove = WitchPiratMove;
                        Cell.transform.GetChild(1).GetComponent<ClickOnBackOfCell>().CanClickOnBack = true;
                        Cell.transform.GetChild(1).GetComponent<ClickOnBackOfCell>().OnMouseUp();
                    }                   
                }
            }
            
        }
        foreach (GameObject Cell in WaterCells)
        {
            if (Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().x == xx)
            {
                if (Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().z == zz)
                {
                    WitchPiratMove.GetComponent<ClickPirat>().x = x;
                    WitchPiratMove.GetComponent<ClickPirat>().z = z;
                    Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().WitchPiratMove = WitchPiratMove;
                    Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().CanClickOnFace = true;
                    Cell.transform.GetChild(0).GetComponent<ClickOnWaterFace>().OnMouseUp();
                }
            }
        }
        foreach (GameObject Cell in ShipCells)
        {
            if (Cell.GetComponent<Ships>().x == xx)
            {
                if (Cell.GetComponent<Ships>().z == zz)
                {
                    WitchPiratMove.GetComponent<ClickPirat>().x = x;
                    WitchPiratMove.GetComponent<ClickPirat>().z = z;
                    Cell.GetComponent<Ships>().WitchPiratMove = WitchPiratMove;
                    Cell.GetComponent<Ships>().CanClickOnFace = true;
                    Cell.GetComponent<Ships>().OnMouseUp();
                }
            }
        }
    }
    public void KillOrNot()
    {
        if (Pirat1 != null)
        {
            if (Pirat1.GetComponent<ClickPirat>().InTrap == true)
            {
                if (Pirat1.GetComponent<ClickPirat>().StageInTrap == WitchPiratMove.GetComponent<ClickPirat>().StageInTrap)
                {
                    if (Pirat1.tag != WitchPiratMove.tag)
                    {
                        if (Pirat1.GetComponent<ClickPirat>().WithGold == true)
                        {
                            Pirat1.GetComponent<ClickPirat>().NowCell.GetComponent<ClickOnFaceOfCell>().MonetHerePlus(Pirat1.GetComponent<ClickPirat>().Monet);
                        }
                        Pirat1.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().WitchPiratMove = Pirat1;
                        Pirat1.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().OnMouseUpForKill();
                        NegativCountOfPlayer(x, z, Pirat1);
                    }
                }
            }
            else 
            {
                if (Pirat1.tag != WitchPiratMove.tag)
                {
                    if (Pirat1.GetComponent<ClickPirat>().WithGold == true)
                    {
                        Pirat1.GetComponent<ClickPirat>().NowCell.GetComponent<ClickOnFaceOfCell>().MonetHerePlus(Pirat1.GetComponent<ClickPirat>().Monet);
                    }
                    Pirat1.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().WitchPiratMove = Pirat1;
                    Pirat1.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().OnMouseUpForKill();
                    NegativCountOfPlayer(x, z, Pirat1);
                }
            }
        }
        if (Pirat2 != null)
        {
            if (Pirat2.GetComponent<ClickPirat>().InTrap == true)
            {
                if (Pirat2.GetComponent<ClickPirat>().StageInTrap == WitchPiratMove.GetComponent<ClickPirat>().StageInTrap)
                {
                    if (Pirat2.tag != WitchPiratMove.tag)
                    {
                        if (Pirat2.GetComponent<ClickPirat>().WithGold == true)
                        {
                            Pirat2.GetComponent<ClickPirat>().NowCell.GetComponent<ClickOnFaceOfCell>().MonetHerePlus(Pirat2.GetComponent<ClickPirat>().Monet);
                        }
                        Pirat2.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().WitchPiratMove = Pirat2;
                        Pirat2.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().OnMouseUpForKill();
                        NegativCountOfPlayer(x, z, Pirat2);
                    }
                }
            }
            else
            {
                if (Pirat2.tag != WitchPiratMove.tag)
                {
                    if (Pirat2.GetComponent<ClickPirat>().WithGold == true)
                    {
                        Pirat2.GetComponent<ClickPirat>().NowCell.GetComponent<ClickOnFaceOfCell>().MonetHerePlus(Pirat2.GetComponent<ClickPirat>().Monet);
                    }
                    Pirat2.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().WitchPiratMove = Pirat2;
                    Pirat2.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().OnMouseUpForKill();
                    NegativCountOfPlayer(x, z, Pirat2);
                }
            }
        }
        if (Pirat3 != null)
        {
            if (Pirat3.GetComponent<ClickPirat>().InTrap == true)
            {
                if (Pirat3.GetComponent<ClickPirat>().StageInTrap == WitchPiratMove.GetComponent<ClickPirat>().StageInTrap)
                {
                    if (Pirat3.tag != WitchPiratMove.tag)
                    {
                        if (Pirat3.GetComponent<ClickPirat>().WithGold == true)
                        {
                            Pirat3.GetComponent<ClickPirat>().NowCell.GetComponent<ClickOnFaceOfCell>().MonetHerePlus(Pirat3.GetComponent<ClickPirat>().Monet);
                        }
                        Pirat3.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().WitchPiratMove = Pirat3;
                        Pirat3.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().OnMouseUpForKill();
                        NegativCountOfPlayer(x, z, Pirat3);
                    }
                }
            }
            else 
            {
                if (Pirat3.tag != WitchPiratMove.tag)
                {
                    if (Pirat3.GetComponent<ClickPirat>().WithGold == true)
                    {
                        Pirat3.GetComponent<ClickPirat>().NowCell.GetComponent<ClickOnFaceOfCell>().MonetHerePlus(Pirat3.GetComponent<ClickPirat>().Monet);
                    }
                    Pirat3.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().WitchPiratMove = Pirat3;
                    Pirat3.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().OnMouseUpForKill();
                    NegativCountOfPlayer(x, z, Pirat3);
                }
            }
        }
        if (Pirat4 != null)
        {
            if (Pirat4.GetComponent<ClickPirat>().InTrap == true)
            {
                if (Pirat4.GetComponent<ClickPirat>().StageInTrap == WitchPiratMove.GetComponent<ClickPirat>().StageInTrap)
                {
                    if (Pirat4.tag != WitchPiratMove.tag)
                    {
                        if (Pirat4.GetComponent<ClickPirat>().WithGold == true)
                        {
                            Pirat4.GetComponent<ClickPirat>().NowCell.GetComponent<ClickOnFaceOfCell>().MonetHerePlus(Pirat4.GetComponent<ClickPirat>().Monet);
                        }
                        Pirat4.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().WitchPiratMove = Pirat4;
                        Pirat4.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().OnMouseUpForKill();
                        NegativCountOfPlayer(x, z, Pirat4);
                    }
                }
            }
            else
            {
                if (Pirat4.tag != WitchPiratMove.tag)
                {
                    if (Pirat4.GetComponent<ClickPirat>().WithGold == true)
                    {
                        Pirat4.GetComponent<ClickPirat>().NowCell.GetComponent<ClickOnFaceOfCell>().MonetHerePlus(Pirat4.GetComponent<ClickPirat>().Monet);
                    }
                    Pirat4.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().WitchPiratMove = Pirat4;
                    Pirat4.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().OnMouseUpForKill();
                    NegativCountOfPlayer(x, z, Pirat4);
                }
            }
        }
        if (Pirat5 != null)
        {
            if (Pirat5.GetComponent<ClickPirat>().InTrap == true)
            {
                if (Pirat5.GetComponent<ClickPirat>().StageInTrap == WitchPiratMove.GetComponent<ClickPirat>().StageInTrap)
                {
                    if (Pirat5.tag != WitchPiratMove.tag)
                    {
                        if (Pirat5.GetComponent<ClickPirat>().WithGold == true)
                        {
                            Pirat5.GetComponent<ClickPirat>().NowCell.GetComponent<ClickOnFaceOfCell>().MonetHerePlus(Pirat5.GetComponent<ClickPirat>().Monet);
                        }
                        Pirat5.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().WitchPiratMove = Pirat5;
                        Pirat5.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().OnMouseUpForKill();
                        NegativCountOfPlayer(x, z, Pirat5);
                    }
                }
            }
            else
            {
                if (Pirat5.tag != WitchPiratMove.tag)
                {
                    if (Pirat5.GetComponent<ClickPirat>().WithGold == true)
                    {
                        Pirat5.GetComponent<ClickPirat>().NowCell.GetComponent<ClickOnFaceOfCell>().MonetHerePlus(Pirat5.GetComponent<ClickPirat>().Monet);
                    }
                    Pirat5.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().WitchPiratMove = Pirat5;
                    Pirat5.GetComponent<ClickPirat>().MyShip.GetComponent<Ships>().OnMouseUpForKill();
                    NegativCountOfPlayer(x, z, Pirat5);
                }
            }
        }
    }
    public void MoveInsideCell() 
    {
        SpName = GetComponent<SpriteRenderer>().sprite.name;
        if (SpName != "19" && SpName != "20" && SpName != "21" && SpName != "22" && SpName != "23" && SpName != "24")
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
                    else
                    {
                        if (Pirat4 == null)
                        {
                            Pirat4 = WitchPiratMove;
                            Pirat4.GetComponent<ClickPirat>().CellToMove = new Vector3(Pirat4.GetComponent<ClickPirat>().CellToMove.x + 0.8f, 0.03f, Pirat4.GetComponent<ClickPirat>().CellToMove.z);
                        }
                        else
                        {
                            if (Pirat5 == null)
                            {
                                Pirat5 = WitchPiratMove;
                                Pirat5.GetComponent<ClickPirat>().CellToMove = new Vector3(Pirat5.GetComponent<ClickPirat>().CellToMove.x - 0.8f, 0.03f, Pirat5.GetComponent<ClickPirat>().CellToMove.z);
                            }
                        }
                    }
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
            if (BeforeCell.GetComponent<ClickOnFaceOfCell>().Pirat1 == Pirat)
            {
                BeforeCell.GetComponent<ClickOnFaceOfCell>().Pirat1 = null;
            }
            if (BeforeCell.GetComponent<ClickOnFaceOfCell>().Pirat2 == Pirat)
            {
                BeforeCell.GetComponent<ClickOnFaceOfCell>().Pirat2 = null;
            }
            if (BeforeCell.GetComponent<ClickOnFaceOfCell>().Pirat3 == Pirat)
            {
                BeforeCell.GetComponent<ClickOnFaceOfCell>().Pirat3 = null;
            }
            if (BeforeCell.GetComponent<ClickOnFaceOfCell>().Pirat4 == Pirat)
            {
                BeforeCell.GetComponent<ClickOnFaceOfCell>().Pirat4 = null;
            }
            if (BeforeCell.GetComponent<ClickOnFaceOfCell>().Pirat5 == Pirat)
            {
                BeforeCell.GetComponent<ClickOnFaceOfCell>().Pirat5 = null;
            }
        }        
        foreach (GameObject Ship in ShipCells)
        {
            if (Ship.GetComponent<Ships>().x == xx)
            {
                if (Ship.GetComponent<Ships>().z == zz)
                {
                    if (PlayerHere > 0)
                    {
                        PlayerHere -= 1;
                    }
                    if (Ship.GetComponent<Ships>().Pirat1 == Pirat)
                    {
                        Ship.GetComponent<Ships>().Pirat1 = null;
                    }
                    if (Ship.GetComponent<Ships>().Pirat2 == Pirat)
                    {
                        Ship.GetComponent<Ships>().Pirat2 = null;
                    }
                    if (Ship.GetComponent<Ships>().Pirat3 == Pirat)
                    {
                        Ship.GetComponent<Ships>().Pirat3 = null;
                    }
                }
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
    public void Vertolet() 
    {
        foreach (GameObject Cell in Cells)
        {
            Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().WitchPiratMove = WitchPiratMove;
            if (Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat1 == null || Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat1.tag == WitchPiratMove.tag)
            {
                if (Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat2 == null || Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat2.tag == WitchPiratMove.tag)
                {
                    if (Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat3 == null || Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().Pirat3.tag == WitchPiratMove.tag)
                    {
                        Cell.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);                       
                        Cell.transform.GetChild(0).GetComponent<ClickOnFaceOfCell>().CanClickOnFace = true;
                        Debug.Log("Vertolet");
                    }
                }               
            }                                               
        }
    }
    public void MonetHerePlus(GameObject Monet)
    {       
        Monet.transform.SetParent(Monet.transform.parent.gameObject.transform.parent);
        MonetInCell.Add(Monet);
        vec = transform.GetChild(3).gameObject.transform.position;       
        if (MonetInCell.Count >= 1)
        {
            vec.y = 0.08f;
            MonetInCell[0].transform.position = new Vector3(vec.x, vec.y, vec.z);
        }
        if (MonetInCell.Count >= 2)
        {
            vec.y = 0.19f;
            vec.z += 0.1f;
            MonetInCell[1].transform.position = new Vector3(vec.x, vec.y, vec.z);
        }
        if (MonetInCell.Count >= 3)
        {
            vec.y = 0.3f;
            vec.z -= 0.1f;
            MonetInCell[2].transform.position = new Vector3(vec.x, vec.y, vec.z);
        }
        if (MonetInCell.Count >= 4)
        {
            vec.y = 0.41f;
            vec.z += 0.1f;
            MonetInCell[3].transform.position = new Vector3(vec.x, vec.y, vec.z);
        }
        if (MonetInCell.Count >= 5)
        {
            vec.y = 0.52f;
            vec.z -= 0.1f;
            MonetInCell[4].transform.position = new Vector3(vec.x, vec.y, vec.z);
        }
        if (MonetInCell.Count >= 6)
        {
            vec.y = 0.63f;
            vec.z += 0.1f;
            MonetInCell[5].transform.position = new Vector3(vec.x, vec.y, vec.z);
        }
        if (MonetInCell.Count >= 7)
        {
            vec.y = 0.74f;
            vec.z -= 0.1f;
            MonetInCell[6].transform.position = new Vector3(vec.x, vec.y, vec.z);
        }
        if (MonetInCell.Count >= 8)
        {
            vec.y = 0.75f;
            vec.z += 0.1f;
            MonetInCell[7].transform.position = new Vector3(vec.x, vec.y, vec.z);
        }
        foreach (GameObject child in AllChildPirat)
        {
            if (child.GetComponentInParent<ClickPirat>() != null)
            {
                child.GetComponentInParent<ClickPirat>().CheckCellMonet();
            }
        }
    }
    public void MonetHereMinus(GameObject witchMonetButton)
    {
        witchMonetButton.GetComponent<buttonForMonet>().MainPirat.GetComponent<ClickPirat>().Monet = MonetInCell[MonetInCell.Count - 1];       
        Vector3 newpos =  new Vector3 (witchMonetButton.GetComponent<buttonForMonet>().MainPirat.transform.position.x, witchMonetButton.GetComponent<buttonForMonet>().MainPirat.transform.position.y, witchMonetButton.GetComponent<buttonForMonet>().MainPirat.transform.position.z);
        witchMonetButton.GetComponent<buttonForMonet>().MainPirat.GetComponent<ClickPirat>().Monet.transform.SetParent(witchMonetButton.GetComponent<buttonForMonet>().MainPirat.gameObject.transform);
        witchMonetButton.GetComponent<buttonForMonet>().MainPirat.GetComponent<ClickPirat>().Monet.transform.position = newpos;
        
        MonetInCell.RemoveAt(MonetInCell.Count - 1);
        foreach (GameObject child in AllChildPirat)
        {
            if (child.GetComponentInParent<ClickPirat>() != null)
            {
                child.GetComponentInParent<ClickPirat>().CheckCellMonet();
            }
        }
    }
}
