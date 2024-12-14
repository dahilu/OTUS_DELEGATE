
public class FileSearcher
{
    public event EventHandler<FileArgs>? FileFound;

    private bool _cancelSearch;

    public void SearchDirectory(string directory)
    {
        _cancelSearch = false;
        Search(directory);
    }

    public void CancelSearch()
    {
        _cancelSearch = true;
    }

    private void Search(string directory)
    {
        if (_cancelSearch)
            return;

        try
        {
            foreach (var file in Directory.GetFiles(directory))
            {
                OnFileFound(file);

                Thread.Sleep(500);

                if (_cancelSearch)
                    return;
            }

            foreach (var subDirectory in Directory.GetDirectories(directory))
            {
                Search(subDirectory);
                if (_cancelSearch)
                    return;
            }
        }
        catch (UnauthorizedAccessException)
        {
            // Обработка исключений доступа
        }
    }

    protected virtual void OnFileFound(string file)
    {
        FileFound?.Invoke(this, new FileArgs(file));
    }
}
