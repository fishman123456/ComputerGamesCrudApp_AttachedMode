using ComputerGamesCrudApp_AttachedMode.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace ComputerGamesCrudApp_AttachedMode
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // поле клиента работы с БД
        private DbGameClient dbClient;

        public MainWindow()
        {
            InitializeComponent();
            dbClient = new DbGameClient(new DbConnectionProvider());
            updateGameList();
        }

        private void updateGameList()
        {
            try
            {
                // обновление списка объектов (можно придумать рациональный способ)
                List<Game> games = dbClient.SelectAll();
                gamesListBox.Items.Clear();
                games.ForEach(game => gamesListBox.Items.Add(game));
            } catch (Exception ex)
            {
                MessageBox.Show($"Error during select object list: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void testDbConnection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // проверка соединения с БД
                DbConnectionProvider provider = new DbConnectionProvider();
                using (SqlConnection connection = provider.OpenDbConnection())
                {
                    MessageBox.Show("Connection is ok", "Connected", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } catch (Exception ex)
            {
                MessageBox.Show($"Connection error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // добавление новой записи
                string name = addNameTextBox.Text;
                int releasedIn = Convert.ToInt32(addRelesedInTextBox.Text);
                decimal price = Convert.ToDecimal(addPriceTextBox.Text);
                Game newGame = new Game() { Name = name, ReleasedIn = releasedIn, Price = price };
                dbClient.Insert(newGame);
                MessageBox.Show("Object successfully added", "Successfull", MessageBoxButton.OK, MessageBoxImage.Information);
                updateGameList();
            } catch (Exception ex)
            {
                MessageBox.Show($"Error during insert object: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
