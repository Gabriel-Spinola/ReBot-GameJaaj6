using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float xAxis;

    public bool keyJump;
    public bool keyJumpHold;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");

        keyJump = Input.GetKeyDown(KeyCode.Space);
        keyJumpHold = Input.GetKey(KeyCode.Space);
    }
}
