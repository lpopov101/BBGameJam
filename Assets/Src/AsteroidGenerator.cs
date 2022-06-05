using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class AsteroidGenerator : MonoBehaviour
{
    [SerializeField]
    private int _numCorners;
    [SerializeField]
    private float _standardRadius;
    [SerializeField]
    private float _radialVariation;

    private SpriteShapeController _spriteShapeController;

    // Start is called before the first frame update
    void Start()
    {
        _spriteShapeController = GetComponent<SpriteShapeController>();

        var center = transform.position;
        var angleIncrement = 360F/(float)_numCorners;

        _spriteShapeController.spline.Clear();
        for(int i = 0; i < _numCorners; i++)
        {
            var radAngle = Mathf.Deg2Rad * angleIncrement * (float)i;
            var offset = new Vector2(
                Mathf.Cos(radAngle),
                Mathf.Sin(radAngle)
            );
            offset *= Random.Range(
                _standardRadius - _radialVariation,
                _standardRadius + _radialVariation
            );
            _spriteShapeController.spline.InsertPointAt(
                i, offset);
        }
    }
}
