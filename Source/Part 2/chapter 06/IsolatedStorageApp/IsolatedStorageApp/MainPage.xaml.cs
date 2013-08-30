using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO;
using System.IO.IsolatedStorage;


namespace IsolatedStorageApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void txtWrite_Click(object sender, RoutedEventArgs e)
        {
            //Obtain the virtual store for application
            IsolatedStorageFile myStore = IsolatedStorageFile.GetUserStoreForApplication();

            //Create a new folder and call it "FavorFolder"
            myStore.CreateDirectory("FavorFolder");

            //Create a new file and assign a StreamWriter to the store and this new file (myFile.txt)
            //Also take the text contents from the txtWrite control and write it to myFile.txt

            StreamWriter writeFile = new StreamWriter(new IsolatedStorageFileStream("FavorFolder\\myFile.txt", FileMode.OpenOrCreate, myStore));
            writeFile.WriteLine(txtWrite.Text);
            writeFile.Close();
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            //Obtain a virtual store for application
            IsolatedStorageFile myStore = IsolatedStorageFile.GetUserStoreForApplication();

            //This code will open and read the contents of myFile.txt
            //Add exception in case the user attempts to click Read button first.

            StreamReader readFile = null;

            try
            {
                readFile = new StreamReader(new IsolatedStorageFileStream("FavorFolder\\myFile.txt", FileMode.Open, myStore));
                string fileText = readFile.ReadLine();

                //The control txtRead will display the text entered in the file
                txtRead.Text = fileText;
                readFile.Close();
            }
            catch
            {
                txtRead.Text = "Need to create directory and the file first.";
            }
        }
    }
}