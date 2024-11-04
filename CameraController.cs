using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;  // �ƶ��ٶ�
    public float rotationSpeed = 100f;  // ��ת�ٶ�

    void Update()
    {
        // ������ƶ�����
        if (Input.GetKey(KeyCode.Z))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.C))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }

        // �������ת����
        if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(Vector3.left * rotationSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
        }

        // �������ԭ
        if (Input.GetKeyUp(KeyCode.X))
        {
            transform.position = new Vector3(0, -1643, -6604);
            transform.rotation = Quaternion.identity;
        }
    }
}