using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//체력바를 관리하는 스크립트
public class HealthBarManager : MonoBehaviour
{
    private Image opacityHPBar;     //불투명 체력바, 체력 감소 시 즉시 줄어듦
    private Image translucentHPBar; //반투명 체력바, 체력 감소 시 감소된 체력값까지 점진적으로 줄어듦

    private float curHPRatio;        //현재 체력 비율
    private float nextHPRatio;       //다음 체력 비율
    private float decreaseRate = 0.05f; //프레임마다 점진적으로 감소하는 양

    private void Start()
    {
        Transform[] childObjects = GetComponentsInChildren<Transform>(true);    //자식 오브젝트를 받아옴

        opacityHPBar = childObjects[2].GetComponent<Image>();       //2번째 자식 오브젝트 할당
        translucentHPBar = childObjects[3].GetComponent<Image>();   //3번째 자식 오브젝트 할당

        curHPRatio = 100f;
    }

    private void Update()
    {
        nextHPRatio = GameManager.Instance.playerHPRatio;   //변경된 체력 비율 받아옴
        
        if(curHPRatio > nextHPRatio)        //체력 감소 시
            DamageTaken();
        else if(curHPRatio < decreaseRate)  //체력 회복 시
            Healed();
    }

    //데미지 받았을 때 체력바 업데이트
    private void DamageTaken()
    {
        UpdateHPBar(opacityHPBar, nextHPRatio);
        StartCoroutine(UpdateHPBarOverTime());
        curHPRatio = nextHPRatio;
    }

    //체력 회복되었을 때 체력바 업데이트
    private void Healed()
    {
        UpdateHPBar(opacityHPBar, nextHPRatio);
        UpdateHPBar(translucentHPBar, nextHPRatio);
        curHPRatio = nextHPRatio;
    }

    //hp비율을 받아와서 체력바 업데이트
    private void UpdateHPBar(Image img, float hpRatio)
    {
        img.fillAmount = hpRatio / 100f;
    }

    //체력바를 점진적으로 감소시키는 코루틴
    private IEnumerator UpdateHPBarOverTime()
    {
        float gradualHPRatio = curHPRatio;

        while (gradualHPRatio != nextHPRatio)
        {
            gradualHPRatio = Mathf.Clamp(gradualHPRatio - decreaseRate, nextHPRatio, 100f); //해당 함수 수행 시 마다 gradualHPRatio의 값을 nextHPRatio이하가 되지 않을 때까지 decreaseRate씩 감소
            UpdateHPBar(translucentHPBar, gradualHPRatio);

            yield return null;
        }
    }
}