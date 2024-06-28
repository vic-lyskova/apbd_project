using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project_s24787.Context;
using Project_s24787.DTOs;
using Project_s24787.Helpers;
using Project_s24787.Models;

namespace Project_s24787.Services;

public class DbService : IDbService
{
    private readonly ABCDbContext _context;
    private readonly IConfiguration _configuration;

    public DbService(ABCDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<bool> DoesIndividualClientExist(string pesel)
    {
        return await _context.Individuals.AnyAsync(i => i.PESEL == pesel);
    }

    public async Task<bool> DoesFirmClientExist(string krsNumber)
    {
        return await _context.Firms.AnyAsync(f => f.KRSNumber == krsNumber);
    }

    public async Task AddIndividualClient(AddIndividualClientDTO addIndividualClientDto)
    {
        var client = new Client()
        {
            Address = addIndividualClientDto.Address,
            Email = addIndividualClientDto.Email,
            PhoneNumber = addIndividualClientDto.PhoneNumber
        };

        var individual = new Individual()
        {
            Name = addIndividualClientDto.Name,
            Surname = addIndividualClientDto.Surname,
            Client = client,
            PESEL = addIndividualClientDto.PESEL
        };
        
        await _context.Individuals.AddAsync(individual);

        client.Individual = individual;
        
        await _context.Clients.AddAsync(client);

        await _context.SaveChangesAsync();
    }

    public async Task AddFirmClient(AddFirmClientDTO addFirmClientDto)
    {
        var client = new Client()
        {
            Address = addFirmClientDto.Address,
            Email = addFirmClientDto.Email,
            PhoneNumber = addFirmClientDto.PhoneNumber
        };

        var firm = new Firm()
        {
            FirmName = addFirmClientDto.FirmName,
            Client = client,
            KRSNumber = addFirmClientDto.KRSNumber
        };

        await _context.Firms.AddAsync(firm);

        client.Firm = firm;

        await _context.Clients.AddAsync(client);

        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUser(string login)
    {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login.Equals(login));
            return user;
    }

    public async Task<bool> DoesUserExist(string login)
    {
        return await _context.Users.AnyAsync(u => u.Login.Equals(login));
    }

    public async Task<LoginResponceDTO> GetTokens(User user)
    {
        Claim[] userclaim;
        if (user.Role == "admin")
        {
            userclaim = new[]
            {
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Role, "employee")
            };
        }
        else
        {
            userclaim = new[]
            {
                new Claim(ClaimTypes.Role, "employee")
            };
        }

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!)),
                SecurityAlgorithms.HmacSha256)
        );

        var stringToken = new JwtSecurityTokenHandler().WriteToken(token);
        var stringRefToken = SecurityHelpers.GenerateRefreshToken(_configuration);
        
        user.RefreshToken = stringRefToken;
        
        await _context.SaveChangesAsync();

        return new LoginResponceDTO()
        {
            Token = stringToken,
            RefreshToken = stringRefToken
        };
    }

    public async Task RegisterNewEmployee(RegisterRequestDTO registerRequestDto)
    {
        var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(registerRequestDto.Password);

        var user = new User()
        {
            Login = registerRequestDto.Login,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = SecurityHelpers.GenerateRefreshToken(_configuration),
            Role = "employee"
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task ChangeEmployeesRole(User user, string role)
    {
        user.Role = role;

        await _context.SaveChangesAsync();
    }

    public async Task<User?> FindUser(string refreshToken)
    {
            return await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
    }

    public async Task EditFirmClient(string krsNumber, EditFirmClientDTO editFirmClientDto)
    {
        var firm = await _context.Firms.FirstAsync(f => f.KRSNumber == krsNumber);
        var client = await _context.Clients.FirstAsync(c => c.Firm.KRSNumber == krsNumber);

        firm.FirmName = editFirmClientDto.FirmName ?? firm.FirmName;
        client.Address = editFirmClientDto.Address ?? client.Address;
        client.Email = editFirmClientDto.Email ?? client.Email;
        client.PhoneNumber = editFirmClientDto.PhoneNumber ?? client.PhoneNumber;

        await _context.SaveChangesAsync();
    }

    public async Task EditIndividualClient(string pesel, EditIndividualClientDTO editIndividualClientDto)
    {
        var individual = await _context.Individuals.FirstAsync(i => i.PESEL == pesel);
        var client = await _context.Clients.FirstAsync(c => c.Individual.PESEL == pesel);

        individual.Name = editIndividualClientDto.Name ?? individual.Name;
        individual.Surname = editIndividualClientDto.Surname ?? individual.Surname;
        client.Address = editIndividualClientDto.Address ?? client.Address;
        client.Email = editIndividualClientDto.Email ?? client.Email;
        client.PhoneNumber = editIndividualClientDto.PhoneNumber ?? client.PhoneNumber;

        await _context.SaveChangesAsync();
    }

    public async Task<bool> DoesDiscountExist(int idDiscount)
    {
        return await _context.Discounts.AnyAsync(d => d.IdDiscount == idDiscount);
    }

    public async Task<Discount> FindDiscount(int idDiscount)
    {
        return await _context.Discounts.FirstAsync(d => d.IdDiscount == idDiscount);
    }

    public async Task<bool> DoesSoftwareExist(int idSoftware)
    {
        return await _context.SoftwareSystems.AnyAsync(s => s.IdSoftware == idSoftware);
    }

    public async Task<bool> DoesClientExist(int idClient)
    {
        return await _context.Clients.AnyAsync(c => c.IdClient == idClient);
    }

    public async Task DrawUpContract(int idClient, DrawUpContractDTO drawUpContractDto)
    {
        var contract = new Contract()
        {
            IdClient = idClient,
            StartDate = drawUpContractDto.StartDate,
            EndDate = drawUpContractDto.EndDate,
            IsActive = false,
            IdSoftware = drawUpContractDto.Software,
            Version = drawUpContractDto.Version
        };
        
        var discounts = new HashSet<Discount>();
        if (drawUpContractDto.Discounts is not null)
        {
            foreach (var discountId in drawUpContractDto.Discounts)
            {
                if (await DoesDiscountExist(discountId))
                {
                    var discount = await FindDiscount(idClient);
                    if (discount.ActiveTo < drawUpContractDto.StartDate)
                    {
                        discounts.Add(discount);
                    }
                }
                
            }
        }

        var software = await _context.SoftwareSystems.FirstAsync(s => s.IdSoftware == drawUpContractDto.Software);
        var price = software.LicencePrice;
            
        if (discounts.Count > 0)
        {
            var discount = discounts
                .OrderByDescending(d => Int32.Parse(d.Value.Replace("%", "")))
                .First();
            var discountValue = Int32.Parse(discount.Value.Replace("%", ""));
            price -= (price * discountValue / 100);
            contract.Discounts.Add(discount);
        }

        if (await _context.Contracts.AnyAsync(c => c.IdClient == idClient))
        {
            price -= (price * 0.5);
            contract.Discounts.Add(await FindDiscount(1));
        }

        if (drawUpContractDto.Updates is not null && drawUpContractDto.Updates > 0)
        {
            price += (1000 * drawUpContractDto.Updates.Value);
        }

        contract.Price = price;

        await _context.Contracts.AddAsync(contract);

        await _context.SaveChangesAsync();
    }

    public async Task<bool> DoesClientHaveActiveContract(int idClient, int idSoftware)
    {
        return await _context.Contracts.AnyAsync(c => c.IdClient == idClient && c.IdSoftware == idSoftware);
    }

    public async Task<bool> DoesContractExist(int idContract)
    {
        return await _context.Contracts.AnyAsync(c => c.IdContract == idContract);
    }

    public async Task<Contract> FindContract(int idContract)
    {
        return await _context.Contracts.FirstAsync(c => c.IdContract == idContract);
    }

    public async Task AddPayment(int idClient, AddPaymentDTO addPaymentDto)
    {
        await _context.Payments.AddAsync(new Payment()
        {
            IdClient = idClient,
            IdContract = addPaymentDto.IdContract,
            Amount = addPaymentDto.Amount,
            Date = addPaymentDto.Date
        });

        var contract = await FindContract(addPaymentDto.IdContract);

        contract.IsActive = true;
        
        await _context.SaveChangesAsync();
    }

    public async Task<double> GetRealIncome()
    {
        return await _context.Contracts.Where(c => c.IsActive == true).SumAsync(c => c.Price);
    }

    public async Task<double> GetExpectedIncome()
    {
        var income = await GetRealIncome();

        income += await _context.Contracts
            .Where(c => c.IsActive == false && c.EndDate > DateTime.Now).SumAsync(c => c.Price);

        return income;
    }
}