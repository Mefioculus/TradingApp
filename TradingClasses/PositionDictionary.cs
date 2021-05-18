using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TradingClasses {

    #region PositionDictionary class

    public class PositionDictionary {

        #region Properties

        public Dictionary<string, string> namesOfPosition { get; private set; }
        public string Source { get; private set; }
        public string PathToDataDirectory { get; private set; }
        public string NameOfDataFile { get; private set; }
        public string PathToDataFile { get; private set; }
        public bool IsChanged { get; private set; } = false;

        #endregion Properties

        #region Constructors

        public PositionDictionary(Settings settings, string source) {
            this.Source = source;
            this.PathToDataDirectory = settings.DataDirectory;
            this.NameOfDataFile = $"{this.Source}.json";
            this.PathToDataFile = Path.Combine(this.PathToDataDirectory, this.NameOfDataFile);

            // Метод производит поиск файла с сохраненным словарем, если такой имеется, и производит возврат пустого словаря,
            // если файл с сохраненным словарем не был обнаружен
            LoadData();
        }

        #endregion Constructors

        #region Methods

        #region Read and Write Methods

        private void LoadData() {
            //Производим чтение файла в зависимости от того, какой выбран источник данных
            CheckPathToDataFile();

            if (File.Exists(this.PathToDataFile)) {
                string jsonString = File.ReadAllText(this.PathToDataFile);
                this.namesOfPosition = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
            }
            else {
                Console.WriteLine($"Модуль PositionDictionary: Не удалось найти файл с данными для словаря {this.Source}");
                Console.WriteLine("Модуль PositionDictionary: Для ведения записей возвращен пустой словарь");
                this.namesOfPosition = new Dictionary<string, string>();
            }
        }
        

        public void SaveData() {
            
            string jsonString = JsonConvert.SerializeObject(this.namesOfPosition);
            File.WriteAllText(this.PathToDataFile, jsonString);
            this.IsChanged = false;
            Console.WriteLine("Произведено сохранение изменений в словаре");
        }

        // Метод для проверки наличия директории, в которой будут располагаться сохраненные файлы с сохраненными словарями
        private void CheckPathToDataFile() {
            // Проверяем наличие директории, в которой будет располагаться файл
            if (!Directory.Exists(Path.GetDirectoryName(this.PathToDataFile))) {
                // Создаем директорию по этому пути
                Directory.CreateDirectory(Path.GetDirectoryName(this.PathToDataFile));
            }
        }
        
        #endregion Read and Write Methods

        #region wraper-methods

        // Метод обертка для получения значения значения по ключу словаря напрямую через класс
        public string this[string key] {
            get {
                return this.namesOfPosition[key];
            }
            set {
                this.IsChanged = true;
                this.namesOfPosition[key] = value;
            }
        }

        // Метод обертка для получения данных о том, находится ли требуемый ключ в словаре
        public bool Contains(string key) {
            return this.namesOfPosition.ContainsKey(key);
        }

        // Метод обертка для очистки данных в словаре
        public void Clear() {
            this.namesOfPosition.Clear();
            // Обнуление данных в самом файле
            SaveData();
        }

        // Метод обертка для получения количества записей в словаре
        public int Count() {
            return this.namesOfPosition.Count;
        }

        #endregion wraper-methods

        public bool Validate(string value) {
            return true;
            //TODO
        }

        #endregion Methods
    }

    #endregion PositionDictionary class
}
