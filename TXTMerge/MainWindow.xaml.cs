using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Forms;

namespace TXTMerge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder outputString = new StringBuilder();
            string line;

            string[] fileEntries = Directory.GetFiles(Origin.Text, "*.csv");
            
            foreach (var file in fileEntries)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(file, Encoding.Default))
                    {

                        while ((line = sr.ReadLine()) != null)
                        {
                            outputString.AppendLine(line);
                        }
                        Console.WriteLine(outputString);
                    }
                }
                catch(Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }

            using(StreamWriter sr = new StreamWriter(Destination.Text, false))
            {
                try
                {
                sr.Write(outputString);
                }
                catch(Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }
        }

        private void OriginButton_Click(object sender, RoutedEventArgs e)
        {
            using(var folderDialog = new FolderBrowserDialog())
            {
                DialogResult dialogResult = folderDialog.ShowDialog();

                if (dialogResult == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    Origin.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void DestinationButton_Click(object sender, RoutedEventArgs e)
        {
            using(var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";
                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Destination.Text = saveFileDialog.FileName;
                }
            }
        }
    }
}
