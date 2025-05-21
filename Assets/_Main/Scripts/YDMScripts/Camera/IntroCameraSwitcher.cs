using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class IntroCameraSwitcher : MonoBehaviour
{
    [Tooltip("인트로 카메라")]
    public CinemachineCamera vCamIntro;
    [Tooltip("플레이어 추적 카메라")]
    public CinemachineCamera vCamFollow;
    [Tooltip("인트로 지속 시간 (초)")]
    public float introDuration = 8f;

    void Start()
    {
        // 시작 시점 우선순위 세팅
        vCamIntro.Priority = 20;
        vCamFollow.Priority = 10;

        StartCoroutine(EndIntro());
    }

    IEnumerator EndIntro()
    {
        yield return new WaitForSeconds(introDuration);

        // 1) 인트로 카메라 멈추기
        var splineDolly = vCamIntro.GetComponent<Unity.Cinemachine.CinemachineSplineDolly>();
        if (splineDolly != null)
        {
            // SplineAutoDolly 구조체를 꺼내서 Enabled만 false로 바꾼 뒤
            var autoDolly = splineDolly.AutomaticDolly;
            autoDolly.Enabled = false;
            // 다시 프로퍼티에 할당
            splineDolly.AutomaticDolly = autoDolly;
        }

        // 2) Priority 낮춰서 Follow 카메라로 전환
        vCamIntro.Priority = 0;
    }
}
