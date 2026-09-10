using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace LibrarySystem.DataAccess
{
    /// <summary>
    /// Provides lightweight enterprise logging capabilities for data access operations.
    /// Acts as an extensible diagnostic placeholder for audit logs, Event Viewer, or distributed loggers.
    /// </summary>
    public static class clsDataLogger
    {
        /// <summary>
        /// Records an exception occurred during a database operation.
        /// </summary>
        /// <param name="ex">The captured exception.</param>
        /// <param name="source">The originating method or component name.</param>
        public static void LogError(Exception ex, string source)
        {
            // Extensible placeholder: writes diagnostic traces and can be hooked to Windows EventLog
            Trace.TraceError($"[DAL ERROR] Source: {source} | Time: {DateTime.UtcNow:O} | Message: {ex.Message} | StackTrace: {ex.StackTrace}");
        }
    }

    /// <summary>
    /// Extension helper methods for robust ADO.NET data extraction, DBNull coercion, and parameter bindings.
    /// </summary>
    public static class clsSqlHelper
    {
        /// <summary>
        /// Safely retrieves a string value from an active SqlDataReader, coercing DBNull to a default string.
        /// </summary>
        /// <param name="reader">The active SqlDataReader instance.</param>
        /// <param name="columnName">The database column name.</param>
        /// <param name="defaultValue">The fallback value when column is DBNull (default is empty string).</param>
        /// <returns>Extracted string or fallback value.</returns>
        public static string SafeGetString(this SqlDataReader reader, string columnName, string defaultValue = "")
        {
            int ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? defaultValue : reader.GetString(ordinal);
        }

        /// <summary>
        /// Safely retrieves an integer value from an active SqlDataReader, coercing DBNull to a default integer.
        /// </summary>
        /// <param name="reader">The active SqlDataReader instance.</param>
        /// <param name="columnName">The database column name.</param>
        /// <param name="defaultValue">The fallback integer when column is DBNull (default is -1).</param>
        /// <returns>Extracted integer or fallback value.</returns>
        public static int SafeGetInt(this SqlDataReader reader, string columnName, int defaultValue = -1)
        {
            int ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? defaultValue : Convert.ToInt32(reader.GetValue(ordinal));
        }

        /// <summary>
        /// Safely retrieves a boolean value from an active SqlDataReader, coercing DBNull to a default boolean.
        /// </summary>
        /// <param name="reader">The active SqlDataReader instance.</param>
        /// <param name="columnName">The database column name.</param>
        /// <param name="defaultValue">The fallback boolean when column is DBNull (default is false).</param>
        /// <returns>Extracted boolean or fallback value.</returns>
        public static bool SafeGetBool(this SqlDataReader reader, string columnName, bool defaultValue = false)
        {
            int ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? defaultValue : Convert.ToBoolean(reader.GetValue(ordinal));
        }

        /// <summary>
        /// Safely retrieves a DateTime value from an active SqlDataReader, coercing DBNull to a default DateTime.
        /// </summary>
        /// <param name="reader">The active SqlDataReader instance.</param>
        /// <param name="columnName">The database column name.</param>
        /// <param name="defaultValue">The fallback DateTime when column is DBNull.</param>
        /// <returns>Extracted DateTime or fallback value.</returns>
        public static DateTime SafeGetDateTime(this SqlDataReader reader, string columnName, DateTime defaultValue = default)
        {
            int ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? defaultValue : reader.GetDateTime(ordinal);
        }

        /// <summary>
        /// Safely retrieves a decimal value from an active SqlDataReader, coercing DBNull to a default decimal.
        /// </summary>
        /// <param name="reader">The active SqlDataReader instance.</param>
        /// <param name="columnName">The database column name.</param>
        /// <param name="defaultValue">The fallback decimal value (default is 0.0m).</param>
        /// <returns>Extracted decimal or fallback value.</returns>
        public static decimal SafeGetDecimal(this SqlDataReader reader, string columnName, decimal defaultValue = 0m)
        {
            int ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? defaultValue : Convert.ToDecimal(reader.GetValue(ordinal));
        }

        /// <summary>
        /// Adds a SqlParameter handling string null/whitespace as DBNull.Value.
        /// </summary>
        /// <param name="parameters">The command's SqlParameterCollection.</param>
        /// <param name="parameterName">The SQL parameter name (including @ prefix).</param>
        /// <param name="value">The string value to bind.</param>
        public static void AddWithNullableString(this SqlParameterCollection parameters, string parameterName, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                parameters.AddWithValue(parameterName, DBNull.Value);
            else
                parameters.AddWithValue(parameterName, value.Trim());
        }

        /// <summary>
        /// Adds a SqlParameter handling null object references as DBNull.Value.
        /// </summary>
        /// <param name="parameters">The command's SqlParameterCollection.</param>
        /// <param name="parameterName">The SQL parameter name (including @ prefix).</param>
        /// <param name="value">The object value to bind.</param>
        public static void AddWithValueOrNull(this SqlParameterCollection parameters, string parameterName, object value)
        {
            parameters.AddWithValue(parameterName, value ?? DBNull.Value);
        }
    }
}
