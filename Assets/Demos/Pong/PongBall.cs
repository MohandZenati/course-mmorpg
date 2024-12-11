using UnityEngine;
using System.Collections;
using TMPro;

public enum PongBallState
{
    Playing = 0,
    PlayerLeftWin = 1,
    PlayerRightWin = 2,
}

public class PongBall : MonoBehaviour
{
    public float Speed = 1;

    Vector3 Direction;
    PongBallState _State = PongBallState.Playing;

    public PongBallState State
    {
        get
        {
            return _State;
        }
    }

    // Variables pour les scores
    public int scoreLeft = 0;
    public int scoreRight = 0;
    public TMPro.TMP_Text scoreLeftText;  // Pour afficher le score du joueur gauche
    public TMPro.TMP_Text scoreRightText;

    void Start()
    {
        Respawn(); // Appel de la méthode Respawn lors du démarrage
    }

    void Update()
    {
        if (State != PongBallState.Playing)
        {
            return;
        }

        transform.position = transform.position + (Direction * Speed * Time.deltaTime);

        Debug.Log("Position actuelle du ballon : " + transform.position); //afficher la position du ballon
        
        scoreRightText.text = scoreRight.ToString();
        scoreLeftText.text = scoreLeft.ToString();
    }

    void OnCollisionEnter(Collision c)
    {
        switch (c.collider.name)
        {
            case "BoundTop":
            case "BoundBottom":
                Direction.y = -Direction.y;
                break;

            case "PaddleLeft":
            case "PaddleRight":
                Direction.x = -Direction.x;
                break;

            case "BoundLeft":
                _State = PongBallState.PlayerRightWin;
                scoreRight++;
                Debug.Log("Score du joueur droit : " + scoreRight);
                StartCoroutine(RespawnWithDelay());
                break;

            case "BoundRight":
                _State = PongBallState.PlayerLeftWin;
                scoreLeft++;
                Debug.Log("Score du joueur gauche : " + scoreLeft);
                StartCoroutine(RespawnWithDelay());
                break;
        }
    }

    IEnumerator RespawnWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        Respawn();
    }

    public void Respawn()
    {

        transform.position = new Vector3(0, 0, 0);

        // Direction aléatoire
        Direction = new Vector3(
            Random.Range(0.5f, 1),
            Random.Range(-0.5f, 0.5f),
            0
        );

        Direction.x *= Mathf.Sign(Random.Range(-100, 100));

        Direction.Normalize();

        _State = PongBallState.Playing;
        Debug.Log("Balle respawnée. Scores -> Gauche: " + scoreLeft + " Droite: " + scoreRight); // Affiche les scores après respawn

    }

}
