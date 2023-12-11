
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace TreynQuiv.Lib.Crypto;

/// <summary>
/// A wrapper around <see cref="RSA"/> for the ease of use.
/// </summary>
/// <remarks>Default <see cref="EncryptionOaepPadding"/> is <see cref="RSAEncryptionPadding.OaepSHA256"/>.</remarks>
/// <remarks>Default <see cref="HashAlgorithmName"/> is <see cref="HashAlgorithmName.SHA256"/>.</remarks>
public class RsaCrypto
{
    private readonly X509Certificate2 _cert;
    public RSAEncryptionPadding EncryptionOaepPadding { get; init; } = RSAEncryptionPadding.OaepSHA256;
    public HashAlgorithmName HashAlgorithmName { get; init; } = HashAlgorithmName.SHA256;

    /// <summary>
    /// Initializes a new instance of the <see cref="RsaCrypto"/> class using a certificate file name and a password used to access the certificate.
    /// </summary>
    /// <exception cref="CryptographicException" />
    public RsaCrypto(string certificatePath, string? certificatePassword = null)
    {
        _cert = new(certificatePath, certificatePassword);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RsaCrypto"/> class using a <see cref="X509Certificate2"/> instance.
    /// </summary>
    public RsaCrypto(X509Certificate2 certificate)
    {
        _cert = certificate;
    }

    /// <summary>
    /// Encrypts the input data using the specified padding mode. If <paramref name="paddingMode"/> is <see cref="RSAEncryptionPaddingMode.Oaep"/>, this will use padding from <see cref="EncryptionOaepPadding"/>; else <see cref="RSAEncryptionPadding.Pkcs1"/>.
    /// </summary>
    /// <returns>The encrypted data</returns>
    /// <exception cref="CryptographicException" />
    /// <exception cref="InvalidDataException" />
    public byte[] Encrypt(byte[] toBeEncrypted, RSAEncryptionPaddingMode paddingMode = RSAEncryptionPaddingMode.Oaep)
    {
        using var rsa = _cert.GetRSAPublicKey() ?? throw new InvalidDataException($"{nameof(X509Certificate2)} has no public key.");
        var padding = paddingMode switch
        {
            RSAEncryptionPaddingMode.Pkcs1 => RSAEncryptionPadding.Pkcs1,
            _ => EncryptionOaepPadding
        };
        return rsa.Encrypt(toBeEncrypted, padding);
    }

    /// <summary>
    /// Decrypts the input data using the specified padding mode. If <paramref name="paddingMode"/> is <see cref="RSAEncryptionPaddingMode.Oaep"/>, this will use padding from <see cref="EncryptionOaepPadding"/>; else <see cref="RSAEncryptionPadding.Pkcs1"/>.
    /// </summary>
    /// <returns>The decrypted data</returns>
    /// <exception cref="CryptographicException" />
    /// <exception cref="InvalidDataException" />
    public byte[] Decrypt(byte[] toBeDecrypted, RSAEncryptionPaddingMode paddingMode = RSAEncryptionPaddingMode.Oaep)
    {
        using var rsa = _cert.GetRSAPrivateKey() ?? throw new InvalidDataException($"{nameof(X509Certificate2)} has no private key.");
        var padding = paddingMode switch
        {
            RSAEncryptionPaddingMode.Pkcs1 => RSAEncryptionPadding.Pkcs1,
            _ => EncryptionOaepPadding
        };
        return rsa.Decrypt(toBeDecrypted, padding);
    }

    /// <summary>
    /// Computes the hash value of the specified byte array using the hash algorithm from <see cref="HashAlgorithmName"/> and <paramref name="paddingMode"/>, and signs the resulting hash value.
    /// </summary>
    /// <returns>The RSA signature for the specified data</returns>
    /// <exception cref="CryptographicException" />
    /// <exception cref="InvalidDataException" />
    public byte[] Sign(byte[] toBeSigned, RSASignaturePaddingMode paddingMode = RSASignaturePaddingMode.Pkcs1)
    {
        using var rsa = _cert.GetRSAPrivateKey() ?? throw new InvalidDataException($"{nameof(X509Certificate2)} has no private key.");
        var padding = paddingMode switch
        {
            RSASignaturePaddingMode.Pkcs1 => RSASignaturePadding.Pkcs1,
            _ => RSASignaturePadding.Pss
        };
        return rsa.SignData(toBeSigned, HashAlgorithmName, padding);
    }

    /// <summary>
    /// Verifies that a digital signature is valid by calculating the hash value of the specified data using the hash algorithm from <see cref="HashAlgorithmName"/> and <paramref name="paddingMode"/>, and signs the resulting hash value.
    /// </summary>
    /// <returns><see langword="true"/> if the signature is valid; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="CryptographicException" />
    /// <exception cref="InvalidDataException" />
    public bool Verify(byte[] toBeVerified, byte[] signature, RSASignaturePaddingMode paddingMode = RSASignaturePaddingMode.Pkcs1)
    {
        using var rsa = _cert.GetRSAPublicKey() ?? throw new InvalidDataException($"{nameof(X509Certificate2)} has no public key.");
        var padding = paddingMode switch
        {
            RSASignaturePaddingMode.Pkcs1 => RSASignaturePadding.Pkcs1,
            _ => RSASignaturePadding.Pss
        };
        return rsa.VerifyData(toBeVerified, signature, HashAlgorithmName, padding);
    }
}
