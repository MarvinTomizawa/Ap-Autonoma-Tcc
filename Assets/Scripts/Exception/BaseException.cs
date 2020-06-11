public class BaseException
{
    public static string FieldNotSetted(string fieldName, string objectName)
    {
        return $"O campo {fieldName} não foi preenchido no objeto {objectName}.";
    }

    public static string FieldNotInScene(string fieldName)
    {
        return $"O campo {fieldName} não existe na cena.";
    }
}
