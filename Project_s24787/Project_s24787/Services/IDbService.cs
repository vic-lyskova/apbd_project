using Project_s24787.DTOs;
using Project_s24787.Models;

namespace Project_s24787.Services;

public interface IDbService
{
    Task<bool> DoesIndividualClientExist(string pesel);
    Task<bool> DoesFirmClientExist(string krsNumber);
    Task AddIndividualClient(AddIndividualClientDTO addIndividualClientDto);
    Task AddFirmClient(AddFirmClientDTO addFirmClientDto);
    Task<User?> GetUser(string login);
    Task<LoginResponceDTO> GetTokens(User user);
    Task RegisterNewEmployee(RegisterRequestDTO registerRequestDto);
    Task ChangeEmployeesRole(User user, string role);
    Task<User?> FindUser(string refreshToken);
    Task<bool> DoesUserExist(string login);
    Task EditFirmClient(string krsNumber, EditFirmClientDTO editFirmClientDto);
    Task EditIndividualClient(string pesel, EditIndividualClientDTO editIndividualClientDto);
    Task<bool> DoesDiscountExist(int idDiscount);
    Task<Discount> FindDiscount(int idDiscount);
    Task<bool> DoesSoftwareExist(int idSoftware);
    Task<bool> DoesClientExist(int idClient);
    Task DrawUpContract(int idClient, DrawUpContractDTO drawUpContractDto);
    Task<bool> DoesClientHaveActiveContract(int idClient, int idSoftware);
    Task<bool> DoesContractExist(int idContract);
    Task<Contract> FindContract(int idContract);
    Task AddPayment(int idClient, AddPaymentDTO addPaymentDto);
    Task<double> GetRealIncome();
    Task<double> GetExpectedIncome();
}