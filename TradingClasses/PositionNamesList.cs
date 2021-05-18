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
            
            PathToListInJson = Path.Combine(settings.DataDitectory, nameOfDirectory, nameOfFile);
            CheckPathToJsonFile();
            Load();

        }

        #endregion Constructors

        #region Methods

        #region Save and Load methods

        private void Load() {
            List<string> result;
            
            if (File.Exits(this.PathToListInJson)) {
                string json = File.ReadAllText(this.PathToListInJson);
                result = JsonConvert.DeserializeObject<List<string>>(json);
            }
            else {
                result = new List<string>();
            }

            return result;
        }

        public void Save() {
            //TODO
            //Реализовать метод сохранения списка в json строку и после сохранения в файл
            //Не производить сохранения, если переменная IsChanged - ложь
            if (this.IsChanged != false) {
                string json = JsonConvert.SerializeObject(this.ListOfNames)
            }
        }

        // Метод для проверки наличия пути к файлу, в котором будет сохранен список с данными
        private void CheckPathToJsonFile() {
            if (this.PathToListInJson == string.Empty) {
                throw new Exception("File Path to json is missing");

                string pathToDirectoryWithJson = Path.GetDirectoryName(this.PathToListInJson);
                if (!Directory.Exists(pathToDirectoryWithJson)) {
                    Directory.CreateDirectory(pathToDirectoryWithJson);
                }
            }
        }

        #endregion Save and Load methods

        #region Wrapper-methods for list<string> methods

        public Add(string) {
            //TODO
            //Реализовать метод обертку для добавления новых объектво в справочник
            //Так же реализовать изменение переменной IsChanged при наличии изменений
        }

        public string this[int index] {
            //TODO
            //Реализовать метод обертку для обращения к листу с родительского класса
        }

        public int Count() {
            //TODO
            //Реализовать метод для определения количества объектов, которые содержатся в списке
        }

        public bool Contains(string value) {
            //TODO
            //Реализовать метод, который показывает, содержится ли уже в списке позиция с таким наименованием
        }

        #endregion Wrapper-methods for list<string> methods

        #endregion Methods
    }
}
