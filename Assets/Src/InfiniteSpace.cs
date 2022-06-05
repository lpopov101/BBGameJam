using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteSpace : MonoBehaviour
{
    [SerializeField]
    private GameObject _spaceTile;
    [SerializeField]
    private Transform _referenceTransform;
    [SerializeField]
    private float _parallaxFactor;

    private Vector2 _tileSize;
    private Vector2 _initialTilesOrigin;
    private Vector2 _tilesOrigin;
    private List<GameObject> _tilePool;
    private Vector2Int _curTileCoords;

    private void Start()
    {
        var tileSpriteRenderer = _spaceTile.GetComponent<SpriteRenderer>();
        _tileSize = new Vector2(
            tileSpriteRenderer.bounds.size.x,
            tileSpriteRenderer.bounds.size.y
        );
        _initialTilesOrigin = new Vector2(
            _referenceTransform.position.x,
            _referenceTransform.position.y
        );
        _tilesOrigin = _initialTilesOrigin;
        _tilePool = new List<GameObject>();
        for(int i = 0; i < 9; i++)
        {
            var tile = Instantiate(_spaceTile);
            _tilePool.Add(tile);
        }
        _curTileCoords = Vector2Int.zero;
        redrawTiles();
    }

    private void Update()
    {
        _curTileCoords = positionToTileCoords(_referenceTransform.position);
        _tilesOrigin = _initialTilesOrigin +
            ((Vector2)_referenceTransform.position - _initialTilesOrigin)
            * _parallaxFactor;
        redrawTiles();
    }

    private void redrawTiles()
    {
        var tileIdx = 0;
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                var tile = _tilePool[tileIdx];
                ++tileIdx;
                var tileCoords = new Vector2Int(
                    _curTileCoords.x + (i-1),
                    _curTileCoords.y + (j-1)
                );
                tile.transform.position = tileCoordsToPosition(tileCoords);
                tile.transform.rotation = tileCoordsToRotation(tileCoords);
            }
        }
    }

    private Vector2 tileCoordsToPosition(Vector2Int tileCoords)
    {
        return new Vector2(
            _tilesOrigin.x + ((float)tileCoords.x * _tileSize.x),
            _tilesOrigin.y + ((float)tileCoords.y * _tileSize.y)
        );
    }

    private Vector2Int positionToTileCoords(Vector2 position)
    {
        return new Vector2Int(
            Mathf.RoundToInt((position.x-_tilesOrigin.x)/_tileSize.x),
            Mathf.RoundToInt((position.y-_tilesOrigin.y)/_tileSize.y)
        );
    }

    private Quaternion tileCoordsToRotation(Vector2Int tileCoords)
    {
        var state = Random.state;
        Random.InitState((tileCoords.x + tileCoords.y) - tileCoords.y/2);
        var angleMultiplier = Random.Range(0, 4);
        Random.state = state;
        return Quaternion.Euler(0, 0, 90F * angleMultiplier);
    }
}
