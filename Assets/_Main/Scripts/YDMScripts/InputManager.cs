using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    // 에셋에서 Generate 한 C# 클래스
    private GameControls controls;

    // 내부 저장용
    private Vector2 moveInput = Vector2.zero;
    private bool jumpPressed = false;
    private bool attackPressed = false;
    private bool interactPressed = false;
    // … 필요하다면 더 추가

    // 외부에서 읽기만 할 프로퍼티
    public float Horizontal => moveInput.x;
    public float Vertical => moveInput.y;
    public Vector2 MoveInput => moveInput;

    public bool JumpPressed => jumpPressed;
    public bool AttackPressed => attackPressed;
    public bool InteractPressed => interactPressed;
    // … 필요하다면 더 추가

    private void Awake()
    {
        // 싱글톤 설정
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // InputSystem_Actions 인스턴스 생성
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

        // 필요하면 더 추가…
    }

    private void OnEnable()
    {
        controls.Player.Enable();
        // UI 맵도 활성화 하고 싶다면: controls.UI.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
        // controls.UI.Disable();
    }
}
