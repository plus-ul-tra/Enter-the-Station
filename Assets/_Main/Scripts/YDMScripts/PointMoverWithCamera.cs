using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class PointMoverWithCamera : MonoBehaviour
{
    [Header("�̵��� ��ǥ��")]
    public Vector2[] points = new Vector2[] {
        new Vector2(-10f, -2.5f),
        new Vector2(-2.5f, -5f),
        new Vector2(-6.6f, -10f),
        new Vector2(-6f, -6f)
    };

    [Header("�� ���� ���� �� ��� �ð�")]
    public float waitTime = 1f;

    [Header("����Ʈ�� ī�޶� ������")]
    [Tooltip("points �迭�� ���� ���̿��� �մϴ�.")]
    public float[] cameraSizes = new float[] { 1f, 100f, 2f, 100f };

    //[Header("����Ʈ�� ��Ⱦ�� (width/height)")]
    //public float[] cameraAspects = { 442f / 545f, 95f / 61f, 56f / 41f, 96f / 125f };

    [Header("������ Cinemachine Virtual Camera")]
    public CinemachineCamera vcam;

    void Start()
    {
        if (vcam == null)
            Debug.LogError("vcam�� �Ҵ���� �ʾҽ��ϴ�!");

        StartCoroutine(MoveAndResizeCamera());
    }

    private IEnumerator MoveAndResizeCamera()
    {
        Camera cam = vcam.GetComponent<Camera>();
        for (int i = 0; i < points.Length; i++)
        {
            // 1) ������Ʈ �����̵�
            transform.position = points[i];

            // 2) ī�޶� ������ ����
            vcam.Lens.OrthographicSize = cameraSizes[i];

            //cam.aspect = cameraAspects[i];
            // 3) ���
            yield return new WaitForSeconds(waitTime);
        }
    }
}
