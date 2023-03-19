namespace Logger
{
    public class FileLogger : ILogger, IDisposable
    {
        private  string _path;
        private  string? _fileName;
        private MinimalLoggerLevel _level;
        private readonly string _delimeter;
        private StreamWriter _writer;

        public FileLogger(string path, string? fileName = null, MinimalLoggerLevel level = MinimalLoggerLevel.Debug, string delimeter = "|") {
            _path = path;
            _fileName = fileName;
            _level = level;
            _delimeter = delimeter;
            _writer = new StreamWriter(GetPathForWriter(), true);
        }

        #region Methods to write messages
        public bool Debug(string message)
        {
            MinimalLoggerLevel levelForMethod = MinimalLoggerLevel.Debug;

            if (!CheckIfShouldBeLogged(levelForMethod))
            {
                return false;
            }

            OpenIfClosed();
            WriteLine(levelForMethod, message);

            return true;
        }
        public bool Info(string message)
        {
            MinimalLoggerLevel levelForMethod = MinimalLoggerLevel.Info;

            if (!CheckIfShouldBeLogged(levelForMethod))
            {
                return false;
            }

            OpenIfClosed();
            WriteLine(levelForMethod, message);

            return true;
        }
        public bool Warn(string message)
        {
            MinimalLoggerLevel levelForMethod = MinimalLoggerLevel.Warn;

            if (!CheckIfShouldBeLogged(levelForMethod))
            {
                return false;
            }

            OpenIfClosed();
            WriteLine(levelForMethod, message);

            return true;
        }
        public bool Success(string message)
        {
            MinimalLoggerLevel levelForMethod = MinimalLoggerLevel.Success;

            if (!CheckIfShouldBeLogged(levelForMethod))
            {
                return false;
            }

            OpenIfClosed();
            WriteLine(levelForMethod, message);

            return true;
        }
        public bool Error(string message)
        {
            MinimalLoggerLevel levelForMethod = MinimalLoggerLevel.Error;

            if (!CheckIfShouldBeLogged(levelForMethod))
            {
                return false;
            }

            OpenIfClosed();
            WriteLine(levelForMethod, message);

            return true;
        }
        #endregion
        #region Setters
        public void SetFileName(string fileName)
        {
            CloseIfOpen();
            this._fileName = fileName;
        }
        public void SetPath(string path)
        {
            CloseIfOpen();
            path = path.Replace("/", "\\");
            this._path= path;
        }
        #endregion

        public void Dispose()
        {
            if(this._writer!= null)
            {
                this._writer.Flush();
                this._writer.Dispose();
            }
        }

        #region Private Methods
        private void CloseIfOpen()
        {
            if (this._writer != null && this._writer.BaseStream != null)
            {
                this._writer.Close();
            }
        }
        private void OpenIfClosed()
        {
            if(this._writer == null || this._writer.BaseStream == null)
            {
                this._writer = new StreamWriter(GetPathForWriter(), true);
            }
        }
        private string GetPathForWriter()
        {
            return $"{_path}\\{GetFileNameWithDate()}";
        }
        private string GetFileNameWithDate()
        {
            string logDate = DateTime.Now.ToString("yyyy-MM-dd");


            return String.IsNullOrEmpty(_fileName) ? $"{logDate}.log" : $"{logDate}-{this._fileName}.log";
        }
        private bool CheckIfShouldBeLogged(MinimalLoggerLevel level)
        {
            return level >= this._level;
        }
        private void WriteLine(MinimalLoggerLevel level, string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string levelName = level.ToString().ToUpper();

            string lineMessage = $"{timestamp}{_delimeter}{levelName}{_delimeter}{message}";

            this._writer.WriteLine(lineMessage);
        }
        #endregion
    }
}
