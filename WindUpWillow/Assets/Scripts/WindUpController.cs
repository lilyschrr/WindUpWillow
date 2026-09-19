using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WindUpController : MonoBehaviour
{

[SerializeField] private GameObject windingKey;


[SerializeField] private float moveAmount;

    public AudioSource Song;

    private bool ableToWind = true;
    public bool AbleToWind =>ableToWind;
    // Can be read from other scripts
    public float MoveAmount => moveAmount; // Total time the t key is held down

    void Update()
    {
        if (ableToWind)
        {
            if (Keyboard.current == null) return;


            // t key is being held down
            if (Keyboard.current.tKey.isPressed)
            {
                windingKey.transform.Rotate(0, 90 * Time.deltaTime, 0);

                // Add sound effect
                moveAmount += Time.deltaTime;
            }

            // Triggers once when t key is released
            if (Keyboard.current.tKey.wasReleasedThisFrame)
            {
                // Game object hidden (data is still accessible)
                windingKey.SetActive(false);
                ableToWind = false;
                Song.Play();
            }
        }
        else
        {
            if (moveAmount >= 0) moveAmount -= Time.deltaTime;
            else Song.Stop();
        }
    }
}
