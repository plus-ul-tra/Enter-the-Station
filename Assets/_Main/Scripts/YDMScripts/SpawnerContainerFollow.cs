using UnityEngine;

public class SpawnerContainerFollow : MonoBehaviour
{
    [Tooltip("���� �÷��̾�")]
    [SerializeField] private Transform player;

    private Vector3 lastPlayerPos;

    void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player").transform;
        lastPlayerPos = player.position;
    }

    void LateUpdate()
    {
        // �÷��̾ �̵��� ��Ÿ ���
        Vector3 delta = player.position - lastPlayerPos;

        // �����̳ʴ� Y�ุ ���󰡰�
        transform.position += new Vector3(0f, delta.y, 0f);

        lastPlayerPos = player.position;
    }
}
