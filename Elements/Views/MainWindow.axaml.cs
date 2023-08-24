using Avalonia.Controls;
using Avalonia.Interactivity;
using Elements.ViewModels;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Drawing;

namespace Elements.Views
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel MainWindowViewModel => DataContext as MainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();
            //AddImage("image1");
            //AddImage("image1");
            var comboBox = this.FindControl<ComboBox>("comboBox");
            comboBox.SelectedIndex = 0;
            var tree = this.FindControl<TreeView>("tree");
            tree.DataContext = MainWindowViewModel;
            //MainWindowViewModel = new MainWindowViewModel();
            //DataContext = MainWindowViewModel;
            //var comboBox = this.FindControl<ComboBox>("comboBox");
            //comboBox.DataContext = mainWindow;
        }

        public void AddImage(string imageStr)
        {
            var image1 = this.FindControl<Avalonia.Controls.Image>(imageStr);
            var imageDock = this.FindControl<DockPanel>("imageDock");
            var image = new Avalonia.Controls.Image();
            image.Source = image1.Source;
            image.Width = image1.Width;
            imageDock.Children.Add(image);
        }

        //public void SelectionChanged(object sender, SelectionChangedEventArgs args)
        //{
        //    if ((sender as ComboBox).DataContext != null)
        //        ((sender as ComboBox).DataContext as TypeOperationViewModel).Lyambda = Convert.ToDouble(((args.AddedItems[0] as ComboBoxItem).Content as TextBlock).Text);
        //    //MainWindowViewModel.
        //    //var json = JsonConvert.SerializeObject(ViewModel.DtConfiguration);
        //    //File.WriteAllText("Models\\DtConfiguration.json", json);
        //}

        //public async void Download_Clicked(object sender, RoutedEventArgs args)
        //{
        //    var fileDialog = new OpenFileDialog();
        //    fileDialog.Filters.Add(new FileDialogFilter() { Name = "Excel", Extensions = { "xlsx" } });
        //    var result = await fileDialog.ShowAsync(this);
        //    if (result != null)
        //    {
        //        (DataContext as MainWindowViewModel).Download(result.FirstOrDefault());
        //    }
        //}

        //public async void Export_Clicked(object sender, RoutedEventArgs args)
        //{
        //    try
        //    {
        //        var fileDialog = new OpenFileDialog();
        //        fileDialog.Filters.Add(new FileDialogFilter() { Name = "Excel", Extensions = { "xlsx" } });
        //        var result = await fileDialog.ShowAsync(this);
        //        if (result != null)
        //        {
        //            var newFile = new FileInfo(result.First());
        //            var Excel_Package = new ExcelPackage(newFile);
        //            var workSheet = Excel_Package.Workbook.Worksheets.First();
        //            var cells = workSheet.Cells;
        //            for (int i = 3; i < Convert.ToInt32(MainWindowViewModel.R) + 3; i++)
        //            {
        //                cells["I" + i.ToString()].Value = MainWindowViewModel.TypeOperations[i - 3].n;
        //                cells["J" + i.ToString()].Value = MainWindowViewModel.TypeOperations[i - 3].P;

        //            }
        //            cells["K3"].Value = MainWindowViewModel.Pop;
        //            cells["L3"].Value = MainWindowViewModel.Pisp;
        //            cells["M3"].Value = MainWindowViewModel.Pd;
        //            Excel_Package.Save();
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}

        public void Date_Clicked(object sender, RoutedEventArgs args)
        {
            var dialog = new DatePicker();
            dialog.ShowDialog(this);
            dialog.DataContext = (sender as TextBox).DataContext;
        }

        public void Add_Clicked(object sender, RoutedEventArgs args)
        {
            try
            {
                var index = this.FindControl<ComboBox>("comboBox").SelectedIndex + 1;
                AddImage("image" + index.ToString());
                MainWindowViewModel.AddElements(index);
                var tree = this.FindControl<TreeView>("tree");
                tree.Items = MainWindowViewModel.Elements;
                //var fileDialog = new OpenFileDialog();
                //fileDialog.Filters.Add(new FileDialogFilter() { Name = "Excel", Extensions = { "xlsx" } });
                //var result = await fileDialog.ShowAsync(this);
                //if (result != null)
                //{
                //    var newFile = new FileInfo(result.First());
                //    var Excel_Package = new ExcelPackage(newFile);
                //    var workSheet = Excel_Package.Workbook.Worksheets.First();
                //    var cells = workSheet.Cells;
                //    for (int i = 3; i < Convert.ToInt32(MainWindowViewModel.R) + 3; i++)
                //    {
                //        cells["I" + i.ToString()].Value = MainWindowViewModel.TypeOperations[i - 3].n;
                //        cells["J" + i.ToString()].Value = MainWindowViewModel.TypeOperations[i - 3].P;

                //    }
                //    cells["K3"].Value = MainWindowViewModel.Pop;
                //    cells["L3"].Value = MainWindowViewModel.Pisp;
                //    cells["M3"].Value = MainWindowViewModel.Pd;
                //    Excel_Package.Save();
                //}
            }
            catch
            {

            }
        }
    }

}
