﻿namespace DataExportLib
{
    public interface ILogger
    {
        void Warn(string message);

        void Error(string message);

        void Fatal(string message);

        void Debug(string message);

        void Info(string message);
    }
}
