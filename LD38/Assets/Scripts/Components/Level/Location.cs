using System.Linq;
using Assets.Scripts.Components.Enteties.Enemies;
using Assets.Scripts.Core.Staff;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Assets.Scripts.Components.Level
{
    public class Location : BindingMonoBehaviour
    {
        [SerializeField] private Grid _grid;
        [SerializeField] private Grid _enemiesGrid;
        [SerializeField] private int _tileWidth;
        [SerializeField] private int _tileHeight;
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        [SerializeField] private DoorHandler _doorHandler;
        [SerializeField] private int _id;
        [SerializeField] private int _enemiesNumber;

        private bool _block;

        public int EnemiesNumber { get { return _enemiesNumber; } }
        public int Id { get { return _id; } }
        public Grid Grid { get { return _grid; } }
        public Grid EnemiesGrid { get { return _enemiesGrid; } }
        public DoorHandler DoorsHandler { get { return _doorHandler; } }

        public bool Block
        {
            get { return _block; }
            set { _block = value; }
        }

        private float TileWidth { get { return _tileWidth*.03125f; } }
        private float TileHeight { get { return _tileHeight*.03125f; } }

        protected virtual void Awake()
        {
            _doorHandler.Close();
        }

        public void Init(int width, int height)
        {
            _width = width;
            _height = height;
            _grid.Init(width, height);
            _enemiesGrid.Init(width, height);
            _doorHandler.Init(width, height);
        }

        public void DisableEnemies()
        {
            for (var row = 0; row < _height; row++)
                for (var column = 0; column < _width; column++)
                {
                    if (_enemiesGrid.GridData[row].List[column].Transform == null) continue;
                    _enemiesGrid.GridData[row].List[column].Transform.gameObject.SetActive(false);
                }
            _block = true;
        }

        public void Reposit()
        {
            _grid.Reposit(TileWidth, TileHeight);
            _enemiesGrid.Reposit(TileWidth, TileHeight);
            _doorHandler.Reposit(TileWidth, TileHeight);
        }

        protected virtual void Update()
        {
            if (_block) return;
            var _counter = 0;
            for (var row = 0; row < _height; row++)
                for (var column = 0; column < _width; column++)
                {
                    if (_enemiesGrid.GridData[row].List[column].Transform == null) continue;
                    if (_enemiesGrid.GridData[row].List[column].Transform.gameObject.activeSelf) _counter ++;
                }
            if (_enemiesGrid.Cash.Any(x=>x.gameObject.activeSelf)) return;
            if (_counter == 0) _doorHandler.Open();
        }

        public void Activate()
        {
            _doorHandler.Close();
            _enemiesGrid.ResetSelf();
            for (var row = 0; row < _height; row ++)
                for (var column = 0; column < _width; column++)
                {
                    if (_enemiesGrid.GridData[row].List[column].Transform == null) continue;
                    _enemiesGrid.GridData[row].List[column].Transform.GetComponent<EnemyBase>().Activate();
                }
        }

        public void HandleDoor(Door.DoorType type, Transform ship)
        {
            _doorHandler.Doors.Single(x=>x.Type.Equals(type)).HandleEnterShip(ship);
        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof (Location))]
    class LocationEditor : BindingMonoBehaviourEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Activate")) (target as Location).Activate();
        }
    }
#endif
}
