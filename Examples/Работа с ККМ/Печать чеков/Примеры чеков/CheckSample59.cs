using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class CheckSample59 : Sample
{
    public const string GroupPath = "Работа с ККМ|Печать чеков|Примеры чеков";
    public const string Title = "Указание отраслевого реквизита";

    public async Task<SkkmConnector> PostCheckSample59()
    {
        connector.DeviceName = deviceName;
        connector.SetCashier(cashierName, cashierVatin);
        connector.SetTaxType(TaxSystem.Osn);
        connector.NewCheck(CheckType.Sale);
        var line1 = connector.AddPosition("Молоко 1л.", 1m, 101.0m, TaxRate.Vat0, signCalculationObject: SignCalculationObject.ТМ, signMethodCalculation: SignMethodCalculation.FullPayment);
        line1.Price = 101m;
        line1.TaxSum = 0m;
        line1.MeasureOfQuantity = MeasureOfQuantity.Piece;
        line1.Marking = "MDEwNDYwNzAxMDM1MDI0NjIxNURzPkpSak5FIWpaIR05M2RHVno=";
        line1.IndustryIdentifierFoiv = "030";
        line1.IndustryAttributeDocumentDate = "21.11.2023";
        line1.IndustryAttributeDocumentNumber = "1944";
        line1.IndustryAttributeValue = "UUID=8f3a9d1c-7e2b-4a5f-9c8d-1e2f3a4b5c6d&Time=1746530733410";
        connector.Cash = 101.0m;
        await connector.PrintCheck();

        if (connector.Ok)
            Console.WriteLine(
                "Чек пробит.\n" +
                $"Время на сервере ККМ: {connector.ServerDateTime}\n" +
                $"Имя устройства: {connector.DeviceName}\n" +
                $"Идентификатор документа (docId): {connector.DocumentId}\n" +
                $"Фискальный признак документа: {connector.FiscalSign}\n" +
                $"Номер фискального документа (ФД): {connector.CheckNumber}\n" +
                $"Номер смены: {connector.ShiftNumber}\n" +
                $"Номер ФН: {connector.FnNumber}\n" +
                $"Регистрационный номер ККТ (РНМ): {connector.RnNumber}\n" +
                $"Адрес сайта ФНС: {connector.FnsUrl}\n" +
                $"Дата и время документа по часам ФН: {connector.FiscalDateTime}");
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
