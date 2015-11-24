using BllSmartNote;
using Entities;
using SmartNoteService.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Text;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
        private int sortBy;

        public MainPage()
        {
            this.InitializeComponent();
            this.smartNoteBll = new SmartNoteBll();
            this.noteList = new List<Note>();
            this.sortBy = 0;
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

                    //Szerkesztés
                    this.smtcEditor.setText(selected.Text);
                    this.smtcEditor.setNoteLinksAndOriginalNote(this.noteList, selected);
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
                    this.cbPriority.SelectedIndex = selected.Priority;

                    //Olvasás
                    this.tbReadTitle.Text = selected.Title;

                    if (selected.Attachments != null && selected.Attachments.Count > 0)
                    {
                        this.attachmentListView.ItemsSource = this.selectedNote.Attachments;
                    }
                    else
                    {
                        this.attachmentListView.ItemsSource = null;
                    }

                    if (selected.Links != null && selected.Links.Count > 0)
                    {
                        this.noteLinkListView.ItemsSource = this.selectedNote.Links;
                    }
                    else
                    {
                        this.noteLinkListView.ItemsSource = null;
                    }


                    this.rtbRead.IsReadOnly = false;
                    this.rtbRead.Document.SetText(TextSetOptions.FormatRtf, selected.Text);
                    this.rtbRead.IsReadOnly = true;
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
                        this.noteList = smartNoteBll.GetAllNote(new User(), this.sortBy);
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
                // Hogy az új notehoz ne adjuk hozzá az előzőleg feltöltött csatolmányokat
                this.noteFileList = null;
            }

            this.selectedNote.ModoficationDate = DateTime.Now;
            this.selectedNote.Text = this.smtcEditor.getText();
            this.selectedNote.PlainText = this.smtcEditor.getPlainText();
            this.selectedNote.Title = this.tbEditTitle.Text;
            List<NoteToNote> linkList = new List<NoteToNote>();
            foreach (var item in this.smtcEditor.getLinkList())
            {
                linkList.Add(item.Value);
            }
            this.selectedNote.Links = linkList;

            int priority;
            if (((ComboBoxItem)this.cbPriority.SelectedValue).Content.ToString() == "Magas")
            {
                priority = 0;
            }
            else if (((ComboBoxItem)this.cbPriority.SelectedValue).Content.ToString() == "Közepes")
            {
                priority = 1;
            }
            else
            {
                priority = 2;
            }
            this.selectedNote.Priority = priority;

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
                MessageDialog msg = new MessageDialog("Hiba történt a mentés során!");
                await msg.ShowAsync();
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

            //Üresre inicializál itt most, Szerkesztés
            this.smtcEditor.setText("");
            this.tbEditTitle.Text = "";
            this.tbPickedFiles.Text = "";

            //Olvasás
            this.tbReadTitle.Text = "";
            this.attachmentListView.ItemsSource = null;

            this.rtbRead.IsReadOnly = false;
            this.rtbRead.Document.SetText(TextSetOptions.FormatRtf, "");
            this.rtbRead.IsReadOnly = true;

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
                    this.noteList = smartNoteBll.GetAllNote(new User(), this.sortBy);
                    this.noteListView.ItemsSource = this.noteList;
                }
            }
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            int priority;
            if (((ComboBoxItem)this.priority.SelectedValue).Content.ToString() == "Magas")
            {
                priority = 0;
            }
            else if (((ComboBoxItem)this.priority.SelectedValue).Content.ToString() == "Közepes")
            {
                priority = 1;
            }
            else
            {
                priority = 2;
            }

            this.noteList = this.smartNoteBll.GetNotesByParams(new User(), this.title.Text, this.content.Text, this.creationDate.Date.DateTime, this.modifyDate.Date.DateTime, priority, this.hasFile.IsChecked, this.byTitle.IsChecked, this.byCreationDate.IsChecked, this.byModifyDate.IsChecked, this.byPriority.IsChecked, this.byContent.IsChecked);
            this.noteListView.ItemsSource = this.noteList;

            this.selectedNote = null;
        }

        private void sortNote_click(object sender, RoutedEventArgs e)
        {
            this.sortBy++;
            if (this.sortBy == 3)
            {
                this.sortBy = 0;
            }
            this.noteList = smartNoteBll.GetAllNote(new User(), sortBy);
            this.noteListView.ItemsSource = this.noteList;
        }

        

        private void attachmentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView attachmentListView = sender as ListView;

            if (attachmentListView != null)
            {
                Attachment selected = attachmentListView.SelectedItem as Attachment;

                if (selected != null)
                {
                    this.smartNoteBll.OpenInAnotherApp(selected.Content, selected.Name);
                }
            }
        }

        private void noteLinkListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView noteLinkListView = sender as ListView;

            if (noteLinkListView != null)
            {
                NoteToNote selected = noteLinkListView.SelectedItem as NoteToNote;
                
                if (selected != null)
                {
                    this.selectedNote = selected.ReferenceNote;
                    //Olvasás
                    this.tbReadTitle.Text = selectedNote.Title;

                    if (selectedNote.Attachments != null && selectedNote.Attachments.Count > 0)
                    {
                        this.attachmentListView.ItemsSource = this.selectedNote.Attachments;
                    }
                    else
                    {
                        this.attachmentListView.ItemsSource = null;
                    }

                    if (selectedNote.Links != null && selectedNote.Links.Count > 0)
                    {
                        this.noteLinkListView.ItemsSource = this.selectedNote.Links;
                    }
                    else
                    {
                        this.noteLinkListView.ItemsSource = null;
                    }


                    this.rtbRead.IsReadOnly = false;
                    this.rtbRead.Document.SetText(TextSetOptions.FormatRtf, selectedNote.Text);
                    this.rtbRead.IsReadOnly = true;

                    this.pTabs.SelectedIndex = 2;
                }
            }
        }
    }
}
