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
            CreateMap<Term, TermDetailDto>()
                .ForMember(dest => dest.DurationUnitText,
                    opt => opt.MapFrom(src => src.DurationUnit.ToString()))
                .ForMember(dest => dest.PenaltyAmountPerDurationUnitText, opt =>
                    opt.MapFrom(d => d.PenaltyAmountPerDurationUnit.ToString()))
                .ForMember(dest => dest.PenaltyEffectiveAfterDurationUnitText, opt =>
                    opt.MapFrom(d => d.PenaltyEffectiveAfterDurationUnit.ToString()));

            CreateMap<Term, TermListDto>();
            CreateMap<Term, ContractTermDetailsDto>();
            CreateMap<TermMiscellaneous, TermMiscellaneousDetailDto>();
            CreateMap<Contract, ContractDetailDto>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.NextDueDate,
                    opt => opt.MapFrom(src => src.NextDueDate))
                .ForMember(dest => dest.AccountStatementsCount,
                    opt => opt.MapFrom(src => src.AccountStatements.Count));
            CreateMap<Contract, ContractListDto>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.IsActive));
            CreateMap<Tenant, ContractTenantDetailsDto>();
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

            CreateMap<PersonalInformationForUpdateDto, Account>();
            CreateMap<TenantForCreationDto, Tenant>();
            CreateMap<TenantForUpdateDto, Tenant>();
            CreateMap<ClientForUpdateDto, Client>();
            CreateMap<ContactPersonForCreationDto, ContactPerson>();
            CreateMap<ContactPersonForUpdateDto, ContactPerson>();
            CreateMap<SpaceTypeForCreationDto, SpaceType>();
            CreateMap<SpaceTypeForUpdateDto, SpaceType>();
            CreateMap<TermForCreationDto, Term>();
            CreateMap<TermForUpdateDto, Term>();
            CreateMap<TermMiscellaneousForCreationDto, TermMiscellaneous>();
            CreateMap<TermMiscellaneousForUpdateDto, TermMiscellaneous>();
            CreateMap<ContractForCreationDto, Contract>();
            CreateMap<ContractForUpdateDto, Contract>();
            CreateMap<ClientSettingsForUpdateDto, ClientSettings>();
        }
    }
}