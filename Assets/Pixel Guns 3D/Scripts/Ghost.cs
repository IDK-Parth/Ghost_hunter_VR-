using UnityEngine;

public class GhostHealth : MonoBehaviour
{
    [SerializeField] private int health = 1;

    // This function is called when your bullet or sword hits the ghost
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // 1. Find the GameManager in the scene
        GameManager gm = FindObjectOfType<GameManager>();

        // 2. Tell the GameManager to add a kill
        if (gm != null)
        {
            gm.AddKill();
        }

        // 3. Optional: Add a ghost death sound or particle effect here!

        // 4. Destroy the ghost object
        Destroy(gameObject);
    }
}