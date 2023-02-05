using UnityEngine;
using UnityEngine.Events;
public class FloatEvent : UnityEvent<float> { }
public class FloatFloatEvent: UnityEvent<float, float> { }
public class UpgradeEvent : UnityEvent<Upgrade> { }
public class ShootEvent : UnityEvent<GameObject, Vector2, Vector2> { }
