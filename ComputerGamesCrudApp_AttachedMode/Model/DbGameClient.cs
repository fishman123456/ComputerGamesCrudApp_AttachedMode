using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGamesCrudApp_AttachedMode.Model
{
    // класс, обеспечивающий выполнение операций с БД
    internal class DbGameClient
    {
        // провайдер подключения к БД
        private DbConnectionProvider connectionProvider;

        // конструктор
        public DbGameClient(DbConnectionProvider connectionProvider)
        {
            this.connectionProvider = connectionProvider;
        }

        // операции с БД

        // 1. получить все записи
        public List<Game> SelectAll()
        {
            using (SqlConnection connection = connectionProvider.OpenDbConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM game_t;", connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // считать строки результат в List<Game>
                    return ReadSelectResult(reader);
                }
            }
        }

        // 2. получить запись по id
        public Game SelectById(int id) { 
            throw new NotImplementedException();
        }

        // 3. добавить запись
        public void Insert(Game game) { 
            using (SqlConnection connection = connectionProvider.OpenDbConnection())
            {
                // формируем команду INSERT с использованием параметров запроса
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO game_t (name_f, released_in_f, price_f) VALUES (@name, @released_in, @price);", 
                    connection
                );
                // добавляем параметры в запрос
                cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = game.Name;
                cmd.Parameters.Add("@released_in", System.Data.SqlDbType.Int).Value = game.ReleasedIn;
                cmd.Parameters.Add("@price", System.Data.SqlDbType.Decimal).Value = game.Price;
                // выполняем запрос
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    // если результат не соответствует ожидаемому значению, то выбросить исключение
                    throw new Exception($"rowsAffected {rowsAffected} != 1");
                }
            }
        }

        // 4. удалить запись по id
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        // 5. обновить запись по id
        public void Update(Game game)
        {
            throw new NotImplementedException();
        }

        // ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ

        // 1. конвертация строки результата в объект Game
        private Game ConvertReaderRow(SqlDataReader reader)
        {
            int id = (int)reader["id"];
            string name = (string)reader["name_f"];
            int releasedIn = (int)reader["released_in_f"];
            decimal price = (decimal)reader["price_f"];
            return new Game() { Id = id, Name = name, ReleasedIn = releasedIn, Price = price };
        }

        // 2. чтение табличного результата в список обектов
        private List<Game> ReadSelectResult(SqlDataReader reader)
        {
            List<Game> games = new List<Game>();
            while (reader.Read())
            {
                Game game = ConvertReaderRow(reader);
                games.Add(game);
            }
            return games;
        }
    }
}
