using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Staff;
using UnityEngine;

namespace Assets.Scripts.Components.Level
{
    class DoorHandler : BindingMonoBehaviour
    {
        [SerializeField] private List<Door> _doors;

        public List<Door> Doors { get { return _doors; } }

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
