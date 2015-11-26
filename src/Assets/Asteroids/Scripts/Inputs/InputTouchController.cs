using UnityEngine;

namespace Asteroids
{
	public class InputTouchController : IInputController
	{
		#region IInputController implementation

		public float GetRotationValue ()
		{
			throw new System.NotImplementedException ();
		}
		
		public bool IsForward ()
		{
			throw new System.NotImplementedException ();
		}

		public bool IsShot ()
		{
			throw new System.NotImplementedException ();
		}
		
		#endregion
	}
}