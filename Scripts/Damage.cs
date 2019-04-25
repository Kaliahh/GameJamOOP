using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public GameObject player;
    
    public float DamageCoolDown; //Seconds before enemy can eat player again
    public float CurrentCooldown; //Current cooldown. If 0 then enemy can hit player
    public float DamageOnPlayer; //How much to eat of the player

    // Start is called before the first frame update
    void Update()
    {
        if (CurrentCooldown > 0)
        {
            CurrentCooldown -= Time.deltaTime;
            if (CurrentCooldown < 0)
            {
                CurrentCooldown = 0;
            }
        }      
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player" && CurrentCooldown == 0)
        {   
            Hit(other.gameObject);
        }
    }

    private void Hit(GameObject gameObject)
    {
        gameObject.GetComponent<Player>().health -= DamageOnPlayer;
        CurrentCooldown = DamageCoolDown;

    } 
}
