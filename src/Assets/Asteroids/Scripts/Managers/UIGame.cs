using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids
{
	[AddComponentMenu("ASTEROIDS / UI Game")]
	public class UIGame : Singleton<UIGame> 
	{
		[Header("In Game UI Componenets")]
		[SerializeField] private RectTransform m_panelInGame;
		[SerializeField] private Image[] m_lifeImages;
		[SerializeField] private Image[] m_powerUpImages;
		[SerializeField] private Text m_currentScoreText;
		[SerializeField] private Text m_previousHighScoreText;
		[SerializeField] private Text m_levelText;

		[Header("Game Over UI Componenets")]
		[SerializeField] private RectTransform m_panelGameOver;
		[SerializeField] private Text m_finalScoreText;
		[SerializeField] private Text m_newHighScoreText;

		void Start()
		{
			UpdatePowerUpImages (1);

			UpdateLevelText ();

			UpdateHighScoreText ();
		}

		public void Retry()
		{
			SoundManager.PlaySoundEffect ("ButtonClick");

			Application.LoadLevel ("GameScene");
		}

		public void Menu()
		{
			SoundManager.PlaySoundEffect ("ButtonClick");

			Application.LoadLevel ("MenuScene");
		}

		public static void ShowGameOver()
		{
			Instance.DoGameOver ();
		}

		public static bool TryUpdateLife()
		{
			return Instance.UpdateLifeImages ();
		}

		public static void UpdatePowerUp(int amountEnabled)
		{
			Instance.UpdatePowerUpImages (amountEnabled + 1);
		}

		public static void UpdateScore(int newScore)
		{
			Instance.StartCoroutine (Instance.UpdateScoreText (newScore));
		}

		public static void UpdateLevel()
		{
			Instance.UpdateLevelText ();
		}

		private bool UpdateLifeImages()
		{
			var playerData = GlobalVariables.Player.Data;
			playerData.Lifes--;

			var lifeImage = m_lifeImages.LastOrDefault(l => l.enabled);
			if (lifeImage != null)
			{
				lifeImage.enabled = false;

				return true;
			}

			return false;
		}

		private void UpdatePowerUpImages (int amountEnabled)
		{
			for (int i = 0; i < m_powerUpImages.Length; i++)
			{
				m_powerUpImages[i].enabled = false;
			}

			for (int i = 0; i < amountEnabled; i++) 
			{
				m_powerUpImages[i].enabled = true;
			}
		}

		private IEnumerator UpdateScoreText(int newScore)
		{
			var previousScore = GlobalVariables.Player.Data.CurrentScore;
			GlobalVariables.Player.Data.CurrentScore += newScore;

			while (previousScore < GlobalVariables.Player.Data.CurrentScore)
			{
				previousScore++;

				m_currentScoreText.text = string.Format("<color=#FFC000FF>{0}</color>", previousScore);

				yield return new WaitForSeconds(.1f);
			}

			m_currentScoreText.text = string.Format("<color=#FFC000FF>{0}</color>", GlobalVariables.Player.Data.CurrentScore);
		}

		private void UpdateHighScoreText()
		{
			m_previousHighScoreText.text = string.Format("HIGH SCORE: <color=#FFC000FF>{0}</color>", GlobalVariables.Player.Data.HighScore);
		}

		private void UpdateLevelText()
		{
			m_levelText.text = string.Format("LEVEL <color=#FFC000FF>{0}</color>", GlobalVariables.Player.Data.Level);
		}

		private void DoGameOver()
		{
			GlobalVariables.Player.Data.CheckHighScore ();
			
			m_panelInGame.localScale = Vector3.zero;
			m_panelGameOver.localScale = Vector3.one;
			
			m_finalScoreText.text = string.Format("FINAL SCORE: <color=#FFC000FF>{0}</color>", GlobalVariables.Player.Data.CurrentScore);
			m_newHighScoreText.text = string.Format("HIGH SSCORE: <color=#FFC000FF>{0}</color>", GlobalVariables.Player.Data.HighScore);
		}
	}
}