﻿using Logger;

namespace ExternalApiHandler.Options
{
    public class LoggerOptions
    {
        public MinimalLoggerLevel LoggerLevel { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
}
