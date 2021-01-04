namespace PasswordHasherandJwtTokenProvider.Interfaces
{
    public interface IPasswordHasher
    {
        /// <summary>
        /// The "personalizedEncrypter" is optional and can be stored in the config or App secret, The password is the customer's password
        /// This can be implemented in the Registration module
        /// The interface can be used in .NetCiore Project and it has it registered in the Extension mthod of the Services
        /// </summary>
        /// <param name="password"></param>
        /// <param name="personalizedEncrypter"></param>
        /// <returns></returns>
        PasswordAndSaltHashes PasswordHashAndSaltToReturn(string password, string personalizedEncrypter = "");
        /// <summary>
        /// The "personalizedEncrypter" is optional and can be recovered if stored in the config or App secret, this verify the password when the user is logging in, it can be implemented in the login class
        /// The interface can be used in .NetCiore Project and it has it registered in the Extension mthod of the Services
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        /// <param name="personalizedEncrypter"></param>
        /// <returns></returns>
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt, string passwordandEncryptor = "");
    }
}