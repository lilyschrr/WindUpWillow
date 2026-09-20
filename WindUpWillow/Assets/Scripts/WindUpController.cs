using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WindUpController : MonoBehaviour
{

[SerializeField] private GameObject windingKey;


[SerializeField] private float moveAmount;

    public AudioSource SoundEffect;

    
    public AudioSource Song;
    [SerializeField] private float secondsPerBeat;
    public float SecondsPerBeat => secondsPerBeat;

    private bool ableToWind = true;
    public bool AbleToWind =>ableToWind;
    // Can be read from other scripts
    public float MoveAmount => moveAmount; // Total time the t key is held down

    void Update()
    {
        if (ableToWind)
        {
            if (Keyboard.current == null) return;

            //speed up crank with right arrow in addition to t key
            if (Keyboard.current.rightArrowKey.isPressed && Keyboard.current.tKey.isPressed)
            {
                windingKey.transform.Rotate(0, 90 * Time.deltaTime * 5, 0);

                // Add sound effect
                if (!SoundEffect.isPlaying) SoundEffect.Play();
                moveAmount += Time.deltaTime *5;
            }
            // t key is being held down
            else if (Keyboard.current.tKey.isPressed)
            {
                windingKey.transform.Rotate(0, 90 * Time.deltaTime, 0);

                // Add sound effect
                if(!SoundEffect.isPlaying)SoundEffect.Play();
                moveAmount += Time.deltaTime;
            }

            // Triggers once when t key is released
            if (Keyboard.current.tKey.wasReleasedThisFrame)
            {
                // Game object hidden (data is still accessible)
                windingKey.SetActive(false);
                ableToWind = false;
                SoundEffect.Stop();
                Song.Play();
                //makes it so everything stops at the end of a beat and not mid-beat
                moveAmount = Mathf.Round(moveAmount / secondsPerBeat) * secondsPerBeat;
            }
        }
        else
        {
            if (moveAmount >= 0) moveAmount -= Time.deltaTime;
            else Song.Stop();
            
        }
    }

    public void CeaseAll()
    {
        moveAmount = 0;
    }
}
