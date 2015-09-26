using SqliteTest.DAL;
using SqliteTest.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SqliteTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new NoteContext())
            {
                notes.ItemsSource = db.notes.ToList();
            }
        }

        private void addNote_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new NoteContext())
            {
                var note = new Note { text = noteText.Text, created = DateTime.Now };
                db.notes.Add(note);
                db.SaveChanges();

                notes.ItemsSource = db.notes.ToList();
            }
        }
    }
}
