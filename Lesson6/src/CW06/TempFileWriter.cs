namespace CW06;

public sealed class TempFileWriter : IDisposable
{
    private readonly StreamWriter _writer;
    private bool _disposed;

    public string FilePath { get; }

    public TempFileWriter()
    {
        FilePath = Path.GetTempFileName();
        // StreamWriter will close the underlying stream on Dispose()
        _writer = new StreamWriter(new FileStream(FilePath, FileMode.Append, FileAccess.Write, FileShare.Read));
    }

    public void WriteLine(string line)
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(TempFileWriter));

        _writer.WriteLine(line);
        _writer.Flush();
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        _writer.Dispose();
    }
}
