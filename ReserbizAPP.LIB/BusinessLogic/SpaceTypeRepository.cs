using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Helpers;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class SpaceTypeRepository
        : BaseRepository<SpaceType>, ISpaceTypeRepository<SpaceType>
    {
        public SpaceTypeRepository(IReserbizRepository<SpaceType> reserbizRepository)
            : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {

        }

        public async Task<IEnumerable<SpaceTypeDetailDto>> GetSpaceTypesBasedOnNameAsync(string spaceTypeName)
        {
            // Performs left join on Space types, Terms and Contracts tables
            var spaceTypesLeftJoinTermsAndContracts = await (from space_type in _reserbizRepository.ClientDbContext.SpaceTypes

                                                                 // Inner Join SpaceTypes and Terms
                                                             join term in _reserbizRepository.ClientDbContext.Terms
                                                             on space_type.Id equals term.SpaceTypeId into spt_group
                                                             from space_term in spt_group.DefaultIfEmpty()

                                                                 // Inner join Terms and Contracts
                                                             join contract in _reserbizRepository.ClientDbContext.Contracts
                                                             on new
                                                             {
                                                                 Id = space_term.Id,
                                                                 IsActive = true,
                                                                 IsDeleted = false
                                                             }
                                                            equals
                                                            new
                                                            {
                                                                Id = contract.TermId,
                                                                IsActive = contract.IsActive,
                                                                IsDeleted = contract.IsDelete
                                                            } into tc_group
                                                             from term_contract in tc_group.DefaultIfEmpty()

                                                                 // Make sure deleted space types will no longer available on the list
                                                             where space_type.IsDelete == false

                                                             select new
                                                             {
                                                                 space_type,
                                                                 space_term,
                                                                 term_contract
                                                             }).ToListAsync();

            // These are space types that are attached to Term/s and Term/s are attached to Contract/s.
            // These are marked as deletable false.
            var spaceTypesWithTermsAndContracts = (from stc in spaceTypesLeftJoinTermsAndContracts

                                                   where stc.space_term != null

                                                   group stc by new
                                                   {
                                                       stc.space_type.Id,
                                                       stc.space_type.Name,
                                                       stc.space_type.Description,
                                                       stc.space_type.Rate,
                                                       stc.space_type.AvailableSlot,
                                                       stc.space_type.IsActive
                                                   } into g

                                                   select new SpaceTypeDetailDto
                                                   {
                                                       Id = g.Key.Id,
                                                       Name = g.Key.Name,
                                                       Description = g.Key.Description,
                                                       Rate = g.Key.Rate,
                                                       CurrentUsedSlot = g.Count(),
                                                       AvailableSlot = (g.Key.AvailableSlot - g.Count()),
                                                       IsActive = g.Key.IsActive,
                                                       IsDeletable = false
                                                   }).ToList();

            // These are space types which are not attached to any terms yet,
            // therefore they are marked as deletable true
            var spaceTypesWithoutTermsAndContracts = (from stc in spaceTypesLeftJoinTermsAndContracts

                                                      where stc.space_term == null && stc.term_contract == null

                                                      select new SpaceTypeDetailDto
                                                      {
                                                          Id = stc.space_type.Id,
                                                          Name = stc.space_type.Name,
                                                          Description = stc.space_type.Description,
                                                          Rate = stc.space_type.Rate,
                                                          CurrentUsedSlot = 0,
                                                          AvailableSlot = stc.space_type.AvailableSlot,
                                                          IsActive = stc.space_type.IsActive,
                                                          IsDeletable = true
                                                      }).ToList();

            var allSpaceTypesFromRepo = spaceTypesWithTermsAndContracts.Concat(spaceTypesWithoutTermsAndContracts);

            // Filter space types based on the name
            if (!string.IsNullOrEmpty(spaceTypeName))
            {
                allSpaceTypesFromRepo = allSpaceTypesFromRepo.Where(s => s.Name.Contains(spaceTypeName));
            }

            return allSpaceTypesFromRepo.OrderBy(s => s.Name).ToList();
        }

        public async Task<SpaceType> GetSpaceTypeAsync(int spaceTypeId)
        {
            var spaceTypeFromRepo = await _reserbizRepository.ClientDbContext.SpaceTypes
                                            .AsQueryable()
                                            .Includes(s => s.Term)
                                            .Where(s => s.Id == spaceTypeId)
                                            .FirstOrDefaultAsync();

            return spaceTypeFromRepo;
        }

        public async Task<bool> DeleteMultipleSpaceTypesAsync(List<int> spaceTypeIds)
        {
            var selectedSpaceTypes = await _reserbizRepository
                .ClientDbContext
                .SpaceTypes
                .Where(t => spaceTypeIds.IndexOf(t.Id) > -1).ToListAsync();

            DeleteMultipleEntities(selectedSpaceTypes);
            return await SaveChanges();
        }
    }
}