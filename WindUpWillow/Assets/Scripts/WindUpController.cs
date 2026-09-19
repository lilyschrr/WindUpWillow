using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WindUpController : MonoBehaviour
{

[SerializeField] private GameObject windingKey;

    void Update()
    {
        if (Keyboard.current == null) return;


        // t key is held down
        if (Keyboard.current.tKey.isPressed)
        {
            windingKey.transform.Rotate(0, 90 * Time.deltaTime, 0);
            
            // Add sound effect
            // Track time that key pressed (limit it?)
        }

        // Triggers once when t key is released
        if (Keyboard.current.tKey.wasReleasedThisFrame)
        {
            // Disable the game object?
        }

    }
}