using UnityEngine;

namespace Asteroids
{
	public interface IWeaponBehaviour
	{
		WeaponType Type { get; }

		void DoMovement(MovementController movementController);
	}
}