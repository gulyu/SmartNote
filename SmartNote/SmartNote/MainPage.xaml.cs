﻿using BllSmartNote;
using SmartNoteService.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SmartNote
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ISmartNoteBll smartNoteBll;
        private List<Note> noteList;

        public MainPage()
        {
            this.InitializeComponent();
            this.smartNoteBll = new SmartNoteBll();
            this.noteList = new List<Note>();
            // Some people said, that it can initialize the size of the screen, but i can't see any difference
            // with it, or without it :)
            Size size = new Size { Height = 600, Width = 800 };
            ApplicationView.PreferredLaunchViewSize = size;
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        /// <summary>
        /// Ki és bekapcsolja a kereső sávot
        /// </summary>
        private void openSerachPane_Click(object sender, RoutedEventArgs e)
        {
            searchSplitView.IsPaneOpen = !searchSplitView.IsPaneOpen;
        }

        /// <summary>
        /// Az oldal betöltődése után elsülő esemény kezelő függvénye.
        /// Az adatbázis elment egy tényleges note objektumot, majd onnan kéri vissza.
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Note n = new Note();
            n.CreationDate = DateTime.Today;
            n.ModoficationDate = DateTime.Today;
            n.Text = "BlablablablablaBlablablablablaBlablablablablaBlablablablablaBlablablablablaBlablablablabla";
            n.Title = "BlaBla tétele";
            bool insertResult = smartNoteBll.InsertNote(n);
            if(insertResult)
            {
                this.noteList = smartNoteBll.GetAllNote(new User());
            }
            noteListView.ItemsSource = this.noteList;
        }
    }
}
