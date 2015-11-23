using Entities;
using SmartNoteService.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SmartNote
{
    public sealed partial class SmartRichTextControl : UserControl
    {
        private Dictionary<int, NoteToNote> LinksList;
        private Note OriginalNote;
        private List<Note> NoteList;
        public SmartRichTextControl()
        {
            this.InitializeComponent();
            this.LinksList = new Dictionary<int, NoteToNote>();
        }

        private void tgbBold_Click(object sender, RoutedEventArgs e)
        {
            this.tgbBold.IsChecked = Bold();
        }

        private void tgbItalic_Click(object sender, RoutedEventArgs e)
        {
            this.tgbItalic.IsChecked = Italic();
        }

        private void tgbUnderline_Click(object sender, RoutedEventArgs e)
        {
            this.tgbUnderline.IsChecked = Underline();
        }

        private void tgbLeft_Click(object sender, RoutedEventArgs e)
        {
            this.tgbLeft.IsChecked = AlignLeft();
            this.tgbCentre.IsChecked = false;
            this.tgbRight.IsChecked = false;
        }

        private void tgbCentre_Click(object sender, RoutedEventArgs e)
        {
            this.tgbLeft.IsChecked = false;
            this.tgbCentre.IsChecked = AlignCentre();
            this.tgbRight.IsChecked = false;
        }

        private void tgbRight_Click(object sender, RoutedEventArgs e)
        {
            this.tgbLeft.IsChecked = false;
            this.tgbCentre.IsChecked = false;
            this.tgbRight.IsChecked = AlignRight();
        }

        private void cbSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.rtbEditor != null)
            {
                string selected = ((ComboBoxItem)this.cbSize.SelectedItem).Tag.ToString();
                this.rtbEditor.Document.Selection.CharacterFormat.Size = float.Parse(selected);
                focus();
            }
        }

        private void cbColour_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.rtbEditor != null)
            {
                string selected = ((ComboBoxItem)this.cbColour.SelectedItem).Tag.ToString();
                Color color = Windows.UI.Color.FromArgb(
                    Byte.Parse(selected.Substring(0, 2), NumberStyles.HexNumber),
                    Byte.Parse(selected.Substring(2, 2), NumberStyles.HexNumber),
                    Byte.Parse(selected.Substring(4, 2), NumberStyles.HexNumber),
                    Byte.Parse(selected.Substring(6, 2), NumberStyles.HexNumber));
                this.rtbEditor.Document.Selection.CharacterFormat.BackgroundColor = color;
                focus();
            }
        }

        private void focus()
        {
            this.rtbEditor.Focus(FocusState.Keyboard);
        }

        public void setText(string value)
        {
            this.rtbEditor.Document.SetText(TextSetOptions.FormatRtf, value);
            focus();
        }

        public string getText()
        {
            string value = string.Empty;
            this.rtbEditor.Document.GetText(TextGetOptions.FormatRtf, out value);
            return value;
        }

        public string getPlainText()
        {
            string value = string.Empty;
            this.rtbEditor.Document.GetText(TextGetOptions.None, out value);
            return value;
        }

        private bool Bold()
        {
            this.rtbEditor.Document.Selection.CharacterFormat.Bold = FormatEffect.Toggle;
            focus();
            return this.rtbEditor.Document.Selection.CharacterFormat.Bold.Equals(FormatEffect.On);
        }

        private bool Italic()
        {
            this.rtbEditor.Document.Selection.CharacterFormat.Italic = FormatEffect.Toggle;
            focus();
            return this.rtbEditor.Document.Selection.CharacterFormat.Italic.Equals(FormatEffect.On);
        }

        private bool Underline()
        {
            this.rtbEditor.Document.Selection.CharacterFormat.Underline = this.rtbEditor.Document.Selection.CharacterFormat.Underline.Equals(UnderlineType.Single) ? UnderlineType.None : UnderlineType.Single;
            focus();
            return this.rtbEditor.Document.Selection.CharacterFormat.Underline.Equals(UnderlineType.Single);
        }

        private bool AlignLeft()
        {
            this.rtbEditor.Document.Selection.ParagraphFormat.Alignment = ParagraphAlignment.Left;
            focus();
            return this.rtbEditor.Document.Selection.ParagraphFormat.Alignment.Equals(ParagraphAlignment.Left);
        }

        private bool AlignCentre()
        {
            this.rtbEditor.Document.Selection.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            focus();
            return this.rtbEditor.Document.Selection.ParagraphFormat.Alignment.Equals(ParagraphAlignment.Center);
        }

        private bool AlignRight()
        {
            this.rtbEditor.Document.Selection.ParagraphFormat.Alignment = ParagraphAlignment.Right;
            focus();
            return this.rtbEditor.Document.Selection.ParagraphFormat.Alignment.Equals(ParagraphAlignment.Right);
        }

        public void setNoteLinksAndOriginalNote(List<Note> notes, Note originalNote)
        {
            this.NoteList = notes;
            this.OriginalNote = originalNote;
            this.cbLinks.ItemsSource = this.NoteList;
            this.LinksList = new Dictionary<int, NoteToNote>();
            if(originalNote != null)
            {
                foreach (var item in originalNote.Links)
                {
                    this.LinksList.Add(item.ReferenceNoteId, item);
                    foreach (var note in this.NoteList)
                    {
                        if(item.ReferenceNoteId == note.Id)
                        {
                            note.Checked = true;
                        }
                    }
                }
            }
        }

        public Dictionary<int, NoteToNote> getLinkList()
        {
            Dictionary<int, NoteToNote> result = new Dictionary<int, NoteToNote>();
            bool needToAdd = true;
            foreach (var newItems in this.LinksList)
            {
                needToAdd = true;
                foreach (var item in OriginalNote.Links)
                {
                    if(item.ReferenceNoteId == newItems.Value.ReferenceNoteId)
                    {
                        needToAdd = false;
                        break;
                    }
                }
                if(needToAdd)
                {
                    result.Add(newItems.Key, newItems.Value);
                }
            }
            return result;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var cb = (CheckBox)sender;
            if(cb != null && cb.Tag != null)
            {
                var noteId = Int32.Parse(cb.Tag.ToString());
                Note referenceNote = null;
                foreach (var item in this.NoteList)
                {
                    if (item.Id == noteId)
                    {
                        referenceNote = item;
                        break;
                    }
                }

                NoteToNote n = new NoteToNote
                {
                    OriginalNoteId = OriginalNote.Id,
                    ReferenceNoteId = referenceNote.Id,
                    OriginalNote = OriginalNote,
                    ReferenceNote = referenceNote
                };
                this.LinksList.Add(noteId, n);
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var cb = (CheckBox)sender;
            var noteId = Int32.Parse(cb.Tag.ToString());
            this.LinksList.Remove(noteId);
        }
    }
}
