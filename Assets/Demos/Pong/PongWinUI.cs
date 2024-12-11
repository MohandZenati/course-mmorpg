using UnityEngine;  // Assurez-vous d'inclure cette ligne
using UnityEngine.SceneManagement;


public class PongWinUI : MonoBehaviour
{
    public GameObject Panel;
    public GameObject PlayerLeft;
    public GameObject PlayerRight;

    PongBall Ball;

    void Start()
    {
        Panel.SetActive(false);
        PlayerLeft.SetActive(false);
        PlayerRight.SetActive(false);
        Ball = GameObject.FindFirstObjectByType<PongBall>();
    }

    void Update()
    {
        switch (Ball.State) {
            case PongBallState.Playing:
                Panel.SetActive(false);
                PlayerLeft.SetActive(false);
                PlayerRight.SetActive(false);
                break;
            case PongBallState.PlayerLeftWin:
                Panel.SetActive(true);
                PlayerLeft.SetActive(true);
                break;
            case PongBallState.PlayerRightWin:
                Panel.SetActive(true);
                PlayerRight.SetActive(true);
                break;
        }
    }

    public void OnReplay() {

        Panel.SetActive(false);
        PlayerLeft.SetActive(false);
        PlayerRight.SetActive(false);
    }
}
