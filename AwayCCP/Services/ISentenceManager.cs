namespace AwayCCP.Services
{
    public interface ISentenceManager
    {
        void Load(string path);
        bool Next();
        bool Previous();
        string CurrentLine { get; }
    }
}