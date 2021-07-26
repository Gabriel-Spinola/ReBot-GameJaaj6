using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float xAxis;

    public bool keyJump;
    public bool keyJumpHold;

    public bool keyGauntlet;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");

        keyJump = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        keyJumpHold = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        keyGauntlet = Input.GetKeyDown(KeyCode.F);
    }
}
