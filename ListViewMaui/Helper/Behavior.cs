﻿using Syncfusion.Maui.ListView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListViewMaui
{
    #region SelectionBehavior
    public class SfListViewSelectionBehavior : Behavior<ContentPage>
    {
        #region Fields

        private Syncfusion.Maui.ListView.SfListView ListView;
        private Grid selectAllIconParent;
        private ListViewSelectionViewModel SelectionViewModel;
        private Grid headerGrid;

        #endregion

        #region Overrides
        protected override void OnAttachedTo(ContentPage bindable)
        {
            ListView = bindable.FindByName<Syncfusion.Maui.ListView.SfListView>("listView");
            ListView.SelectionChanged += ListView_SelectionChanged;

            SelectionViewModel = new ListViewSelectionViewModel();
            ListView.BindingContext = SelectionViewModel;
            ListView.ItemsSource = SelectionViewModel.MusicInfo;

            headerGrid = bindable.FindByName<Grid>("headerGrid");
            headerGrid.BindingContext = SelectionViewModel;

            selectAllIconParent = bindable.FindByName<Grid>("selectAllIconParent");
            var selectAllIconTapped = new TapGestureRecognizer() { Command = new Command(selectAllIconTapped_Tapped) };
            selectAllIconParent.GestureRecognizers.Add(selectAllIconTapped);
            base.OnAttachedTo(bindable);

        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            ListView.SelectionChanged -= ListView_SelectionChanged;
            ListView = null;
            selectAllIconParent = null;
            SelectionViewModel = null;
            headerGrid = null;
            base.OnDetachingFrom(bindable);
        }

        #endregion

        #region Events
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

        #endregion

        #region Methods
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

        #endregion

    }

    #endregion

}
