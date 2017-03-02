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
using Assignment.ViewModelClasses;

namespace Assignment
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

        #region Customers
        private void Button_Customers_Click(object sender, RoutedEventArgs e)
        {
            tabItem_Customers.IsSelected = true;
            UpdateDatagridCustomer();
            UpdateCustmoersCities();
        }

        private void textBox_CustomerFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDatagridCustomer();
        }

        private void textBox_CustomerFamily_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDatagridCustomer();
        }

        private void textBox_CustomerEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDatagridCustomer();
        }

        private void UpdateDatagridCustomer()
        {
            DataGrid_Customers.DataContext = null;
            DataGrid_Customers.DataContext = DataLayer.SearchCustomers(textBox_CustomerFirstName.Text, textBox_CustomerFamily.Text, textBox_CustomerEmail.Text);
            try { DataGrid_Customers.SelectedIndex = 0; }
            catch { }
        }

        private void UpdateCustmoersCities()
        {
            DataGrid_CustomersCities.DataContext = null;
            DataGrid_CustomersCities.DataContext = DataLayer.GetCustomersCities();
            try { DataGrid_CustomersCities.SelectedIndex = 0; }
            catch { }
        }

        private void DataGrid_Customers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid_Customers.SelectedItem != null)
            {
                Point location = (DataGrid_Customers.SelectedItem as PersonPhoneAddress).Location;
                // Rectangle rectangle = new Rectangle { Width = 2, Height = 2, Fill = Brushes.Red };
                Ellipse elipse = new Ellipse();
                elipse.Width = 8;
                elipse.Height = 8;
                elipse.Fill = Brushes.Red;
                elipse.Stroke = Brushes.Black;
                Canves_CustomerLocation.Children.Clear();
                Canves_CustomerLocation.Children.Add(elipse);
                elipse.SetValue(Canvas.LeftProperty, location.X);
                elipse.SetValue(Canvas.TopProperty, location.Y);
            }
            else
                Canves_CustomerLocation.Children.Clear();
        }

        private void DataGrid_CustomersCities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid_CustomersCities.SelectedItem != null)
            {
                string city = (DataGrid_CustomersCities.SelectedItem as CustomersCities)._City;
                Grid_MinMax.DataContext = null;
                Grid_MinMax.DataContext = DataLayer.GetCityDOB(city);
            }
        }

        #endregion

        #region Items
        private void button_Items_Click(object sender, RoutedEventArgs e)
        {
            tabItem_Items.IsSelected = true;
            UpdateDataGridItems();
            UpdateDataGridItemsStoresSold();
            UpdateDataGridRunningOut();
            UpdateBestSellerItemBrand();
        }

        private void UpdateDataGridItems()
        {
            DataGrid_Items.DataContext = null;
            DataGrid_Items.DataContext = DataLayer.SearchItems(textBox_ItemName.Text, textBox_ItemBrand.Text);
            try { DataGrid_Items.SelectedIndex = 0; }
            catch { }
        }

        private void UpdateDataGridItemsStoresSold()
        {
            DataGrid_ItemsStoresSold.DataContext = null;
            DataGrid_ItemsStoresSold.DataContext = DataLayer.GetItemsStoresSold();
        }

        private void UpdateDataGridRunningOut()
        {
            DataGrid_ItemsStoresRunnigOut.DataContext = null;
            DataGrid_ItemsStoresRunnigOut.DataContext = DataLayer.FindRunnuingOutItems();
        }

        private void UpdateBestSellerItemBrand()
        {
            StackPanel_BestSeller.DataContext = StackPanel_MostPopular.DataContext = null;

            StackPanel_BestSeller.DataContext = DataLayer.GetBestSeller();
            StackPanel_MostPopular.DataContext = DataLayer.GetMostPopular();
        }

        private void textBox_ItemName_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDataGridItems();
        }

        private void textBox_ItemBrand_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDataGridItems();
        }
        #endregion //Items

        #region Transactions
        private void button_Transactions_Click(object sender, RoutedEventArgs e)
        {
            tabItem_Transactions.IsSelected = true;
            UpdateDataGridtransactions();
            UpdateDataGridTransactionsCities();
        }

        private void UpdateDataGridtransactions()
        {
            DataGrid_Transactions.DataContext = null;
            DataGrid_Transactions.DataContext = DataLayer.SearchTransactions(textBox_TPersonFamily.Text, textBox_PaymentMethod.Text);
            try { DataGrid_Transactions.SelectedIndex = 0; }
            catch { }
        }

        private void UpdateDataGridTransactionsCities()
        {
            DataGrid_TransactionsCity.DataContext = null;
            DataGrid_TransactionsCity.DataContext = DataLayer.TransactionsCities();
        }

        private void textBox_PaymentMethod_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDataGridtransactions();
        }

        private void textBox_TPersonFamily_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDataGridtransactions();
        }

        private void DataGrid_Transactions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Item_Include> transactionDetails;
            string summary = "";
            double total = 0;

            if (DataGrid_Transactions.SelectedIndex >= 0 && Textblock_TransactionCode.Text != string.Empty)
            {
                transactionDetails = DataLayer.GetItemDeatils(Convert.ToInt32(Textblock_TransactionCode.Text));

                foreach (Item_Include itemInclude in transactionDetails)
                {
                    summary += itemInclude._Item._NAME + ": " + itemInclude._Quantity + " X $" + itemInclude._Item._PRICE + " = $" + itemInclude._Total + "\n";
                    total += itemInclude._Total;
                }
                summary = summary.Substring(0, summary.Length - 1); //Removing the last \n
                TextBlock_TransactionItems.Text = summary;
                TextBlock_Total.Text = "$" + total.ToString();
            }
        }
        #endregion

        #region Map
        private void button_Map_Click(object sender, RoutedEventArgs e)
        {
            tabItem_Map.IsSelected = true;
        }

        private void Canvas_Map_MouseMove(object sender, MouseEventArgs e)
        {
            Point mouseLocation = e.GetPosition(this.Canvas_Map);

            textBlock_X.Text = "X: " + ((int)mouseLocation.X / 4).ToString();
            textBlock_Y.Text = "Y:" + ((int)mouseLocation.Y / 4).ToString();
        }

        private void Canvas_Map_MouseLeave(object sender, MouseEventArgs e)
        {
            textBlock_X.Text = "X: NA";
            textBlock_Y.Text = "Y: NA";
        }

        private void Canvas_Map_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            List<StoreDistance> result = new List<StoreDistance>();

            if (radioButton_Nearest.IsChecked == true)
            {
                result = DataLayer.FindNearestStore(e.GetPosition(this.Canvas_Map).X / 4, e.GetPosition(this.Canvas_Map).Y / 4);

            }

            else if (radioButton_Range.IsChecked == true)
            {
                try
                {
                    result = DataLayer.FindStoresInRange(e.GetPosition(this.Canvas_Map).X / 4, e.GetPosition(this.Canvas_Map).Y / 4, Int32.Parse(textBox_Range.Text));
                }
                catch
                {
                    result = DataLayer.FindStoresInRange(e.GetPosition(this.Canvas_Map).X / 4, e.GetPosition(this.Canvas_Map).Y / 4, 15);
                }
            }
                DataGrid_StoreDistance.DataContext = null;
                DataGrid_StoreDistance.DataContext = result;
                UpdateMap(result);
        }

        private void UpdateMap(List<StoreDistance> locations)
        {
            Canvas_Map.Children.Clear();

            foreach(StoreDistance storeDistance in locations)
            {
                Point location = new Point(storeDistance._X * 4, storeDistance._Y * 4);
                Ellipse elipse = new Ellipse();

                elipse.Width = 8;
                elipse.Height = 8;
                elipse.Fill = Brushes.Red;
                elipse.Stroke = Brushes.Black;

                Canvas_Map.Children.Add(elipse);
                elipse.SetValue(Canvas.LeftProperty, location.X);
                elipse.SetValue(Canvas.TopProperty, location.Y);
            }
        }
        #endregion 

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Button_Customers_Click(sender, e);
        }
    }
}
