using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class ButtonNavigation : MonoBehaviour 
{

	int index = 0;
	public int totalLevels = 4;
	public float yOffset = 1f;

	public AudioSource Audio;
	public AudioClip Navigate;

	bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	// Update is called once per frame
	void Update () 
	{
		if (!playerIndexSet || !prevState.IsConnected)
		{
			for (int i = 0; i < 4; ++i)
			{
				PlayerIndex testPlayerIndex = (PlayerIndex)i;
				GamePadState testState = GamePad.GetState(testPlayerIndex);
				if (testState.IsConnected)
				{
					Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
					playerIndex = testPlayerIndex;
					playerIndexSet = true;
				}
			}
		}

		prevState = state;
		state = GamePad.GetState(playerIndex);


		if (Input.GetKeyDown (KeyCode.DownArrow) || prevState.DPad.Down == ButtonState.Pressed && state.DPad.Down == ButtonState.Released )
		{
			Audio.PlayOneShot(Navigate);
			
				if (index < totalLevels - 1) 
				{
					index++;
					Vector2 Position = transform.position;
					Position.y -= yOffset;
					transform.position = Position;
				}

		}

		if (Input.GetKeyDown (KeyCode.UpArrow) || prevState.DPad.Up == ButtonState.Pressed && state.DPad.Up == ButtonState.Released )
		{
			Audio.PlayOneShot(Navigate);

				if (index > 0) 
				{
					index--;
					Vector2 Position = transform.position;
					Position.y += yOffset;
					transform.position = Position;
				}
		}

		if (Input.GetKeyDown (KeyCode.Return) || Input.GetButtonDown("P1A"))
		{
			if (index == 0) 
			{
				SceneManager.LoadScene ("Character Select");
			}

            if (index == 1)
            {
                SceneManager.LoadScene("4 Player Character Select");
            }

            if (index == 2) 
			{
				SceneManager.LoadScene ("Tutorial");
			}

			if (index == 3) 
			{
				Application.Quit();
			}
				
		}



	}

}
