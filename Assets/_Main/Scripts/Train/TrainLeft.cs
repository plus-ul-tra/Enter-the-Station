using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainLeft : MonoBehaviour
{
    [Header("기차 이동 속도")]
    [SerializeField]
    private float trainSpeed = 1f;

    private Vector3 startPos;//열차 시작 위치 저장

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
       
        // 기차 이동
        transform.position += Vector3.right * trainSpeed * Time.deltaTime;

        // 끝 지점 도달 시 리셋
        if (transform.position.x > startPos.x + 70f)
        {
            transform.position = startPos;
        }
    }

   
}
