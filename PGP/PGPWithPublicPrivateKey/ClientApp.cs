using System;
using PgpCoreLib;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PGPWithPublicPrivateKey
{
    class ClientApp
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (PGP pgp = new PGP())
            {
                // Generate keys
                //pgp.GenerateKey(@"C:\TEMP\keys\public.asc", @"C:\TEMP\keys\private.asc", "email@email.com", "password");

                // Encrypt file
                pgp.EncryptFile(@"F:\Virtusa_WorkAssigned\GitHub\PGP_Encryption\PGP\PGPWithPublicPrivateKey\TEMP\Content\SampleCSVFile_2kb.csv",
                    @"F:\Virtusa_WorkAssigned\GitHub\PGP_Encryption\PGP\PGPWithPublicPrivateKey\TEMP\Keys\SampleCSVFile_2kb__encrypted.pgp",
                    @"F:\Virtusa_WorkAssigned\GitHub\PGP_Encryption\PGP\PGPWithPublicPrivateKey\TEMP\Keys\0xB94665C1-pub.asc", true, true);

                // Encrypt and sign file
                pgp.EncryptFileAndSign(@"F:\Virtusa_WorkAssigned\GitHub\PGP_Encryption\PGP\PGPWithPublicPrivateKey\TEMP\Content\SampleCSVFile_2kb.csv",
                    @"F:\Virtusa_WorkAssigned\GitHub\PGP_Encryption\PGP\PGPWithPublicPrivateKey\TEMP\Keys\SampleCSVFile_2kb__encrypted_signed.pgp",
                    @"F:\Virtusa_WorkAssigned\GitHub\PGP_Encryption\PGP\PGPWithPublicPrivateKey\TEMP\Keys\0xB94665C1-pub.asc",
                    @"F:\Virtusa_WorkAssigned\GitHub\PGP_Encryption\PGP\PGPWithPublicPrivateKey\TEMP\Keys\0xB94665C1-sec.asc", "0718@Gk", true, true);


                // Decrypt file
                pgp.DecryptFile(@"F:\Virtusa_WorkAssigned\GitHub\PGP_Encryption\PGP\PGPWithPublicPrivateKey\TEMP\Keys\SampleCSVFile_2kb__encrypted.pgp",
                    @"F:\Virtusa_WorkAssigned\GitHub\PGP_Encryption\PGP\PGPWithPublicPrivateKey\TEMP\Keys\SampleCSVFile_2kb__decrypted.csv",
                    @"F:\Virtusa_WorkAssigned\GitHub\PGP_Encryption\PGP\PGPWithPublicPrivateKey\TEMP\Keys\0xB94665C1-sec.asc",
                    "0718@Gk");

                // Decrypt signed file
                pgp.DecryptFile(@"F:\Virtusa_WorkAssigned\GitHub\PGP_Encryption\PGP\PGPWithPublicPrivateKey\TEMP\Keys\SampleCSVFile_2kb__encrypted_signed.pgp",
                    @"F:\Virtusa_WorkAssigned\GitHub\PGP_Encryption\PGP\PGPWithPublicPrivateKey\TEMP\Keys\SampleCSVFile_2kb__decrypted_signed.csv",
                    @"F:\Virtusa_WorkAssigned\GitHub\PGP_Encryption\PGP\PGPWithPublicPrivateKey\TEMP\Keys\0xB94665C1-sec.asc", "0718@Gk");

                // Encrypt stream
                using (FileStream inputFileStream = new FileStream(@"F:\Virtusa_WorkAssigned\GitHub\PGP_Encryption\PGP\PGPWithPublicPrivateKey\TEMP\Content\SampleCSVFile_2kb.csv", FileMode.Open))
                using (Stream outputFileStream = File.Create(@"F:\Virtusa_WorkAssigned\GitHub\PGP_Encryption\PGP\PGPWithPublicPrivateKey\TEMP\Content\SampleCSVFile_2kb__encrypted2.pgp"))
                using (Stream publicKeyStream = new FileStream(@"F:\Virtusa_WorkAssigned\GitHub\PGP_Encryption\PGP\PGPWithPublicPrivateKey\TEMP\Keys\0xB94665C1-pub.asc", FileMode.Open))
                    pgp.EncryptStream(inputFileStream, outputFileStream, publicKeyStream, true, true);

                // Decrypt stream
                using (FileStream inputFileStream = new FileStream(@"F:\Virtusa_WorkAssigned\GitHub\PGP_Encryption\PGP\PGPWithPublicPrivateKey\TEMP\Content\SampleCSVFile_2kb__encrypted2.pgp", FileMode.Open))
                using (Stream outputFileStream = File.Create(@"F:\Virtusa_WorkAssigned\GitHub\PGP_Encryption\PGP\PGPWithPublicPrivateKey\TEMP\Content\SampleCSVFile_2kb__decrypted2.csv"))
                using (Stream privateKeyStream = new FileStream(@"F:\Virtusa_WorkAssigned\GitHub\PGP_Encryption\PGP\PGPWithPublicPrivateKey\TEMP\Keys\0xB94665C1-sec.asc", FileMode.Open))
                    pgp.DecryptStream(inputFileStream, outputFileStream, privateKeyStream, "0718@Gk");
            }
        }
    }
}
