using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    // ���¿��� Generate �� C# Ŭ����
    private GameControls controls;

    // ���� �����
    private Vector2 moveInput = Vector2.zero;
    private bool jumpPressed = false;
    private bool attackPressed = false;
    private bool interactPressed = false;
    // �� �ʿ��ϴٸ� �� �߰�

    // �ܺο��� �б⸸ �� ������Ƽ
    public float Horizontal => moveInput.x;
    public float Vertical => moveInput.y;
    public Vector2 MoveInput => moveInput;

    public bool JumpPressed => jumpPressed;
    public bool AttackPressed => attackPressed;
    public bool InteractPressed => interactPressed;
    // �� �ʿ��ϴٸ� �� �߰�

    private void Awake()
    {
        // �̱��� ����
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // InputSystem_Actions �ν��Ͻ� ����
        controls = new GameControls();

        // Move (Vector2)
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        // Jump (Button)
        controls.Player.Jump.started += ctx => jumpPressed = true;
        controls.Player.Jump.canceled += ctx => jumpPressed = false;

        // Attack (Button)
        controls.Player.Attack.started += ctx => attackPressed = true;
        controls.Player.Attack.canceled += ctx => attackPressed = false;

        // Interact (Button)
        controls.Player.Interact.started += ctx => interactPressed = true;
        controls.Player.Interact.canceled += ctx => interactPressed = false;

        // �ʿ��ϸ� �� �߰���
    }

    private void OnEnable()
    {
        controls.Player.Enable();
        // UI �ʵ� Ȱ��ȭ �ϰ� �ʹٸ�: controls.UI.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
        // controls.UI.Disable();
    }
}
