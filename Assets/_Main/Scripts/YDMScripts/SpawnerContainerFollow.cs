using UnityEngine;

public class SpawnerContainerFollow : MonoBehaviour
{
    [Tooltip("따라갈 플레이어")]
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
        // 플레이어가 이동한 델타 계산
        Vector3 delta = player.position - lastPlayerPos;

        // 컨테이너는 Y축만 따라가게
        transform.position += new Vector3(0f, delta.y, 0f);

        lastPlayerPos = player.position;
    }
}
