using System.Linq;
using System.Threading.Tasks;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class DataSeedRepository : BaseRepository<IEntity>, IDataSeedRepository<IEntity>
    {
        public DataSeedRepository(IReserbizRepository<IEntity> reserbizRepository)
        : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {

        }

        public void SeedAccounts()
        {
            var password = "Starta123";
            byte[] passwordHash, passwordSalt;
            var accountsCount = _reserbizRepository.ClientDbContext.Accounts.ToList().Count;

            SystemUtility.EncryptionUtility.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            if (accountsCount == 0)
            {
                var account = new Account();
                _reserbizRepository.ClientDbContext.Accounts.Add(
                    new Account
                    {
                        FirstName = "James",
                        MiddleName = "Harden",
                        LastName = "Harden",
                        Gender = GenderEnum.Male,
                        PhotoUrl = "",
                        Username = "jaha",
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt
                    }
                );

                _reserbizRepository.ClientDbContext.Accounts.Add(
                    new Account
                    {
                        FirstName = "Russell",
                        MiddleName = "Westbrook",
                        LastName = "Westbrook",
                        Gender = GenderEnum.Male,
                        PhotoUrl = "",
                        Username = "ruwe",
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt
                    }
                );

                _reserbizRepository.ClientDbContext.Accounts.Add(
                    new Account
                    {
                        FirstName = "Russell",
                        MiddleName = "Westbrook",
                        LastName = "Westbrook",
                        Gender = GenderEnum.Male,
                        PhotoUrl = "",
                        Username = "ruwe",
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt
                    }
                );

                _reserbizRepository.ClientDbContext.Accounts.Add(
                    new Account
                    {
                        FirstName = "Eric",
                        MiddleName = "Gordon",
                        LastName = "Gordon",
                        Gender = GenderEnum.Male,
                        PhotoUrl = "",
                        Username = "ergo",
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt
                    }
                );

                _reserbizRepository.ClientDbContext.Accounts.Add(
                    new Account
                    {
                        FirstName = "PJ",
                        MiddleName = "Tucker",
                        LastName = "Tucker",
                        Gender = GenderEnum.Male,
                        PhotoUrl = "",
                        Username = "pjtu",
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt
                    }
                );

                _reserbizRepository.ClientDbContext.Accounts.Add(
                    new Account
                    {
                        FirstName = "Clint",
                        MiddleName = "Capella",
                        LastName = "Capella",
                        Gender = GenderEnum.Male,
                        PhotoUrl = "",
                        Username = "clca",
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt
                    }
                );

                _reserbizRepository.ClientDbContext.SaveChanges();
            }
        }

        public void SeedAccountStatementsPerContract()
        {
            throw new System.NotImplementedException();
        }

        public void SeedContactPersonPerTenant()
        {
            throw new System.NotImplementedException();
        }

        public void SeedContracts()
        {
            throw new System.NotImplementedException();
        }

        public void SeedMiscellaneousPerTerm()
        {
            throw new System.NotImplementedException();
        }

        public void SeedPaymentBreakdownsPerAccountStatement()
        {
            throw new System.NotImplementedException();
        }

        public void SeedPenaltyBreakdownsPerAccountStatement()
        {
            throw new System.NotImplementedException();
        }

        public void SeedSettings()
        {
            throw new System.NotImplementedException();
        }

        public void SeedSpaceTypes()
        {
            throw new System.NotImplementedException();
        }

        public void SeedTenants()
        {
            throw new System.NotImplementedException();
        }

        public void SeedTerms()
        {
            throw new System.NotImplementedException();
        }
    }
}