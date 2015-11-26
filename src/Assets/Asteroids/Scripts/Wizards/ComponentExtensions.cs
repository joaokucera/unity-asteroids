using UnityEngine;

namespace Asteroids
{
    public static class ComponentExtensions
    {
        public static T GetOrAddComponent<T>(this Component component) where T : Component
        {
            T result = component.GetComponent<T>();

            if (result == null)
            {
                result = component.gameObject.AddComponent<T>();
            }

            return result;
        }

		public static bool IsPlayerShot(this Collider2D collider)
		{
			return collider.CompareTag ("Player Shot");
		}
		
		public static bool IsEnemyShot(this Collider2D collider)
		{
			return collider.CompareTag ("Enemy Shot");
		}

		public static bool IsObstacle(this Collider2D collider)
		{
			return collider.CompareTag ("Obstacle");
		}

		public static bool IsPowerUp(this Collider2D collider)
		{
			return collider.CompareTag ("PowerUp");
		}
    }
}