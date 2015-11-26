using UnityEngine;

namespace Asteroids
{
	public class InputTouchController : IInputController
	{
		private const float CooldownRate = .25f;
		private float m_cooldownTime;

		public InputTouchController ()
		{
			UIGame.ShowTouchControls ();

			m_cooldownTime = Time.time;
		}

		#region IInputController implementation

		public float GetRotationValue ()
		{
			float leftValue = 0;
			float rightValue = 0;

			if (UIGame.IsClickButtonLeft) leftValue = -1;
			if (UIGame.IsClickButtonRight) rightValue = 1;

			return leftValue + rightValue;
		}
		
		public bool IsForward ()
		{
			return UIGame.IsClickButtonFoward;
		}

		public bool IsShot ()
		{
			if (Time.time - m_cooldownTime > CooldownRate)
			{
				m_cooldownTime = Time.time;

				return true;
			}

			return false;
		}
		
		#endregion
	}
}