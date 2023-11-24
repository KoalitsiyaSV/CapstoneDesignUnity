using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float playerMaxHP { get; private set; }
    public float playerCurHP { get; private set; }
    public float playerHPRatio { get; private set; }

    //싱글톤 인스턴스
    private static GameManager _instance = null;

    //외부에서 접근 가능한 싱글톤 인스턴스 속성
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                //인스턴스가 없으면 새로 생성
                GameObject gameMgr = new GameObject("GameManager");
                _instance = gameMgr.AddComponent<GameManager>();
            }

            return _instance;
        }
    }

    private void Start()
    {
        Initialize();
    }

    private void Awake()
    {
        //이미 다른 인스턴스가 있는 경우 중복 생성 방지
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            //씬 전환 시 파괴되지 않도록 설정
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {
        
        
        
        
    }

    //초기화, 현재는 플레이어 체력 관리만 함
    private void Initialize()
    {
        playerMaxHP = PlayerData.Instance.playerHP;
        playerCurHP = playerMaxHP;
        playerHPRatio = 100f;
    }

    public void PlayerTakeDamage(int dmgAmount)
    {
        playerCurHP -= dmgAmount;
        playerHPRatio = (playerCurHP / playerMaxHP) * 100f;
    }

    
    ////UI
    //[Header("UI")]
    //public GameObject MenuUI;
    //public GameObject InGameUI;
    //public GameObject InventoryUI;
    //public RectTransform ButtonGroup;
    //private float SlideSpeed = 1.5f;
    //
    //[Header("Cursor")]
    //private bool isCursorActivated;
    //
    ////test
    //[Header("test")]
    //public float PullMinX;
    //public float PullMaxY;
    //
    //[SerializeField] Texture2D CursorImage;
    //
    //private bool isButtonGroupPulled;
    //private bool isInventoryOpen;
    //private bool isMenuOpen;
    //
    //private Vector2 targetPosition;
    //private float cameraHalfWidth;
    //
    //// Start is called before the first frame update
    //void Start()
    //{
    //    cameraHalfWidth = Screen.width / 2 - 400;
    //    targetPosition = ButtonGroup.anchoredPosition;
    //
    //    Cursor.SetCursor(CursorImage, Vector2.zero, CursorMode.ForceSoftware);
    //}
    //
    //// Update is called once per frame
    //void Update()
    //{
    //    // 마우스 제어
    //    Cursor.lockState = CursorLockMode.Confined;
    //
    //    if (Input.GetKeyDown(KeyCode.LeftAlt)) isCursorActivated = true;
    //    if (Input.GetKeyUp(KeyCode.LeftAlt)) isCursorActivated = false;
    //
    //    if (!isCursorActivated)
    //    {
    //        Cursor.visible = false;
    //        Cursor.lockState = CursorLockMode.Locked;
    //    }
    //    else
    //    {
    //        Cursor.visible = true;
    //        Cursor.lockState = CursorLockMode.None;
    //    }
    //
    //    // 버튼 슬라이드 기능
    //    ButtonGroup.anchoredPosition = Vector2.Lerp(ButtonGroup.anchoredPosition, targetPosition, SlideSpeed * Time.deltaTime);
    //    
    //    if(isMenuOpen)
    //    {
    //        ButtonGroup.anchoredPosition = Vector2.Lerp(ButtonGroup.anchoredPosition, targetPosition, SlideSpeed * Time.deltaTime);
    //    }
    //    else
    //    {
    //        ButtonGroup.anchoredPosition = Vector2.Lerp(ButtonGroup.anchoredPosition, targetPosition, SlideSpeed * Time.deltaTime);
    //    }
    //}
    //
    ////슬라이드 버튼 클릭 시 이벤트
    //public void OnClickSlideBtn()
    //{
    //    isButtonGroupPulled = !isButtonGroupPulled;
    //
    //    if (isButtonGroupPulled)
    //    {
    //        // 버튼들을 당기는 위치 계산
    //        targetPosition = new Vector2(targetPosition.x - cameraHalfWidth, targetPosition.y);
    //    }
    //    else
    //    {
    //        // 버튼들을 숨기는 위치 계산
    //        targetPosition = new Vector2(targetPosition.x + cameraHalfWidth, targetPosition.y);
    //    }
    //}
    //
    ////메뉴 버튼 클릭 시 이벤트
    //public void OnClickMenuBtn()
    //{
    //    MenuUI.SetActive(true);
    //    InGameUI.SetActive(false);
    //}
    //
    ////인벤토리 버튼 클릭 시 이벤트
    //public void OnClickInvencoryBtn()
    //{
    //    InventoryUI.SetActive(true);
    //    InGameUI.SetActive(false);
    //}
    //
    ////메뉴 닫기 버튼 클릭 시 이벤트
    //public void OnClickCloseMenuBtn()
    //{
    //    MenuUI.SetActive(false);
    //    InGameUI.SetActive(true);
    //}
    //
    ////인벤토리 닫기 버튼 클릭 시 이벤트
    //public void OnClickCloseInventoryBtn()
    //{
    //    InventoryUI.SetActive(false);
    //    InGameUI.SetActive(true);
    //}
    //
    //메뉴창->게임 종료 버튼
    public void OnClickExitBtn()
    {
        OnApplicationQuit();
    }


    //종료 대충
    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    //void OnEnable()
    //{
    //    // 델리게이트 체인 추가
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    ////scene을 로드할때마다 한번씩 동작
    //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //   if (scene.name == "Village")
    //    {
    //        DataManager.instance.SaveData();
    //        Debug.Log("saveData");
    //    }
    //}

    //void OnDisable()
    //{
    //    // 델리게이트 체인 제거
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}
}