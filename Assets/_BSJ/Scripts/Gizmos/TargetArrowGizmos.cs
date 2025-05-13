using UnityEngine;

[ExecuteAlways]
public class TargetArrowGizmos : MonoBehaviour
{
    public float radius = 0.2f;
    public float yOffset = 0.3f; // ���� �󸶳� �Ʒ��� ������ ����

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        int segments = 60;
        Vector3 prevPoint = Vector3.zero;

        for (int i = 0; i <= segments; i++)
        {
            float angle = 2 * Mathf.PI * i / segments;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            // �߽ɿ��� yOffset��ŭ �Ʒ��� ����
            Vector3 point = transform.position + new Vector3(x, y - yOffset, 0f);

            if (i > 0)
            {
                Gizmos.DrawLine(prevPoint, point);
            }

            prevPoint = point;
        }
    }
}