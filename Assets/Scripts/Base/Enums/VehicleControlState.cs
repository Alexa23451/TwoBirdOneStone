/// <summary>
/// State control of vehicle
/// </summary>
/// <remarks>
/// NONE - disable control and/or camera/UI.
/// DRIVE - enable drive mod and/or carControl camera/UI.
/// LADDER - enable ladder and/or ladder camera/UI.
/// WATER_TURRET - enable water turret and/or ladder camera/UI. Water turret don't used in this moment.
/// </remarks>
public enum VehicleControlState
{   
    NONE,
    DRIVE,
    LADDER,
    TURRET
}