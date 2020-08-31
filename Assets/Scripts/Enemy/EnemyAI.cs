using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    private Enemy enemy = default;

    public void Init()
    {
        enemy = GetComponent<Enemy>();
    }
}
