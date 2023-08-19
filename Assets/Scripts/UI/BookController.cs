using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BookController : MonoBehaviour
{
    private int bookPage;
    private int currentPage;
    public int repeatCount = 0;
    public int currentRepeatCount = 0;

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
        Invoke("BookOpen", 1f);
        Invoke("ActivateCloseBtn", 2.3f);
        Invoke("ActivateNextBtn", 2.3f);
        currentRepeatCount = 0;
        bookPage = 0;
        currentPage = 0;
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

        PageChanger(bookPage);
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
    }

    public void PreviousPage()
    {
        bookPage--;

        if (!animator.GetBool("isReverse")) animator.SetBool("isReverse", true);
        
        currentRepeatCount = 0;
        animator.SetBool("isTurnThePage", true);
        repeatCount = 1;
    }

    public void NextPage()
    {
        bookPage++;

        if (animator.GetBool("isReverse")) animator.SetBool("isReverse", false);

        currentRepeatCount = 0;
        animator.SetBool("isTurnThePage", true);
        repeatCount = 1;
    }

    private void ActivateCloseBtn()
    {
        closeBtn.gameObject.SetActive(true);
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

    private void ActivateCurrentPage()
    {
        for(int i = 0; i < 5; i++)
        {
            pages[i].gameObject.SetActive(i == bookPage);
        }
    }

    private void PageChanger(int page)
    {
        ActivateCurrentPage();
    }
}