using System;
using Microsoft.Win32;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    /// <summary>
    /// Provides global static access to runtime session data and system configuration.
    /// Manages client-side credential caching using the Windows Registry for the "Remember Me" feature.
    /// </summary>
    public static class clsGlobal
    {
        /// <summary>
        /// Holds the authenticated user entity in memory for the duration of the application lifecycle.
        /// Consumed globally by UI components and business rules to verify permissions and track audits.
        /// </summary>
        public static clsUser CurrentUser { get; set; }

        /// <summary>
        /// The registry key path under HKEY_CURRENT_USER designated for storing local application settings.
        /// </summary>
        private const string RegistryKeyPath = @"HKEY_CURRENT_USER\SOFTWARE\LibrarySystem";

        /// <summary>
        /// Persists user credentials into the Windows Registry under the current user's hive.
        /// Executed when the user successfully authenticates with the "Remember Me" option checked.
        /// </summary>
        /// <param name="username">The username to store.</param>
        /// <param name="password">The password to store.</param>
        /// <returns>True if the registry write operation succeeds; otherwise, false.</returns>
        public static bool RememberUsernameAndPassword(string username, string password)
        {
            try
            {
                // Registry.SetValue automatically creates the subkey if it does not already exist.
                // We use RegistryValueKind.String to enforce explicit string data typing in the registry hive.
                Registry.SetValue(RegistryKeyPath, "Username", username, RegistryValueKind.String);
                Registry.SetValue(RegistryKeyPath, "Password", password, RegistryValueKind.String);

                return true;
            }
            catch (Exception)
            {
                // In production, log the exception details to the Windows Event Log or an audit file.
                return false;
            }
        }

        /// <summary>
        /// Retrieves previously cached credentials from the Windows Registry to pre-populate the login form.
        /// </summary>
        /// <param name="username">Output parameter receiving the retrieved username.</param>
        /// <param name="password">Output parameter receiving the retrieved password.</param>
        /// <returns>True if valid non-empty credentials were found; otherwise, false.</returns>
        public static bool GetStoredCredential(ref string username, ref string password)
        {
            try
            {
                // Read stored values from the registry; returns null if the key or value does not exist.
                username = Registry.GetValue(RegistryKeyPath, "Username", null) as string;
                password = Registry.GetValue(RegistryKeyPath, "Password", null) as string;

                // Validate that both retrieved fields contain meaningful data.
                return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes stored credential values from the Windows Registry when the user unchecks "Remember Me".
        /// </summary>
        /// <returns>True if entries were successfully cleared or did not exist; otherwise, false.</returns>
        public static bool ClearStoredCredentials()
        {
            try
            {
                // Open the base registry hive with default 32/64-bit view compatibility.
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default))
                {
                    // Open the target application subkey with write permissions (writable: true).
                    using (RegistryKey subKey = baseKey.OpenSubKey(@"SOFTWARE\LibrarySystem", true))
                    {
                        if (subKey != null)
                        {
                            // Passing false as the second argument suppresses exceptions if the value does not exist.
                            subKey.DeleteValue("Username", false);
                            subKey.DeleteValue("Password", false);
                        }
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}