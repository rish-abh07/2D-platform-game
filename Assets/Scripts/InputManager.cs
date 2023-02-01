
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerInput playerInput;
    private InputAction touchPA;
    private InputAction touchPressAction;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPA = playerInput.actions.FindAction("TouchPosition");
        touchPressAction = playerInput.actions.FindAction("TouchPress");
    }
}
