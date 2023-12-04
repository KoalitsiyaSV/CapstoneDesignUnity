using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("대화 시스템 관련")]
    public DialogueManager dialogueManager;
    public GameObject targetObject;
    public GameObject dialoguePanel;
    public TypeEffect dialogueEffect;
    public bool isAction;
    public int dialogueIndex;

    //플레이어 스테이터스
    public float playerMaxHP { get; private set; }
    public float playerCurHP { get; private set; }
    public float playerHPRatio { get; private set; }
    public float playerAttackPoint { get; private set; }
    public float playerArmorPoint { get; private set; }
    public float playerMovementSpeedScale { get; private set; }
    public float playerPotionAmount { get; private set; }

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
        //layerName끼리 충돌판정이 생기지 않도록 함
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Item"), LayerMask.NameToLayer("Item"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Item"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerMovement"), LayerMask.NameToLayer("Enemy"));
        InitializePlayerStatus();
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

    //초기화, 현재는 플레이어 체력 관리만 함 + 다른 스테이터스 추가 되었음. 장비 장착 등으로 인한 스테이터스 변화도 이걸 활용하면 가능할듯
    private void InitializePlayerStatus()
    {
        playerMaxHP = PlayerData.Instance.playerHealthPoint;
        playerCurHP = playerMaxHP;
        playerHPRatio = 100f;

        playerAttackPoint = PlayerData.Instance.playerAttackPoint;
        playerArmorPoint = PlayerData.Instance.playerArmorPoint;
        playerMovementSpeedScale = PlayerData.Instance.playerMovementSpeedScale;
    }

    //플레이어가 데미지를 받는 기능
    public void PlayerTakeDamage(int dmgAmount)
    {
        playerCurHP -= dmgAmount;
        playerHPRatio = (playerCurHP / playerMaxHP) * 100f;
    }

    //대화를 시작하는 기능
    public void DialogueAction(ObjectData objectData)
    {
        Talk(objectData.id);

        dialoguePanel.SetActive(isAction);
    }
    
    //npcId에 해당하는 npc와 대화하는 기능
    private void Talk(int npcId)
    {
        string dialogueData = dialogueManager.GetDialogue(npcId, dialogueIndex);

        if(dialogueData == null)
        {
            NPCManager.instance.activeNpcFunction();

            //isAction = false;
            //dialogueIndex = 0;
            return;
        }

        dialoguePanel.SetActive(true);

        dialogueEffect.SetDialogue(dialogueData);

        isAction = true;
        dialogueIndex++;
    }

    //씬 변경
    public void SceneChange(int changeSceneIndex)
    {
        SceneManager.LoadScene(changeSceneIndex);
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