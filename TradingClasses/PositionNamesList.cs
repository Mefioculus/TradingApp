using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace TradingClasses
{
    public class PositionNamesList {
        // Класс для хранения списка всех имен позиций (для последующего сопоставления с словарем позиций для получения
        // конкретного значения для разных источников данных
        
        #region Properties

        private List<string>ListOfNames = new List<string>();
        private string nameOfFile = "listOfPosition.json";
        private string nameOfDirectory = "PositionList";
        public string PathToListInJson { get; private set; } = string.Empty;
        public bool IsChanged { get; private set; } = false;

        #endregion Properties
        
        #region Constructors

        public PositionNamesList(Settings settings) {
            // Для начала пытаемся получить список данных, которые могли быть сохранены
            // в результате предыдущего запуска программы
            
            PathToListInJson = Path.Combine(settings.DataDirectory, nameOfDirectory, nameOfFile);
            CheckPathToJsonFile();
            Load();

        }

        #endregion Constructors

        #region Methods

        #region Save and Load methods

        private void Load() {

            if (File.Exists(this.PathToListInJson)) {
                string json = File.ReadAllText(this.PathToListInJson);
                this.ListOfNames = JsonConvert.DeserializeObject<List<string>>(json);
            }
            else {
                this.ListOfNames = new List<string>();
            }
        }

        public void Save() {
            if (this.IsChanged != false) {
                string json = JsonConvert.SerializeObject(this.ListOfNames);
                File.WriteAllText(this.PathToListInJson, json);
            }

        }

        // Метод для проверки наличия пути к файлу, в котором будет сохранен список с данными
        private void CheckPathToJsonFile() {
            if (this.PathToListInJson == string.Empty)
                throw new Exception("File Path to json is missing");
            else{
                string pathToDirectoryWithJson = Path.GetDirectoryName(this.PathToListInJson);
                if (!Directory.Exists(pathToDirectoryWithJson)) {
                    Directory.CreateDirectory(pathToDirectoryWithJson);
                }
            }
        }

        #endregion Save and Load methods

        #region Wrapper-methods for list<string> methods

        public bool Add(string value) {
            if (!this.ListOfNames.Contains(value)) {
                this.ListOfNames.Add(value);

                return true;
            }

            return false;
        }

        public string this[int index] {
            get {
                return this.ListOfNames[index];
            }
        }

        public int Count() {
            return this.ListOfNames.Count;
        }

        public bool Contains(string value) {
            return this.ListOfNames.Contains(value);
        }

        #endregion Wrapper-methods for list<string> methods

        #endregion Methods
    }
}
