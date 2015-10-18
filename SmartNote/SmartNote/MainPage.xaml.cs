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

        public List<MockedNotes> mcList;

        public MainPage()
        {
            this.InitializeComponent();
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
        /// Az oldal betöltődése után elsülő esemény kezelő függvénye. Egyenlőre
        /// mockolt objektumok beállítására használom, később lehet nem is kell majd.
        /// Kivételkor figyelni kell, hogy a xaml-ben lévő "Loaded" attribútumát
        /// ki kell venni a page tag-nek.
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.mcList = new List<MockedNotes>();
            MockedNotes mn = new MockedNotes();
            MockedNotes mn1 = new MockedNotes();
            MockedNotes mn2 = new MockedNotes();

            mn.creationDate = "2014.07.08.";
            mn.modifyDate = "2015.02.03.";
            mn.name = "Ez egy jegyzet az angoil történelemről";
            mn.isChecked = true;

            mn1.creationDate = "2015.01.02.";
            mn1.modifyDate = "2015.09.06.";
            mn1.name = "Bináris futtatható objektumok visszafejtése";
            mn1.isChecked = true;

            mn2.creationDate = "2015.06.05.";
            mn2.modifyDate = "2015.10.18.";
            mn2.name = "Matematika struktúrák alkalmazása";
            mn2.isChecked = true;

            mcList.Add(mn);
            mcList.Add(mn1);
            mcList.Add(mn2);

            noteListView.ItemsSource = this.mcList;
        }
    }

    /// <summary>
    /// Mockolt Note osztály, később biztosan törölni kell.
    /// Most az adatbázist helyettesíti.
    /// </summary>
    public class MockedNotes
    {
        public String name { get; set; }
        public String creationDate { get; set; }
        public String modifyDate { get; set; }
        public Boolean isChecked { get; set; }
    }
}
