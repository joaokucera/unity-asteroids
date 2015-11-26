using UnityEngine;

namespace Asteroids
{
	public interface IInputController
	{
		float GetRotationValue();

		bool IsForward();

		bool IsShot();
	}
}