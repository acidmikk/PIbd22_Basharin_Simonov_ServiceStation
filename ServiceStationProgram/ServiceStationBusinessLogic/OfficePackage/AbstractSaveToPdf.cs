using ServiceStationBusinessLogic.OfficePackage.HelperEnums;
using ServiceStationBusinessLogic.OfficePackage.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToPdf
    {
        public void CreatePdfInspector(PdfInfo info)
        {
            CreatePdf(info);
            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });
            CreateParagraph(new PdfParagraph
            {
                Text = $"с { info.DateFrom.ToShortDateString() } по { info.DateTo.ToShortDateString() }",
                Style = "Normal"
            });
            CreateTable(new List<string> { "5cm", "3cm", "5cm", "5cm" });
            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "Наименование авто", "Дата обращения", "Неисправность", "ТО" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });
            foreach (var car in info.Cars)
            {
                CreateRow(new PdfRowParameters
                {

                    Texts = new List<string> { car.CarName,
                                                car.DateIn.ToShortDateString(),
                                                car.DefectName,
                                                car.TechnicalMaintenanceName},
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
            }
            SavePdf(info);
        }

        /*public void CreateDocManager(PdfInfo info)
        {
            CreatePdf(info);
            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });
            CreateParagraph(new PdfParagraph
            {
                Text = $"с { info.DateFrom.ToShortDateString() } по { info.DateTo.ToShortDateString() }",
                Style = "Normal"
            });
            CreateTable(new List<string> { "3cm", "6cm", "3cm", "3cm", "3cm" });
            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "Дата валюты", "Название", "Кредитные программы", "Вклады" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });
            foreach (var currency in info.Currencies)
            {
                CreateRow(new PdfRowParameters
                {
                    
                    Texts = new List<string> { currency.DateAdding.ToShortDateString(),
                                                currency.CurrencyName,
                                                currency.LoanPrograms,
                                                currency.Deposits},
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
            }
            SavePdf(info);
        }*/

        /// <summary>/// Создание doc-файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void CreatePdf(PdfInfo info);
        /// <summary>
        /// Создание параграфа с текстом
        /// </summary>
        /// <param name="title"></param>
        /// <param name="style"></param>
        protected abstract void CreateParagraph(PdfParagraph paragraph);
        /// <summary>
        /// Создание таблицы
        /// </summary>
        /// <param name="title"></param>
        /// <param name="style"></param>
        protected abstract void CreateTable(List<string> columns);
        /// <summary>
        /// Создание и заполнение строки
        /// </summary>
        /// <param name="rowParameters"></param>
        protected abstract void CreateRow(PdfRowParameters rowParameters);
        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void SavePdf(PdfInfo info);        
    }
}

//string.Join(", ",client.DepositCurrencies.Select(cur => cur.Item2).ToList())
/*foreach (var dep in client.DepositCurrencies)
                {
                    CreateRow(new PdfRowParameters
                    {

                        Texts = new List<string> { client.ClientFIO,
                                                client.DateVisit.ToShortDateString(),
                                                dep.Item1.DepositName,
                                                string.Join(", ",dep.Item2.Select(cur => cur.CurrencyName).ToList())},
                        Style = "Normal",
                        ParagraphAlignment = PdfParagraphAlignmentType.Left
                    });
                }   */