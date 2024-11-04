using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ver241028_Demo : MonoBehaviour
{
    public GameObject _plane;
    public GameObject _body;
    public GameObject _wingLeft;
    public GameObject _wingRight;


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.H))
        {
            ModelTreeNode.Explode(_plane, 500.0f);
        }
        else if (Input.GetKeyUp(KeyCode.J))
        {
            ModelTreeNode.Explode(_body, 500.0f);
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            ModelTreeNode.Explode(_wingLeft, 500.0f);
        }
        else if (Input.GetKeyUp(KeyCode.L))
        {
            ModelTreeNode.Explode(_wingRight, 500.0f);
        }
        else if (Input.GetKeyUp(KeyCode.Y))
        {
            ModelTreeNode.Explode(_plane, -500.0f);
        }
        else if (Input.GetKeyUp(KeyCode.U))
        {
            ModelTreeNode.Explode(_body, -500.0f);
        }
        else if (Input.GetKeyUp(KeyCode.I))
        {
            ModelTreeNode.Explode(_wingLeft, -500.0f);
        }
        else if (Input.GetKeyUp(KeyCode.O))
        {
            ModelTreeNode.Explode(_wingRight, -500.0f);
        }
        else if (Input.GetKeyUp(KeyCode.P))
        {
            ModelTreeNode.Recover(_plane);
        }
    }
}
