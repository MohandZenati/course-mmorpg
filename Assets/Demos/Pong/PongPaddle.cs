using UnityEngine;
using UnityEngine.InputSystem;


/// Énumération pour identifier les joueurs dans le jeu Pong.

public enum PongPlayer {
    PlayerLeft = 1,   // Joueur gauche
    PlayerRight = 2   // Joueur droit
}

public class PongPaddle : MonoBehaviour
{ 

    public PongPlayer Player = PongPlayer.PlayerLeft;

    public float Speed = 1;

    public float MinY = -4;
    public float MaxY = 4;

    private PongInput inputActions;
    private InputAction PlayerAction;

    void Start()
    {
   
        inputActions = new PongInput();

        switch (Player) {
            case PongPlayer.PlayerLeft:
                PlayerAction = inputActions.Pong.Player1; 
                break;
            case PongPlayer.PlayerRight:
                PlayerAction = inputActions.Pong.Player2;  
                break;
        }
        
        PlayerAction.Enable();
    }
    void Update()
    {
  
        float direction = PlayerAction.ReadValue<float>();

       
        Vector3 newPos = transform.position + (Vector3.up * Speed * direction * Time.deltaTime);

        newPos.y = Mathf.Clamp(newPos.y, MinY, MaxY);

        transform.position = newPos;

    
        Debug.Log($"{Player} Paddle Position: {transform.position}");

    }

    void OnDisable() {
        PlayerAction.Disable();
    }
}
