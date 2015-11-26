using UnityEngine;

namespace Asteroids
{
	public class WeaponBombBehaviour : IWeaponBehaviour
	{
		#region IWeaponBehaviour implementation

		public WeaponType Type { get { return WeaponType.Bomb; } }

		public void DoMovement (MovementController movementController)
		{
			movementController.ThrustForce = 25;

			movementController.DoForce (-movementController.transform.up);
		}
		
		#endregion
	}
}