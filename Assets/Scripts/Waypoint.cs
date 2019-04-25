using System.Collections;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    protected float debugDrawRadius = 1.0F;
    public virtual void onDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, debugDrawRadius);
    }
}
