namespace Logger
{
    internal class Logger : ILogger, IDisposable
    {
        private  string _path;
        private  string? _fileName;
        private MinimalLoggerLevel _level;
        private StreamWriter _writer;

        public Logger(string path, string? fileName, MinimalLoggerLevel level = MinimalLoggerLevel.Debug) {
            _path = path;
            _fileName = fileName;
            _level = level;
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
            return $"{_path}/{_fileName}";
        }
        private bool CheckIfShouldBeLogged(MinimalLoggerLevel level)
        {
            return level >= this._level;
        }
        private void WriteLine(MinimalLoggerLevel level, string message)
        {
            string timestamp = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            string levelName = level.ToString().ToUpper();

            string lineMessage = $"{timestamp}|{levelName}|{message}";

            this._writer.WriteLine(lineMessage);
        }
        #endregion
    }
}
