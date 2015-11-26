using UnityEngine;

namespace Asteroids
{
	public class InputKeyboardController : IInputController 
	{
		#region IInputController implementation

		public float GetRotationValue ()
		{
			return Input.GetAxis ("Horizontal");
		}

		public bool IsForward ()
		{
			return Input.GetAxis ("Vertical") > 0;
		}

		public bool IsShot ()
		{
			return Input.GetButtonDown ("Fire1");
		}

		#endregion
	}
}