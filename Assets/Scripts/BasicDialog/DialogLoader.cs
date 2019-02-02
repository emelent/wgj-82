using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;


namespace BasicDialog{
    public class DialogLoader {

        private static DialogLoader _instance;
        public static DialogLoader instance {
            get {
                if(_instance == null){
                    _instance = new DialogLoader();
                }
                return _instance;
            }
        }

        public Dictionary<string, List<Dialog>> characterDialogs { get; protected set; } = new Dictionary<string, List<Dialog>>();

        private DialogLoader(){
        }
        
        public List<Dialog> LoadDialog(string characterName){
            if(characterDialogs.ContainsKey(characterName)){
                return characterDialogs[characterName];
            }
            Debug.Log("Loading dialogs");
            TextAsset json = (TextAsset) AssetDatabase.LoadAssetAtPath("Assets/Data/" + characterName + ".json", typeof(TextAsset));
            List<Dialog> dialogs = JsonConvert.DeserializeObject<List<Dialog>>(json.ToString());
            characterDialogs[characterName] = dialogs;
            return dialogs;
        }
    }

    
}