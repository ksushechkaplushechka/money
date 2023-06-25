using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
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
using System.Xml.Linq;
using Newtonsoft.Json;

namespace учет_буджета
{
    public partial class MainWindow : Window
    {
        private List<GetSet> zapisi = new List<GetSet>();
        public List<string> tip = new List<string>();
        private double price;

        public MainWindow()
        {
            InitializeComponent();
            zapisi = SerDeser.JasonOut<List<GetSet>>() ?? new List<GetSet>();
            Tips.ItemsSource = tip;
            LoadDataFromJson();
            Update();
        }

        private void Load()
        {
            Lis.ItemsSource = zapisi;
            Lis.Columns.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var data = datapicker.SelectedDate;

            if (data == null)
            {
                MessageBox.Show("Выберите дату");
                return;
            }

            var date2 = data.Value;
            var note = new GetSet
            {
                datagrid = date2.ToString("dd.MM.yyyy"),
                zapisi = NameP.Text,
                tip = Tips.Text,
                price = priceV.Text,
            };

            zapisi.Add(note);
            SerDeser.JasonIn(zapisi);
            Update();
        }

        private void Update()
        {
            var selectedDate = datapicker.SelectedDate;
            if (selectedDate != null)
            {
                var selectedDateString = selectedDate.Value.ToString("dd.MM.yyyy");
                var selectedNotes = zapisi.Where(n => n.datagrid == selectedDateString).ToList();
                Lis.ItemsSource = selectedNotes;
            }
            else
            {
                Lis.ItemsSource = null;
            }

            Tips.ItemsSource = zapisi.Where(n => n.tip != null).Select(n => n.tip).Distinct().ToList();

            price = zapisi.Sum(n => Convert.ToDouble(n.price));
            Itog.Content = $"{price:F2}";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Lis.SelectedItem != null)
            {
                var selectedNote = (GetSet)Lis.SelectedItem;

                zapisi.Remove(selectedNote);

                string json = JsonConvert.SerializeObject(zapisi);
                File.WriteAllText("C:\\Users\\porfi\\source\\repos\\учет_буджета\\file.json", json);

                Update();
                Load();
            }
        }


        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (Lis.SelectedItem != null)
            {
                var selectedNote = (GetSet)Lis.SelectedItem;
                selectedNote.zapisi = NameP.Text;
                selectedNote.tip = Tips.Text;

                SerDeser.JasonIn(zapisi);

                Update();
            }
        }

        private void datepicker_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void LoadDataFromJson()
        {
            if (File.Exists("C:\\Users\\porfi\\source\\repos\\учет_буджета\\file.json"))
            {
                string json = File.ReadAllText("C:\\Users\\porfi\\source\\repos\\учет_буджета\\file.json");
                if (!string.IsNullOrEmpty(json))
                {
                    zapisi = JsonConvert.DeserializeObject<List<GetSet>>(json);
                }
            }

            Load();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Page newtip = new Page();
            newtip.ShowDialog();

            string newType = newtip.type;

            if (!string.IsNullOrEmpty(newType))
            {
                tip.Add(newType);
                Lis.ItemsSource = null;
                Lis.ItemsSource = tip;
            }
        }
    }
}
