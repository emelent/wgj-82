using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace BasicDialog{
  public class StoryElement: MonoBehaviour{
      
      public string characterName;
      public Sprite sprite;

      public Dictionary<string, Dialog> dialogs = new Dictionary<string, Dialog>();
      public Dialog currentDialog;

      void Awake(){
        loadDialogs();
      }

      protected void loadDialogs(){
        var dialogList = DialogLoader.instance.LoadDialog(characterName);
        // set initial dialog to the first one in list
        currentDialog = dialogList[0];

        // setup dialog lookup
        foreach(Dialog d in dialogList){
          d.sprite = sprite;
          d.title = characterName;
          d.owner = this;
          dialogs[d.id] = d;
        }
      }

      public void StartDialog(){
        // open current dialog
        if(currentDialog != null)
          GM.instance.dialogManager.StartDialog(currentDialog);
        else
          Debug.LogWarning("StoryElement::StartDialog() -> currentDialog is null");
      }

      public void OpenDialog(string id){
        // open dialog by id
        print("opening dialog " + id);
        if(dialogs.ContainsKey(id)){
          currentDialog = dialogs[id];
          StartDialog();
        } else {
          print("No dialog with id '" + id + "' found for '" + characterName + "'");
        }
      }

      public void CloseDialog(string id){
        print("closing dialog");
        currentDialog = dialogs[id];
        GM.instance.dialogManager.StopDialog();
      }
  }
    
}