using RBSoftSkkm;

namespace SkkmNugetSample.Examples;

public class GetOperationKm : Sample
{
    public const string GroupPath = "Работа с ККМ|Операции";
    public const string Title = "Коды маркировки операции";
    public const bool NeedDocumentId = true;
    public const int SortOrder = 4;

    public async Task<SkkmConnector> GetGetOperationKm()
    {
        await connector.GetOperationKm(documentId);
        if (connector.Ok)
        {
            Console.WriteLine($"Кодов маркировки: {connector.OperationKm.Length}");
            foreach (var km in connector.OperationKm)
                Console.WriteLine(
                    $"  {km.PositionName}: {km.Cis}\n" +
                    $"    статус проверки={km.CheckStatus} ({km.Message}), проверен={km.CheckedAt:dd.MM.yyyy HH:mm:ss}\n" +
                    $"    способ проверки={km.KmVerificationMethod}, инициатор={km.KmCheckInitiator}\n" +
                    $"    печатное представление={km.PrintView}, цена продажи={km.SalePrice}, идентификатор={km.MarkId}\n" +
                    $"    касса={km.DeviceName}, документы={string.Join(", ", km.DocIds ?? [])}");
        }
        else
            Console.WriteLine($"Ошибка [{connector.ErrorCode}]: {connector.ErrorDescription}");

        return connector;
    }
}
