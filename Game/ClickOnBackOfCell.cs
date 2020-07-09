using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnBackOfCell : MonoBehaviour
{
    public bool CanClickOnBack = false;
    public GameObject FaceCell;
    public GameObject WitchPiratMove;
    GameObject[] AllChildPirat;

    void Start()
    {
        AllChildPirat = GameObject.FindGameObjectsWithTag("ChildPirat");
    }
    public void OnMouseUp()
    {       
        if (CanClickOnBack == true)
        {
            WitchPiratMove.GetComponent<ClickPirat>().InShip = false;
            FaceCell.GetComponent<ClickOnFaceOfCell>().WitchPiratMove = WitchPiratMove;
            transform.parent.gameObject.transform.Rotate(180.0f, 0.0f, 0.0f, Space.World);
            FaceCell.GetComponent<ClickOnFaceOfCell>().CanClickOnFace = true;
            FaceCell.GetComponent<ClickOnFaceOfCell>().OnMouseUp();
        }
    }
}
