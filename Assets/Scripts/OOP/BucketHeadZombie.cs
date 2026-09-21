using UnityEngine;

// INHERITANCE + POLYMORPHISM
public class BucketHeadZombie : Enemy
{
    public bool bucket = true;

    public override void Serang()
    {
        Debug.Log("BucketHeadZombie menggigit dengan armor ember!");
    }
}