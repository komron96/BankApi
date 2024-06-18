using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankApp.WebApi;


[ApiController]
[Route("user")]
public sealed class ClientContoller : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public ClientContoller(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpPost]
    public async Task<Client> CreateClientAsync([FromBody] Client client, CancellationToken cancellationToken)
    {
        await _appDbContext.AddAsync(client, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        return client;
    }


    [HttpGet]
    public async Task<IEnumerable<Client>> GetClientsAsync(CancellationToken cancellationToken)
    {
        IEnumerable<Client> clients = await _appDbContext.Clients.ToListAsync(cancellationToken);
        return clients;
    }

    [HttpGet("id/{id}")]
    public async Task<Client> GetClientByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        Client client = await _appDbContext.Clients.FirstOrDefaultAsync(t => t.Id == id, cancellationToken: cancellationToken) ?? throw new NotFoundEntityException("Not found siski");
        return client;
    }

    [HttpGet("phone/{phoneNumber}")]
    public async Task<Client> GetClientByPhoneAsync([FromRoute] string phoneNumber, CancellationToken cancellationToken)
    {
        if (phoneNumber.Contains('+'))
        {
            Client client = await _appDbContext.Clients.FirstOrDefaultAsync(t => t.PhoneNumber == phoneNumber, cancellationToken: cancellationToken) ?? throw new NotFoundEntityException("Not found siski");
            return client;
        }
        return null;
    }

    // public async Task<Client> ChangePhoneNumberAsync([FromRoute] string phoneNumber)

}