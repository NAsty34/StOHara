namespace Data.Model.Options;

public class FilesOptions
{
    public const string Files = "Files";
    public string EntityIdGallery { get; set; }
    public string PathFolder { get; set; }
    public string RootPath { get; set; }
    public string EntityIdMenu { get; set; }
    public string[] Extension { get; set; }
    
}