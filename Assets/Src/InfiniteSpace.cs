using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteSpace : MonoBehaviour
{
    [SerializeField]
    private GameObject _spaceTile;
    [SerializeField]
    private Transform _referenceTransform;

    private Vector2 _tileSize;
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
        _tilesOrigin = new Vector2(
            _referenceTransform.position.x,
            _referenceTransform.position.y
        );
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
        var tileCoords = positionToTileCoords(_referenceTransform.position);
        Debug.Log(tileCoords);
        if (tileCoords != _curTileCoords)
        {
            _curTileCoords = tileCoords;
            redrawTiles();
        }
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
        Random.InitState((tileCoords.x + tileCoords.y) - tileCoords.y/2);
        var angleMultiplier = Random.Range(0, 4);
        return Quaternion.Euler(0, 0, 90F * angleMultiplier);
    }
}
