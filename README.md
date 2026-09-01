# How to change the selected item image in .NET MAUI ListView?
This example demonstrates how to change the selected item image in .NET MAUI ListView.

**[View document in Syncfusion .NET MAUI Knowledge Base](https://www.syncfusion.com/kb/13080/how-to-change-selected-image-in-net-maui-listview-sflistview)**

## Sample

```xaml
 <ContentPage.Resources>
    <ResourceDictionary>
        <local:IconColorConverter x:Key="IconColorConverter"/>
        <local:SelectionIconConverter x:Key="SelectionIconConverter"/>
        <DataTemplate x:Name="ItemTemplate" x:Key="ItemTemplate" >
            <code>
            . . .
            . . .
            <code>
        </DataTemplate>
    </ResourceDictionary>
</ContentPage.Resources>

<syncfusion:SfListView x:Name="listView"
                    Grid.Row="1"
                    AutoFitMode="None"
                    SelectionGesture="Tap"
                    SelectionMode="Multiple"
                    SelectedItems="{Binding SelectedItems}"
                    SelectionBackground ="#E4E4E4"
                    ItemTemplate="{StaticResource ItemTemplate}"
                    ItemSize="65" ItemSpacing="0,0,0,1"
                    IsStickyHeader="True" ItemsSource="{Binding MusicInfo}">
</syncfusion:SfListView>

C#:

ListView.SelectionChanged += ListView_SelectionChanged;

private void ListView_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
{
    if (ListView.SelectionMode == Syncfusion.Maui.ListView.SelectionMode.Multiple)
    {
        SelectionViewModel.HeaderInfo = ListView.SelectedItems.Count + " item(s) selected";
        for (int i = 0; i < e.AddedItems.Count; i++)
        {
            var item = e.AddedItems[i];
            (item as Musiqnfo).IsSelected = true;
        }
        for (int i = 0; i < e.RemovedItems.Count; i++)
        {
            var item = e.RemovedItems[i];
            (item as Musiqnfo).IsSelected = false;
        }

        if (ListView.SelectedItems.Count == SelectionViewModel.MusicInfo.Count)
            SelectionViewModel.IsAllSelected = true;
        else
            SelectionViewModel.IsAllSelected = false;
    }
}

private void selectAllIconTapped_Tapped()
{
    if (ListView.SelectedItems.Count == SelectionViewModel.MusicInfo.Count)
    {
        this.ListView.SelectedItems.Clear();
    }
    else
    {
        this.ListView.SelectAll();
    }

    UpdateSelectionTempate(null, true);
}

public void UpdateSelectionTempate(ItemLongPressEventArgs args = null, bool tappedSelectAll = false)
{
    if (ListView.SelectedItems.Count > 0 || args != null)
    {
        ListView.SelectionMode = Syncfusion.Maui.ListView.SelectionMode.Multiple;
        if (!tappedSelectAll)
            ListView.SelectedItems.Clear();
        if (args != null)
        {
            var currentItem = args.DataItem as Musiqnfo;
            if (currentItem != null)
            {
                currentItem.IsSelected = true;
                this.ListView.SelectedItems.Add(currentItem);
            }
        }
        SelectionViewModel.HeaderInfo = ListView.SelectedItems.Count + " item(s) selected";

        if (tappedSelectAll)
        {
            if (ListView.SelectedItems.Count == SelectionViewModel.MusicInfo.Count)
            {
                for (int i = 0; i < SelectionViewModel.MusicInfo.Count; i++)
                {
                    var item = SelectionViewModel.MusicInfo[i];
                    (item as Musiqnfo).IsSelected = true;
                }

                SelectionViewModel.IsAllSelected = true;
            }
        }

    }
    else
    {
        SelectionViewModel.HeaderInfo = "0 item(s) selected";
        for (int i = 0; i < SelectionViewModel.MusicInfo.Count; i++)
        {
            var item = SelectionViewModel.MusicInfo[i];
            (item as Musiqnfo).IsSelected = false;
        }
        SelectionViewModel.IsAllSelected = false;
    }
}

public class IconColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((bool)value)
            return Color.FromArgb("#1a75ff");
        else
            return Color.FromArgb("#b3b3b3");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class SelectionIconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((bool)value)
            return "\ue748";
        else
            return "\ue718";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
```
