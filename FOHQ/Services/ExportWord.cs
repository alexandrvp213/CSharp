using System;
using System.IO;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;

namespace FOHQ.Services
{
    public class ExportWord
    {
        private Models.Document _document;

        private void Set_document(Models.Document value)
        {
            _document = value;
        }

        /// <summary>
        /// Выполнение экспорта
        /// </summary>
        public void Execute()
        {
            //Saves the Word document to MemoryStream
            MemoryStream stream = new MemoryStream();
            //Creates a new instance of WordDocument (Empty Word Document)
            using (WordDocument wordDdocument = new WordDocument())
            {
                //Adding a new section to the document
                WSection section = wordDdocument.AddSection() as WSection;
                //Set Margin of the section
                section.PageSetup.Margins.All = 72;
                //Set page size of the section
                section.PageSetup.PageSize = new Syncfusion.Drawing.SizeF(612, 792);

                //Create Paragraph styles
                WParagraphStyle style = wordDdocument.AddParagraphStyle("Normal") as WParagraphStyle;
                style.CharacterFormat.FontName = "Calibri";
                style.CharacterFormat.FontSize = 11f;
                style.ParagraphFormat.BeforeSpacing = 0;
                style.ParagraphFormat.AfterSpacing = 8;
                style.ParagraphFormat.LineSpacing = 13.8f;

                style = wordDdocument.AddParagraphStyle("Heading 1") as WParagraphStyle;
                style.ApplyBaseStyle("Normal");
                style.CharacterFormat.FontName = "Calibri Light";
                style.CharacterFormat.FontSize = 16f;
                style.CharacterFormat.TextColor = Syncfusion.Drawing.Color.FromArgb(46, 116, 181);
                style.ParagraphFormat.BeforeSpacing = 12;
                style.ParagraphFormat.AfterSpacing = 0;
                style.ParagraphFormat.Keep = true;
                style.ParagraphFormat.KeepFollow = true;
                style.ParagraphFormat.OutlineLevel = Syncfusion.DocIO.OutlineLevel.Level1;

                //Appends paragraph
                AppendParagraph(section, "Расчет сил и средств", 18f, true);

                // ТУШЕНИЕ
                AppendQuenching(section);

                // РАСХОД
                AppendRequiredConsumption(section);

                // КОЛИЧЕСТВО
                AppendNumberOfDevices(section);

                // ГЖ/ЛВЖ
                AppendQuenchingSolution(section);

                // МЕТОДЫ ПОДАЧИ
                AppendFilingMethods(section);

                //Appends paragraph
                section.AddParagraph();

                //Saves the Word document to  MemoryStream
                wordDdocument.Save(stream, FormatType.Docx);
            }

            //Save the stream as a file in the device and invoke it for viewing
            _ = Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView("Расчет сил и средств " + DateTime.Now.ToString("yyyy'_'MM'_'dd'T'HH'_'mm'_'ss") + ".docx", "application/msword", stream);
        }

        /// <summary>
        /// Добавление текста
        /// </summary>
        private void AppendParagraph(WSection section, string text, float fontcolor, bool isBold)
        {
            IWParagraph paragraph = section.AddParagraph();
            paragraph.BreakCharacterFormat.FontSize = fontcolor;
            WTextRange textRange = paragraph.AppendText(text) as WTextRange;
            textRange.CharacterFormat.FontSize = fontcolor;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = isBold;
        }

        /// <summary>
        /// Добавление таблицы
        /// </summary>
        private IWTable AppendTable(WSection section, int colNum)
        {
            //Appends table
            IWTable table = section.AddTable();
            table.ResetCells(1, colNum);
            table.TableFormat.Borders.BorderType = BorderStyle.Single;
            table.TableFormat.IsAutoResized = true;
            return table;
        }

        /// <summary>
        /// Добавление заголовка таблицы
        /// </summary>
        private void AppendTableHeader(IWTable table, string captionText, string captionData, string captionFormula = null)
        {
            WTableRow row = table.Rows[0];
            WTableCell cellText, cellValue, cellFormula;

            cellText = row.Cells[0];
            cellValue = row.Cells[1];
            AppendCellParagraph(cellText, captionText);
            AppendCellParagraph(cellValue, captionData);

            if (captionFormula != null)
            {
                cellFormula = row.Cells[2];
                AppendCellParagraph(cellFormula, captionFormula);
            }
        }

        /// <summary>
        /// Добавление текста в ячейку таблицы
        /// </summary>
        private void AppendCellParagraph(WTableCell cell, string text)
        {
            IWParagraph paragraph = cell.AddParagraph();
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 12f;
            WTextRange textRange = paragraph.AppendText(text) as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
        }

        /// <summary>
        /// Добавление строки в таблицу
        /// </summary>
        private void AppendTableRow(IWTable table, string text, string value, string formula = null)
        {
            WTableRow row;
            row = table.AddRow();
            WTableCell cellText, cellValue, cellFormula;

            cellText = row.Cells[0];
            cellValue = row.Cells[1];

            // Вводимые данные или расчетные данные
            if (value == "" || value == null)
            {
                value = "<не выбрано>";
            }
            AppendCellParagraph(cellText, text);
            AppendCellParagraph(cellValue, value);

            if (formula != null)
            {
                cellFormula = row.Cells[2];
                AppendCellParagraph(cellFormula, formula);
            }            
        }

        /// <summary>
        /// Добавление расчетов ТУШЕНИЕ
        /// </summary>
        private void AppendQuenching(WSection section)
        {
            AppendParagraph(section, "1. Тушение", 14f, true);
                 
            string shapeStr = "";
            var formOfQuenching = App.DataAccess.QuenchingRepository.GetFormOfQuenching(_document.Quenching.Shape);
            if (formOfQuenching != null)
            {
                shapeStr = formOfQuenching.Name;
            }
            AppendParagraph(section, "Данные", 12f, false);
            IWTable table = AppendTable(section, 2);
            AppendTableHeader(table, "Наименование", "Данные");

            AppendTableRow(table, "Форма пожара", shapeStr);
            AppendTableRow(table, "Путь, пройденный огнем", _document.Quenching.Path + " м");
            AppendTableRow(table, "Ширина", _document.Quenching.WidthPath + " м");
            string quenchingTrunkStr = "";
            var quenchingTrunk = App.DataAccess.QuenchingRepository.GetQuenchingBarrel(_document.Quenching.QuenchingTrunk);
            if (quenchingTrunk != null)
            {
                quenchingTrunkStr = quenchingTrunk.Name;
            }
            AppendTableRow(table, "Тушение производится:", quenchingTrunkStr);
            section.AddParagraph();
            AppendParagraph(section, "Расчетные данные", 12f, false);
            table = AppendTable(section, 3);
            AppendTableHeader(table, "Наименование", "Данные", "Формула расчета");

            AppendTableRow(table, "Площадь пожара", _document.Quenching.FireArea + " кв.м", _document.Quenching.FireAreaFormula);
            if (_document.Quenching.ExtinguishingAreaPerimeterFormula != null && _document.Quenching.ExtinguishingAreaPerimeterFormula != "")
            {
                AppendTableRow(table, "Площадь тушения по периметру", _document.Quenching.ExtinguishingAreaPerimeter + " кв.м", _document.Quenching.ExtinguishingAreaPerimeterFormula);
            }
            if (_document.Quenching.ExtinguishingAreaFrontFormula != null && _document.Quenching.ExtinguishingAreaFrontFormula != "")
            {
                AppendTableRow(table, "Площадь тушения по фронту", _document.Quenching.ExtinguishingAreaFront + " кв.м", _document.Quenching.ExtinguishingAreaFrontFormula);
            }
            if (_document.Quenching.SizeAreaPerimeterFormula != null && _document.Quenching.SizeAreaPerimeterFormula != "")
            {
                AppendTableRow(table, "Величина в метрах : периметр", Convert.ToString(_document.Quenching.SizeAreaPerimeter), _document.Quenching.SizeAreaPerimeterFormula);
            }
            if (_document.Quenching.SizeAreaFrontFormula != null && _document.Quenching.SizeAreaFrontFormula != "")
            {
                AppendTableRow(table, "Величина в метрах : фронт", Convert.ToString(_document.Quenching.SizeAreaFront), _document.Quenching.SizeAreaFrontFormula);
            }
            section.AddParagraph();
        }

        /// <summary>
        /// Добавление расчетов РАСХОД
        /// </summary>
        private void AppendRequiredConsumption(WSection section)
        {
            AppendParagraph(section, "2. Требуемый расход на защиту и тушение", 14f, true);
            AppendParagraph(section, "Данные", 12f, false);
            IWTable table = AppendTable(section, 2);
            AppendTableHeader(table, "Наименование", "Данные");
            string sectionByObjectStr = "";
            var sectionObj = App.DataAccess.RequiredConsumptionRepository.GetSectionByObject(_document.RequiredConsumption.SectionByObject);
            if (sectionObj != null)
            {
                sectionByObjectStr = sectionObj.Name;
            }
            AppendTableRow(table, "Раздел по виду объекта", sectionByObjectStr);
            AppendTableRow(table, "Раздел по назначению", _document.RequiredConsumption.SectionByDestination);
            AppendTableRow(table, "Показатель", _document.RequiredConsumption.Indicator + " л/(кв.м*с)");
            string extinguishingTypeStr = "";
            if (_document.RequiredConsumption.ExtinguishingType == Data.ExtinguishingType.ExtinguishingFront)
            {
                extinguishingTypeStr = "По фронту";
            }
            else if (_document.RequiredConsumption.ExtinguishingType == Data.ExtinguishingType.ExtinguishingPerimeter)
            {
                extinguishingTypeStr = "По периметру";
            }
            AppendTableRow(table, "Тушение производится", extinguishingTypeStr);
            section.AddParagraph();
            AppendParagraph(section, "Расчетные данные", 12f, false);
            table = AppendTable(section, 3);
            AppendTableHeader(table, "Наименование", "Данные", "Формула расчета");
            AppendTableRow(table, "Требуемый расход на тушение", _document.RequiredConsumption.ConsumptionExtinguishing + " л/c", _document.RequiredConsumption.ConsumptionExtinguishingFormula);
            AppendTableRow(table, "Требуемый расход на защиту", _document.RequiredConsumption.ConsumptionDefense + " л/c", _document.RequiredConsumption.ConsumptionDefenseFormula);
            section.AddParagraph();
        }

        /// <summary>
        /// Добавление расчетов КОЛИЧЕСТВО
        /// </summary>
        private void AppendNumberOfDevices(WSection section)
        {
            AppendParagraph(section, "3. Количество приборов", 12f, true);
            AppendParagraph(section, "Данные", 12f, false);
            IWTable table = AppendTable(section, 2);
            AppendTableHeader(table, "Наименование", "Данные");
            var method = App.DataAccess.NumberOfDevicesRepository.GetExtinguishingMethod(_document.NumberOfDevices.Method);
            string methodStr = "";
            if (method != null)
            {
                methodStr = method.Name;
            }
            AppendTableRow(table, "Способ тушения", methodStr);
            if (_document.NumberOfDevices.Method == Data.ExtinguishingMethodType.Water)
            {
                AppendTableRow(table, "Водяные стволы", _document.NumberOfDevices.WaterBarrels);
                AppendTableRow(table, "Расход 1", _document.NumberOfDevices.Consumption + " л/с");
            }
            else
            {
                AppendTableRow(table, "Пенные стволы", _document.NumberOfDevices.FoamBarrels);
                AppendTableRow(table, "Расход 2", _document.NumberOfDevices.Consumption2 + " л/с");
                AppendTableRow(table, "Расход по пене", _document.NumberOfDevices.ConsumptionFoam + " м3/мин");
                AppendTableRow(table, "Площадь тушения пенного ствола", _document.NumberOfDevices.ExtinguishingAreaFoamBarrel + " м2");
            }
            AppendTableRow(table, "Стволы на защиту", _document.NumberOfDevices.BarrelsForDefense);
            AppendTableRow(table, "Расход 3", _document.NumberOfDevices.Consumption3 + " л/с");
            section.AddParagraph();
            AppendParagraph(section, "Расчетные данные", 12f, false);
            table = AppendTable(section, 3);
            AppendTableHeader(table, "Наименование", "Данные", "Формула расчета");
            AppendTableRow(table, "Требуемое количество приборов на тушение", Convert.ToString(_document.NumberOfDevices.CountExtinguishing), _document.NumberOfDevices.CountExtinguishingFormula);
            AppendTableRow(table, "Требуемое количество приборов на защиту", Convert.ToString(_document.NumberOfDevices.CountDefense), _document.NumberOfDevices.CountDefenseFormula);
            if (_document.NumberOfDevices.AlternativeSolution1)
            {
                section.AddParagraph();
                AppendParagraph(section, "Альтернативное решение 1", 12f, false);
                table = AppendTable(section, 3);
                AppendTableHeader(table, "Наименование", "Данные", "Расчет");
                AppendTableRow(table, "Вид ствола", _document.NumberOfDevices.ExtinguishingBarrel1, "");
                AppendTableRow(table, "Количество", Convert.ToString(_document.NumberOfDevices.CountExtinguishingBarrel1), "");
                AppendTableRow(table, "Вид ствола", _document.NumberOfDevices.ExtinguishingBarrel2, "");
                AppendTableRow(table, "Количество", Convert.ToString(_document.NumberOfDevices.CountExtinguishingBarrel2), "");
                AppendTableRow(table, "Итого", Convert.ToString(_document.NumberOfDevices.TotalCountExtinguishingBarrel), _document.NumberOfDevices.TotalCountExtinguishingBarrelFormula);
            }
            if (_document.NumberOfDevices.AlternativeSolution2)
            {
                section.AddParagraph();
                AppendParagraph(section, "Альтернативное решение 2", 12f, false);
                table = AppendTable(section, 3);
                AppendTableHeader(table, "Наименование", "Данные", "Расчет");
                AppendTableRow(table, "Вид ствола", _document.NumberOfDevices.DefenseBarrel1, "");
                AppendTableRow(table, "Количество", Convert.ToString(_document.NumberOfDevices.CountDefenseBarrel1), "");
                AppendTableRow(table, "Вид ствола", _document.NumberOfDevices.DefenseBarrel2, "");
                AppendTableRow(table, "Количество", Convert.ToString(_document.NumberOfDevices.CountDefenseBarrel2), "");
                AppendTableRow(table, "Итого", Convert.ToString(_document.NumberOfDevices.TotalCountDefenseBarrel), _document.NumberOfDevices.TotalCountDefenseBarrelFormula);
            }
            section.AddParagraph();
        }

        /// <summary>
        /// Добавление расчетов ЛВЖ\ГЖ
        /// </summary>
        private void AppendQuenchingSolution(WSection section)
        {
            AppendParagraph(section, "4. Тушение ЛВХ (ГЖ) по раствору", 12f, true);
            AppendParagraph(section, "Данные", 12f, false);
            IWTable table = AppendTable(section, 2);
            AppendTableHeader(table, "Наименование", "Данные");
            string multiplicityStr = App.DataAccess.QuenchingSolutionRepository.GetMultiplicityStr(_document.QuenchingSolution.Multiplicity);
            AppendTableRow(table, "Кратность ПО", multiplicityStr);
            string liquorTypeStr = App.DataAccess.QuenchingSolutionRepository.GetLiquorTypeStr(_document.QuenchingSolution.LiquorType);
            AppendTableRow(table, "Тип раствора", liquorTypeStr);
            AppendTableRow(table, "Объем цистерны", _document.QuenchingSolution.CisternVolume + " л");
            AppendTableRow(table, "Объем ПО", _document.QuenchingSolution.VolumePO + " л");
            AppendTableRow(table, "Фактический коэффициент Кф", Convert.ToString(_document.QuenchingSolution.FactKoeff));
            AppendTableRow(table, "Расчетное время тушения", Convert.ToString(_document.QuenchingSolution.EstimateTime) + " мин");
            AppendTableRow(table, "Количество рукавов", Convert.ToString(_document.QuenchingSolution.HoseCount) + " шт.");
            AppendTableRow(table, "Диаметр рукава", Convert.ToString(_document.QuenchingSolution.HoseDiameter) + " мм");
            AppendTableRow(table, "Объем", Convert.ToString(_document.QuenchingSolution.HoseVolume) + " л");
            AppendTableRow(table, "Пенные стволы", _document.QuenchingSolution.FoamBarrels);
            AppendTableRow(table, "Расход", Convert.ToString(_document.QuenchingSolution.Сonsumption) + " л/с");
            AppendTableRow(table, "Количество", Convert.ToString(_document.QuenchingSolution.BarrelsCount) + " шт.");
            AppendTableRow(table, "Кратность пены", Convert.ToString(_document.QuenchingSolution.FoamMultiplicity));
            AppendTableRow(table, "Условие", "Кф" + _document.QuenchingSolution.ConditionMore + "Кв");
            section.AddParagraph();

            AppendParagraph(section, "Расчетные данные", 12f, false);
            table = AppendTable(section, 3);
            AppendTableHeader(table, "Наименование", "Данные", "Формула расчета");
            AppendTableRow(table, "Объем раствора", Convert.ToString(_document.QuenchingSolution.LiquorVolume) + " л", _document.QuenchingSolution.LiquorVolumeFormula);
            AppendTableRow(table, "Объем ВМП", Convert.ToString(_document.QuenchingSolution.VolumeVMP) + " л", _document.QuenchingSolution.VolumeVMPFormula);
            AppendTableRow(table, "Площадь тушения", Convert.ToString(_document.QuenchingSolution.QuenchingArea) + " м2", _document.QuenchingSolution.QuenchingAreaFormula);
            AppendTableRow(table, "Время работы", Convert.ToString(_document.QuenchingSolution.WorkTime) + " мин", _document.QuenchingSolution.WorkTimeFormula);
            section.AddParagraph();
        }

        /// <summary>
        /// Добавление расчетов МЕТОДЫ ПОДАЧИ
        /// </summary>
        private void AppendFilingMethods(WSection section)
        {
            if (_document.FilingMethods.IsWaterSupplyMethod || _document.FilingMethods.IsWaterSupplyToPumpMethod)
            {
                AppendParagraph(section, "5. Методы подачи ОВ к месту пожара", 12f, true);
            }
            if (_document.FilingMethods.IsWaterSupplyMethod)
            {
                AppendParagraph(section, "Подвоз воды", 12f, true);
                AppendParagraph(section, "Данные", 12f, false);
                IWTable table = AppendTable(section, 2);
                AppendTableHeader(table, "Наименование", "Данные");
                AppendTableRow(table, "Расстояние", Convert.ToString(_document.FilingMethods.Distance) + " км");
                AppendTableRow(table, "Скорость движения АЦ", Convert.ToString(_document.FilingMethods.TravelSpeed) + " км/ч");
                AppendTableRow(table, "Объем цистерны", Convert.ToString(_document.FilingMethods.CisterneVolume) + " л");
                AppendTableRow(table, "Средняя подача воды насосом", Convert.ToString(_document.FilingMethods.WaterMediumPumping) + " л/с");
                AppendTableRow(table, "Расход воды по наиболее загруженной линии", Convert.ToString(_document.FilingMethods.LineWaterConsumption) + " л/с");
                section.AddParagraph();

                AppendParagraph(section, "Расчетные данные", 12f, false);
                table = AppendTable(section, 3);
                AppendTableHeader(table, "Наименование", "Данные", "Формула расчета");
                AppendTableRow(table, "Время следования", Convert.ToString(_document.FilingMethods.TravelTime) + " мин", _document.FilingMethods.TravelTimeFormula);
                AppendTableRow(table, "Время заправки АЦ", Convert.ToString(_document.FilingMethods.RefuelingTime) + " мин", _document.FilingMethods.RefuelingTimeFormula);
                AppendTableRow(table, "Время расхода воды от АЦ", Convert.ToString(_document.FilingMethods.WaterConsumptionTime) + " мин", _document.FilingMethods.WaterConsumptionTimeFormula);
                AppendTableRow(table, "Количество АЦ для подвоза", Convert.ToString(_document.FilingMethods.ACCount) + " ед.", _document.FilingMethods.ACCountFormula);

                section.AddParagraph();
            }
            if (_document.FilingMethods.IsWaterSupplyToPumpMethod)
            {
                AppendParagraph(section, "Подача воды в перекачку", 12f, true);
                AppendParagraph(section, "Данные", 12f, false);
                IWTable table = AppendTable(section, 2);
                AppendTableHeader(table, "Наименование", "Данные");
                AppendTableRow(table, "Напор на насосе", Convert.ToString(_document.FilingMethods.PressureOnPump) + " м.вод.ст.");
                AppendTableRow(table, "Высота подъема/спуска местности", Convert.ToString(_document.FilingMethods.AscentDescentHeight) + " м");
                AppendTableRow(table, "Высота подъема/спуска ствола", Convert.ToString(_document.FilingMethods.HeightLiftingLoweringTrunk) + " м");
                AppendTableRow(table, "Расход воды по наиболее загруженной линии", Convert.ToString(_document.FilingMethods.LineWaterConsumption2) + " л/с");
                AppendTableRow(table, "Расстояние до ИППВ", Convert.ToString(_document.FilingMethods.DistanceIPPV) + " м");
                section.AddParagraph();

                AppendParagraph(section, "Расчетные данные", 12f, false);
                table = AppendTable(section, 3);
                AppendTableHeader(table, "Наименование", "Данные", "Формула расчета");
                AppendTableRow(table, "Расстояние до головной ПА", Convert.ToString(_document.FilingMethods.DistanceMainPA) + " рук.", _document.FilingMethods.DistanceMainPAFormula);
                AppendTableRow(table, "Расстояние между ПА", Convert.ToString(_document.FilingMethods.DistanceBetweenPA) + " рук.", _document.FilingMethods.DistanceBetweenPAFormula);
                AppendTableRow(table, "Общее количество рукавов в магистральной линии", Convert.ToString(_document.FilingMethods.TotalNumberHoses) + " рук.", _document.FilingMethods.TotalNumberHosesFormula);
                AppendTableRow(table, "Количество ступеней перекачки", Convert.ToString(_document.FilingMethods.NumberPumpingStages), _document.FilingMethods.NumberPumpingStagesFormula);
                AppendTableRow(table, "Количество ПА для перекачки", Convert.ToString(_document.FilingMethods.NumberPAForPumping) + " ед.", _document.FilingMethods.NumberPAForPumpingFormula);
                AppendTableRow(table, "Фактическое расстояние от головного ПА до разветвления", Convert.ToString(_document.FilingMethods.ActualDistanceFromPAToJunction) + " рук.", _document.FilingMethods.ActualDistanceFromPAToJunctionFormula);
            }
        }

        public ExportWord(Models.Document document)
        {
            Set_document(document ?? throw new ArgumentNullException(nameof(document)));
        }

    }
}
