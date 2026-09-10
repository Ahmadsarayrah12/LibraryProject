using System;
using System.Diagnostics;
using Microsoft.Win32;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    /// <summary>
    /// Provides global static access to runtime session context, active user credentials,
    /// and local machine registry persistence for client configuration.
    /// </summary>
    public static class clsGlobal
    {
        /// <summary>
        /// Holds the authenticated user entity in memory for the duration of the application session.
        /// </summary>
        public static clsUser CurrentUser { get; set; }

        /// <summary>
        /// The registry key path under HKEY_CURRENT_USER designated for storing local application settings.
        /// </summary>
        private const string RegistryKeyPath = @"HKEY_CURRENT_USER\SOFTWARE\LibrarySystem";

        /// <summary>
        /// Persists user credentials into the Windows Registry under the current user's hive.
        /// </summary>
        /// <param name="username">The username to store.</param>
        /// <param name="password">The password to store.</param>
        /// <returns>True if the registry write operation succeeds; otherwise, false.</returns>
        public static bool RememberUsernameAndPassword(string username, string password)
        {
            try
            {
                Registry.SetValue(RegistryKeyPath, "Username", username ?? string.Empty, RegistryValueKind.String);
                Registry.SetValue(RegistryKeyPath, "Password", password ?? string.Empty, RegistryValueKind.String);

                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError($"[REGISTRY ERROR] RememberUsernameAndPassword failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Retrieves previously cached credentials from the Windows Registry to pre-populate login input.
        /// </summary>
        /// <param name="username">Output parameter receiving the retrieved username.</param>
        /// <param name="password">Output parameter receiving the retrieved password.</param>
        /// <returns>True if valid non-empty credentials were found; otherwise, false.</returns>
        public static bool GetStoredCredential(ref string username, ref string password)
        {
            try
            {
                username = Registry.GetValue(RegistryKeyPath, "Username", null) as string;
                password = Registry.GetValue(RegistryKeyPath, "Password", null) as string;

                return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"[REGISTRY ERROR] GetStoredCredential failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Clears stored credential values from the Windows Registry when the user unchecks "Remember Me".
        /// </summary>
        /// <returns>True if entries were successfully cleared; otherwise, false.</returns>
        public static bool ClearStoredCredentials()
        {
            try
            {
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default))
                {
                    using (RegistryKey subKey = baseKey.OpenSubKey(@"SOFTWARE\LibrarySystem", true))
                    {
                        if (subKey != null)
                        {
                            subKey.DeleteValue("Username", false);
                            subKey.DeleteValue("Password", false);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError($"[REGISTRY ERROR] ClearStoredCredentials failed: {ex.Message}");
                return false;
            }
        }
    }
}