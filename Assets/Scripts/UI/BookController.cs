using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BookController : MonoBehaviour
{
    private int bookPage;
    private int currentPage;
    private int repeatCount = 0;
    private int currentRepeatCount = 0;

    public bool isTurnThePage = false;

    private Animator animator;

    private Transform closeBtn;
    private Transform previousBtn;
    private Transform nextBtn;

    //���� = page 0, �κ��丮 = page 1, �������ͽ� = page 2, ��ų = page 3, ����â = page 4(last)
    private Transform[] pages;
    private Transform iventoryPage;

    void Awake()
    {
        pages = new Transform[5];

        bookPage = 0;

        animator = GetComponent<Animator>();

        closeBtn = transform.Find("CloseBtn");
        previousBtn = transform.Find("PreviousBtn");
        nextBtn = transform.Find("NextBtn");

        pages[0] = transform.Find("ContentsPage");
        pages[1] = transform.Find("InventoryPage");
        pages[2] = transform.Find("StatusPage");
        pages[3] = transform.Find("SkillPage");
        pages[4] = transform.Find("OptionPage");

        closeBtn.gameObject.SetActive(false);
        previousBtn.gameObject.SetActive(false);
        nextBtn.gameObject.SetActive(false);

        for(int i = 0; i < 5; i++)
        {
            pages[i].gameObject.SetActive(false);
        }
        
    }

    private void OnEnable()
    {
        currentRepeatCount = 0;
        bookPage = 0;
        currentPage = 0;

        Invoke("BookOpen", 1f);
        Invoke("ActivateCloseBtn", 2.3f);
        Invoke("ActivateNextBtn", 2.3f);
    }

    private void Update()
    {
        if (bookPage != currentPage)
        {
            if (bookPage > 0)
            {
                ActivatePreviousBtn();
            }
            else
            {
                previousBtn.gameObject.SetActive(false);
            }

            if (bookPage >= 4)
            {
                nextBtn.gameObject.SetActive(false);
            }
            else
            {
                ActivateNextBtn();
            }
        }

        currentPage = bookPage;
    }

    private void FixedUpdate()
    {
        //if (isTurnThePage)
        //{
        //    animator.SetBool("isTurnThePage", true);
        //    isTurnThePage = false;
        //    currentRepeatCount = 0;
        //}

        if (animator.GetBool("isTurnThePage"))
        {
            if (currentRepeatCount >= repeatCount)
            {
                animator.SetBool("isTurnThePage", false);
            }
        }
    }

    private void OnDisable()
    {

    }

    private void BookOpen()
    {
        animator.SetBool("isOpen",true);
    }

    public void BookClose()
    {
        animator.SetBool("isOpen", false);
        closeBtn.gameObject.SetActive(false);
        previousBtn.gameObject.SetActive(false);
        nextBtn.gameObject.SetActive(false);

        for (int i = 0; i < 5; i++)
        {
            pages[i].gameObject.SetActive(false);
        }
    }

    public void PreviousPage()
    {
        PageChanger(--bookPage, 1, true);
    }

    public void NextPage()
    {
        PageChanger(++bookPage, 1, false);
    }

    public void ToInventoryPage()
    {
        PageChanger(1, 1, false);
    }

    public void ToStatusPage()
    {
        PageChanger(2, 2, false);
    }

    public void ToSkillPage()
    {
        PageChanger(3, 3, false);
    }

    public void ToOptionPage()
    {
        PageChanger(4, 4, false);
    }

    private void PageChanger(int page, int count, bool reverse)
    {
        bookPage = page;

        if(reverse)
            if (!animator.GetBool("isReverse")) animator.SetBool("isReverse", true);
        else
            if (animator.GetBool("isReverse")) animator.SetBool("isReverse", false);

        ControlCurrentPage();

        PageChanger();

        currentRepeatCount = 0;
        animator.SetBool("isTurnThePage", true);
        repeatCount = count;
    }

    public void ToInventoryPage()
    {
        bookPage = 1;

    public void ToInventoryPage()
    {
        PageChanger(1, 1, false);
    }

    public void ToStatusPage()
    {
        PageChanger(2, 2, false);
    }

    public void ToSkillPage()
    {
        PageChanger(3, 3, false);
    }

    public void ToOptionPage()
    {
        PageChanger(4, 4, false);
    }

    private void PageChanger(int page, int count, bool reverse)
    {
        bookPage = page;

        if(reverse)
            if (!animator.GetBool("isReverse")) animator.SetBool("isReverse", true);
        else
            if (animator.GetBool("isReverse")) animator.SetBool("isReverse", false);

        ControlCurrentPage();

        PageChanger();

        currentRepeatCount = 0;
        animator.SetBool("isTurnThePage", true);
        repeatCount = count;
    }

    public void ToStatusPage()
    {
        bookPage = 2;

        if (animator.GetBool("isReverse")) animator.SetBool("isReverse", false);

        PageChanger();

        currentRepeatCount = 0;
        animator.SetBool("isTurnThePage", true);
        repeatCount = 2;
    }

    public void ToSkillPage()
    {
        bookPage = 3;

        if (animator.GetBool("isReverse")) animator.SetBool("isReverse", false);

        PageChanger();

        currentRepeatCount = 0;
        animator.SetBool("isTurnThePage", true);
        repeatCount = 3;
    }

    public void ToOptionPage()
    {
        bookPage = 4;

        if (animator.GetBool("isReverse")) animator.SetBool("isReverse", false);

        PageChanger();

        currentRepeatCount = 0;
        animator.SetBool("isTurnThePage", true);
        repeatCount = 4;
    }


    private void ActivateCloseBtn()
    {
        closeBtn.gameObject.SetActive(true);
        pages[0].gameObject.SetActive(true);
    }

    private void ActivatePreviousBtn()
    {
        previousBtn.gameObject.SetActive(true);
    }

    private void ActivateNextBtn() 
    {  
        nextBtn.gameObject.SetActive(true);
    }

    // �ִϸ��̼� Ŭ�� �̺�Ʈ���� ���
    private void RepeatCount()
    {
        if(animator.GetBool("isReverse"))
        {
            currentRepeatCount++;
        }
        if (!animator.GetBool("isReverse"))
        {
            currentRepeatCount++;
        }
    }

    private void ControlCurrentPage()
    {
        for(int i = 0; i < 5; i++)
        {
            pages[i].gameObject.SetActive(false);
        }

        Invoke("ActivateCurrentPage", 1f);
    }

    private void ActivateCurrentPage()
    {
        pages[bookPage].gameObject.SetActive(true);
    }
}