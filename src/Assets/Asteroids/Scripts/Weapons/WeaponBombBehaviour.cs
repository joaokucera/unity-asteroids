using UnityEngine;

namespace Asteroids
{
	public class WeaponBombBehaviour : IWeaponBehaviour
	{
		private const float ThrustForce = 25;

		#region IWeaponBehaviour implementation

		public WeaponType Type { get { return WeaponType.Bomb; } }

		public void DoMovement (MovementController movementController)
		{
			movementController.ThrustForce = ThrustForce;

			movementController.DoForce (-movementController.transform.up);
		}
		
		#endregion
	}
}