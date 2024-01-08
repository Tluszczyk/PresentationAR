using AirFishLab.ScrollingList;

// The bank for providing the content for the box to display
// Must be inherit from the class BaseListBank
public class IntListBank : BaseListBank
{
    private readonly int[] _contents = {
        1, 2, 3, 4, 5, 6, 7, 8, 9, 10
    };

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
        return _contents.Length;
    }
}