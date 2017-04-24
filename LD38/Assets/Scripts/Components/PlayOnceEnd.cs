﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Core.Staff;
using UnityEngine;

namespace Assets.Scripts.Components
{
    class PlayOnceEnd : BindingMonoBehaviour
    {
        [SerializeField] private string _audio;

        protected virtual void OnDisable()
        {
            AudioController.Play(_audio);
        }
    }
}
