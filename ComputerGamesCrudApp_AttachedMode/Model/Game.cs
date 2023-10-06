using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static DevExpress.Data.Filtering.Helpers.SubExprHelper;

namespace ComputerGamesCrudApp_AttachedMode.Model
{
    // Класс, описывающий сущность "игра" - dataclass
    internal class Game
    {
        // поля - столбцы таблицы БД
        public long Id { get; set; }
        public string Name { get; set; }
        public int ReleasedIn { get; set; }
        public decimal Price { get; set; }

        // 
        public override string ToString()
        {
            return $"{Id} - {Name} - {ReleasedIn} - {Price}";
        }
    }
}
// запросы для проверки 05-10-2023
//use computer_game_db
//update game_t  set name_f = 'name', released_in_f = 1980, price_f = 5566
//                    where id = 2
//select * from game_t