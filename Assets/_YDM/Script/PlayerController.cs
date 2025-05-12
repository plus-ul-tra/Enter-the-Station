using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [Header("���� �̵� �ӵ�")]
    [SerializeField] private float horizontalSpeed = 5f;
    [Header("���� �̵� �ӵ�")]
    [SerializeField] private float verticalSpeed = 0.5f;

    [Header("�̵� ���� ����")]
    [SerializeField] private float minX = -5f, maxX = 5f;
    [SerializeField] private float minY = -3f, maxY = 3f;

    [Header("���ͽ�����")]
    [SerializeField] private Transform[] spawner;

    private SpriteRenderer spriteRenderer;
    private PlayerAnimator playerAnim;
    private Rigidbody2D rigidbody;
    private int horizontalDirection = 1;
    private bool canMove = true;

    // --------------------------------------------------

    RandomEventObject randomEventObject;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<PlayerAnimator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!canMove) return;

        // New Input System �� �б�
        float inputX = InputManager.Instance.Horizontal;
        float inputY = InputManager.Instance.Vertical;
        //bool jump = InputManager.Instance.JumpPressed;

        bool horizontalKey = !Mathf.Approximately(inputX, 0f);
        bool verticalKey = !Mathf.Approximately(inputY, 0f);

        // �¿� ���� ��ȯ
        if (horizontalKey)
        {
            horizontalDirection = inputX > 0f ? 1 : -1;
            spriteRenderer.flipX = (horizontalDirection < 0);
        }

        // ����/���� �̵� ���� (������ ����)
        float dx = horizontalSpeed * horizontalDirection;
        if (verticalKey && !horizontalKey) dx = 0f;
        float dy = verticalKey
                 ? inputY * horizontalSpeed * verticalSpeed
                 : 0f;

        // ��: ���� ó�� (������ �α�)
        //if (jump)
        //    Debug.Log("Jump!");

        // ���� �̵�
        Vector3 move = new Vector3(dx, dy, 0f) * Time.deltaTime;
        transform.Translate(move, Space.World);

        // ȭ�� ����
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;

        // E Ű ������ ��ȣ�ۿ�
        if (Input.GetKeyDown(KeyCode.E) && randomEventObject != null)
        {
            randomEventObject.CompleteInteractEvent(); // �̺�Ʈ ����
            randomEventObject = null; // ���� �ʱ�ȭ
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 delta = Vector3.zero;

        if (other.CompareTag("obstacle"))
        {
            playerAnim.SetStunned(true);
            StartCoroutine(StunRoutine(2f));
            StartCoroutine(PauseMovement(2f));
        }
        if (other.CompareTag("Stairs_up"))// ���� ���� ���� �����ʿ�
            delta = new Vector3(-1, 5.5f, 0f);
        else if (other.CompareTag("Stairs_down"))
            delta = new Vector3(+1, -5.5f, 0f);

        if (delta != Vector3.zero)
        {
            // 1) �÷��̾� �̵�
            rigidbody.transform.position += delta;

            // 2) ��� ������ �̵�
            foreach (var spawner in spawner)
            {
                // ������ ������Ʈ ��ü�� �ű� ���
                spawner.transform.position += delta;

                // ���� MonsterSpawner ���ο� ShiftSpawnPoints(delta)�� �����Ǿ� �ִٸ�
                // spawner.ShiftSpawnPoints(delta);
            }
        }

        if (other.CompareTag("EventObject"))
        {
            randomEventObject = other.GetComponent<RandomEventObject>();
        }
    }
    private IEnumerator StunRoutine(float duration)
    {
        canMove = false;
        yield return new WaitForSeconds(duration);

        // 3) �˴ٿ� ����
        playerAnim.SetStunned(false);
        canMove = true;
    }
    private IEnumerator PauseMovement(float duration)
    {
        canMove = false;
        yield return new WaitForSeconds(duration);
        canMove = true;
    }
}
