using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace BasicDialog{
    
    [System.Serializable]
    public class Dialog {

        public string id;
        
        public StoryElement owner;

        public Sprite sprite;

        public string title;
        [TextArea(3, 10)]
        public string text;
        public Option[] options;
        
    }

    
}