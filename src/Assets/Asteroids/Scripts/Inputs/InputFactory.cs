using UnityEngine;

namespace Asteroids
{
	public static class InputFactory 
	{
		public static IInputController GetCurrentInputController()
		{
			#if UNITY_ANDROID
			return new TouchInputController();
			#else
			return new InputKeyboardController();
			#endif
		}
	}
}