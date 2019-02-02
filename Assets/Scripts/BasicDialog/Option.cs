using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BasicDialog{
        
    [System.Serializable]
    public class Option {
        public string title;
        public string action;
        public object[] parameters;
    }

}