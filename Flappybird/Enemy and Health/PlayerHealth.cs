using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = startingHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collided with an object that can damage it
        if (other.CompareTag("Enemy"))
        {
            // Assuming that the "Enemy" tag is used for objects that can damage the player
            TakeDamage(20); // Adjust the amount of damage as needed
        }
    }

    private void TakeDamage(int damageAmount)
    {
        // Reduce the player's health
        currentHealth -= damageAmount;

        // Check if the player's health is zero or below
        if (currentHealth <= 0)
        {
            // Player is out of health, destroy it
            Destroy(gameObject);
        }
    }
}
