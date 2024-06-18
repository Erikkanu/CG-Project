using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            Debug.Log("Hello, Unity!");
            // Get the active scene
            Scene currentScene = SceneManager.GetActiveScene();

            // Reload the active scene
            SceneManager.LoadScene(currentScene.name);
            
        }
    }
}