﻿using System.Diagnostics;

namespace NetworkDiscovery;

public static class ListViewItemAppender
{
    delegate void VoidResultListViewListViewItemParams(ListView listView, ListViewItem listViewItem);
    public static void AddToList(ListView listView, ListViewItem? listViewItem)
    {
        if (listViewItem == null)
        {
            return;
        }

        try
        {
            if (!listView.InvokeRequired)
            {
                var backColor = listView.Items.Count % 2 == 0 ? listView.BackColor : Color.LightBlue;
                listViewItem.BackColor = backColor;
                listView.Items.Add(listViewItem);
            }
            else
            {
                var addItemToListDelegate = new VoidResultListViewListViewItemParams(AddToList);
                listView.Invoke(addItemToListDelegate, listView, listViewItem);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ListViewItemAppender error: {ex.Message}");
        }
    }
}
