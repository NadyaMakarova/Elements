using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Npgsql;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;

namespace Elements.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";

        public List<ElementViewModel> Elements { get; set; }

        public List<(int, List<ElementViewModel>)> SystemElements { get; set; }

        public ICommand CalculationCommand { get; set; }

        public ICommand SaveCommand { get; set; }

        public object? Ptr { get; set; }

        //public object? Pob { get; set; }

        //public object? Pi { get; set; }

        //public double? Pop { get; set; }

        //public double? Pisp { get; set; }

        //public double? Pd { get; set; }

        //object? _r;

        //public object? R
        //{
        //    get
        //    {
        //        return _r;
        //    }
        //    set
        //    {
        //        if (value == _r)
        //            return;
        //        _r = value;
        //        try
        //        {
        //            this.RaisePropertyChanged(nameof(R));
        //            if (Convert.ToInt32(R) > 0)
        //            {
        //                TypeOperations = new List<ElementViewModel>();
        //                for (int i = 1; i <= Convert.ToInt32(R); i++)
        //                {
        //                    TypeOperations.Add(new ElementViewModel("Операция " + i.ToString(), i));
        //                }
        //                this.RaisePropertyChanged(nameof(TypeOperations));
        //            }
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}

        public MainWindowViewModel()
        {
            try
            {
                Elements = new List<ElementViewModel>();
                SystemElements = new List<(int, List<ElementViewModel>)>();
                CalculationCommand = ReactiveCommand.Create(Calculation);
                SaveCommand = ReactiveCommand.Create(Save);
            }
            catch
            {

            }
        }

        public void Save()
        {
            try
            {
                string conn_param = "Server=127.0.0.1;Port=5432;User Id=postgres;Password=20056865;Database=postgres;"; //Например: "Server=127.0.0.1;Port=5432;User Id=postgres;Password=goodforyouatmonth1973;Database=postgres;"
                NpgsqlConnection conn = new NpgsqlConnection(conn_param);
                conn.Open();
                foreach (var it in SystemElements.SelectMany(o => o.Item2))
                {
                    var date = it.Date.Year + "-" + it.Date.Month + "-" + it.Date.Day.ToString();
                    var dateEnd = it.DateEnd.Year + "-" + it.DateEnd.Month + "-" + it.DateEnd.Day.ToString();
                    string sql = "insert into public.\"Elements\" (name,date,\"T\",a,\"P\",t,\"dateEnd\") values('" + it.Name + "','" + date + "'," + it.T.ToString().Replace(",", ".") + "," + it.Lyambda.ToString().Replace(",", ".") + "," + it.P.ToString().Replace(",", ".") + "," + it.Time.ToString().Replace(",", ".") + ",'" + dateEnd + "'" + ");";
                    NpgsqlCommand comm = new NpgsqlCommand(sql, conn);
                    comm.ExecuteScalar();
                }
                //Открываем соединение.

                //result = comm.ExecuteScalar().ToString(); //Выполняем нашу команду.
                conn.Close();
            }
            catch
            {

            }
        }

        public void AddElements(int systemIndex)
        {
            try
            {
                var newList = new List<ElementViewModel>();
                var elements = new List<ElementViewModel>(Elements);
                for (int i = 0; i < systemIndex; i++)
                {
                    var newElement = new ElementViewModel(systemIndex.ToString() + "." + (i + 1).ToString(), Elements.Count + 1);
                    newList.Add(newElement);
                    elements.Add(newElement);
                }
                SystemElements.Add((systemIndex, newList));
                Elements = new List<ElementViewModel>(elements);
                this.RaisePropertyChanged(nameof(Elements));
            }
            catch
            {

            }
        }

        //public void Download(string fileName)
        //{
        //    try
        //    {
        //        var newFile = new FileInfo(fileName);
        //        var Excel_Package = new ExcelPackage(newFile);
        //        var workSheet = Excel_Package.Workbook.Worksheets[0];
        //        var cells = workSheet.Cells;
        //        //foreach (var type in TypeOperations)
        //        //{
        //        //    type.AddData(cells);
        //        //}
        //        Pk = Convert.ToDouble(cells["F3"].Value.ToString().Replace(".", ","));
        //        this.RaisePropertyChanged(nameof(Pk));
        //        Pob = Convert.ToDouble(cells["G3"].Value.ToString().Replace(".", ","));
        //        this.RaisePropertyChanged(nameof(Pob));
        //        Pi = Convert.ToDouble(cells["H3"].Value.ToString().Replace(".", ","));
        //        this.RaisePropertyChanged(nameof(Pi));
        //        this.RaisePropertyChanged(nameof(TypeOperations));

        //    }
        //    catch
        //    {
        //    }
        //}

        public void Calculation()
        {
            try
            {
                foreach (var item in Elements)
                {
                    item.Calculation();
                }
                double count = Convert.ToDouble(Elements.Count);
                double coren = (1 / count);
                var p = Math.Pow(Convert.ToDouble(Convert.ToDouble(Ptr)), coren);
                foreach (var item in SystemElements)
                {
                    switch (item.Item1)
                    {
                        case 1:
                            {
                                item.Item2.First().P = p;
                                break;
                            }
                        case 2:
                            {
                                foreach (var it in item.Item2)
                                {
                                    it.P = (2 - Math.Sqrt(4 - 4 * p)) / 2;
                                }
                                break;
                            }
                        case 3:
                            {
                                item.Item2.First().P = (2 - Math.Sqrt(4 - 4 * p)) / 2;
                                item.Item2[1].P = Math.Sqrt((2 - Math.Sqrt(4 - 4 * p)) / 2);
                                item.Item2[2].P = Math.Sqrt((2 - Math.Sqrt(4 - 4 * p)) / 2);
                                break;
                            }
                        case 4:
                            {
                                foreach (var it in item.Item2)
                                {
                                    it.P = Math.Sqrt((2 - Math.Sqrt(4 - 4 * p)) / 2);
                                }
                                break;
                            }
                    }

                    //foreach (var it in SystemElements.SelectMany(o => o.Item2))
                    //{
                    //    if (dataGridView1.Rows[it].Cells[5].Value != null)
                    //    {
                    //        var hours = -Math.Log(Convert.ToDouble(dataGridView1.Rows[it].Cells[5].Value)) / Convert.ToDouble(dataGridView1.Rows[it].Cells[4].Value) * 10000;
                    //        dataGridView1.Rows[it].Cells[6].Value = hours;
                    //        var date = Convert.ToDateTime(dataGridView1.Rows[it].Cells[2].Value);
                    //        var newDate = date.AddHours(hours);
                    //        dataGridView1.Rows[it].Cells[7].Value = newDate;
                    //        str += "Элемент " + dataGridView1.Rows[it].Cells[0].Value + " нужно заменить:" + newDate.ToString() + "\n";
                    //    }
                    //}
                    //if (!File.Exists("C:\\Users\\Надя\\Desktop\\MakarovaLr2\\отчет.txt"))
                    //    File.Create("C:\\Users\\Надя\\Desktop\\MakarovaLr2\\отчет.txt");
                    //File.WriteAllText("C:\\Users\\Надя\\Desktop\\MakarovaLr2\\отчет.txt", str);
                }
                var str = "";
                foreach (var it in SystemElements.SelectMany(o => o.Item2))
                {
                    var hours = -Math.Log(Convert.ToDouble(it.P)) / Convert.ToDouble(it.Lyambda) * 10000;
                    it.Time = hours;
                    var newDate = it.Date.AddHours(hours);
                    it.DateEnd = newDate;
                    str += "Элемент " + it.Name + " нужно заменить:" + it.DateEnd + "\n";
                }
                File.WriteAllText("C:\\Users\\Дмитрий\\Desktop\\Elements\\Elements\\отчет.txt", str);
            }
            catch
            {

            }
        }

        //public void Calculation()
        //{
        //    try
        //    {
        //        TypeOperations.ForEach(o => o.Calculation());
        //        Pop = 1;
        //        foreach (var oper in TypeOperations)
        //        {
        //            Pop = Pop * Math.Pow(Convert.ToDouble(oper.P), Convert.ToDouble(oper.k));
        //        }
        //        Pisp = Convert.ToDouble(Pk) * Convert.ToDouble(Pob) * Convert.ToDouble(Pi);
        //        Pd = Pop + (1 - Pop) * Pisp;
        //        this.RaisePropertyChanged(nameof(Pop));
        //        this.RaisePropertyChanged(nameof(Pisp));
        //        this.RaisePropertyChanged(nameof(Pd));
        //    }
        //    catch
        //    {

        //    }
        //}

    }
}
