using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Set in Inspector: Game Options")]
    [SerializeField]private float timeToEndGame=2;
    [SerializeField] private GameObject panelEndGame;
    [SerializeField] private SpawnCars spawnCars;
    private float timeWhenCarExitFromDisplay=-10;
    private bool carExitFromDisplay = false;
    void Update()
    {
        if (PlayerCar.ExecuteCodeIfCarStayInDisplay() && !carExitFromDisplay) {
            carExitFromDisplay = true;
            timeWhenCarExitFromDisplay = Time.time;
            spawnCars.StopAllCoroutines();
            return;
        }
        if (timeWhenCarExitFromDisplay + timeToEndGame < Time.time && carExitFromDisplay) {
            panelEndGame.SetActive(true);
            UIManager.SaveSatistics();
        }
    }
}
