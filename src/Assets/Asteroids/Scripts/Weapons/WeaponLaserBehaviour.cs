using UnityEngine;

namespace Asteroids
{
	public class WeaponLaserBehaviour : IWeaponBehaviour
	{
		private const float ThrustForce = 50;

		#region IWeaponBehaviour implementation

		public WeaponType Type { get { return WeaponType.Laser; } }

		public void DoMovement (MovementController movementController)
		{
			movementController.ThrustForce = ThrustForce;

			movementController.DoForce (movementController.transform.up);
		}

		#endregion
	}
}