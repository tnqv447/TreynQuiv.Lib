using System.Security.Cryptography;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TreynQuiv.Lib.Cryptography;

/// <summary>
/// A wrapper around <see cref="Aes"/> for the ease of use.
/// </summary>
public static class AesCrypto
{
    /// <summary>
    /// Encrypts plain text using <see cref="Aes"/> algorithm.
    /// <para>If <paramref name="iv"/> is specified, the encryption will use CBC mode, otherwise ECB mode.</para>
    /// </summary>
    /// <returns>Base64 encrypted string</returns>
    /// <exception cref="ArgumentException" />
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="ArgumentOutOfRangeException" />
    /// <exception cref="CryptographicException" />
    public static string Encrypt(string plainText, byte[] key, byte[]? iv = null)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        using var aes = Aes.Create();
        aes.Key = MD5.HashData(key);
        var encrypted = iv is null
            ? aes.EncryptEcb(plainTextBytes, PaddingMode.PKCS7)
            : aes.EncryptCbc(plainTextBytes, MD5.HashData(iv), PaddingMode.PKCS7);
        return Convert.ToBase64String(encrypted);
    }

    /// <summary>
    /// Overload from <see cref="Encrypt"/> with <paramref name="key"/>as a Base64 encoded string.
    /// <para>If <paramref name="iv"/> is specified, the encryption will use CBC mode, otherwise ECB mode.</para>
    /// </summary>
    /// <returns>Base64 encrypted string</returns>
    /// <exception cref="ArgumentException" />
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="ArgumentOutOfRangeException" />
    /// <exception cref="CryptographicException" />
    public static string Encrypt(string plainText, [Base64String] string key, byte[]? iv = null)
    {
        var keyBytes = Convert.FromBase64String(key);
        return Encrypt(plainText, keyBytes, iv);
    }

    /// <summary>
    /// Overload from <see cref="Encrypt"/> with both <paramref name="key"/> and <paramref name="iv"/>  as Base64 encoded strings
    /// <para>If <paramref name="iv"/> is specified, the encryption will use CBC mode, otherwise ECB mode.</para>
    /// </summary>
    /// <returns>Base64 encrypted string</returns>
    /// <exception cref="ArgumentException" />
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="ArgumentOutOfRangeException" />
    /// <exception cref="CryptographicException" />
    public static string Encrypt(string plainText, [Base64String] string key, [Base64String] string? iv = null)
    {
        var ivBytes = iv is null ? null : Convert.FromBase64String(iv);
        return Encrypt(plainText, key, ivBytes);
    }

    /// <summary>
    /// Decrypts a <see cref="System.Security.Cryptography.Aes"/> encrypted, Base64 encoded string using <see cref="System.Security.Cryptography.Aes"/> algorithm.
    /// <para>If <paramref name="iv"/> is specified, the decryption will use CBC mode, otherwise ECB mode.</para>
    /// </summary>
    /// <returns>UTF-8 encoded decrypted string</returns>
    /// <exception cref="ArgumentException" />
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="ArgumentOutOfRangeException" />
    /// <exception cref="CryptographicException" />
    public static string Decrypt([Base64String] string encrypted, byte[] key, byte[]? iv = null)
    {
        var encryptedBytes = Convert.FromBase64String(encrypted);

        using var aes = Aes.Create();
        aes.Key = MD5.HashData(key);
        var plainText = iv is null
            ? aes.DecryptEcb(encryptedBytes, PaddingMode.PKCS7)
            : aes.DecryptCbc(encryptedBytes, MD5.HashData(iv), PaddingMode.PKCS7);
        return Encoding.UTF8.GetString(plainText);
    }

    /// <summary>
    /// Overload from <see cref="Decrypt"/> with <paramref name="key"/>as a Base64 encoded string.
    /// <para>If <paramref name="iv"/> is specified, the decryption will use CBC mode, otherwise ECB mode.</para>
    /// </summary>
    /// <returns>UTF-8 encoded decrypted string</returns>
    /// <exception cref="ArgumentException" />
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="ArgumentOutOfRangeException" />
    /// <exception cref="CryptographicException" />
    public static string Decrypt([Base64String] string encrypted, [Base64String] string key, byte[]? iv = null)
    {
        var keyBytes = Convert.FromBase64String(key);
        return Decrypt(encrypted, keyBytes, iv);
    }

    /// <summary>
    /// Overload from <see cref="Decrypt"/> with both <paramref name="key"/> and <paramref name="iv"/> as Base64 encoded strings.
    /// <para>If <paramref name="iv"/> is specified, the decryption will use CBC mode, otherwise ECB mode.</para>
    /// </summary>
    /// <returns>UTF-8 encoded decrypted string</returns>
    /// <exception cref="ArgumentException" />
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="ArgumentOutOfRangeException" />
    /// <exception cref="CryptographicException" />
    public static string Decrypt([Base64String] string encrypted, [Base64String] string key, [Base64String] string? iv = null)
    {
        var ivBytes = iv is null ? null : Convert.FromBase64String(iv);
        return Decrypt(encrypted, key, ivBytes);
    }
}
