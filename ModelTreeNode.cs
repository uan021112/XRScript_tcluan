using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelTreeNode : MonoBehaviour
{
    #region SerializeVar
    // 标记该节点是否为叶子结点
    public bool _isLeafNode;
    // 标记该节点是否为模型中的根节点
    public bool _isRoot;
    // frame表示在层级爆炸中不发生变化的组件
    public bool _isFrame;
    public List<GameObject> _children;
    #endregion

    #region PrivateVar
    private Vector3 _center;    
    #endregion

    #region Interface
    public Vector3 center 
    { 
        get { return _center; } 
    }
    
    /// <summary>
    /// 计算当前节点的中心位置，并保存到变量_center中
    /// </summary>
    public void CalculateCenter()
    {
        // 如果当前节点是叶子节点，则模型的中心即为当前节点的中心
        if (_isLeafNode == true)
        {
            _center = _children[0].GetComponent<ModelComponent>().CalculateCenter();
        }
        // 否则，若当前节点为内部节点，则模型的中心为其各个子节点的中心
        else
        {
            _center = Vector3.zero;
            foreach (GameObject child in _children)
            {
                ModelTreeNode childNode = child.GetComponent<ModelTreeNode>();
                childNode.CalculateCenter();
                _center += childNode.center;
            }
            _center /= _children.Count;
        }
        //Debug.Log("[tcluan Debug] " + name + " center: " + _center);
    }

    /// <summary>
    /// 对parent下一层级的部件进行爆炸
    /// </summary>
    /// <param name="parent">爆炸层级的父节点</param>
    /// <param name="intensity">爆炸强度（对应爆炸距离）</param>
    public static void Explode(GameObject parent, float intensity)
    {
        //Debug.Log("[tcluan Debug] Explode is trigged.\n");
        ModelTreeNode parentNode = parent.GetComponent<ModelTreeNode>();
        // 找到frame节点
        ModelTreeNode frameNode = null;
        foreach (GameObject child in parentNode._children)
        {
            ModelTreeNode childNode = child.GetComponent<ModelTreeNode>();
            if (childNode._isFrame == true)
            {
                frameNode = childNode;
                break;
            }
        } 
        if (frameNode == null)
        {
            Debug.Log("[tcluan Debug] frame node not found.\n");
        }
        // 以frame为中心其他子节点
        foreach (GameObject child in parentNode._children)
        {
            ModelTreeNode childNode = child.GetComponent<ModelTreeNode>();
            // frame节点在爆炸时不移动
            if (childNode._isFrame == true)
            {
                continue;
            }
            childNode.Movement(frameNode, intensity);
        }
    }

    /// <summary>
    /// 将parent的子节点恢复为初始位置
    /// </summary>
    /// <param name="parent">待恢复层级的父节点</param>
    public static void Recover(GameObject parent)
    {
        ModelTreeNode parentNode = parent.GetComponent<ModelTreeNode>();
        if (parentNode._isLeafNode == true)
        {
            parentNode.transform.position = Vector3.zero;
            return;
        }
        foreach(GameObject child in parentNode._children)
        {
            ModelTreeNode.Recover(child);
        }
    }
    
    /// <summary>
    /// 当前模型以frameNode为中心爆炸
    /// </summary>
    /// <param name="frameNode">爆炸中心</param>
    /// <param name="intensity">爆炸强度（即爆炸距离）</param>
    public void Movement(ModelTreeNode frameNode, float intensity)
    {
        Vector3 direction = (_center - frameNode.center).normalized;
        transform.position += direction * intensity;
    }
    #endregion

    #region PrivateFunction
    private void Start()
    {
        if (_isRoot)
        {
            CalculateCenter();
            Debug.Log("[tcluan Debug] Plane center: " + _center);
        }
    }
    #endregion
}
