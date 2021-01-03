using AutoMapper;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.API.Helpers
{
    public class ReserbizAutoMapperProfile : Profile
    {
        public ReserbizAutoMapperProfile()
        {
            CreateMap<Client, ClientDetailsDto>();
            CreateMap<Client, ClientForListDto>();
            CreateMap<Tenant, TenantDetailsDto>()
                .ForMember(dest => dest.IsDeletable,
                    opt => opt.MapFrom(src => src.IsDeletable));
            CreateMap<Tenant, TenantForListDto>()
                .ForMember(dest => dest.IsDeletable,
                    opt => opt.MapFrom(src => src.IsDeletable));
            CreateMap<Account, AccountForListDto>();
            CreateMap<Account, AccountForDetailDto>();
            CreateMap<ContactPerson, ContactPersonDetailDto>();
            CreateMap<SpaceType, SpaceTypeDetailDto>()
             .ForMember(dest => dest.IsDeletable,
                    opt => opt.MapFrom(src => src.IsDeletable));
            CreateMap<Space, SpaceDetailDto>()
             .ForMember(dest => dest.IsDeletable,
                    opt => opt.MapFrom(src => src.IsDeletable))
                .ForMember(dest => dest.SpaceTypeName,
                    opt => opt.MapFrom(src => src.SpaceType.Name))
                .ForMember(dest => dest.SpaceTypeRate,
                    opt => opt.MapFrom(src => src.SpaceType.Rate));
            CreateMap<SpaceType, SpaceTypeOptionDto>();
            CreateMap<Space, SpaceOptionDto>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.OccupiedByContractId,
                    opt => opt.MapFrom(src => src.OccupiedByContractId))
                .ForMember(dest => dest.IsNotOccupied,
                    opt => opt.MapFrom(src => src.IsNotOccupied));
            CreateMap<Term, TermOptionDto>();
            CreateMap<SpaceType, SpaceTypeTermDetailDto>();
            CreateMap<Term, TermDetailDto>()
                .ForMember(dest => dest.DurationUnitText,
                    opt => opt.MapFrom(src => src.DurationUnit.ToString()))
                .ForMember(dest => dest.IsDeletable,
                    opt => opt.MapFrom(src => src.IsDeletable))
                .ForMember(dest => dest.PenaltyAmountPerDurationUnitText, opt =>
                    opt.MapFrom(d => d.PenaltyAmountPerDurationUnit.ToString()))
                .ForMember(dest => dest.PenaltyEffectiveAfterDurationUnitText, opt =>
                    opt.MapFrom(d => d.PenaltyEffectiveAfterDurationUnit.ToString()))
                .ForMember(dest => dest.PenaltyAmount, opt =>
                    opt.MapFrom(d => d.PenaltyAmount));
            CreateMap<Term, TermListDto>()
                .ForMember(dest => dest.IsDeletable,
                    opt => opt.MapFrom(src => src.IsDeletable));
            CreateMap<Term, ContractTermDetailsDto>();
            CreateMap<TermMiscellaneous, TermMiscellaneousDetailDto>();
            CreateMap<Contract, ContractDetailDto>()
                .ForMember(dest => dest.TenantName,
                    opt => opt.MapFrom(src => src.Tenant.PersonFullName))
                .ForMember(dest => dest.SpaceName,
                    opt => opt.MapFrom(src => src.Space.Description))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.IsEditable,
                    opt => opt.MapFrom(src => src.IsEditable))
                .ForMember(dest => dest.NextDueDate,
                    opt => opt.MapFrom(src => src.NextDueDate))
                .ForMember(dest => dest.AccountStatementsCount,
                    opt => opt.MapFrom(src => src.AccountStatements.Count));
            CreateMap<Contract, ContractListDto>()
                .ForMember(dest => dest.TenantName,
                    opt => opt.MapFrom(src => src.Tenant.PersonFullName));
            CreateMap<Tenant, ContractTenantDetailsDto>();
            CreateMap<Tenant, TenantOptionDto>()
                .ForMember(dest => dest.Name,
                        opt => opt.MapFrom(src => src.PersonFullName));
            CreateMap<AccountStatement, AccountStatementDetailsDto>()
                .ForMember(dest => dest.PenaltyTotalAmount,
                    opt => opt.MapFrom(src => src.PenaltyTotalAmount))
                .ForMember(dest => dest.PenaltyNextDueDate,
                    opt => opt.MapFrom(src => src.PenaltyNextDueDate))
                .ForMember(dest => dest.MiscellaneousTotalAmount,
                    opt => opt.MapFrom(src => src.MiscellaneousTotalAmount))
                .ForMember(dest => dest.AccountStatementTotalAmount,
                    opt => opt.MapFrom(src => src.AccountStatementTotalAmount))
                .ForMember(dest => dest.CurrentAmountPaid,
                    opt => opt.MapFrom(src => src.CurrentAmountPaid))
                .ForMember(dest => dest.CurrentBalance,
                    opt => opt.MapFrom(src => src.CurrentBalance))
                .ForMember(dest => dest.IsFullyPaid,
                    opt => opt.MapFrom(src => src.IsFullyPaid));
            CreateMap<AccountStatementMiscellaneous, AccountStatementMiscellaneousDetailsDto>();
            CreateMap<PenaltyBreakdown, AccountStatementPenaltyItemDetailsDto>();
            CreateMap<PaymentBreakdown, AccountStatementPaymentItemDetailsDto>();
            CreateMap<AccountStatement, AccountStatementForListDto>()
                .ForMember(dest => dest.PenaltyTotalAmount,
                    opt => opt.MapFrom(src => src.PenaltyTotalAmount))
                .ForMember(dest => dest.PenaltyNextDueDate,
                    opt => opt.MapFrom(src => src.PenaltyNextDueDate))
                .ForMember(dest => dest.MiscellaneousTotalAmount,
                    opt => opt.MapFrom(src => src.MiscellaneousTotalAmount))
                .ForMember(dest => dest.AccountStatementTotalAmount,
                    opt => opt.MapFrom(src => src.AccountStatementTotalAmount))
                .ForMember(dest => dest.CurrentAmountPaid,
                    opt => opt.MapFrom(src => src.CurrentAmountPaid))
                .ForMember(dest => dest.CurrentBalance,
                    opt => opt.MapFrom(src => src.CurrentBalance))
                .ForMember(dest => dest.IsFullyPaid,
                    opt => opt.MapFrom(src => src.IsFullyPaid));
            CreateMap<PaymentBreakdown, PaymentBreakdownForDetailsDto>()
                .ForMember(dest => dest.ReceivedBy,
                    opt => opt.MapFrom(src => src.ReceivedBy.PersonFullName));
            CreateMap<Space, SpaceDetailDto>();
            CreateMap<ClientSettings, ClientSettingsDetailsDto>();

            CreateMap<PersonalInformationForUpdateDto, Account>();
            CreateMap<TenantForCreationDto, Tenant>();
            CreateMap<TenantForUpdateDto, Tenant>();
            CreateMap<ClientForUpdateDto, Client>();
            CreateMap<ContactPersonForCreationDto, ContactPerson>();
            CreateMap<ContactPersonForUpdateDto, ContactPerson>();
            CreateMap<SpaceTypeForCreationDto, SpaceType>();
            CreateMap<SpaceTypeForUpdateDto, SpaceType>();
            CreateMap<SpaceForCreationDto, Space>();
            CreateMap<SpaceForUpdateDto, Space>();
            CreateMap<TermForManageDto, Term>();
            CreateMap<TermForUpdateDto, Term>();
            CreateMap<TermMiscellaneousManageDto, TermMiscellaneous>();
            CreateMap<TermMiscellaneousForUpdateDto, TermMiscellaneous>();
            CreateMap<ContractManageDto, Contract>();
            CreateMap<ClientSettingsForUpdateDto, ClientSettings>();
        }
    }
}