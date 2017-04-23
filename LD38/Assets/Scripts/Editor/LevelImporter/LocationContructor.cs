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

        public LocationContructor(Location defaultLocation)
        {
            _locationPrefab = defaultLocation;
        }

        public void GenerateLocation(int width, int height, int[] map)
        {
            _tilesRepository = EditorGUIUtility.Load("Tiles repository.asset") as TilesRepository;
            var location = Object.Instantiate(_locationPrefab.gameObject).GetComponent<Location>();
            location.name = DefaultName;
            location.Init(width, height);
            GenerateGrid(location.Grid, width, height, map);
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
    }
}
