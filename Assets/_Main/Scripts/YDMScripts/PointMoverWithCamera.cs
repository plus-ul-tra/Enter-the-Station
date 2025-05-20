using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class PointMoverWithCamera : MonoBehaviour
{
    [Header("이동할 좌표들")]
    public Vector2[] points = new Vector2[] {
        new Vector2(-10f, -2.5f),
        new Vector2(-2.5f, -5f),
        new Vector2(-6.6f, -10f),
        new Vector2(-6f, -6f)
    };

    [Header("각 지점 도착 후 대기 시간")]
    public float waitTime = 1f;

    [Header("포인트별 카메라 사이즈")]
    [Tooltip("points 배열과 같은 길이여야 합니다.")]
    public float[] cameraSizes = new float[] { 1f, 100f, 2f, 100f };

    //[Header("포인트별 종횡비 (width/height)")]
    //public float[] cameraAspects = { 442f / 545f, 95f / 61f, 56f / 41f, 96f / 125f };

    [Header("참조할 Cinemachine Virtual Camera")]
    public CinemachineCamera vcam;

    void Start()
    {
        if (vcam == null)
            Debug.LogError("vcam이 할당되지 않았습니다!");

        StartCoroutine(MoveAndResizeCamera());
    }

    private IEnumerator MoveAndResizeCamera()
    {
        Camera cam = vcam.GetComponent<Camera>();
        for (int i = 0; i < points.Length; i++)
        {
            // 1) 오브젝트 순간이동
            transform.position = points[i];

            // 2) 카메라 사이즈 변경
            vcam.Lens.OrthographicSize = cameraSizes[i];

            //cam.aspect = cameraAspects[i];
            // 3) 대기
            yield return new WaitForSeconds(waitTime);
        }
    }
}
