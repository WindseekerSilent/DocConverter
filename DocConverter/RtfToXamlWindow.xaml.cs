using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DocConverter
{
    /// <summary>
    /// Interaction logic for RtfToXamlWindow.xaml
    /// </summary>
    public partial class RtfToXamlWindow : Window
    {
        public RtfToXamlWindow()
        {
            InitializeComponent();
        }

        private void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "RichText Files (*.rtf)|*.rtf|All Files (*.*)|*.*";

            if (openFile.ShowDialog() == true)
            {
                TextRange documentTextRange = new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd);
                using (FileStream fs = File.Open(openFile.FileName, FileMode.Open))
                {
                    documentTextRange.Load(fs, DataFormats.Rtf);
                }
            }
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "XAML Files (*.xaml)|*.xaml|RichText Files (*.rtf)|*.rtf|All Files (*.*)|*.*";

            if (saveFile.ShowDialog() == true)
            {
                TextRange documentTextRange = new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd);

                using (FileStream fs = File.Create(saveFile.FileName))
                {
                    string extension = System.IO.Path.GetExtension(saveFile.FileName).ToLower();
                    switch (extension)
                    {
                        case ".rtf":
                            documentTextRange.Save(fs, DataFormats.Rtf);
                            break;
                        case ".xaml":
                            documentTextRange.Save(fs, DataFormats.Xaml);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
