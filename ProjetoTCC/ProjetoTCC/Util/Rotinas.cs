//using Model;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ProjetoTCC.Model;

namespace Util
{
    public static class Rotinas
    {
        public static string SHA1(string senha)
        {
            var sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            var _senha = BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(senha))).Replace("-", string.Empty);
            return _senha;
        }

        public static List<Estado> ListaEstados()
        {
            return new List<Estado>
            {
                new Estado { CdEstado = "RJ", CdUfIbge = 33 },
                new Estado { CdEstado = "SP", CdUfIbge = 35 },
                new Estado { CdEstado = "AC", CdUfIbge = 12 },
                new Estado { CdEstado = "AL", CdUfIbge = 27 },
                new Estado { CdEstado = "AM", CdUfIbge = 13 },
                new Estado { CdEstado = "BA", CdUfIbge = 29 },
                new Estado { CdEstado = "CE", CdUfIbge = 23 },
                new Estado { CdEstado = "DF", CdUfIbge = 53 },
                new Estado { CdEstado = "ES", CdUfIbge = 32 },
                new Estado { CdEstado = "GO", CdUfIbge = 52 },
                new Estado { CdEstado = "MA", CdUfIbge = 21 },
                new Estado { CdEstado = "MG", CdUfIbge = 31 },
                new Estado { CdEstado = "MS", CdUfIbge = 50 },
                new Estado { CdEstado = "MT", CdUfIbge = 51 },
                new Estado { CdEstado = "PA", CdUfIbge = 15 },
                new Estado { CdEstado = "PB", CdUfIbge = 25 },
                new Estado { CdEstado = "PE", CdUfIbge = 26 },
                new Estado { CdEstado = "PI", CdUfIbge = 22 },
                new Estado { CdEstado = "PR", CdUfIbge = 41 },
                new Estado { CdEstado = "RN", CdUfIbge = 24 },
                new Estado { CdEstado = "RO", CdUfIbge = 11 },
                new Estado { CdEstado = "RR", CdUfIbge = 14 },
                new Estado { CdEstado = "RS", CdUfIbge = 43 },
                new Estado { CdEstado = "SC", CdUfIbge = 42 },
                new Estado { CdEstado = "SE", CdUfIbge = 28 },
                new Estado { CdEstado = "TO", CdUfIbge = 17 }
            };
        }

        public static List<Racas> ListaRacasCachorro()
        {
            return new List<Racas>
            {
                new Racas { raca = "Vira-Lata" },
                new Racas { raca = "Akita" },
                new Racas { raca = "Affenpinscher" },
                new Racas { raca = "American bully" },
                new Racas { raca = "Basenji" },
                new Racas { raca = "Basset Hound" },
                new Racas { raca = "Beagle" },
                new Racas { raca = "Bearded Collie" },
                new Racas { raca = "Bernese" },
                new Racas { raca = "Bichon Frisé" },
                new Racas { raca = "Bichon Havanês" },
                new Racas { raca = "Border Collie" },
                new Racas { raca = "Boiadeiro Australiano" },
                new Racas { raca = "Boston Terrier" },
                new Racas { raca = "Boxer" },
                new Racas { raca = "Bull Terrier" },
                new Racas { raca = "Bulldog Francês" },
                new Racas { raca = "Bulldog Inglês" },
                new Racas { raca = "Cão de Crista Chinês" },
                new Racas { raca = "Cane Corso Italiano" },
                new Racas { raca = "Cairn Terrier" },
                new Racas { raca = "Cavalier King Charles" },
                new Racas { raca = "Chihuahua" },
                new Racas { raca = "Chow Chow" },
                new Racas { raca = "Cocker Spaniel" },
                new Racas { raca = "Collie" },
                new Racas { raca = "Corgi" },
                new Racas { raca = "Coton de Tulear" },
                new Racas { raca = "Dálmata" },
                new Racas { raca = "Dachshund" },
                new Racas { raca = "Dobermann" },
                new Racas { raca = "Dogo Argentino" },
                new Racas { raca = "Dogue Alemão" },
                new Racas { raca = "Fox Terrier" },
                new Racas { raca = "Foxhound" },
                new Racas { raca = "Galgo" },
                new Racas { raca = "Golden Retrivier" },
                new Racas { raca = "Griffon de Bruxelas" },
                new Racas { raca = "Husky Siberiano" },
                new Racas { raca = "Jack Russell" },
                new Racas { raca = "Kuvasz" },
                new Racas { raca = "Labrador" },
                new Racas { raca = "Lhasa Apso" },
                new Racas { raca = "Lulu da Pomerânia" },
                new Racas { raca = "Malamute do Alasca" },
                new Racas { raca = "Maltês" },
                new Racas { raca = "Mastim Napolitano" },
                new Racas { raca = "Pastor da Nova Zelândia" },
                new Racas { raca = "Norfolk Terrier" },
                new Racas { raca = "Pastor Inglês" },
                new Racas { raca = "Pastor Australiano" },
                new Racas { raca = "Pastor Alemão" },
                new Racas { raca = "Pastor Belga" },
                new Racas { raca = "Pastor Branco Suíço" },
                new Racas { raca = "Pastror de Shetland" },
                new Racas { raca = "Pequinês" },
                new Racas { raca = "Perdigueiro" },
                new Racas { raca = "Pointer Inglês" },
                new Racas { raca = "Pinscher" },
                new Racas { raca = "Pitbull" },
                new Racas { raca = "Podengo Português" },
                new Racas { raca = "Poodle" },
                new Racas { raca = "Pug" },
                new Racas { raca = "Leão Rodésia" },
                new Racas { raca = "Rottweiler" },
                new Racas { raca = "Samoieda" },
                new Racas { raca = "São Bernardo" },
                new Racas { raca = "Schnauzer" },
                new Racas { raca = "Scottish Terrier" },
                new Racas { raca = "Setter Irlandês" },
                new Racas { raca = "Staffordshire" },
                new Racas { raca = "Staffordshire Bull Terrier" },
                new Racas { raca = "Shar Pei" },
                new Racas { raca = "Shih Tzu" },
                new Racas { raca = "Shiba Inu" },
                new Racas { raca = "Spaniel Bretão" },
                new Racas { raca = "Spitz Alemão" },
                new Racas { raca = "Spitz Japonês" },
                new Racas { raca = "Teckel" },
                new Racas { raca = "Terra-Nova" },
                new Racas { raca = "Terrier Brasileiro" },
                new Racas { raca = "Terrier Tibetano" },
                new Racas { raca = "Vizsla" },
                new Racas { raca = "Weimaraner" },
                new Racas { raca = "West Highland Terrier" },
                new Racas { raca = "Whippet" },
                new Racas { raca = "Yorkshire" },
                new Racas { raca = "Outra" }
            };
        }

        public static List<Racas> ListaRacasGato()
        {
            return new List<Racas>
            {
                new Racas {raca = "Vira-Lata"},
                new Racas {raca = "American Wirehair"},
                new Racas {raca = "Angorá"},
                new Racas {raca = "Azul Russo"},
                new Racas {raca = "Balinês"},
                new Racas {raca = "Bobtail Americano"},
                new Racas {raca = "Bombaim"},
                new Racas {raca = "British Longhair"},
                new Racas {raca = "Burmilla"},
                new Racas {raca = "Chartreux"},
                new Racas {raca = "Cornish Rex"},
                new Racas {raca = "Curl Americano"},
                new Racas {raca = "Cymric"},
                new Racas {raca = "Donskoy"},
                new Racas {raca = "Exótico"},
                new Racas {raca = "Gasto de Pelo Curto Inglês"},
                new Racas {raca = "Gato Abissínio"},
                new Racas {raca = "Gato Birmanês"},
                new Racas {raca = "Gato Comum Europeu"},
                new Racas {raca = "Gato de Pelo Curto Americano"},
                new Racas {raca = "Gato Manês"},
                new Racas {raca = "Gato Savannah"},
                new Racas {raca = "Gato-de-Bengala"},
                new Racas {raca = "Himalaio"},
                new Racas {raca = "Kinkalow"},
                new Racas {raca = "Korat"},
                new Racas {raca = "LaPerm"},
                new Racas {raca = "Lykoi"},
                new Racas {raca = "Maine Coon"},
                new Racas {raca = "Munchkin"},
                new Racas {raca = "Nebelung"},
                new Racas {raca = "Norueguês da Floresta"},
                new Racas {raca = "Ocicat"},
                new Racas {raca = "Oriental Havana"},
                new Racas {raca = "Oriental Longhair"},
                new Racas {raca = "Oriental Shorthair"},
                new Racas {raca = "Persa"},
                new Racas {raca = "Peterbald"},
                new Racas {raca = "Ragamuffin"},
                new Racas {raca = "Ragdoll"},
                new Racas {raca = "Sagrado da Birmânia"},
                new Racas {raca = "Scottish Fold"},
                new Racas {raca = "Selkirk Rex"},
                new Racas {raca = "Siamês"},
                new Racas {raca = "Siberiano"},
                new Racas {raca = "Singapura"},
                new Racas {raca = "Somali"},
                new Racas {raca = "Sphynx"},
                new Racas {raca = "Thai"},
                new Racas {raca = "Tonquinês"},
                new Racas {raca = "Toyger"},
                new Racas {raca = "Van Turco"}
            };
        }

        public static string RetiraCaracteresNaoNumericos(string txt)
        {
            var apenasDigitos = new Regex(@"[^\d]");
            return apenasDigitos.Replace(txt ?? "", "");
        }

        public static bool CelularValido(string cel)
        {
            var tel = RetiraCaracteresNaoNumericos(cel);
            return tel.Length == 11;
        }

        public static bool EmailValido(string email)
        {
            return Regex.IsMatch(email ?? "", @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        public static bool CpfValido(string cpf)
        {
            cpf = RetiraCaracteresNaoNumericos(cpf);

            if (string.IsNullOrEmpty(cpf))
            {
                return false;
            }

            string[] cpfsSequenciaisInvalidos = new string[] { "00000000000", "11111111111", "22222222222", "33333333333", "44444444444", "55555555555", "66666666666", "77777777777", "88888888888", "99999999999" };
            foreach (var item in cpfsSequenciaisInvalidos)
            {
                if (cpf == item)
                {
                    return false;
                }
            }

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
            {
                return false;
            }

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito = resto.ToString();
            tempCpf += digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            digito += resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static bool CnpjValido(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            cnpj = cnpj.Trim();

            if (cnpj.Length != 14)
            {
                return false;
            }

            tempCnpj = cnpj.Substring(0, 12);

            soma = 0;

            for (int i = 0; i < 12; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            }

            resto = soma % 11;

            resto = resto < 2 ? 0 : 11 - resto;

            digito = resto.ToString();

            tempCnpj += digito;

            soma = 0;

            for (int i = 0; i < 13; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;

            resto = resto < 2 ? 0 : 11 - resto;

            digito += resto.ToString();

            return cnpj.EndsWith(digito);

        }

        public static string DescompactaGzip(byte[] origem)
        {
            string txt = null;
            System.IO.MemoryStream _ms = new System.IO.MemoryStream();
            System.IO.Compression.GZipStream _zipStream = null;

            try
            {
                byte[] _buffer = origem;
                int msgLength = BitConverter.ToInt32(_buffer, 0);
                _ms.Write(_buffer, 0, _buffer.Length);
                byte[] _bytes = new byte[msgLength - 1 + 1];
                _ms.Position = 0;
                _zipStream = new System.IO.Compression.GZipStream(_ms, System.IO.Compression.CompressionMode.Decompress);
                _zipStream.Read(_bytes, 0, _bytes.Length);
                txt = Encoding.UTF8.GetString(_bytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _ms.Close();
                _zipStream.Close();
            }
            return txt;
        }

    }
}

