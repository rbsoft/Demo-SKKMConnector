using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class Check : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков";
    public const string Title = "Чек по id";
    public const bool NeedDocumentId = true;
    public const int SortOrder = 4;

    public async Task<SkkmConnector> GetCheck()
    {
        connector.DeviceName = deviceName;
        await connector.GetCheck(documentId);

        if (connector.Ok)
        {
            Console.WriteLine(
                "Чек получен.\n" +
                $"Идентификатор документа (docId): {connector.DocumentId}\n" +
                $"Фискальный признак документа: {connector.FiscalSign}\n" +
                $"Номер фискального документа (ФД): {connector.CheckNumber}\n" +
                $"Номер чека за смену: {connector.CheckNumberInShift}\n" +
                $"Номер смены: {connector.ShiftNumber}\n" +
                $"Сумма чека: {connector.CheckSum}\n" +
                $"Сдача: {connector.CheckChange}\n" +
                $"Дата по часам ФН: {connector.CheckFiscalDate:dd.MM.yyyy HH:mm}\n" +
                $"Кассир: {connector.CheckCashierName}, ИНН: {connector.CheckCashierVatin}\n" +
                $"СНО: {connector.CheckTaxType}\n" +
                $"Версия ФФД: {connector.CheckFfdVersion}\n" +
                $"Замена ставки НДС: {connector.CheckIsReplaceTax}\n" +
                $"Позиций в чеке: {connector.CheckItemsCount}\n" +
                $"Покупатель: {connector.CustomerDetailInfo}\n" +
                $"ИНН покупателя: {connector.CustomerDetailInn}\n" +
                $"Эл. почта покупателя: {connector.CustomerDetailEmail}\n" +
                $"Оплата наличными: {connector.PaymentsCash}\n" +
                $"Оплата безналичными: {connector.PaymentsElectronic}\n" +
                $"Предоплата: {connector.PaymentsPrePaid}\n" +
                $"Постоплата: {connector.PaymentsCredit}\n" +
                $"Встречное предоставление: {connector.PaymentsBarter}\n" +
                $"QR — сумма: {connector.QrDataAmount}, ФН: {connector.QrDataFn}, ФД: {connector.QrDataFd}, ФП: {connector.QrDataFp}\n" +
                $"Организация: {connector.OrganizationInfo}\n" +
                $"ИНН организации: {connector.OrganizationVatin}\n" +
                $"Регистрационный номер ККТ: {connector.RnNumber}\n" +
                $"Номер ФН: {connector.FnNumber}\n" +
                $"Адрес сайта ФНС: {connector.FnsUrl}\n" +
                $"ОФД: {connector.OfdName}\n" +
                $"ИНН ОФД: {connector.OfdVatin}\n" +
                $"Модель ККТ: {connector.DeviceModel}\n" +
                $"Заводской номер: {connector.DeviceSerialNumber}\n" +
                $"Версия прошивки: {connector.DeviceFirmwareVersion}");

            // Позиции чека — список, перебираем вручную
            foreach (var item in connector.CheckItems)
                {
                    Console.WriteLine(
                        $"  {item.Name}: кол-во={item.Quantity}, цена={item.Price}, сумма={item.Sum}\n" +
                        $"    Ставка НДС: {item.TaxValue}, сумма НДС: {item.TaxSum}\n" +
                        $"    Признак предмета расчёта: {item.SignCalculationObject}\n" +
                        $"    Признак способа расчёта: {item.SignMethodCalculation}\n" +
                        $"    Отдел: {item.Department}, мера количества: {item.MeasureOfQuantity}, акциз: {item.ExciseAmount}\n" +
                        $"    Код маркировки: {item.MarkingCode}");
                    if (item.ImcCheckFlag || !string.IsNullOrEmpty(item.IndustryAttributeValue))
                        Console.WriteLine(
                            $"    Проверка КМ: результат={item.ImcCheckResult}, статус ОК={item.ImcStatusInfo}, план. статус ОК={item.ImcEstimatedStatusCorrect}\n" +
                            $"    Отраслевой реквизит: ФОИВ={item.IndustryIdentifierFoiv}, док={item.IndustryDocumentNumber} от {item.IndustryDocumentDate}, значение={item.IndustryAttributeValue}");
                }
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}