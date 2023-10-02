using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
