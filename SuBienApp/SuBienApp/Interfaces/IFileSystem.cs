namespace SuBienApp.Interfaces
{
    public interface IFileSystem
    {
        string DirectoryFile { get; }

        bool ResizeImageAndSave(string pathFrom, string pathTo,string nameFile, float width, float height);

        byte[] GetSizeImage(string pathFrom);
        
    }
}
