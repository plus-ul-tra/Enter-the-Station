using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainLeft : MonoBehaviour
{
    [Header("���� �̵� �ӵ�")]
    [SerializeField]
    private float trainSpeed = 1f;

    private Vector3 startPos;//���� ���� ��ġ ����
    private bool isWaiting = false;//���� ���� �÷���
    private bool hasPaused = false;//���� ���� ���� Ȯ�� �÷���

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
       
        // ���� �̵�
        transform.position += Vector3.right * trainSpeed * Time.deltaTime;

        // �� ���� ���� �� ����
        if (transform.position.x > startPos.x + 70f)
        {
            transform.position = startPos;
            hasPaused = false;
        }
    }

   
}
