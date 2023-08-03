using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BookController : MonoBehaviour
{
    private int bookPage;
    public int repeatCount = 0;
    public int currentRepeatCount = 0;

    public bool isTurnThePage = false;

    private Animator animator;

    private Transform closeBtn;
    private Transform previousBtn;
    private Transform nextBtn;

    void Awake()
    {
        bookPage = 0;

        animator = GetComponent<Animator>();

        closeBtn = transform.Find("CloseBtn");
        previousBtn = transform.Find("PreviousBtn");
        nextBtn = transform.Find("NextBtn");

        closeBtn.gameObject.SetActive(false);
        previousBtn.gameObject.SetActive(false);
        nextBtn.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Invoke("BookOpen", 1f);
        Invoke("ActivateCloseBtn", 2.3f);
        Invoke("ActivateNextBtn", 2.3f);
        currentRepeatCount = 0;
    }

    private void Update()
    {
        if (bookPage > 0)
        {
            ActivatePreviousBtn();
        }
        else
        {
            previousBtn.gameObject.SetActive(false);
        }
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
        bookPage = 0;
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
}