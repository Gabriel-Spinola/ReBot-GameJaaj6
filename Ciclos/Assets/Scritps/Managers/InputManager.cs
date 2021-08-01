using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float xAxis;

    public bool keyJump;
    public bool keyJumpHold;

    public bool keyUse;
    public bool keyUseHold;

    public static bool DisableInput = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (DisableInput)
            return;

        xAxis = Input.GetAxisRaw("Horizontal");

        keyJump = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        keyJumpHold = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        keyUse = Input.GetKeyDown(KeyCode.E);
        keyUseHold = Input.GetKey(KeyCode.E);
    }
}
