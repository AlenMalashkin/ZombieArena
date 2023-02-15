using ModestTree;
using UnityEngine;

public class Eqipment : MonoBehaviour
{
    [SerializeField] private Transform[] _gunInitPositions;
    [SerializeField] private WeaponAbstract[] _weapons;

    private void Awake()
    {
        if (!_weapons.IsEmpty())
        {
            for (int i = 0; i < _weapons.Length; i++)
            {
                var gun = Instantiate(_weapons[i], _gunInitPositions[i], true);
            }
        }
    }
}
