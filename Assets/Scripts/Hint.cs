using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hint : CollectableItem
{
    public TextMeshProUGUI text;
    
    [TextArea()]
    public string hint;

    private void Awake() {
        text.text = "";
    }
    public override void ActivateEffect()
    {
        text.text = hint;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == targetTag)
            text.text = "";
    }
}
