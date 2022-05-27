using ServiceStationBusinessLogic.OfficePackage.HelperEnums;
using ServiceStationBusinessLogic.OfficePackage.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToWord
    {
        public void CreateDocManager(WordInfo info)
        {

            CreateWord(info);
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "24", }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });
            foreach (var lpd in info.LoanProgramDeposit)
            {
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> { (lpd.LoanProgramName, new WordTextProperties { Size = "24", Bold = true})},
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Both
                    }
                });
                foreach(var dep in lpd.Deposits)
                {
                    CreateParagraph(new WordParagraph
                    {
                        Texts = new List<(string, WordTextProperties)> { (dep.Item1, new WordTextProperties { Size = "24", Bold = false }),
                                                                         (": "+dep.Item2.ToString(), new WordTextProperties { Size = "24", Bold = false })},
                        TextProperties = new WordTextProperties
                        {
                            Size = "24",
                            JustificationType = WordJustificationType.Right
                        }
                    });
                }
            }
            SaveWord(info);
        }
        public void CreateDocClerk(WordInfo info)
        {

            CreateWord(info);
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "24", }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });
            foreach (var cc in info.ClientCurrency)
            {
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> { (cc.ClientFIO, new WordTextProperties { Size = "24", Bold = true }) },
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Both
                    }
                });
                foreach (var cur in cc.Currencies)
                {
                    CreateParagraph(new WordParagraph
                    {
                        Texts = new List<(string, WordTextProperties)> { (cur, new WordTextProperties { Size = "24", Bold = false })},
                        TextProperties = new WordTextProperties
                        {
                            Size = "24",
                            JustificationType = WordJustificationType.Both
                        }
                    });
                }
            }
            SaveWord(info);
        }
        /// <summary>
        /// Создание doc-файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void CreateWord(WordInfo info);
        /// <summary>
        /// Создание абзаца с текстом
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        protected abstract void CreateParagraph(WordParagraph paragraph);
        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void SaveWord(WordInfo info);
    }
}
