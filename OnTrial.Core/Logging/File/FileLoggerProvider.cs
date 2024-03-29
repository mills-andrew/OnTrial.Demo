﻿using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace OnTrial
{
    /// <summary>
    /// Provides the ability to log to file
    /// </summary>
    public class FileLoggerProvider : ILoggerProvider
    {
        #region Protected Members

        /// <summary>
        /// The path to log to
        /// </summary>
        protected string mFilePath;

        /// <summary>
        /// The configuration to use when creating loggers
        /// </summary>
        protected readonly FileLoggerConfiguration mConfiguration;

        /// <summary>
        /// Keeps track of the loggers already created
        /// </summary>
        protected readonly ConcurrentDictionary<string, FileLogger> mLoggers = new ConcurrentDictionary<string, FileLogger>();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="pPath">The path to log to</param>
        /// <param name="pConfiguration">The configuration to use</param>
        public FileLoggerProvider(string pPath, FileLoggerConfiguration pConfiguration)
        {
            // Set the configuration
            mConfiguration = pConfiguration;

            // Set the path
            mFilePath = pPath;
        }

        #endregion

        #region ILoggerProvider Implementation

        /// <summary>
        /// Creates a file logger based on the category name
        /// </summary>
        /// <param name="pCategoryName">The category name of this logger</param>
        /// <returns></returns>
        public ILogger CreateLogger(string pCategoryName)
        {
            // Get or create the logger for this category
            return mLoggers.GetOrAdd(pCategoryName, name => new FileLogger(name, mFilePath, mConfiguration));
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose() =>
            mLoggers.Clear();

        #endregion
    }
}