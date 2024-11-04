using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelComponent : MonoBehaviour
{
    #region Interface
    /// <summary>
    /// 根据mesh的顶点坐标，计算当前模型的中心位置
    /// </summary>
    /// <returns>当前模型的中心位置</returns>
    public Vector3 CalculateCenter()
    {
        Vector3 center = Vector3.zero;
        MeshFilter meshFilter = transform.GetChild(0).Find("default").GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            Vector3[] vertices = meshFilter.mesh.vertices;

            foreach (Vector3 vertex in vertices)
            {
                center += transform.TransformPoint(vertex); // 转换到世界坐标
            }

            center /= vertices.Length;
        }
        return center;
    }
    #endregion
}
