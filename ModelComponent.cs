using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelComponent : MonoBehaviour
{
    #region Interface
    /// <summary>
    /// ����mesh�Ķ������꣬���㵱ǰģ�͵�����λ��
    /// </summary>
    /// <returns>��ǰģ�͵�����λ��</returns>
    public Vector3 CalculateCenter()
    {
        Vector3 center = Vector3.zero;
        MeshFilter meshFilter = transform.GetChild(0).Find("default").GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            Vector3[] vertices = meshFilter.mesh.vertices;

            foreach (Vector3 vertex in vertices)
            {
                center += transform.TransformPoint(vertex); // ת������������
            }

            center /= vertices.Length;
        }
        return center;
    }
    #endregion
}
