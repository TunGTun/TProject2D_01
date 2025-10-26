using UnityEngine;

public static class SCharStaticData
{
    public static readonly float GravityScale = 2.5f;
    public static readonly float BufferWindow = 0.2f;

    //Stat
    public static readonly int MaxMP_MPSlot = 100;

    //Move
    public static readonly float MoveSpeed = 4f;

    //Jump
    public static readonly float JumpForce = 13f;

    //DoubleJump
    public static readonly float DoubleJumpForce = 8f;

    //Attack
    public static readonly float AttackDuration = 0.5f;
        //Hitbox
        public static readonly float[] AttackOneSize = { 2.2f, 0.8f };
        public static readonly float[] AttackOnePos = { 1.4f, -0.08f - 0.06f };
        public static readonly float[] AttackTwoSize = { 2.4f, 1.2f };
        public static readonly float[] AttackTwoPos = { 1.5f, 0.12f - 0.06f };

    //Dash
    public static readonly float DashDuration = 0.1f;
    public static readonly float DashForce = 30f;

    //Combat
    public static readonly float HurtTime = 0.25f;
    public static readonly float HealDuration = 2f;
    public static readonly int HealHP = 2;
    public static readonly int HealMP = 100;
    public static readonly int AttackNeedToHeal = 10; // HealMP chia het cho AttackNeedToHeal

    //VoidRify
    public static readonly float RiftOffset = 0.5f;
    public static readonly float RiftExtraOffset = 3.0f;
    public static readonly float RiftDespawnTime = 3.0f;
}
