using AirFishLab.ScrollingList;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// The bank for providing the content for the box to display
// Must be inherit from the class BaseListBank
public class StringListBank : BaseListBank
{

    [SerializeField]
    private GameObject buttonList;
    List<string> _contents = new List<string>();

    private void Start() {
        foreach(
            Button button in this.buttonList
                .GetComponentsInChildren<Button>()
        ) {
            _contents.Add(button.GetComponentInChildren<Text>().text);
        }
    }

    // This function will be invoked by the `CircularScrollingList`
    // when acquiring the content to display
    // The object returned will be converted to the type `object`
    // which will be converted back to its own type in `IntListBox.UpdateDisplayContent()`
    public override object GetListContent(int index)
    {
        return _contents[index];
    }

    public override int GetListLength()
    {
        return _contents.Count;
    }
}