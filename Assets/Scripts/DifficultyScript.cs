using UnityEngine;
using UnityEngine.UI;

public class DifficultyScript : MonoBehaviour
{
    private Button button;
    private SpawnObstacles manager;
    public int difficulty;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
        manager = GameObject.Find("Spawn Manager").GetComponent<SpawnObstacles>();
    }

    public void SetDifficulty()
    {
        manager.StartGame(difficulty);
    }
}