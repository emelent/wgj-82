using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace BasicDialog{
    public class DialogManager : MonoBehaviour{
        public static DialogManager instance { get; private set; }
        public CanvasGroup dialogBox;
        public Image image;
        public Text title;
        public Text text;

        public Button optionA;
        public Button optionB;
        public Text optionAText;
        public Text optionBText;

        private bool _isShowing = false;
        public bool isShowing { 
            get{
                return _isShowing;
            }

            protected set {
                _isShowing = value;
                dialogBox.alpha = value? 1f: 0f;
                dialogBox.blocksRaycasts = value;
            }
        }

        public Dialog dialog;

        // Start is called before the first frame update
        void Start(){
            if(instance == null)
                instance = this;
            isShowing = false;
        }

        void OnDestroy(){
            instance = null;
        }

        public void StartDialog(Dialog d){
            isShowing = true;
            title.text = d.title;
            image.sprite = d.sprite;
            optionAText.text = d.options[0].title;
            
            optionB.gameObject.SetActive(true);
            if(d.options.Length > 1)
                optionBText.text = d.options[1].title;
            else
                optionB.gameObject.SetActive(false);
            
            dialog = d;

            StopAllCoroutines();
            StartCoroutine(typeText(d.text));
        }

        IEnumerator typeText(string txt){
            text.text = "";
            foreach(var c in txt){
                text.text += c;
                yield return null;
            }
        }
        public void StopDialog(){
            isShowing = false;
            print("Dialog stopped");
            title.text = "";
            text.text = "";
            image.sprite = null;
            optionAText.text = "";
            optionBText.text = "";
        }
        public void SelectOptionA(){
            print("Option A selected");
            var option = dialog.options[0];
            var owner = dialog.owner;
            var action = owner.GetType().GetMethod(option.action);
            action.Invoke(owner, option.parameters);
        }

        public void SelectOptionB(){
            if(dialog.options.Length < 2)
                return;
            print("Opiton B selected");
            var option = dialog.options[1];
            var owner = dialog.owner;
            var action = owner.GetType().GetMethod(option.action);
            action.Invoke(owner, option.parameters);
        }
    }
   
}
