using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class RefreshTokenRepository
        : BaseRepository<RefreshToken>, IRefreshTokenRepository<RefreshToken>
    {
        public RefreshTokenRepository(IReserbizRepository<RefreshToken> reserbizRepository)
                    : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {

        }
        public async Task<RefreshToken> GetRefreshToken(string refreshToken)
        {
            var refreshTokenFromRepo = await _reserbizRepository.ClientDbContext
                    .RefreshTokens
                    .FirstOrDefaultAsync(r => r.Token == refreshToken);

            return refreshTokenFromRepo;
        }
    }
}