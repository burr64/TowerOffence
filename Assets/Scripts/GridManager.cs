using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Transform _cam;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateGrid(){
        for (int x = 0; x < _width; x++){
            for (int y = 0; y < _height; y++){
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x,y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
            }
        }

        //_cam.transform.position
    }
}
