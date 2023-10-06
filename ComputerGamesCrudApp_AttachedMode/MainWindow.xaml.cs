using ComputerGamesCrudApp_AttachedMode.Model;


using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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
using System.Windows.Forms;
using DevExpress.Utils.CommonDialogs.Internal;
using System.Text.RegularExpressions;

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
                int price = Convert.ToInt32(addPriceTextBox.Text);
                Game newGame = new Game() { Name = name, ReleasedIn = releasedIn, Price = price };
                dbClient.Insert(newGame);
                MessageBox.Show("Object successfully added", "Successfull", MessageBoxButton.OK, MessageBoxImage.Information);
                updateGameList();
            } catch (Exception ex)
            {
                MessageBox.Show($"Error during insert object: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // выбранный обьект, планирую удалять в листбоксе
        private void usersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Game> games = dbClient.SelectAll();

            if (gamesListBox.SelectedItem is Game)
            {
                //MessageBoxResult result = MessageBox.Show($"Выбранный обьект: " + "\n" + gamesListBox.SelectedItem.ToString());
                string selectrow = gamesListBox.SelectedItem.ToString().Split().First();
                int id_game_t = Convert.ToInt32(selectrow);
                MessageBoxResult resdelete = MessageBox.Show("Удаляем строку?", " ", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (resdelete == MessageBoxResult.Yes)
                {
                    //do something
                    dbClient.Delete(id_game_t);
                    updateGameList();
                }
                else if (resdelete == MessageBoxResult.No)
                {
                    MessageBoxResult resupgrade = MessageBox.Show("Изменяем строку?", " ", MessageBoxButton.YesNo, MessageBoxImage.Information);

                    // считываем выбранную запись как строку 
                    List<string> strings = new List<string>();
                    string selectrowid = gamesListBox.SelectedItem.ToString();
                    // создаем массив для разбиения строки на подстроки
                    char[] splitchar = { '-' };
                    // разбиваем строку на подстроки с применением массива разбивочных элементов
                    foreach (string sel in selectrowid.Split(splitchar))
                    {
                        strings.Add(sel);
                    }
                    //  добавляем её в обьект c индексом
                    int id = Convert.ToInt32(strings[0]);
                    string name = strings[1];
                    int releasedIn = Convert.ToInt32(strings[2]);
                    int price = Convert.ToInt32(strings[3]);
                    Game newGame = new Game() { Id = id, Name = name, ReleasedIn = releasedIn, Price = price };
                    // вписываем обьект по полям в textbox для изменения
                    modifiedIdTextBox.Text = id.ToString();
                    modifiedNameTextBox.Text = name;
                    modifiedRelesedInTextBox.Text = releasedIn.ToString();
                    modifiedPriceTextBox.Text = price.ToString();

                }
            }
        }
        // принимаем изменения по нажатию на modifiedButton
        private void ModifiedButton_Click(object sender, RoutedEventArgs e)
        {
            // добавление измененной  записи
            int id = Convert.ToInt32(modifiedIdTextBox.Text);
            string name = modifiedNameTextBox.Text;
            int releasedIn = Convert.ToInt32(modifiedRelesedInTextBox.Text);
            int price = Convert.ToInt32(modifiedPriceTextBox.Text);
            Game newGame = new Game() {Id = id, Name = name, ReleasedIn = releasedIn, Price = price };
            // вызывавем метод добавления обьекта в базу данных
            dbClient.Update(newGame);
            updateGameList();
        }


        // проверка на ввод только чисел addRelesedInTextBox,addPriceTextBox
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

      
    }
}
