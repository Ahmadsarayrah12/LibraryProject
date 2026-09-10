using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    /// <summary>
    /// Provides global static access to runtime session context, active user credentials,
    /// and local machine registry persistence for client configuration.
    /// Password storage uses Windows DPAPI encryption for security.
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
        /// Password is encrypted using Windows DPAPI (DataProtectionScope.CurrentUser).
        /// </summary>
        /// <param name="username">The username to store.</param>
        /// <param name="password">The password to store (will be encrypted before writing).</param>
        /// <returns>True if the registry write operation succeeds; otherwise, false.</returns>
        public static bool RememberUsernameAndPassword(string username, string password)
        {
            try
            {
                Registry.SetValue(RegistryKeyPath, "Username", username ?? string.Empty, RegistryValueKind.String);

                // Encrypt password using Windows DPAPI before storing
                string encryptedPassword = _EncryptPassword(password ?? string.Empty);
                Registry.SetValue(RegistryKeyPath, "Password", encryptedPassword, RegistryValueKind.String);

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
        /// Password is decrypted using Windows DPAPI.
        /// </summary>
        /// <param name="username">Output parameter receiving the retrieved username.</param>
        /// <param name="password">Output parameter receiving the decrypted password.</param>
        /// <returns>True if valid non-empty credentials were found; otherwise, false.</returns>
        public static bool GetStoredCredential(ref string username, ref string password)
        {
            try
            {
                username = Registry.GetValue(RegistryKeyPath, "Username", null) as string;
                string encryptedPassword = Registry.GetValue(RegistryKeyPath, "Password", null) as string;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(encryptedPassword))
                    return false;

                password = _DecryptPassword(encryptedPassword);
                return !string.IsNullOrEmpty(password);
            }
            catch (CryptographicException)
            {
                // Encrypted data is corrupt or was created by a different user context — clear it
                ClearStoredCredentials();
                return false;
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

        #region DPAPI Helpers

        /// <summary>
        /// Encrypts a plaintext password using Windows DPAPI and returns a Base64-encoded string.
        /// </summary>
        private static string _EncryptPassword(string plainText)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedBytes = ProtectedData.Protect(plainBytes, null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedBytes);
        }

        /// <summary>
        /// Decrypts a Base64-encoded DPAPI-protected string back to plaintext.
        /// </summary>
        private static string _DecryptPassword(string encryptedBase64)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedBase64);
            byte[] plainBytes = ProtectedData.Unprotect(encryptedBytes, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(plainBytes);
        }

        #endregion
    }
}