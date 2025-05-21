using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainLeft : MonoBehaviour
{
    [Header("기차 이동 속도")]
    [SerializeField]
    private float trainSpeed = 1f;

    private Vector3 startPos;//열차 시작 위치 저장
    private bool isWaiting = false;//열차 정지 플래그
    private bool hasPaused = false;//현재 정지 상태 확인 플래그

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
            hasPaused = false;
        }
    }

   
}
