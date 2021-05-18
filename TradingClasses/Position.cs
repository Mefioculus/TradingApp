using System;
using System.IO;
using System.Net; //Для совершения запросов
using System.Globalization;
// Добавить библиотеку для парсинга html
using HtmlAgilityPack;
using TradingInterfaces;

namespace TradingClasses
{
    #region Position class

    // Данный класс будет отображать позиции на рынке акций, которые можно приобрести
    public abstract class Position : ITradingPosition {

        #region Properties and Fields

        public string UrlTemplate = string.Empty;
        public string XPath = string.Empty;

        public string Name { get; set; } = string.Empty;
        public double CurrentCost { get; set; } = 0.0;
        public string Url { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public DateTime LastUpdate { get; set; } == new DateTime();

        #endregion Properties and Fields

        #region Constructors

        public Position(string name, string urlTemp, string xpath) {
            this.UrlTemplate = urlTemp;
            this.XPath = xpath;
            this.Name = name;
            this.Url = string.Format(UrlTemplate, name);
            UpdateCost();
        }

        #endregion Constructors

        #region Methods

        public void UpdateCost() {
            // Создаем новый запрос
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(Url);
            WebResponse resp = req.GetResponse();

            // Получаем дату последнего обновления
            LastUpdate = DateTime.Now;

            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            
            string resultHtml = sr.ReadToEnd();
            
            // Создаем новый документ
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(resultHtml);

            ParseHTML(doc);
        }

        // Реализация данных методов лежит на потомках
        public abstract void ParseHTML(HtmlDocument doc);

        public abstract string CheckInputString(string input);

        public override string ToString() {
            return string.Format("Цена по инструменту {0}: {1} {2} на дату {3}",
                                Name,
                                CurrentCost,
                                Currency,
                                LastUpdate.ToString("HH:mm yyyy.MM.dd"));
        }

        #endregion Methods
    }

    #endregion Position class

    #region FinamPosition class

    public class FinamPosition : Position {

        #region Properties and Fields

        private const string ConstUrlTemplate = "https://www.finam.ru/quote/moex-akcii/{0}/";
        private const string ConstXPath = "//*[@id=\"finambackground\"]/div/div/div[1]/div[2]/div[3]/div[2]/div[1]/div[1]/div[2]/span[1]";

        #endregion Properties and Fields

        #region Constructors

        public FinamPosition(string name) : base(name, ConstUrlTemplate, ConstXPath) {
        }

        #endregion Constructors

        #region Methods

        public override void ParseHTML(HtmlDocument doc) {
            string cost = doc.DocumentNode.SelectSingleNode(XPath).WriteContentTo();
            cost = CheckInputString(cost);

            string[] data = cost.Split(' ');
            // Получаем значение цены
            CurrentCost = double.Parse(data[0].Replace(",", "."));

            // Получаем значене валюты
            Currency = data[1];
        }

        public override string CheckInputString(string input) {
            // Замена знака пробела (для значений, которые превышают 999)
            // Для этого я вычислил значение этого символа (&nbsp;) и получил
            // его представление в числах через приведение к типу int (получилось 65533)
            // после чего привел его к шестнадцатеричной системе счисления - FFFD
            input = input.Replace("\uFFFD", "");
            
            return input;
        }
        
        #endregion Methods
    }

    #endregion FinamPosition class

    #region SberPosition class

    public class SberPosition : Position {

        #region Properties and Fields

        private const string ConstUrlTemplate = "";
        private const string ConstXPath = "";

        #endregion Properties and Fields

        #region Constructors

        public SberPosition(string name) : base(name, ConstUrlTemplate, ConstXPath) {
        }

        #endregion Constructors

        #region Methods

        public override void ParseHTML(HtmlDocument doc) {
        }

        public override string CheckInputString(string input) {
            return input;
        }

        #endregion Methods
    }

    #endregion SberPosition class
}
