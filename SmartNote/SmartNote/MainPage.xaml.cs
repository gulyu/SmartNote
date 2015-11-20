using BllSmartNote;
using SmartNoteService.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Text;
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
        private List<StorageFile> noteFileList;
        private Note selectedNote;

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
            //Note n = new Note();
            //n.CreationDate = DateTime.Today;
            //n.ModoficationDate = DateTime.Today;
            //n.Text = "BlablablablablaBlablablablablaBlablablablablaBlablablablablaBlablablablablaBlablablablabla";
            //n.Title = "BlaBla tétele";
            //bool insertResult = smartNoteBll.InsertNote(n);
            //if(insertResult)
            //{
                //this.noteList = smartNoteBll.GetAllNote(new User());
            //}
            //this.noteListView.ItemsSource = this.noteList;
        }

        private async void btnFilePicker_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            openPicker.FileTypeFilter.Add("*");
            IReadOnlyList<StorageFile> files = await openPicker.PickMultipleFilesAsync();
            if (files != null && files.Count > 0)
            {
                string output = "";

                foreach (StorageFile file in files)
                {
                    output += file.Name + Environment.NewLine;
                }
                tbPickedFiles.Text = output;

                if (this.noteFileList != null)
                {
                    this.noteFileList.AddRange(files);
                }
                else
                {
                    this.noteFileList = new List<StorageFile>();
                    this.noteFileList.AddRange(files);
                }
            }
            else
            {
                tbPickedFiles.Text = "";
            }
        }

        private void noteListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView noteListView = sender as ListView;

            if (noteListView != null)
            {
                Note selected = noteListView.SelectedItem as Note;

                //Bele tesszük ha null, ha nem mert lehet, hogy induláskor nincs kiválasztva semmi
                this.selectedNote = selected;
                if (selected != null)
                {
                    this.piSzerkesztes.IsEnabled = true;
                    this.piOlvasas.IsEnabled = true;

                    this.smtcEditor.setText(selected.Text);
                    this.tbEditTitle.Text = selected.Title;

                    string fileNames = "";

                    if (selected.Attachments != null)
                    {
                        foreach (var file in selected.Attachments)
                        {
                            fileNames += file.Name + Environment.NewLine;
                        }
                    }

                    this.tbPickedFiles.Text = fileNames;
                    this.runReaderText.Text = selected.Text;
                }
                else
                {
                    this.piSzerkesztes.IsEnabled = false;
                    this.piOlvasas.IsEnabled = false;
                }
            }
        }

        private void pTabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Pivot pivot = sender as Pivot;

            if (pivot != null)
            {
                PivotItem selectedPivotItem = pivot.SelectedItem as PivotItem;

                if (selectedPivotItem != null)
                {
                    if (selectedPivotItem.TabIndex == 0)
                    {
                        this.noteList = smartNoteBll.GetAllNote(new User());
                        this.noteListView.ItemsSource = this.noteList;
                    }    
                }
            }
        }

        private async void btnSaveNote_Click(object sender, RoutedEventArgs e)
        {
            //Csatolmányok feldolgozása
            List<Attachment> attachmentList = new List<Attachment>();
            if (this.noteFileList != null)
            {
                foreach (StorageFile file in this.noteFileList)
                {
                    Attachment attachment = new Attachment();
                    attachment.Name = file.Name;
                    attachment.Extension = file.FileType;
                    attachment.Size = (await file.GetBasicPropertiesAsync()).Size;
                    attachment.Content = await ReadFile(file);

                    attachmentList.Add(attachment);
                }
                this.selectedNote.Attachments = attachmentList;
            }

            this.selectedNote.ModoficationDate = DateTime.Now;
            this.selectedNote.Text = this.smtcEditor.getText();
            this.selectedNote.Title = this.tbEditTitle.Text;

            bool res;
            if (this.selectedNote.Id == 0)
            {
                res = this.smartNoteBll.InsertNote(this.selectedNote);
            }
            else
            {
                res = this.smartNoteBll.UpdateNote(this.selectedNote);
            }

            if (res)
            {
                this.pTabs.SelectedIndex = 0;
            }
            else
            {
                //Hibaüzenet
            }
        }

        public async Task<byte[]> ReadFile(StorageFile file)
        {
            byte[] fileBytes = null;
            using (IRandomAccessStreamWithContentType stream = await file.OpenReadAsync())
            {
                fileBytes = new byte[stream.Size];
                using (DataReader reader = new DataReader(stream))
                {
                    await reader.LoadAsync((uint)stream.Size);
                    reader.ReadBytes(fileBytes);
                }
            }
            return fileBytes;
        }

        private void addNewNote_Click(object sender, RoutedEventArgs e)
        {
            Note newNote = new Note();
            newNote.CreationDate = DateTime.Now;

            //TODO: User kezelés
            //newNote.Author = User...

            //this.noteList.Add(newNote);
            this.selectedNote = newNote;

            //Üresre inicializál itt most
            this.smtcEditor.setText("");
            this.tbEditTitle.Text = "";
            this.tbPickedFiles.Text = "";
            this.runReaderText.Text = "";

            this.piSzerkesztes.IsEnabled = true;
            this.piOlvasas.IsEnabled = true;
            this.pTabs.SelectedIndex = 1;
        }

        private void deleteNote_Click(object sender, RoutedEventArgs e)
        {
            if (this.selectedNote != null)
            {
                bool res = this.smartNoteBll.DeleteNote(this.selectedNote);

                if (res)
                {
                    this.selectedNote = null;
                    this.noteList = smartNoteBll.GetAllNote(new User());
                    this.noteListView.ItemsSource = this.noteList;
                }
            }
        }
    }
}
