using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Staff;
using UnityEngine;

namespace Assets.Scripts.Components.Level
{
    public class DoorHandler : Grid
    {
        [SerializeField] private List<Door> _doors;

        public List<Door> Doors { get { return _doors; } }

        public override void AddTile(GameObject tile, int row, int column)
        {
            _doors.Add(tile.GetComponent<Door>());
            base.AddTile(tile, row, column);
        }

        public void Close()
        {
            _doors.ForEach(x=>x.Close());
        }

        public void Open()
        {
            _doors.ForEach(x=>x.Open());
        }
    }
}
