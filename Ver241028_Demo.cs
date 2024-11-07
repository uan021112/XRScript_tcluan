//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Ver241028_Demo : MonoBehaviour
//{
//    public GameObject _plane;
//    public GameObject _body;
//    public GameObject _wingLeft;
//    public GameObject _wingRight;


//    private void Update()
//    {
//        if (Input.GetKeyUp(KeyCode.H))
//        {
//            ModelTreeNode.Explode(_plane, 50.0f);
//        }
//        else if (Input.GetKeyUp(KeyCode.J))
//        {
//            ModelTreeNode.Explod(_body, 50.0f);
//        }
//        else if (Input.GetKeyUp(KeyCode.K))
//        {
//            ModelTreeNode.Explod(_wingLeft, 50.0f);
//        }
//        else if (Input.GetKeyUp(KeyCode.L))
//        {
//            ModelTreeNode.Explod(_wingRight, 50.0f);
//        }
//        else if (Input.GetKeyUp(KeyCode.Y))
//        {
//            ModelTreeNode.Explod(_plane, -50.0f);
//        }
//        else if (Input.GetKeyUp(KeyCode.U))
//        {
//            ModelTreeNode.Explod(_body, -50.0f);
//        }
//        else if (Input.GetKeyUp(KeyCode.I))
//        {
//            ModelTreeNode.Explod(_wingLeft, -50.0f);
//        }
//        else if (Input.GetKeyUp(KeyCode.O))
//        {
//            ModelTreeNode.Explod(_wingRight, -50.0f);
//        }
//        else if (Input.GetKeyUp(KeyCode.P))
//        {
//            ModelTreeNode.Recover(_plane);
//        }
//    }
//}
