using UnityEngine;

namespace Asteroids
{
	public static class InputFactory 
	{
		public static IInputController GetCurrentInputController()
		{
			#if UNITY_ANDROID
			return new InputTouchController();
			#else
			return new InputKeyboardController();
			#endif
		}
	}
}