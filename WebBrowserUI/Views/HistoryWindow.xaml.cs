﻿using Microsoft.Win32;
using Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WebBrowserUI
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {

        public static HistoryViewModel ViewModel { get; set; }
        

        public HistoryWindow()
        {
            InitializeComponent();

            ViewModel = new HistoryViewModel();

            DataContext = ViewModel;

        }

        private void historyLView_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            ListView listView = sender as ListView;
            GridView gridView = listView.View as GridView;

            double width = listView.ActualWidth;
            double ratioTitle = 0.3;
            double ratioUrl = 0.5;
            double ratioDate = 0.2;

            gridView.Columns[0].Width = width * ratioTitle;
            gridView.Columns[1].Width = width * ratioUrl;
            gridView.Columns[2].Width = width * ratioDate;

        }

        private void historyLView_SelectionChanged(object sender, RoutedEventArgs e)
        {

            if(historyLView.SelectedItems.Count > 0)
            {
                ViewModel.SelectedHistory = historyLView.SelectedItems.Cast<HistoryWebPage>().ToList();
            }
        }

        private void deleteHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RemoveSelectedHistory();
        }

        protected void OnClosing(object sender, CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void saveHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Xml file|*.xml";
            saveFileDialog.Title = "Save history to xml";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                ViewModel.SaveHistoryToXML(saveFileDialog.FileName);
            }
        }

        private void clearHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ClearHistory();
        }

        private void loadHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Xml file|*.xml";
            openFileDialog.Title = "Save history to xml";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                ViewModel.LoadHistoryFromXML(openFileDialog.FileName);
            }
        }
    }
}
