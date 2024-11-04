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
            // 运行设备为Android时的更新逻辑
            case RuntimePlatform.Android:
                AndroidMovementAndRotation();
                break;
            // 运行设备为PC（例如Unity, Windows, Linux等)时的更新逻辑
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

        // 隐藏UI界面
        _worldCamera = GameObject.Find("World Camera");
        _startUI = GameObject.Find("Welcome UI");
        _worldCamera.gameObject.SetActive(false);
        _startUI.gameObject.SetActive(false);
        // 创建摄像机
        switch (Application.platform)
        {
            // 如果运行设备为Android，则绑定AR Camera
            case RuntimePlatform.Android:
                InitAndroidCamera();
                InitAndroidUi();
                break;
            // 如果运行设备为PC（例如Unity, Windows, Linux等)，则绑定普通Camera
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
        // 请求相机权限
        if (Permission.HasUserAuthorizedPermission(Permission.Camera) == false)
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
        // 创建本地AR Session实例
        _arSession = Instantiate<ARSession>(_prefabOfArSession);
        // 创建本地AR Session Origin实例（即相机）
        _playerCamera = Instantiate(_prefabOfArSessionOrigin, _playerGlass.transform.position, _playerGlass.transform.rotation);
        _playerHead.gameObject.SetActive(false);
    }

    private void InitPcCamera()
    {
        _playerCamera = Instantiate(_prefabOfPcCamera);
        // 对于PC端，玩家的直接控制对象对Player Prefab，因此将_playerCamera作为子对象绑定在Player Prefab上
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
        // 获取输入
        float horizontal = Input.GetAxis("Horizontal"); // A/D 或 左/右箭头
        float vertical = Input.GetAxis("Vertical"); // W/S 或 上/下箭头

        // 创建移动方向
        Vector3 direction = gameObject.transform.right * horizontal + gameObject.transform.forward * vertical;
        direction.Normalize();

        // 更新角色位置
        gameObject.transform.Translate(direction * _movementSpeed * Time.deltaTime, Space.World);
    }

    private void PcRotation()
    {
        // 获取鼠标输入
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        // 应用旋转到角色身体
        gameObject.transform.Rotate(Vector3.up * mouseX);
        _playerHead.transform.Rotate(Vector3.left * mouseY);
    }
    #endregion
}
