using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{

		//add all sound clips here 
		public AudioClip BackgroundNoise,
				BackgroundMusic,
				VirgilIncorrect;

		void Start ()
		{
				//Initializing all sounds that should play at the start of the game

				//Invoke Repeating since we would like these audio clips to loop.
				InvokeRepeating ("PlayBackgroundNoise", 0.0f, BackgroundNoise.length);
                Debug.Log("Temporarily disabled play background music since there is no background music right now.");
				//InvokeRepeating ("PlayBackgroundMusic", 0.0f, BackgroundMusic.length);

		}


		void PlayBackgroundMusic ()
		{
				throw new System.NotImplementedException ();
		}

		void PlayBackgroundNoise ()
		{
		
				audio.PlayOneShot (BackgroundNoise, .4f);
		
		}

		//The following methods exposed
		public void PlayVirgilIncorrect ()
		{

			audio.PlayOneShot (VirgilIncorrect, 4f);
		}

		
		
		
}
