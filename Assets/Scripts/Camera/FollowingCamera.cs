using System;
using UnityEngine;
using Zenject;

public class FollowingCamera : MonoBehaviour
{
    private Player _player;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smooth = 1f;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }

    private void FixedUpdate()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        var nextPosition = Vector3.Lerp(transform.position,
                                                _player.transform.position + _offset, 
                                                Time.fixedDeltaTime * _smooth
                                                );

        transform.position = nextPosition;
    }
}
