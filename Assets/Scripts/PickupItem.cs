using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class PickupItem : MonoBehaviour, Interactable
{

    bool picked = false;
    Player player;
    private void Awake() {
        player = FindObjectOfType<Player>();
    }
    public void Interact()
    {   
        picked = !picked;
    }

    private void Update() {
        if(picked)
        {
             
            transform.position = player.transform.position;
            transform.position += player.transform.localScale.x > 0 ? player.transform.right : (-player.transform.right);
            
        }
    }
}
