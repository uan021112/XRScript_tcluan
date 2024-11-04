using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelTreeNode : MonoBehaviour
{
    #region SerializeVar
    // ��Ǹýڵ��Ƿ�ΪҶ�ӽ��
    public bool _isLeafNode;
    // ��Ǹýڵ��Ƿ�Ϊģ���еĸ��ڵ�
    public bool _isRoot;
    // frame��ʾ�ڲ㼶��ը�в������仯�����
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
    /// ���㵱ǰ�ڵ������λ�ã������浽����_center��
    /// </summary>
    public void CalculateCenter()
    {
        // �����ǰ�ڵ���Ҷ�ӽڵ㣬��ģ�͵����ļ�Ϊ��ǰ�ڵ������
        if (_isLeafNode == true)
        {
            _center = _children[0].GetComponent<ModelComponent>().CalculateCenter();
        }
        // ��������ǰ�ڵ�Ϊ�ڲ��ڵ㣬��ģ�͵�����Ϊ������ӽڵ������
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
    /// ��parent��һ�㼶�Ĳ������б�ը
    /// </summary>
    /// <param name="parent">��ը�㼶�ĸ��ڵ�</param>
    /// <param name="intensity">��ըǿ�ȣ���Ӧ��ը���룩</param>
    public static void Explode(GameObject parent, float intensity)
    {
        //Debug.Log("[tcluan Debug] Explode is trigged.\n");
        ModelTreeNode parentNode = parent.GetComponent<ModelTreeNode>();
        // �ҵ�frame�ڵ�
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
        // ��frameΪ���������ӽڵ�
        foreach (GameObject child in parentNode._children)
        {
            ModelTreeNode childNode = child.GetComponent<ModelTreeNode>();
            // frame�ڵ��ڱ�ըʱ���ƶ�
            if (childNode._isFrame == true)
            {
                continue;
            }
            childNode.Movement(frameNode, intensity);
        }
    }

    /// <summary>
    /// ��parent���ӽڵ�ָ�Ϊ��ʼλ��
    /// </summary>
    /// <param name="parent">���ָ��㼶�ĸ��ڵ�</param>
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
    /// ��ǰģ����frameNodeΪ���ı�ը
    /// </summary>
    /// <param name="frameNode">��ը����</param>
    /// <param name="intensity">��ըǿ�ȣ�����ը���룩</param>
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
