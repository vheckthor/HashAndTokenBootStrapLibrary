using PasswordHasherandJwtTokenProvider.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PasswordHasherandJwtTokenProvider
{
    public class PasswordHasher : IPasswordHasher
    {
        /// <summary>
        /// The "personalizedEncrypter" is optional and can be recovered if stored in the config or App secret, this verify the password when the user is logging in, it can be implemented in the login class
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        /// <param name="personalizedEncrypter"></param>
        /// <returns></returns>
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt, string personalizedEncrypter = "")
        {
            password = password + personalizedEncrypter;
            using (var haser = new HMACSHA512(passwordSalt))
            {
                var ComputedpasswordHash = haser.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < ComputedpasswordHash.Length; i++)
                {
                    if (ComputedpasswordHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }

            }
            return true;

        }

        private void CreatePasswordHash(string passwordandEncryptor, out byte[] passwordhash, out byte[] passwordSalt)
        {

            using (var haser = new HMACSHA512())
            {
                passwordSalt = haser.Key;
                passwordhash = haser.ComputeHash(Encoding.UTF8.GetBytes(passwordandEncryptor));
            }
        }


        /// <summary>
        /// The "personalizedEncrypter" is optional and can be stored in the config or App secret, The password is the customer's password
        /// This can be implemented in the Registration module
        /// </summary>
        /// <param name="password"></param>
        /// <param name="personalizedEncrypter"></param>
        /// <returns></returns>
        public PasswordAndSaltHashes PasswordHashAndSaltToReturn(string password, string personalizedEncrypter = "")
        {
            byte[] passwordhash;
            byte[] passwordSalt;
            password = password + personalizedEncrypter;

            CreatePasswordHash(password, out passwordhash, out passwordSalt);
            PasswordAndSaltHashes value = new PasswordAndSaltHashes();
            value.PasswordHash = passwordhash;
            value.PasswordSalt = passwordSalt;
            return value;
        }


    }

    public struct PasswordAndSaltHashes
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

    }
}
