using UnityEngine;

namespace Asteroids
{
	public class WeaponLaserBehaviour : IWeaponBehaviour
	{
		#region IWeaponBehaviour implementation

		public WeaponType Type { get { return WeaponType.Laser; } }

		public void DoMovement (MovementController movementController)
		{
			movementController.ThrustForce = 50;

			movementController.DoForce (movementController.transform.up);
		}

		#endregion
	}
}