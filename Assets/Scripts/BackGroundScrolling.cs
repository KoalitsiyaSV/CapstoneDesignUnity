using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScrolling : MonoBehaviour
{
    public float speed;
    public Transform[] scrollingObject;
    
    [Header("Import width")]
    public float width;

    //Village BackGround Width = 36

    // Start is called before the first frame update
    void Start()
    {
        //SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        //width = sprite.size.x;

        //scrollingObject = new Transform[transform.childCount];

        //for(int i = 0; i < transform.childCount; i++)
        //{
        //    scrollingObject[i] = transform.GetChild(i);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        // 오브젝트의 위치를 스크롤링
        foreach (Transform obj in scrollingObject)
        {
            Vector3 position = obj.position;

            position.x += -speed * Time.deltaTime;
            obj.position = position;

            if (obj.position.x < -width * 2)
            {
                position.x = width * 2;
                obj.position = position;
            }
        }
    }
}