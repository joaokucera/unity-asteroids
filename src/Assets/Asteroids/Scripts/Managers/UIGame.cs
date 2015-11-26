using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids
{
	[AddComponentMenu("ASTEROIDS/UI Game")]
	public class UIGame : Singleton<UIGame> 
	{
		[Header("In Game UI Components")]
		[SerializeField] private GameObject m_panelInGame;
		[SerializeField] private Image[] m_lifeImages;
		[SerializeField] private Image[] m_powerUpImages;
		[SerializeField] private Text m_currentScoreText;
		[SerializeField] private Text m_previousHighScoreText;
		[SerializeField] private Text m_levelText;

		[Header("Game Over UI Components")]
		[SerializeField] private GameObject m_panelGameOver;
		[SerializeField] private Text m_finalScoreText;
		[SerializeField] private Text m_newHighScoreText;

		[Header("Touch Controls UI Components")]
		[SerializeField] private GameObject m_panelTouchControls;
		[SerializeField] private InputTouchPointer m_buttonLeft;
		[SerializeField] private InputTouchPointer m_buttonRight;
		[SerializeField] private InputTouchPointer m_buttonFoward;

		public static bool IsClickButtonLeft { get { return Instance.m_buttonLeft.IsClicked; } }
		public static bool IsClickButtonRight { get { return Instance.m_buttonRight.IsClicked; } }
		public static bool IsClickButtonFoward { get { return Instance.m_buttonFoward.IsClicked; } }

		void Start()
		{
			UpdatePowerUpImages (1);

			UpdateLevelText ();

			UpdateHighScoreText ();
		}

		void Update()
		{
			if (Input.GetKeyDown (KeyCode.Escape)) 
			{
				Menu();
			}
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

		public static void ShowTouchControls ()
		{
			Instance.m_panelTouchControls.SetActive (true);
		}

		public static void ShowGameOver()
		{
			SoundManager.PlaySoundEffect ("GameOver");

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
			GlobalVariables.Player.Data.LoseLife();

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

			GlobalVariables.Player.Data.AddScore(newScore);

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
			
			m_panelInGame.SetActive (false);
			m_panelGameOver.SetActive (true);
			
			m_finalScoreText.text = string.Format("FINAL SCORE: <color=#FFC000FF>{0}</color>", GlobalVariables.Player.Data.CurrentScore);
			m_newHighScoreText.text = string.Format("HIGH SSCORE: <color=#FFC000FF>{0}</color>", GlobalVariables.Player.Data.HighScore);
		}
	}
}