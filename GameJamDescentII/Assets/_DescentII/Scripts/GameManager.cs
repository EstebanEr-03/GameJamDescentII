using UnityEngine;

namespace DescentII
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] int scoreForWin = 3;

        public static GameManager Instance { get; private set; }

        int score;

        public string Score => $"{score}/{scoreForWin}";

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        void Update()
        {
            if (score >= scoreForWin)
            {
                Debug.Log("You win!");
            }
        }

        public void AddScore(int amount)
        {
            score += amount;
        }
    }
}
