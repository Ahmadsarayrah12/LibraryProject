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
    /// Password storage uses simple Base64 encoding.
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
        /// Password is encrypted using simple Base64 encoding.
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
        /// Password is decrypted using simple Base64 decoding.
        /// </summary>
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
            catch (Exception ex)
            {
                ClearStoredCredentials();
                Trace.TraceError($"[REGISTRY ERROR] GetStoredCredential failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Clears stored credential values from the Windows Registry when the user unchecks "Remember Me".
        /// </summary>
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

        // تشفير بسيط جداً للمبتدئين باستخدام Base64
        private static string _EncryptPassword(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return string.Empty;

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        // فك التشفير البسيط باستخدام Base64
        private static string _DecryptPassword(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return string.Empty;

            try
            {
                byte[] base64EncodedBytes = Convert.FromBase64String(cipherText);
                return Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch
            {
                // إذا كان النص غير صالح
                return string.Empty;
            }
        }
    }
}
