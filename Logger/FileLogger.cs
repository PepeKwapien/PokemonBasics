using System.IO;

namespace Logger
{
    public class FileLogger : ILogger, IDisposable
    {
        private string _path;
        private string? _fileName;
        private readonly MinimalLoggerLevel _level;
        private string _delimiter;
        private StreamWriter? _writer;

        public FileLogger(string path, string? fileName = null, MinimalLoggerLevel level = MinimalLoggerLevel.Debug, string delimiter = "|") {
            _path = path;
            _fileName = fileName;
            _level = level;
            _delimiter = delimiter;
        }

        #region Properties
        public string Path
        {
            get { return _path; }
            set
            {
                CloseIfOpen();
                value = value.Replace("/", "\\");
                this._path = value;
            }
        }
        public string? FileName
        {
            get { return _fileName; }
            set
            {
                CloseIfOpen();
                this._fileName = value;
            }
        }
        public MinimalLoggerLevel Level { get { return _level; } }
        public string Delimiter { get { return _delimiter; } set { this._delimiter = value;  } }
        #endregion
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

        public void Dispose()
        {
            if(this._writer!= null)
            {
                try
                {
                    this._writer.Flush();
                    this._writer.Dispose();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        #region Private Methods
        private void CloseIfOpen()
        {
            if (this._writer != null && this._writer.BaseStream != null)
            {
                try
                {
                    this._writer.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        private void OpenIfClosed()
        {
            if(this._writer == null || this._writer.BaseStream == null)
            {
                try
                {
                    _writer = new StreamWriter(GetPathForWriter(), true);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
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

            string lineMessage = $"{timestamp}{_delimiter}{levelName}{_delimiter}{message}";

            this._writer?.WriteLine(lineMessage);
        }
        #endregion
    }
}
