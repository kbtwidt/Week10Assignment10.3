using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Week10Assignment10._3.Models;
using Week10Assignment10._3.Services;

namespace Week10Assignment10._3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CRUD cRUD = new CRUD();
        private int selectedCarId = 0;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCars();
        }

        private void LoadCars()
        {
            dgCars.ItemsSource = cRUD.GetAllCars();
        }

        // CREATE: Add a new car
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                Car car = GetCarFromInput();

                cRUD.AddCar(car);

                MessageBox.Show("Car added successfully!");

                ClearFields();
                LoadCars();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding car: " + ex.Message);
            }
        }

        // UPDATE: Update selected car
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCarId == 0)
            {
                MessageBox.Show("Please select a car to update.");
                return;
            }

            if (!ValidateInput())
                return;

            try
            {
                Car car = GetCarFromInput();

                car.Id = selectedCarId;

                cRUD.UpdateCar(car);

                MessageBox.Show("Car updated successfully!");

                ClearFields();
                LoadCars();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating car: " + ex.Message);
            }
        }

        // DELETE: Delete selected car
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCarId == 0)
            {
                MessageBox.Show(
                    "Please select a car to delete.");
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to delete this car?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
                return;

            try
            {
                cRUD.DeleteCar(selectedCarId);

                MessageBox.Show("Car deleted successfully!");

                ClearFields();
                LoadCars();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error deleting car: " + ex.Message);
            }
        }

        // Get car details from input fields
        private Car GetCarFromInput()
        {
            return new Car
            {
                VIN = txtVIN.Text.Trim(),
                Make = txtMake.Text.Trim(),
                Model = txtModel.Text.Trim(),
                Year = int.Parse(txtYear.Text),
                Price = decimal.Parse(txtPrice.Text),
                Color = txtColor.Text.Trim()
            };
        }

        // Select a car from DataGrid
        private void dgCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCars.SelectedItem is Car car)
            {
                selectedCarId = car.Id;

                txtVIN.Text = car.VIN;
                txtMake.Text = car.Make;
                txtModel.Text = car.Model;
                txtYear.Text = car.Year.ToString();
                txtPrice.Text = car.Price.ToString();
                txtColor.Text = car.Color ?? "";
            }
        }

        // CLEAR: Clear input fields
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            selectedCarId = 0;

            txtVIN.Clear();
            txtMake.Clear();
            txtModel.Clear();
            txtYear.Clear();
            txtPrice.Clear();
            txtColor.Clear();

            dgCars.SelectedItem = null;
        }

        // REFRESH: Reload DataGrid
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadCars();
        }

        // Validate input fields
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtVIN.Text) ||
                string.IsNullOrWhiteSpace(txtMake.Text) ||
                string.IsNullOrWhiteSpace(txtModel.Text) ||
                string.IsNullOrWhiteSpace(txtYear.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show(
                    "Please fill in all required fields.");
                return false;
            }

            if (txtVIN.Text.Trim().Length != 17)
            {
                MessageBox.Show(
                    "VIN must contain exactly 17 characters.");
                return false;
            }

            if (!int.TryParse(txtYear.Text, out int year) ||
                year < 1886 ||
                year > DateTime.Now.Year + 1)
            {
                MessageBox.Show(
                    "Please enter a valid manufacturing year.");
                return false;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) ||
                price < 0)
            {
                MessageBox.Show(
                    "Please enter a valid price.");
                return false;
            }

            return true;
        }
    }
}