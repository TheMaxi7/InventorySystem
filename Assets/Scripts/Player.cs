
using TMPro;
using UnityEngine;


public class Player : MonoBehaviour
{
    // Singleton instance of the Player class
    public static Player Instance;

    public int health = 100;

    public int moveSpeed = 10;

    // UI element to display the player's health
    public TextMeshProUGUI healthValue;

    // UI element to display the player's movement speed
    public TextMeshProUGUI moveSpeedValue;

    // Called when the script instance is being loaded
    void Awake()
    {
        // Initialize the singleton instance
        Instance = this;
    }

    /// <summary>
    /// Method to increase the player's health
    /// </summary>
    /// <param name="amount"></param>
    public void IncreaseHealth(int amount)
    {
        // Add the specified amount to the player's health
        health += amount;

        // Update the health UI element with the new health value
        healthValue.text = health.ToString();
    }

    void Start()
    {
        // Initialize the health and move speed UI elements with the current values
        healthValue.text = health.ToString();
        moveSpeedValue.text = moveSpeed.ToString();
    }

    /// <summary>
    /// Method to increase the player's movement speed
    /// </summary>
    /// <param name="amount"></param>
    public void IncreaseMoveSpeed(int amount)
    {
        // Add the specified amount to the player's move speed
        moveSpeed += amount;

        // Update the move speed UI element with the new move speed value
        moveSpeedValue.text = moveSpeed.ToString();
    }
}