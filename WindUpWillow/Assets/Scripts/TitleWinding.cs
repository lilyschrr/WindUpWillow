using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TitleWinding : MonoBehaviour
{

[SerializeField] private Image windingKey;
public AudioSource SoundEffect;

    
    public AudioSource Song;
    [SerializeField] private float secondsPerBeat;
    public float SecondsPerBeat => secondsPerBeat;

    private bool ableToWind = true;
    public bool AbleToWind => ableToWind;

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
            }
            // t key is being held down
            else if (Keyboard.current.tKey.isPressed)
            {
                windingKey.transform.Rotate(0, 90 * Time.deltaTime, 0);
                // Add sound effect
                if(!SoundEffect.isPlaying)SoundEffect.Play();
            }

            // Triggers once when t key is released
            if (Keyboard.current.tKey.wasReleasedThisFrame)
            {
                ableToWind = false;
                SoundEffect.Stop();
                Song.Play();
            }
        }
    }
}
