using UnityEngine;

public class SkillGizmo : BaseChar
{
    [Header("Gizmo Settings")]
    public bool showAttackOne = true;
    public bool showAttackTwo = true;
    public Color attackOneColor = Color.red;
    public Color attackUpColor = Color.green;
    public Color attackDownColor = Color.blue;
    public Color attackTwoColor = Color.yellow;

    private void OnDrawGizmos()
    {
        if (CharCtrl == null || CharCtrl.PointCtrl == null)
            return;

        // --- Attack One ---
        if (showAttackOne)
        {
            // Front
            DrawHitbox(CharCtrl.PointCtrl.AttackPointFront.transform.position,
                new Vector2(SCharStaticData.AttackOneSize[0], SCharStaticData.AttackOneSize[1]),
                0f, attackOneColor);

            // Up
            float upAngle = Mathf.Approximately(CharCtrl.transform.localScale.x, -1) ? -90f : 90f;
            DrawHitbox(CharCtrl.PointCtrl.AttackPointUp.transform.position,
                new Vector2(SCharStaticData.AttackOneSize[0], SCharStaticData.AttackOneSize[1]),
                upAngle, attackUpColor);

            // Down
            float downAngle = Mathf.Approximately(CharCtrl.transform.localScale.x, -1) ? 90f : -90f;
            DrawHitbox(CharCtrl.PointCtrl.AttackPointDown.transform.position,
                new Vector2(SCharStaticData.AttackOneSize[0], SCharStaticData.AttackOneSize[1]),
                downAngle, attackDownColor);
        }

        // --- Attack Two ---
        if (showAttackTwo)
        {
            DrawHitbox(CharCtrl.PointCtrl.AttackTwoPoint.transform.position,
                new Vector2(SCharStaticData.AttackTwoSize[0], SCharStaticData.AttackTwoSize[1]),
                0f, attackTwoColor);
        }
    }

    private void DrawHitbox(Vector2 center, Vector2 size, float angle, Color color)
    {
        Gizmos.color = color;
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(center, Quaternion.Euler(0, 0, angle), Vector3.one);
        Gizmos.matrix = rotationMatrix;
        Gizmos.DrawWireCube(Vector3.zero, size);
    }
}
