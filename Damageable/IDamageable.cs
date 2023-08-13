using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//If a thing should take damage inherit from this
public interface IDamageable
{
    public void TakeDamage(float damage);
}
