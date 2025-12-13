using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class GridManager : MonoBehaviour {
    [SerializeField] private int _width, _height;
 
    [SerializeField] private Tile _tilePrefab;
 
    [SerializeField] private Transform _cam;
 
    private Dictionary<Vector2, Tile> _tiles;
    private float _tileSize;
 
    void Start() {
        GenerateGrid();
    }
 
    void GenerateGrid() {
        _tiles = new Dictionary<Vector2, Tile>();
        _tileSize = _tilePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        for (int x = 0; x < _width; x++) {
            for (int y = 0; y < _height; y++) {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x * _tileSize, y * _tileSize), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
 
                bool randomOffset = UnityEngine.Random.value > 0.5f;
                spawnedTile.Init(randomOffset);
                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }
 
        _cam.position = new Vector3(
            (_width * _tileSize) / 2f - _tileSize / 2f,
            (_height * _tileSize) / 2f - _tileSize / 2f,
            -10
        );
    }
 
    public Tile GetTileAtPosition(Vector2 pos) {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
}