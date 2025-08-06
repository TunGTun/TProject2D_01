using UnityEngine;

public static class BossData
{
    //move
    public static float moveSpeed = 3f;

    //attack
    public static float attackRange = 2f;
    public static Transform attackPos;
    public static float attackSpeed = 1f;
    public static float waitDuration = 0.5f;

    //charge
    public static float chargeDuration = 2f;
    public static Color chargedColor = Color.red;
    //public static Color chargedColor = new Color(255f / 255f, 100f / 255f, 0f / 255f, 1f);

    //heavy jump
    public static float airTimeHeavy = 2f;

    //fast jump
    public static float airTimeFast = 1f;
    public static float newGravity = 4f;

    //throw rock
    public static float throwInterval = 1.0f;
    public static int throwNumber = 3;
}