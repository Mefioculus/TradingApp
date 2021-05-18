using System;
using System.Collections;
using System.Collections.Generic;
using TradingClasses;


namespace Monitor
{
    class Monitor
    {
        static void Main(string[] args)
        {
            const string source = "Sber";
            Settings appSettings = SettingsHandler.GetSettings("develop");
            PositionDictionary dict = new PositionDictionary(appSettings, source);
            
            List<string> namesOfPosition = new List<string>() {
                "vtb",
                "aeroflot",
                "gazprom",
                "nornickel-gmk",
                "lukoil"
            };

            // Проверка, есть ли позиции, которые перечислены в списке позиций, в словаре для конкретного источника данных
            foreach (string name in namesOfPosition) {
                if (!dict.Contains(name)) {
                    Console.WriteLine($"Для словаря {source} отсутствует ключ {name}");
                    Console.WriteLine($"Для продолжения осуществите ввод значения, которое соответствует ключe {name} для словаря {source}");
                    string value = Console.ReadLine();
                    if (dict.Validate(value)) {
                        dict[name] = value;
                    }
                    else
                        Console.WriteLine($"Введенное значение '{value}' не прошло валидацию и не будет добавлено в справочник");
                }
            }
            
            // Если в словарь были применены изменения - провести сохранение данных изменений
            if (dict.IsChanged)
                dict.SaveData();
            
            
            foreach (string name in namesOfPosition) {
                FinamPosition test = new FinamPosition(name);
                Console.WriteLine(test);
            }


        }
    }
}
