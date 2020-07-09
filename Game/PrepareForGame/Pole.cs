using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : MonoBehaviour
{
   
    //для растановки кораблей
    GameObject[] ShipsSprites;
    public Sprite RedShip;
    public Sprite YellowShip;
    public Sprite BlackShip;
    public Sprite WhiteShip;
    
    //ячейки и их рисунки
    GameObject[] Cells;
   
    public List<Sprite> list = new List<Sprite>();//список спрайтов ячеек земли
    
    public GameObject Cell;
    public GameObject Water;
    public GameObject Ships;

    //размер всего поля
    static int col = 13, row = 13;

    public float x = 0;
    public float z = 0;

    //для выделение первого пирата
    public GameObject FirstPirat;
    public GameObject[] AllChildPirat;

    void CreateGamePole()
    {
        float Dx = -2.6f, Dz = -2.6f;
        Vector3 MyPoze = new Vector3(15.6f, 0, 12.6f);
        for (int XX = 0; XX < row; XX++)
        {
            for (int ZZ = 0; ZZ < col; ZZ++)
            {
                if (StaticGameSettings.CountOfPlayer >= 2 ) 
                {
                    if (ZZ == 0 && XX == 6 || ZZ == 12 && XX == 6 )
                    {
                        Ships.name = "Ship" + XX.ToString() + ZZ.ToString();
                        Ships.GetComponent<Ships>().x = XX;
                        Ships.GetComponent<Ships>().z = ZZ;
                        Instantiate(Ships, MyPoze, Quaternion.identity);
                        MyPoze.x += Dx;
                    }
                }
                if (StaticGameSettings.CountOfPlayer >= 3)
                {
                    if (ZZ == 6 && XX == 0)
                    {
                        Ships.name = "Ship" + XX.ToString() + ZZ.ToString();
                        Ships.GetComponent<Ships>().x = XX;
                        Ships.GetComponent<Ships>().z = ZZ;
                        Instantiate(Ships, MyPoze, Quaternion.identity);
                        MyPoze.x += Dx;
                    }
                }
                if (StaticGameSettings.CountOfPlayer >= 4)
                {
                    if (ZZ == 6 && XX == 12)
                    {
                        Ships.name = "Ship" + XX.ToString() + ZZ.ToString();
                        Ships.GetComponent<Ships>().x = XX;
                        Ships.GetComponent<Ships>().z = ZZ;
                        Instantiate(Ships, MyPoze, Quaternion.identity);
                        MyPoze.x += Dx;
                    }
                }
                //вода на месте кораблей
                if (StaticGameSettings.CountOfPlayer == 2)
                {
                    if (ZZ == 6 && XX == 0 || ZZ == 6 && XX == 12)
                    {
                        Water.name = "Water" + XX.ToString() + ZZ.ToString();
                        Water.gameObject.transform.GetChild(0).gameObject.GetComponent<ClickOnWaterFace>().x = XX;
                        Water.gameObject.transform.GetChild(0).gameObject.GetComponent<ClickOnWaterFace>().z = ZZ;
                        Instantiate(Water, MyPoze, Quaternion.identity);
                        MyPoze.x += Dx;
                    }
                }
                //вода на месте корабля
                if (StaticGameSettings.CountOfPlayer == 3)
                {
                    if (ZZ == 6 && XX == 12)
                    {
                        Water.name = "Water" + XX.ToString() + ZZ.ToString();
                        Water.gameObject.transform.GetChild(0).gameObject.GetComponent<ClickOnWaterFace>().x = XX;
                        Water.gameObject.transform.GetChild(0).gameObject.GetComponent<ClickOnWaterFace>().z = ZZ;
                        Instantiate(Water, MyPoze, Quaternion.identity);
                        MyPoze.x += Dx;
                    }
                }
                if (ZZ == 1 && XX == 1 || ZZ == 11 && XX == 1 || ZZ == 11 && XX == 11 || ZZ == 1 && XX == 11 || ZZ == 0 && XX != 6 || ZZ != 6 && XX == 0 || ZZ == 12 && XX != 6 || ZZ != 6 && XX == 12)
                {
                    //не нужны ячейки по углам, тут исключили их
                    Water.name = "Water" + XX.ToString() + ZZ.ToString();
                    Water.gameObject.transform.GetChild(0).gameObject.GetComponent<ClickOnWaterFace>().x = XX;
                    Water.gameObject.transform.GetChild(0).gameObject.GetComponent<ClickOnWaterFace>().z = ZZ;
                    Instantiate(Water, MyPoze, Quaternion.identity);
                    MyPoze.x += Dx;
                }

                else
                {
                    if (ZZ != 0 && XX != 0 && ZZ != 12 && XX != 12)
                    {
                        Cell.name = "" + XX.ToString() + ZZ.ToString();
                        Cell.gameObject.transform.GetChild(0).gameObject.GetComponent<ClickOnFaceOfCell>().x = XX;
                        Cell.gameObject.transform.GetChild(0).gameObject.GetComponent<ClickOnFaceOfCell>().z = ZZ;                       
                        Instantiate(Cell, MyPoze, Quaternion.identity);
                        MyPoze.x += Dx;
                    }
                }
                
            }
            //обнуляем позицию X
            MyPoze.x = 15.6f;
            //задаем новые координаты для Z
            MyPoze.z += Dz;

        }
    } 
    void Awake() 
    {
        CreateGamePole();
        ShuffleList();       
    }
    void Start()
    {
        AllChildPirat = GameObject.FindGameObjectsWithTag("ChildPirat");
        Cells = GameObject.FindGameObjectsWithTag("CellImage");
        FillingPole();       
        ShuffleRotation();
        ShipsSprites = GameObject.FindGameObjectsWithTag("Ships");
        SetColor();
        Invoke("selectFirstPirat", 0.3f);
    } 
    void ShuffleRotation()//рандом поворота спрайтов ячеек земли
    {
        foreach (GameObject celli in Cells)
        {
            int stepSize = 90;
            int min = 0;
            int max = 360;
            int random = Random.Range(min, max);
            float numSteps = Mathf.Floor(random / stepSize);
            float aglle = numSteps * stepSize;
            celli.transform.Rotate(0.0f, 0.0f, aglle);
            celli.GetComponent<ClickOnFaceOfCell>().CellRotation = aglle;
        } 
    }
    void ShuffleList()//рандом спрайтов ячеек земли
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    void FillingPole() 
    {
        for (int i = 0; i < list.Count; i++)
        {
            Cells[i].GetComponent<SpriteRenderer>().sprite = list[i];
            //переворот ячеек, показывает рисунки ячеек земли, нужен для теста
            //Cells[i].gameObject.transform.parent.rotation = Quaternion.Euler(180, 0, 0);           
        }      
    }
    void SetColor()
    {
        if (ShipsSprites[1] != null)
        {
            ShipsSprites[1].GetComponentInChildren<SpriteRenderer>().sprite = WhiteShip;
            foreach (GameObject Pirat in AllChildPirat) 
            {
                Pirat.GetComponentInParent<ClickPirat>().WhiteInGame = true;
            }
        }
        if (ShipsSprites[0] != null)
        {
            ShipsSprites[0].GetComponentInChildren<SpriteRenderer>().sprite = RedShip;
            foreach (GameObject Pirat in AllChildPirat)
            {
                Pirat.GetComponentInParent<ClickPirat>().RedInGame = true;
            }
        }
        if (ShipsSprites.Length > 2)
        {           
            ShipsSprites[2].GetComponentInChildren<SpriteRenderer>().sprite = YellowShip;
            foreach (GameObject Pirat in AllChildPirat)
            {
                Pirat.GetComponentInParent<ClickPirat>().YellowInGame = true;
            }
        }
        if (ShipsSprites.Length > 3)
        {
            ShipsSprites[3].GetComponentInChildren<SpriteRenderer>().sprite = BlackShip;
            foreach (GameObject Pirat in AllChildPirat)
            {
                Pirat.GetComponentInParent<ClickPirat>().BlackInGame = true;
            }
        }
    }
    void selectFirstPirat()//выбор первого пирата в начале игры
    {
        //красный заканчивает ход и начинает белый
        StaticGameSettings.WhichTurn = 3;
        string Tag = FirstPirat.tag;
        GameObject[] FirstMoveTeam = GameObject.FindGameObjectsWithTag("" +Tag);
        foreach (GameObject PiratOfFirstMoveTeam in FirstMoveTeam) 
        {
            PiratOfFirstMoveTeam.GetComponent<ClickPirat>().CanSelected = true;
            PiratOfFirstMoveTeam.GetComponent<ClickPirat>().ButtonForSellectPirat.GetComponent<buttonForSellectPirat>().canSelect = true;
            PiratOfFirstMoveTeam.GetComponent<ClickPirat>().ButtonForMonet.GetComponent<buttonForMonet>().canclick = true;
        }       
        FirstPirat.GetComponent<ClickPirat>().OnMouseUp();
    }
}
