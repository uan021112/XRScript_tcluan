using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Android;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    //private const bool DEBUG_MODE = true;

    #region SerializeVar
    [Header("PC Settings")]
    public float _movementSpeed = 2.0f;
    public float _mouseSensitivity = 1.5f;
    [Header("Player Components")]
    public GameObject _playerHead;
    public GameObject _playerGlass;
    public GameObject _prefabOfPcCamera;
    [Header("Android Components")]
    public Canvas _prefabOfAndroidUi;
    [Header("World Components")]
    public GameObject _prefabOfArSessionOrigin;
    public ARSession _prefabOfArSession;
    public GameObject _prefabOfSjs1009;
    #endregion

    #region PrivateVar
    // World Components
    private GameObject _worldCamera;
    private GameObject _startUI;
    private ARSession _arSession;
    private GameObject _sceneOfSjs1009;
    // Player Components
    private GameObject _playerCamera;
    // Android Components
    private Canvas _androidUi;
    private Button _buttonOfPlaceScene;
    #endregion

    private void Update()
    {
        if (isLocalPlayer == false)
        {
            return;
        }
        switch (Application.platform)
        {
            // �����豸ΪAndroidʱ�ĸ����߼�
            case RuntimePlatform.Android:
                AndroidMovementAndRotation();
                break;
            // �����豸ΪPC������Unity, Windows, Linux��)ʱ�ĸ����߼�
            default:
                PcMovement();
                PcRotation();
                break;
        }
    }

    #region Client
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (isLocalPlayer == false)
        {
        }
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        // ����UI����
        _worldCamera = GameObject.Find("World Camera");
        _startUI = GameObject.Find("Welcome UI");
        _worldCamera.gameObject.SetActive(false);
        _startUI.gameObject.SetActive(false);
        // ���������
        switch (Application.platform)
        {
            // ��������豸ΪAndroid�����AR Camera
            case RuntimePlatform.Android:
                InitAndroidCamera();
                InitAndroidUi();
                break;
            // ��������豸ΪPC������Unity, Windows, Linux��)�������ͨCamera
            default:
                InitPcCamera();
                InitAndroidUi();
                break;
        }
    }

    private void InitAndroidUi()
    {
        _androidUi = Instantiate<Canvas>(_prefabOfAndroidUi);
        _buttonOfPlaceScene = _androidUi.transform.Find("Button_PlaceScene").GetComponent<Button>();
        _buttonOfPlaceScene.onClick.AddListener(() =>
        {
            Debug.Log("[tcluan Debug] Button is touched.\n");
            CommandPlaceSjs1009();
        });
    }

    [Command]
    private void CommandPlaceSjs1009()
    {
        Debug.Log("[tcluan Debug] command is trigged.\n");
        _sceneOfSjs1009 = Instantiate(_prefabOfSjs1009, new Vector3(0, 1.8f, 0), Quaternion.identity);
        if (_sceneOfSjs1009 == null) Debug.Log("[tcluan Debug] _sceneOfSjs1009 is null.\n");
        NetworkServer.Spawn(_sceneOfSjs1009);
    }

    private void InitAndroidCamera()
    {
        // �������Ȩ��
        if (Permission.HasUserAuthorizedPermission(Permission.Camera) == false)
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
        // ��������AR Sessionʵ��
        _arSession = Instantiate<ARSession>(_prefabOfArSession);
        // ��������AR Session Originʵ�����������
        _playerCamera = Instantiate(_prefabOfArSessionOrigin, _playerGlass.transform.position, _playerGlass.transform.rotation);
        _playerHead.gameObject.SetActive(false);
    }

    private void InitPcCamera()
    {
        _playerCamera = Instantiate(_prefabOfPcCamera);
        // ����PC�ˣ���ҵ�ֱ�ӿ��ƶ����Player Prefab����˽�_playerCamera��Ϊ�Ӷ������Player Prefab��
        _playerCamera.transform.SetParent(_playerGlass.transform);
        _playerCamera.transform.localPosition = Vector3.zero;
        _playerCamera.transform.localRotation = Quaternion.identity;
    }

    private void AndroidMovementAndRotation()
    {
        //gameObject.transform.rotation = _playerCamera.GetComponent<Camera>().transform.rotation;
        return;
    }

    private void PcMovement()
    {
        // ��ȡ����
        float horizontal = Input.GetAxis("Horizontal"); // A/D �� ��/�Ҽ�ͷ
        float vertical = Input.GetAxis("Vertical"); // W/S �� ��/�¼�ͷ

        // �����ƶ�����
        Vector3 direction = gameObject.transform.right * horizontal + gameObject.transform.forward * vertical;
        direction.Normalize();

        // ���½�ɫλ��
        gameObject.transform.Translate(direction * _movementSpeed * Time.deltaTime, Space.World);
    }

    private void PcRotation()
    {
        // ��ȡ�������
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        // Ӧ����ת����ɫ����
        gameObject.transform.Rotate(Vector3.up * mouseX);
        _playerHead.transform.Rotate(Vector3.left * mouseY);
    }
    #endregion
}
