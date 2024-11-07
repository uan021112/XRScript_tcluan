using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ver241104_Demo : MonoBehaviour
{
    public GameObject _plane;
    public GameObject _mid;
    public GameObject _body;
    public GameObject _wingLeft;
    public GameObject _wingRight;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.G))
        {
            ModelTreeNode.OneDofExplosion(_plane);
        }
        else if (Input.GetKeyUp(KeyCode.T))
        {
            ModelTreeNode.OneDofRecovery(_plane);
        }

        else if (Input.GetKeyUp(KeyCode.H))
        {
            ModelTreeNode.TwoDofExplosion(_mid);
        }
        else if (Input.GetKeyUp(KeyCode.Y))
        {
            ModelTreeNode.TwoDofRecovery(_mid);
        }

        else if (Input.GetKeyUp(KeyCode.J))
        {
            ModelTreeNode.TwoDofExplosion(_body);
        }
        else if (Input.GetKeyUp(KeyCode.U))
        {
            ModelTreeNode.TwoDofRecovery(_body);
        }

        else if (Input.GetKeyUp(KeyCode.K))
        {
            ModelTreeNode.TwoDofExplosion(_wingLeft);
        }
        else if (Input.GetKeyUp(KeyCode.I))
        {
            ModelTreeNode.TwoDofRecovery(_wingLeft);
        }

        else if (Input.GetKeyUp(KeyCode.L))
        {
            ModelTreeNode.TwoDofExplosion(_wingRight);
        }
        else if (Input.GetKeyUp(KeyCode.O))
        {
            ModelTreeNode.TwoDofRecovery(_wingRight);
        }
    }
}