using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class IntroCameraSwitcher : MonoBehaviour
{
    [Tooltip("��Ʈ�� ī�޶�")]
    public CinemachineCamera vCamIntro;
    [Tooltip("�÷��̾� ���� ī�޶�")]
    public CinemachineCamera vCamFollow;
    [Tooltip("��Ʈ�� ���� �ð� (��)")]
    public float introDuration = 8f;

    void Start()
    {
        // ���� ���� �켱���� ����
        vCamIntro.Priority = 20;
        vCamFollow.Priority = 10;

        StartCoroutine(EndIntro());
    }

    IEnumerator EndIntro()
    {
        yield return new WaitForSeconds(introDuration);

        // 1) ��Ʈ�� ī�޶� ���߱�
        var splineDolly = vCamIntro.GetComponent<Unity.Cinemachine.CinemachineSplineDolly>();
        if (splineDolly != null)
        {
            // SplineAutoDolly ����ü�� ������ Enabled�� false�� �ٲ� ��
            var autoDolly = splineDolly.AutomaticDolly;
            autoDolly.Enabled = false;
            // �ٽ� ������Ƽ�� �Ҵ�
            splineDolly.AutomaticDolly = autoDolly;
        }

        // 2) Priority ���缭 Follow ī�޶�� ��ȯ
        vCamIntro.Priority = 0;
    }
}
