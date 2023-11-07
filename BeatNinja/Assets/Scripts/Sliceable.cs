using UnityEngine;

public abstract class Sliceable : MonoBehaviour
{
    public bool IsSliced = false;

    public SpawnEvent EventOrigin;

    public GameObject WholeObject;
    public GameObject SlicedObject;
    public ParticleSystem SliceEffect;

    /// <summary>
    /// Slices the object
    /// </summary>
    /// <param name="dir">Normalized angle of the slicing operation</param>
    /// <returns>Points for slicing the object</returns>
    public abstract float Slice(Vector2 dir);

    protected void SanityCheck()
    {
        if (WholeObject == null) throw new UnassignedReferenceException();
        if (SlicedObject == null) throw new UnassignedReferenceException();
        if (SliceEffect == null) throw new UnassignedReferenceException();
    }
}
