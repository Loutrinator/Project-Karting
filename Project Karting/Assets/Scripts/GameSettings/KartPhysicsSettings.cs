using UnityEngine;
using UnityEngine.Rendering;
using UnityExtendedEditor.ExtendedAttributes;

[CreateAssetMenu(fileName = "KartPhysicsSettings", menuName = "ScriptableObjects/KartPhysicsSettings")]
public class KartPhysicsSettings : ScriptableObject
{
    
    #region Singleton
    public static KartPhysicsSettings instance;
        
    private void OnEnable()
    {
        if (instance == null) instance = this;
    }

    private void OnDisable()
    {
        instance = null;
    }
    #endregion
    #region Kart Stats
    [MinMaxSlider(25f,75f)]
    public Vector2 topSpeed = new Vector2(80f,120f);
    public float acceleration = 20f;
    public float reverseSpeed = 15;
    public float reverseAcceleration = 5f;
    public float braking = 15;

    public float getTopSpeed(float stat)
    {
        return getStat(stat, topSpeed);
    }

    private float getStat(float value, Vector2 range)
    {
        return range.x + (range.y - range.x) * value;
    }
    
    #endregion
    #region Kart physics
    public float steeringSpeed = 80f;
    public float minDriftAngle = 0.182f;
    public float maxDriftAngle = 1.8f;
    public float kartRotationCoeff = 15f;
    public float kartModelRotationCoeff = 15f;
    public float kartRollCoeff = 4f;
    public float kartRotationLerpSpeed = 5f;
    public float kartWheelAngle = 25f;
    public float boostStrength = 1f;
    public float engineBrakeSpeed = 10f;
    public float gravityMultiplier = 10f;
    public float respawnHeight = 2f;
    public float borderVelocityLossPercent = 0.2f;

    #endregion
}
