namespace ReserbizAPP.LIB.Helpers.Constants
{
    public static class ApiRoutes
    {
        public static class AccountStatementControllerRoutes
        {
            public const string AutoGenerateContractAccountStatementsForNewDatabaseURL = "/api/accountstatement/autoGenerateContractAccountStatementsForNewDatabase/{currentUserId}";
            public const string AutoGenerateContractAccountStatementsURL = "/api/accountstatement/autoGenerateContractAccountStatements";
            public const string AutoGenerateAccountStatementPenaltiesURL = "/api/accountstatement/autoGenerateContractAccountStatementPenalties";
            public const string CreateNewAccountStatementURL = "/api/accountstatement/createNewAccountStatement/{marksAsPaid}";
            public const string DeleteAccountStatementURL = "/api/accountstatement/deleteAccountStatement/{accountStatementId}";
            public const string GetAccountStatementsPerContractURL = "/api/accountstatement/getAccountStatementsPerContract";
            public const string GetAccountStatementsAmountSummaryURL = "/api/accountstatement/getAccountStatementsAmountSummary";
            public const string GetFirstAccountStatementURL = "/api/accountstatement/getFirstAccountStatement/{contractId}";
            public const string GetSuggestedNewAccountStatementURL = "/api/accountstatement/suggestedAccountStatement/{contractId}";
            public const string GetUnpaidAccountStatementsAsyncURL = "/api/accountstatement/getUnpaidAccountStatements";
            public const string SendAccountStatementURL = "/api/accountstatement/sendAccountStatement/{id}";
            public const string UpdateWaterAndElectricBillAmountURL = "/api/accountstatement/updateWaterAndElectricBillAmount";
        }

        public static class AuthControllerRoutes
        {
            public const string LoginURL = "/api/auth/login";
            public const string GetAccountURL = "/api/auth/{id}";
            public const string RefreshTokenURL = "/api/auth/refresh";
            public const string RegisterURL = "/api/auth/register";
            public const string RemoveExpiredRefreshTokensURL = "/api/auth/removeExpiredRefreshTokens";
            public const string UpdateAccountURL = "/api/auth/updateAccountInformation/{id}";
            public const string UpdatePersonalInformationURL = "/api/auth/updatePersonalInformation/{id}";
            public const string ValidateUsernameExistsURL = "/api/auth/validateUsernameExists/{id}/{username}";
        }

        public static class ContractControllerRoutes
        {
            public const string CalculateExpirationDateURL = "/api/contract/calculateExpirationDate/{effectiveDate}/{durationUnit}/{durationValue}";
            public const string CheckTermCodeIfExistsURL = "/api/contract/checkContractCodeIfExists/{contractId}/{contractCode}";
            public const string CreateContractURL = "/api/contract/create";
            public const string DeleteContractURL = "/api/contract/deleteContract";
            public const string DeleteMultipleContractsURL = "/api/contract/deleteMultipleContracts";
            public const string GetAllUpcomingDueDateContractsPerMonthURL = "/api/contract/getAllUpcomingDueDateContractsPerMonth";
            public const string GetContractURL = "/api/contract/{id}";
            public const string GetAllContracts = "/api/contract/getAllContracts";
            public const string GetContractsPerTenantURL = "/api/contract/getAllContractsPerTenant/{tenantId}";
            public const string GetActiveContractsCountURL = "/api/contract/getActiveContractsCount";
            public const string GetContractAccountStatementsURL = "/api/contract/getContractAccountStatements/{id}";
            public const string SetEncashDepositAmountStatusURL = "/api/contract/setEncashDepositAmountStatus/{contractId}/{status}";
            public const string SetMultipleContractsStatusURL = "/api/contract/setMultipleContractsStatus/{status}";
            public const string SetContractStatusURL = "/api/contract/setStatus/{id}/{status}";
            public const string UpdateContractURL = "/api/contract/{contractId}/{termId}";
            public const string ValidateExpirationDateURL = "/api/contract/validateExpirationDate/{contractId}/{effectiveDate}/{durationUnit}/{durationValue}";
        }

        public static class ClientsControllerRoutes
        {
            public const string DeleteClientURL = "/api/clients/{id}";
            public const string GetClientInformationURL = "/api/clients/{clientName}";
            public const string RegisterClientURL = "/api/clients/registerClient";
            public const string RegisterDemoURL = "/api/clients/registerDemo";
            public const string UpdateClientURL = DeleteClientURL;
        }

        public static class ClientDbManagerControllerRoutes
        {
            public const string PopulateDatabaseURL = "/api/clientdbmanager/populateDatabase";
            public const string SyncDatabaseURL = "/api/clientdbmanager/syncDatabase";
            public const string SyncAllDatabasesURL = "/api/clientdbmanager/syncAllDatabases";
        }
        
        public static class PaymentBreakdownControllerRoutes
        {
            public const string AddPaymentURL = "/api/paymentbreakdown/addPayment";
            public const string GetPaymentDetailsURL = "/api/paymentbreakdown/{id}";
            public const string GetPaymentsPerAccountStatementURL = "/api/paymentbreakdown/getPaymentsPerAccountStatement";
        }

        public static class TestControllerRoutes
        {
            public const string GetCurrentDateTimeURL = "/api/test/getCurrentDateTime";
        }

        public static class TermControllerRoutes
        {
            public const string CreateTermURL = "/api/term/create";
            public const string CheckTermCodeIfExistsURL = "/api/term/checkTermCodeIfExists/{termId}/{termCode}";
            public const string DeleteMultipleTermsURL = "/api/term/deleteMultipleTerms";
            public const string GetTermURL = "/api/term/{id}";
            public const string UpdateTermURL = GetTermURL;
            public const string DeleteTermURL = GetTermURL;
            public const string GetTermsAsOptionsURL = "/api/term/getTermsAsOptions";
        }
    }
}