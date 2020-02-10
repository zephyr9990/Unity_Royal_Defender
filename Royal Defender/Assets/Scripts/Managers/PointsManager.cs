//Senior Desgin Project Points Manager
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
    public static int points;
  
    public Text pointsText;

    void Awake()
    {
        pointsText = GetComponent <Text> ();
        points = 0;
    }

    public void IncreasePoints(int monsterPoints)
    {
        points += monsterPoints;
        UpdateTextField();
    }

    public void DecreasePoints(int amount)
    {
        points -= amount;
        UpdateTextField();
    }

    private void UpdateTextField()
    {
        pointsText.text = points.ToString();
    }

}
