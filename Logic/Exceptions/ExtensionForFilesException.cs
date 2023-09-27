using System.Net;

namespace Logic.Exceptions;

public class ExtensionForFilesException:BaseException
{
    public ExtensionForFilesException() : base("Формат файла должен быть png, jpg или jpeg", HttpStatusCode.NotFound)
    {
    }
}