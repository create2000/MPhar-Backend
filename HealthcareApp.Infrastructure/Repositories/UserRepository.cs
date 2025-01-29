using HealthcareApp.Domain.Interfaces;
using HealthcareApp.Domain.Entities;
using HealthcareApp.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<AppUser> _passwordHasher;

    public UserRepository(AppDbContext context, IPasswordHasher<AppUser> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    // Method to get a user by email
    public async Task<AppUser> GetByEmailAsync(string email)
    {
        try
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while finding the user by email.", ex);
        }
    }

    // Method to get a user by ID
    public async Task<AppUser> FindByIdAsync(string userId)
    {
        try
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id.ToString() == userId);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while finding the user by ID.", ex);
        }
    }

    // Method to create a new user
    public async Task CreateUserAsync(AppUser user)
    {
        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while creating the user.", ex);
        }
    }

    // Method to add a user (matches the interface requirement)
    public async Task AddAsync(AppUser user)
    {
        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while adding the user.", ex);
        }
    }

    // Method to update an existing user
    public async Task UpdateUserAsync(AppUser user)
    {
        try
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating the user.", ex);
        }
    }

    // Method to delete a user by AppUser object
    public async Task DeleteUserAsync(AppUser user)
    {
        try
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting the user.", ex);
        }
    }

    // Method to delete a user by user ID
    public async Task DeleteUserAsync(string userId)
    {
        try
        {
            var user = await FindByIdAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting the user by ID.", ex);
        }
    }

    // Method to retrieve all users
    public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
    {
        try
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving all users.", ex);
        }
    }
}
