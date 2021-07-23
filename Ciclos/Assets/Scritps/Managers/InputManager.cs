using UnityEngine;

#if UNITY_EDITOR
using UnityEngine.SceneManagement;
#endif

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

        keyJump = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        keyJumpHold = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.RightAlt)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
#endif
    }
}
