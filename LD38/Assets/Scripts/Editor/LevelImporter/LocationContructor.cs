using Assets.Scripts.Components.Level;
using Assets.Scripts.Editor.LevelImporter.TileDescription;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor.LevelImporter
{
    public class LocationContructor
    {
        private const string DefaultName = "Location default name";
        private readonly Location _locationPrefab;
        private TilesRepository _tilesRepository;
        private TilesRepository _enemiesRepository;

        public LocationContructor(Location defaultLocation)
        {
            _locationPrefab = defaultLocation;
        }

        public void GenerateLocation(int width, int height, int[] map, int[] enemies)
        {
            _tilesRepository = EditorGUIUtility.Load("Tiles repository.asset") as TilesRepository;
            _enemiesRepository = EditorGUIUtility.Load("Enemies repository.asset") as TilesRepository;
            var location = Object.Instantiate(_locationPrefab.gameObject).GetComponent<Location>();
            location.name = DefaultName;
            location.Init(width, height);
            GenerateGrid(location.Grid, width, height, map);
            GenerateEnemies(location, width, height, enemies);
            location.Reposit();
        }

        private void GenerateGrid(Grid grid, int width, int height, int[] map)
        {
            for (var row = 0; row < height; row ++)
                for (var column = 0; column < width; column++)
                {
                    var index = row*width + column;
                    var id = map[index];
                    if (id == 0) continue;
                    var tile = Object.Instantiate(_tilesRepository.GetTile(id));
                    grid.AddTile(tile.gameObject, row, column);
                }
        }


        private void GenerateEnemies(Location location, int width, int height, int[] enemies)
        {
            for (var index = 0; index < enemies.Length; index++)
            {
                if (enemies[index] ==0 ) continue;
                var row = index/width;
                var column = index%width;
                Debug.Log(_enemiesRepository.GetTile(enemies[index]));
                var enemy = Object.Instantiate(_enemiesRepository.GetTile(enemies[index]));
                location.EnemiesGrid.AddTile(enemy.gameObject, row, column);
            }
        }
    }
}
