using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public int health = 100;
    public int moveSpeed = 10;
    public TextMeshProUGUI healthValue;
    public TextMeshProUGUI moveSpeedValue;

    void Awake()
    {
        Instance = this;
    }
    public void IncreaseHealth(int amount)
    {
        health += amount;
        healthValue.text = health + "";
    }
    void Start()
    {
        healthValue.text = health + "";
        moveSpeedValue.text = moveSpeed + "";
    }
    public void IncreaseMoveSpeed(int amount)
    {
        moveSpeed += amount;
        moveSpeedValue.text = moveSpeed + "";
    }
}
