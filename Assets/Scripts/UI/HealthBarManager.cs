using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ü�¹ٸ� �����ϴ� ��ũ��Ʈ
public class HealthBarManager : MonoBehaviour
{
    private Image opacityHPBar;     //������ ü�¹�, ü�� ���� �� ��� �پ��
    private Image translucentHPBar; //������ ü�¹�, ü�� ���� �� ���ҵ� ü�°����� ���������� �پ��

    private float curHPRatio;        //���� ü�� ����
    private float nextHPRatio;       //���� ü�� ����
    private float decreaseRate = 0.05f; //�����Ӹ��� ���������� �����ϴ� ��

    private void Start()
    {
        Transform[] childObjects = GetComponentsInChildren<Transform>(true);    //�ڽ� ������Ʈ�� �޾ƿ�

        opacityHPBar = childObjects[2].GetComponent<Image>();       //2��° �ڽ� ������Ʈ �Ҵ�
        translucentHPBar = childObjects[3].GetComponent<Image>();   //3��° �ڽ� ������Ʈ �Ҵ�

        curHPRatio = 100f;
    }

    private void Update()
    {
        nextHPRatio = GameManager.Instance.playerHPRatio;   //����� ü�� ���� �޾ƿ�
        
        if(curHPRatio > nextHPRatio)        //ü�� ���� ��
            DamageTaken();
        else if(curHPRatio < decreaseRate)  //ü�� ȸ�� ��
            Healed();
    }

    //������ �޾��� �� ü�¹� ������Ʈ
    private void DamageTaken()
    {
        UpdateHPBar(opacityHPBar, nextHPRatio);
        StartCoroutine(UpdateHPBarOverTime());
        curHPRatio = nextHPRatio;
    }

    //ü�� ȸ���Ǿ��� �� ü�¹� ������Ʈ
    private void Healed()
    {
        UpdateHPBar(opacityHPBar, nextHPRatio);
        UpdateHPBar(translucentHPBar, nextHPRatio);
        curHPRatio = nextHPRatio;
    }

    //hp������ �޾ƿͼ� ü�¹� ������Ʈ
    private void UpdateHPBar(Image img, float hpRatio)
    {
        img.fillAmount = hpRatio / 100f;
    }

    //ü�¹ٸ� ���������� ���ҽ�Ű�� �ڷ�ƾ
    private IEnumerator UpdateHPBarOverTime()
    {
        float gradualHPRatio = curHPRatio;

        while (gradualHPRatio != nextHPRatio)
        {
            gradualHPRatio = Mathf.Clamp(gradualHPRatio - decreaseRate, nextHPRatio, 100f); //�ش� �Լ� ���� �� ���� gradualHPRatio�� ���� nextHPRatio���ϰ� ���� ���� ������ decreaseRate�� ����
            UpdateHPBar(translucentHPBar, gradualHPRatio);

            yield return null;
        }
    }
}